using DAL;
using DevExpress.XtraEditors;
using EncryptSign.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptSign
{
    public partial class MainFrm : Form
    {
        string senderPublicKey = String.Empty;
        string senderPrivateKey = String.Empty;
        string receiverPublicKey = String.Empty;

        public MainFrm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        /// <summary>
        /// Sender 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.UserPublicKeyMemoEdit.Text = string.Empty;
            string senderUserEmail = this.UserEmailTextEdit.Text;
            Regex reg = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            //check email 
            if ((!String.IsNullOrWhiteSpace(senderUserEmail)) && (reg.IsMatch(senderUserEmail)))
            {
                string publickey = string.Empty;
                string privatekey = string.Empty;
                createNewUser(senderUserEmail, out publickey,out privatekey);
                this.UserPublicKeyMemoEdit.Text = publickey;
                senderPublicKey = publickey;
                senderPrivateKey = privatekey;
                XtraMessageBox.Show("密钥生成成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("请正确输入邮箱地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string receiverUserEmail = this.ReceiverEmailTextEdit.Text;
            Regex reg = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            //check email 
            if ((!String.IsNullOrWhiteSpace(receiverUserEmail)) && (reg.IsMatch(receiverUserEmail)))
            {
                string publickey = string.Empty;
                string privatekey = string.Empty;
                createNewUser(receiverUserEmail, out publickey, out privatekey);
                receiverPublicKey = publickey;
                //send msg

                List<SendMessage> msgList = new List<SendMessage>();
                string path = @"Data\MsgInfo.txt";
                if (File.Exists(path))
                {
                    string datatext = System.IO.File.ReadAllText(path);
                    if (!String.IsNullOrEmpty(datatext))
                    {
                        msgList = JsonHelper.DeserializeJsonToList<SendMessage>(datatext);
                    }
                }
                SendMessage newmsg = new SendMessage();
                newmsg.SenderUid = this.UserEmailTextEdit.Text;
                newmsg.ReceicerUid = this.ReceiverEmailTextEdit.Text;

                string plainText = this.MeassageMemoEdit.Text;

                //Step 1 Alice用Bob公钥加密
                string EnryptedData = RSA_Encrypt(plainText, receiverPublicKey);
                //Step 2 Alice对Data签名
                string SignedData = HashAndSign(plainText, senderPrivateKey);

                newmsg.MessageCiphertext = EnryptedData;
                newmsg.MessageSignature = SignedData;
                newmsg.CreateTime = DateTime.Now;
                msgList.Add(newmsg);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(JsonHelper.SerializeObject(msgList));
                    fs.Write(info, 0, info.Length);
                }
                XtraMessageBox.Show("消息发送成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                /// 
            }
            else
            {
                XtraMessageBox.Show("请正确输入接收者邮箱地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void createNewUser(string uid,out string publickey,out string privatekey)
        {
            publickey = string.Empty;
            privatekey = string.Empty;
            //将文件读取为文件流并写入byte数组
            bool needcreateuser = false;
            List<User> userInfoList = new List<User>();

            string path = @"Data\UserInfo.txt";
            if (File.Exists(path))
            {
                string usertext = System.IO.File.ReadAllText(path);

                if (!String.IsNullOrEmpty(usertext))
                {
                    userInfoList = JsonHelper.DeserializeJsonToList<User>(usertext);
                    if (userInfoList != null && userInfoList.Count() > 0)
                    {
                        var userAlice = userInfoList.Where(u => u.Uid == uid.Trim()).ToList().FirstOrDefault();
                        if (userAlice == null || String.IsNullOrWhiteSpace(userAlice.PublicKey))
                        {
                            needcreateuser = true;
                        }
                        else
                        {
                            publickey = userAlice.PublicKey;
                            privatekey = userAlice.PrivateKey;
                        }
                    }
                    else
                    {
                        needcreateuser = true;
                    }
                }
            }
            else
            {
                needcreateuser = true;
            }
            if (needcreateuser)
            {
                RSACryptoServiceProvider new_RSAalg = new RSACryptoServiceProvider();
                User newuser = new User();
                newuser.Uid = uid.Trim();
                newuser.PublicKey = Convert.ToBase64String(new_RSAalg.ExportCspBlob(false));
                newuser.PrivateKey = Convert.ToBase64String(new_RSAalg.ExportCspBlob(true));
                userInfoList.Add(newuser);
                publickey = newuser.PublicKey;
                privatekey = newuser.PrivateKey;
            }
            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(JsonHelper.SerializeObject(userInfoList));
                fs.Write(info, 0, info.Length);
            }
        }

        /// <summary>
        /// 公钥加密数据
        /// </summary>
        /// <param name="str_Plain_Text">明文数据</param>
        /// <param name="str_Public_Key">公钥</param>
        /// <returns></returns>
        static public string RSA_Encrypt(string str_Plain_Text, string str_Public_Key)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(str_Plain_Text); 
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] bytes_Public_Key = Convert.FromBase64String(str_Public_Key);
                RSA.ImportCspBlob(bytes_Public_Key);

                //OAEP padding is only available on Microsoft Windows XP or later.  
                byte[] bytes_Cypher_Text = RSA.Encrypt(DataToEncrypt, true);
                string str_Cypher_Text = Convert.ToBase64String(bytes_Cypher_Text);
                return str_Cypher_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        //对数据签名
        public static string HashAndSign(string str_DataToSign, string str_Private_Key)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToSign = ByteConverter.GetBytes(str_DataToSign);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Private_Key));
                byte[] signedData = RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
                string str_SignedData = Convert.ToBase64String(signedData);
                var lenth = str_SignedData.Length;
                return str_SignedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        //RSA解密
        static public string RSA_Decrypt(string str_Cypher_Text, string str_Private_Key)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(str_Cypher_Text);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                //RSA.ImportParameters(RSAKeyInfo);
                byte[] bytes_Private_Key = Convert.FromBase64String(str_Private_Key);
                RSA.ImportCspBlob(bytes_Private_Key);

                //OAEP padding is only available on Microsoft Windows XP or later.  
                byte[] bytes_Plain_Text = RSA.Decrypt(DataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string str_Plain_Text = ByteConverter.GetString(bytes_Plain_Text);
                return str_Plain_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //验证签名
        public static bool VerifySignedHash(string str_DataToVerify, string str_SignedData, string str_Public_Key)
        {
            byte[] SignedData = Convert.FromBase64String(str_SignedData);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToVerify = ByteConverter.GetBytes(str_DataToVerify);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Public_Key));

                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.memoEdit2.Text = string.Empty;
             string path = @"Data\MsgInfo.txt";
            if (File.Exists(path))
            {
                string datatext = System.IO.File.ReadAllText(path);
                if (!String.IsNullOrEmpty(datatext))
                {
                    string useruid = this.UserEmailTextEdit.Text.Trim();
                    List<SendMessage> msgList = JsonHelper.DeserializeJsonToList<SendMessage>(datatext);
                    List<SendMessage> thisUserMsg = msgList.Where(m => m.ReceicerUid == useruid).ToList();
                    if (thisUserMsg?.Count() > 0)
                    { 
                        if (File.Exists(@"Data\UserInfo.txt"))
                        {
                            string usertext = System.IO.File.ReadAllText(@"Data\UserInfo.txt");
                            if (!String.IsNullOrEmpty(usertext))
                            {
                                List<User> userInfoList = JsonHelper.DeserializeJsonToList<User>(usertext);
                                User receiveUser = userInfoList.Where(ul => ul.Uid == useruid).FirstOrDefault();

                                thisUserMsg = thisUserMsg.OrderBy(o=>o.CreateTime).ToList();
                                StringBuilder txtsb = new StringBuilder();
                                foreach (SendMessage smsg in thisUserMsg)
                                {
                                    User sendUser = userInfoList.Where(ul => ul.Uid == smsg.SenderUid).FirstOrDefault();
                                    //解密
                                    string plaintTxt = RSA_Decrypt(smsg.MessageCiphertext, receiveUser.PrivateKey);
                                    if (VerifySignedHash(plaintTxt, smsg.MessageSignature, sendUser.PublicKey))
                                    {
                                        //this.memoEdit2.Text.a  
                                        txtsb.Append(Environment.NewLine);
                                        txtsb.Append(string.Format($"{sendUser.Uid}: {smsg.CreateTime.ToString("yyyy-MM-dd")}"));
                                        txtsb.Append(Environment.NewLine);
                                        txtsb.Append(plaintTxt); 
                                    }
                                }
                                this.memoEdit2.Text = txtsb.ToString();
                            }
                        }
                    } 
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    BaseDAL.InsertWithNoResult("inserttets","");
        //}
    }
}

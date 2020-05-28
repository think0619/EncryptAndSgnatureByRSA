using DAL;
using DevExpress.XtraEditors;
using EncryptSign.Entity;
using EncryptSign.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptSign
{
    public partial class MessageFrm : Form
    {
        public MessageFrm()
        {
            InitializeComponent();
           
        }
        public MessageFrm(SendMessage msg)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
          
            //处理密文消息 
            //获取接收者的私钥后解密
            string receiverPrivateKey = BaseDAL.Get<string>("GetReceiverPrivateKey", msg.ReceiverID);
            string plainText = RSAHelper.RSA_Decrypt(msg.MessageCiphertext, receiverPrivateKey);
            if (plainText != null)
            {
                //解密成功，
                //验证签名，获取发送者的公钥
                string senderPublic = BaseDAL.Get<string>("GetSenderPublicKey", msg.SenderID);
                if (RSAHelper.VerifySignedHash(plainText, msg.MessageSignature, senderPublic))
                {
                    //验证成功
                    this.SenderTextEdit.Text = msg.SenderName;
                    this.SendMailTextEdit.Text = msg.SendeUid;
                    if (msg.SentTime != null && msg.SentTime != DateTime.MinValue)
                    {
                        this.SentTimeTextEdit.Text = msg.SentTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        this.SentTimeTextEdit.Text = String.Empty;
                    }
                    this.memoEdit1.Text = plainText; 
                }
                else
                {
                    this.Close();
                    //失败
                    XtraMessageBox.Show(String.Format(@"收件人签名信息验证错误"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Close();
                //解密失败
                XtraMessageBox.Show(String.Format(@"密文消息解密失败"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MessageFrm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SendMailTextEdit.ReadOnly = true;
            this.SenderTextEdit.ReadOnly = true;
            this.SentTimeTextEdit.ReadOnly = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}

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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptSign
{
    public partial class RegisterFrm : Form
    {
        Form loginfrm = null;
        public RegisterFrm()
        {
            InitializeComponent();
        }
        public RegisterFrm(Form _loginfrm)
        {
            loginfrm = _loginfrm;
            
            InitializeComponent();
        }

        private void CheckEmailSimpleButton_Click(object sender, EventArgs e)
        {
            string inputEmailText = this.EmailTextEdit.Text;
            if (!String.IsNullOrWhiteSpace(inputEmailText))
            {
                inputEmailText = inputEmailText.Trim();
                string tips = string.Empty;
                inputEmailText = inputEmailText.Trim();
                if (checkEmail(inputEmailText, out tips))
                {
                    XtraMessageBox.Show(tips, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(tips, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else
            {
                XtraMessageBox.Show("请输入邮箱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        /// <summary>
        /// 检查邮箱准确性
        /// </summary>
        /// <param name="inputEmailTxt"></param>
        /// <param name="tips"></param>
        /// <returns></returns>
        public bool checkEmail(string inputEmailText, out string tips)
        {
            inputEmailText = inputEmailText.Trim();
            bool checkResult = false;
            Regex reg = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            if (reg.IsMatch(inputEmailText))
            {
                if (BaseDAL.Get("GetEmailCount", inputEmailText) == 0)
                {
                    checkResult = true;
                    tips = String.Format($"\"{inputEmailText}\"可用。");
                  //  XtraMessageBox.Show(String.Format($"\"{inputEmailText}\"可用。"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    tips = String.Format($"\"{inputEmailText}\"已存在。");
                   // XtraMessageBox.Show(String.Format($"\"{inputEmailText}\"已存在。"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tips = String.Format($"请检查邮箱格式");
               // XtraMessageBox.Show("请检查邮箱格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return checkResult;
        }

        private void RegisterSimpleButton_Click(object sender, EventArgs e)
        {
            string usernametxt = this.UsernameTextEdit.Text;
            string emailtxt = this.EmailTextEdit.Text;
            string passwordtxt = this.PwdTextEdit.Text;
            string sndpasswordtxt = this.SndPwdTextEdit.Text;

            bool checkflag = true;
            //验证用户名
            if (String.IsNullOrEmpty(usernametxt))
            {
                checkflag = false;
                XtraMessageBox.Show(String.Format("请输入用户名"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (checkflag)
            {
                if (String.IsNullOrEmpty(emailtxt))
                {
                    checkflag = false;
                    XtraMessageBox.Show(String.Format("请输入邮箱地址"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!checkEmail(emailtxt, out string tips))
                    {
                        checkflag = false;
                        XtraMessageBox.Show(tips, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                }
            }

            if (checkflag)
            {
                if (String.IsNullOrEmpty(passwordtxt))
                {
                    checkflag = false;
                    XtraMessageBox.Show(String.Format("请输入密码"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (passwordtxt != sndpasswordtxt)
                    {
                        checkflag = false;
                        XtraMessageBox.Show(String.Format("两次密码不一致"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (checkflag)
            {
                try
                {
                    //检查结束
                    User newuser = new User()
                    {
                        Uid = emailtxt.Trim(),
                        Name = usernametxt.Trim(),
                        PwdHash = CommonHandle.sha256_hash(passwordtxt)
                    };
                    CspParameters cspParams = new CspParameters();
                    cspParams.KeyContainerName = newuser.Uid;
                    RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048,cspParams); 
                    newuser.PrivateKey = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
                    newuser.PublicKey = Convert.ToBase64String(RSAalg.ExportCspBlob(false));
                    BaseDAL.InsertWithNoResult("InsertNewUser", newuser);
                    if (XtraMessageBox.Show(String.Format("注册成功"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        if (loginfrm != null)
                        { 
                            loginfrm.Show();
                            this.Close();
                        }
                    } 
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(String.Format("注册失败"+ ex.Message), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegisterFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loginfrm != null)
            {
                loginfrm.Show();
                loginfrm.TopLevel = true; 
            }
        }
    }
}

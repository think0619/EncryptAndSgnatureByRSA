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
using XRails.Controls;

namespace EncryptSign
{
    public partial class EncryptFrm : Form
    {
        public EncryptFrm()
        {
            InitializeComponent();
        }

        private void EncryptFrm_Load(object sender, EventArgs e)
        {
            this.label1.Text =String.Format($"hi { LoginUser.Name},");

            //初始化接收用户下拉框  
            ReceiveUserLookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 0));
            ReceiveUserLookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "姓名", 100));
            ReceiveUserLookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Uid", "邮箱", 150));
            ReceiveUserLookUpEdit1.Properties.Columns["ID"].Visible = false;
            ReceiveUserLookUpEdit1.Properties.DataSource = receiveUserList(LoginUser.ID);
            ReceiveUserLookUpEdit1.Properties.ValueMember = "ID";
            ReceiveUserLookUpEdit1.Properties.DisplayMember = "Name";
            ReceiveUserLookUpEdit1.Properties.PopupWidth = 250;
            ReceiveUserLookUpEdit1.Properties.NullText = string.Empty;

            //收件箱初始化
            this.dataGridView1.ColumnCount = 5;
            this.dataGridView1.Columns[0].Name = "状态";
            this.dataGridView1.Columns[1].Name = "发件人";
            this.dataGridView1.Columns[2].Name = "发件人邮箱";
            this.dataGridView1.Columns[3].Name = "发送时间";
            this.dataGridView1.Columns[4].Name = "MsgID";
            this.dataGridView1.Columns[0].Width = 100;
            this.dataGridView1.Columns[1].Width = 150;
            this.dataGridView1.Columns[2].Width = 200;
            this.dataGridView1.Columns[3].Width = 200;
            this.dataGridView1.Columns[4].Visible = false; 
           
            refreshDatagrid(this.dataGridView1);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false; 
        }

        public static void refreshDatagrid(DataGridView dv)
        {
            dv.Rows.Clear();
            //绑定数据
            // this.dataGridView1.Rows.Clear();
            IList<SendMessage> receiveList = BaseDAL.QueryForList<SendMessage>("QueryAllReceiceMsgList", LoginUser.ID);
            if (receiveList != null && receiveList.Count() > 0)
            {
                foreach (SendMessage msg in receiveList)
                {
                    var msgStatus = string.Empty;
                    if (msg.Status == 1)
                    {
                        msgStatus = "未读";
                    }
                    else if (msg.Status == 2)
                    {
                        msgStatus = "已读";
                    }
                    string[] rowItem = new string[] {
                        msgStatus,
                        msg.SenderName,
                        msg.SendeUid,
                        (msg.SentTime!=null &&msg.SentTime!=DateTime.MinValue)?msg.SentTime.ToString("yyyy-MM-dd HH:mm:ss"):"",
                        msg.ID.ToString()
                    };
                    dv.Rows.Add(rowItem);
                }
            }
        }


        private void EncryptFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        //获取可接受用户的信息，除了登录用户
        public IList<User> receiveUserList(int LoginUserID)
        {
            IList<User> userlist = BaseDAL.QueryForList<User>("GetAllReceiveUserList", LoginUserID);
            return userlist;
        }

        /// <summary>
        /// 刷新接受用户下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckEmailSimpleButton_Click(object sender, EventArgs e)
        {
            ReceiveUserLookUpEdit1.EditValue = null;
            ReceiveUserLookUpEdit1.Properties.DataSource = receiveUserList(LoginUser.ID);
        }

        private void SentBtn_Click(object sender, EventArgs e)
        {
            bool checkmsg = true;
            if (ReceiveUserLookUpEdit1.EditValue == null)
            {
                checkmsg = false;
                XtraMessageBox.Show(String.Format(@"请选择接收人"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (checkmsg)
            {
                if (String.IsNullOrWhiteSpace(MeassageMemoEdit.Text))
                {
                    checkmsg = false;
                    XtraMessageBox.Show(String.Format(@"请输入发送的消息"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (MeassageMemoEdit.Text.Length>100)
                {
                    checkmsg = false;
                    XtraMessageBox.Show(String.Format(@"发送消息的长度不要超过100位"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                return;
            }

            if (checkmsg)
            { 
                if (XtraMessageBox.Show(String.Format($"确认发送给{this.ReceiveUserLookUpEdit1.Text}吗？"), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                { 
                    string _id = this.ReceiveUserLookUpEdit1.EditValue.ToString();
                    int ReceiveID = 0;
                    if (Int32.TryParse(_id, out ReceiveID))
                    {
                        //发送
                        User receiveUser = BaseDAL.Get<User>("GetUserInfoByUserID", ReceiveID);
                        if (receiveUser != null)
                        {
                            SendMessage newmsg = new SendMessage(); 
                            //明文消息
                            string plainmsg = MeassageMemoEdit.Text;

                            //加密
                            //用收件人公钥加密
                            newmsg.MessageCiphertext = RSAHelper.RSA_Encrypt(plainmsg, receiveUser.PublicKey);

                            //签名
                            //用发件人私钥对消息摘要签名
                            newmsg.MessageSignature = RSAHelper.HashAndSign(plainmsg, LoginUser.PrivateKey);

                            newmsg.ReceiverID = ReceiveID;
                            newmsg.SenderID = LoginUser.ID;
                          //  newmsg.SentTime = DateTime.Now;
                            BaseDAL.InsertWithNoResult("InsertNewSendMsg", newmsg);
                            XtraMessageBox.Show(String.Format(@"发送成功"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
            } 
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow rowobj = this.dataGridView1.Rows[e.RowIndex]; 
                var msgIDStr = rowobj.Cells["MsgID"].Value;
                if (msgIDStr != null)
                {
                    int msgID = 0;
                    if (Int32.TryParse(msgIDStr.ToString(), out msgID))
                    {
                        SendMessage singleMsg = BaseDAL.Get<SendMessage>("GetMsgInfo", msgID);
                        if (singleMsg != null)
                        {
                            // Running on the worker thread
                            this.dataGridView1.Invoke((MethodInvoker)delegate {
                                BaseDAL.Update("UpdateMsgReadStatus", singleMsg.ID);
                                refreshDatagrid(this.dataGridView1);
                            });
                           
                            MessageFrm msgfrm = new MessageFrm(singleMsg);
                            msgfrm.TopMost = true;
                            msgfrm.Show();
                        }
                    }
                }
               
            } 
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Frm_Login")
                {
                    this.Hide();
                    frm.Show();
                    break;
                }
            }
        }
    }
}

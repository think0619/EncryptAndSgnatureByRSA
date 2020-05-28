namespace EncryptSign
{
    partial class RegisterFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UsernameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.EmailTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.PwdTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.SndPwdTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.CheckEmailSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.RegisterSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PwdTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SndPwdTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "邮  箱：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密  码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "确认密码：";
            // 
            // UsernameTextEdit
            // 
            this.UsernameTextEdit.Location = new System.Drawing.Point(93, 43);
            this.UsernameTextEdit.Name = "UsernameTextEdit";
            this.UsernameTextEdit.Size = new System.Drawing.Size(258, 20);
            this.UsernameTextEdit.TabIndex = 8;
            // 
            // EmailTextEdit
            // 
            this.EmailTextEdit.Location = new System.Drawing.Point(93, 69);
            this.EmailTextEdit.Name = "EmailTextEdit";
            this.EmailTextEdit.Size = new System.Drawing.Size(258, 20);
            this.EmailTextEdit.TabIndex = 9;
            // 
            // PwdTextEdit
            // 
            this.PwdTextEdit.Location = new System.Drawing.Point(93, 95);
            this.PwdTextEdit.Name = "PwdTextEdit";
            this.PwdTextEdit.Properties.PasswordChar = '*';
            this.PwdTextEdit.Size = new System.Drawing.Size(258, 20);
            this.PwdTextEdit.TabIndex = 10;
            // 
            // SndPwdTextEdit
            // 
            this.SndPwdTextEdit.Location = new System.Drawing.Point(93, 121);
            this.SndPwdTextEdit.Name = "SndPwdTextEdit";
            this.SndPwdTextEdit.Properties.PasswordChar = '*';
            this.SndPwdTextEdit.Size = new System.Drawing.Size(258, 20);
            this.SndPwdTextEdit.TabIndex = 11;
            // 
            // CheckEmailSimpleButton
            // 
            this.CheckEmailSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.CheckEmailSimpleButton.Location = new System.Drawing.Point(357, 69);
            this.CheckEmailSimpleButton.Name = "CheckEmailSimpleButton";
            this.CheckEmailSimpleButton.Size = new System.Drawing.Size(80, 20);
            this.CheckEmailSimpleButton.TabIndex = 12;
            this.CheckEmailSimpleButton.Text = "检查可用";
            this.CheckEmailSimpleButton.Click += new System.EventHandler(this.CheckEmailSimpleButton_Click);
            // 
            // RegisterSimpleButton
            // 
            this.RegisterSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.RegisterSimpleButton.Location = new System.Drawing.Point(93, 148);
            this.RegisterSimpleButton.Name = "RegisterSimpleButton";
            this.RegisterSimpleButton.Size = new System.Drawing.Size(98, 23);
            this.RegisterSimpleButton.TabIndex = 13;
            this.RegisterSimpleButton.Text = "注  册";
            this.RegisterSimpleButton.Click += new System.EventHandler(this.RegisterSimpleButton_Click);
            // 
            // RegisterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 223);
            this.Controls.Add(this.RegisterSimpleButton);
            this.Controls.Add(this.CheckEmailSimpleButton);
            this.Controls.Add(this.SndPwdTextEdit);
            this.Controls.Add(this.PwdTextEdit);
            this.Controls.Add(this.EmailTextEdit);
            this.Controls.Add(this.UsernameTextEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RegisterFrm";
            this.Text = "用户注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterFrm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PwdTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SndPwdTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit UsernameTextEdit;
        private DevExpress.XtraEditors.TextEdit EmailTextEdit;
        private DevExpress.XtraEditors.TextEdit PwdTextEdit;
        private DevExpress.XtraEditors.TextEdit SndPwdTextEdit;
        private DevExpress.XtraEditors.SimpleButton CheckEmailSimpleButton;
        private DevExpress.XtraEditors.SimpleButton RegisterSimpleButton;
    }
}
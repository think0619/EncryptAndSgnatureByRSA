namespace EncryptSign
{
    partial class MessageFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.SentTimeTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.SendMailTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.SenderTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SentTimeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendMailTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SenderTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "发件人：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "发件人邮箱：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "发送时间：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.memoEdit1);
            this.groupBox1.Controls.Add(this.SentTimeTextEdit);
            this.groupBox1.Controls.Add(this.SendMailTextEdit);
            this.groupBox1.Controls.Add(this.SenderTextEdit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 291);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(92, 115);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(454, 152);
            this.memoEdit1.TabIndex = 6;
            // 
            // SentTimeTextEdit
            // 
            this.SentTimeTextEdit.Location = new System.Drawing.Point(92, 77);
            this.SentTimeTextEdit.Name = "SentTimeTextEdit";
            this.SentTimeTextEdit.Size = new System.Drawing.Size(454, 20);
            this.SentTimeTextEdit.TabIndex = 5;
            // 
            // SendMailTextEdit
            // 
            this.SendMailTextEdit.Location = new System.Drawing.Point(92, 51);
            this.SendMailTextEdit.Name = "SendMailTextEdit";
            this.SendMailTextEdit.Size = new System.Drawing.Size(454, 20);
            this.SendMailTextEdit.TabIndex = 4;
            // 
            // SenderTextEdit
            // 
            this.SenderTextEdit.Location = new System.Drawing.Point(92, 25);
            this.SenderTextEdit.Name = "SenderTextEdit";
            this.SenderTextEdit.Size = new System.Drawing.Size(454, 20);
            this.SenderTextEdit.TabIndex = 3;
            // 
            // MessageFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 324);
            this.Controls.Add(this.groupBox1);
            this.Name = "MessageFrm";
            this.Text = "消息";
            this.Load += new System.EventHandler(this.MessageFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SentTimeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendMailTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SenderTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit SenderTextEdit;
        private DevExpress.XtraEditors.TextEdit SendMailTextEdit;
        private DevExpress.XtraEditors.TextEdit SentTimeTextEdit;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}
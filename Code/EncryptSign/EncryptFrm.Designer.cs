namespace EncryptSign
{
    partial class EncryptFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncryptFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.SentBtn = new DevExpress.XtraEditors.SimpleButton();
            this.MeassageMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.ReceiveUserLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.CheckEmailSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.MeassageMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiveUserLookUpEdit1.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 26);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(84, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "接收用户邮箱：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(31, 60);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "消息内容：";
            // 
            // SentBtn
            // 
            this.SentBtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("SentBtn.ImageOptions.Image")));
            this.SentBtn.Location = new System.Drawing.Point(99, 143);
            this.SentBtn.Name = "SentBtn";
            this.SentBtn.Size = new System.Drawing.Size(93, 23);
            this.SentBtn.TabIndex = 6;
            this.SentBtn.Text = "发 送消息";
            this.SentBtn.Click += new System.EventHandler(this.SentBtn_Click);
            // 
            // MeassageMemoEdit
            // 
            this.MeassageMemoEdit.Location = new System.Drawing.Point(99, 60);
            this.MeassageMemoEdit.Name = "MeassageMemoEdit";
            this.MeassageMemoEdit.Size = new System.Drawing.Size(912, 77);
            this.MeassageMemoEdit.TabIndex = 7;
            // 
            // ReceiveUserLookUpEdit1
            // 
            this.ReceiveUserLookUpEdit1.Location = new System.Drawing.Point(100, 26);
            this.ReceiveUserLookUpEdit1.Name = "ReceiveUserLookUpEdit1";
            this.ReceiveUserLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ReceiveUserLookUpEdit1.Size = new System.Drawing.Size(343, 20);
            this.ReceiveUserLookUpEdit1.TabIndex = 8;
            // 
            // CheckEmailSimpleButton
            // 
            this.CheckEmailSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("CheckEmailSimpleButton.ImageOptions.Image")));
            this.CheckEmailSimpleButton.Location = new System.Drawing.Point(449, 26);
            this.CheckEmailSimpleButton.Name = "CheckEmailSimpleButton";
            this.CheckEmailSimpleButton.Size = new System.Drawing.Size(80, 20);
            this.CheckEmailSimpleButton.TabIndex = 13;
            this.CheckEmailSimpleButton.Text = "刷 新";
            this.CheckEmailSimpleButton.Click += new System.EventHandler(this.CheckEmailSimpleButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CheckEmailSimpleButton);
            this.groupBox3.Controls.Add(this.ReceiveUserLookUpEdit1);
            this.groupBox3.Controls.Add(this.MeassageMemoEdit);
            this.groupBox3.Controls.Add(this.SentBtn);
            this.groupBox3.Controls.Add(this.labelControl5);
            this.groupBox3.Controls.Add(this.labelControl4);
            this.groupBox3.Location = new System.Drawing.Point(18, 376);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1017, 184);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1020, 331);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "收件箱";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1008, 305);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(955, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "退 出";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // EncryptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 572);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Name = "EncryptFrm";
            this.Text = "消息框";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EncryptFrm_FormClosed);
            this.Load += new System.EventHandler(this.EncryptFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MeassageMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiveUserLookUpEdit1.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton SentBtn;
        private DevExpress.XtraEditors.MemoEdit MeassageMemoEdit;
        private DevExpress.XtraEditors.LookUpEdit ReceiveUserLookUpEdit1;
        private DevExpress.XtraEditors.SimpleButton CheckEmailSimpleButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
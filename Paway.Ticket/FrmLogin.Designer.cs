namespace Paway.Ticket
{
    partial class FrmLogin
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
            this.components = new System.ComponentModel.Container();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtLoginPwd = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblLoginPwd = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.chkRememberPwd = new System.Windows.Forms.CheckBox();
            this.picCode = new Paway.Ticket.UI.PictureBoxExt();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Location = new System.Drawing.Point(384, 117);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 31);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(159, 119);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(132, 26);
            this.txtCode.TabIndex = 3;
            // 
            // txtLoginPwd
            // 
            this.txtLoginPwd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLoginPwd.ForeColor = System.Drawing.Color.Black;
            this.txtLoginPwd.Location = new System.Drawing.Point(159, 80);
            this.txtLoginPwd.Name = "txtLoginPwd";
            this.txtLoginPwd.PasswordChar = '*';
            this.txtLoginPwd.Size = new System.Drawing.Size(219, 26);
            this.txtLoginPwd.TabIndex = 2;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLoginName.ForeColor = System.Drawing.Color.Black;
            this.txtLoginName.Location = new System.Drawing.Point(159, 41);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(219, 26);
            this.txtLoginName.TabIndex = 1;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.ForeColor = System.Drawing.Color.Black;
            this.lblCode.Location = new System.Drawing.Point(109, 126);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(57, 12);
            this.lblCode.TabIndex = 6;
            this.lblCode.Text = "验证码：";
            // 
            // lblLoginPwd
            // 
            this.lblLoginPwd.AutoSize = true;
            this.lblLoginPwd.ForeColor = System.Drawing.Color.Black;
            this.lblLoginPwd.Location = new System.Drawing.Point(121, 87);
            this.lblLoginPwd.Name = "lblLoginPwd";
            this.lblLoginPwd.Size = new System.Drawing.Size(44, 12);
            this.lblLoginPwd.TabIndex = 7;
            this.lblLoginPwd.Text = "密码：";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.ForeColor = System.Drawing.Color.Black;
            this.lblLoginName.Location = new System.Drawing.Point(109, 48);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(57, 12);
            this.lblLoginName.TabIndex = 5;
            this.lblLoginName.Text = "登录名：";
            // 
            // chkRememberPwd
            // 
            this.chkRememberPwd.AutoSize = true;
            this.chkRememberPwd.ForeColor = System.Drawing.Color.Black;
            this.chkRememberPwd.Location = new System.Drawing.Point(159, 161);
            this.chkRememberPwd.Name = "chkRememberPwd";
            this.chkRememberPwd.Size = new System.Drawing.Size(76, 16);
            this.chkRememberPwd.TabIndex = 4;
            this.chkRememberPwd.Text = "记住密码";
            this.chkRememberPwd.UseVisualStyleBackColor = true;
            // 
            // picCode
            // 
            this.picCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCode.Image = null;
            this.picCode.Location = new System.Drawing.Point(300, 118);
            this.picCode.Name = "picCode";
            this.picCode.Size = new System.Drawing.Size(78, 28);
            this.picCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCode.TabIndex = 12;
            this.picCode.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(216)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(531, 215);
            this.Controls.Add(this.picCode);
            this.Controls.Add(this.chkRememberPwd);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtLoginPwd);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblLoginPwd);
            this.Controls.Add(this.lblLoginName);
            this.ForeColor = System.Drawing.Color.White;
            this.IsResize = false;
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 20);
            this.ShowIcon = false;
            this.ShowMenu = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtLoginPwd;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblLoginPwd;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.CheckBox chkRememberPwd;
        private UI.PictureBoxExt picCode;
        private System.Windows.Forms.Timer timer;

    }
}
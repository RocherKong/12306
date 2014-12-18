namespace Paway.Ticket
{
    partial class Form1
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
            this.group3 = new Paway.Ticket.UI.Group();
            this.group2 = new Paway.Ticket.UI.Group();
            this.group1 = new Paway.Ticket.UI.Group();
            this.SuspendLayout();
            // 
            // group3
            // 
            this.group3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.group3.Location = new System.Drawing.Point(573, 118);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(261, 365);
            this.group3.TabIndex = 2;
            // 
            // group2
            // 
            this.group2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.group2.Location = new System.Drawing.Point(12, 118);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(555, 365);
            this.group2.TabIndex = 1;
            // 
            // group1
            // 
            this.group1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.group1.Location = new System.Drawing.Point(12, 12);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(822, 100);
            this.group1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 495);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.group1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Group group1;
        private UI.Group group2;
        private UI.Group group3;

    }
}
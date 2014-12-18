namespace Paway.Ticket
{
    partial class FrmConfirmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmOrder));
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.grpPassengersPanel = new System.Windows.Forms.GroupBox();
            this.pnlPassenger = new System.Windows.Forms.Panel();
            this.dgvPassenger = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdentity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpTrainInfo = new System.Windows.Forms.GroupBox();
            this.pnlTrainInfo = new System.Windows.Forms.Panel();
            this.lblTicketInfo = new System.Windows.Forms.Label();
            this.lblTrainInfo = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.picCode = new Paway.Ticket.UI.PictureBoxExt();
            this.grpPassengersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassenger)).BeginInit();
            this.grpTrainInfo.SuspendLayout();
            this.pnlTrainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnter.ForeColor = System.Drawing.Color.Black;
            this.btnEnter.Location = new System.Drawing.Point(291, 402);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(87, 31);
            this.btnEnter.TabIndex = 13;
            this.btnEnter.Text = "确定";
            this.btnEnter.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(67, 404);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(132, 26);
            this.txtCode.TabIndex = 12;
            // 
            // lblCode
            // 
            this.lblCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(17, 407);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(57, 12);
            this.lblCode.TabIndex = 14;
            this.lblCode.Text = "验证码：";
            // 
            // grpPassengersPanel
            // 
            this.grpPassengersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPassengersPanel.Controls.Add(this.pnlPassenger);
            this.grpPassengersPanel.Location = new System.Drawing.Point(16, 119);
            this.grpPassengersPanel.Name = "grpPassengersPanel";
            this.grpPassengersPanel.Size = new System.Drawing.Size(651, 81);
            this.grpPassengersPanel.TabIndex = 19;
            this.grpPassengersPanel.TabStop = false;
            this.grpPassengersPanel.Text = "乘客信息";
            // 
            // pnlPassenger
            // 
            this.pnlPassenger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPassenger.AutoScroll = true;
            this.pnlPassenger.Location = new System.Drawing.Point(6, 21);
            this.pnlPassenger.Name = "pnlPassenger";
            this.pnlPassenger.Size = new System.Drawing.Size(639, 54);
            this.pnlPassenger.TabIndex = 0;
            // 
            // dgvPassenger
            // 
            this.dgvPassenger.AllowUserToAddRows = false;
            this.dgvPassenger.AllowUserToDeleteRows = false;
            this.dgvPassenger.AllowUserToResizeColumns = false;
            this.dgvPassenger.AllowUserToResizeRows = false;
            this.dgvPassenger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPassenger.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPassenger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPassenger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colIdentity,
            this.colSeat,
            this.colType,
            this.colName,
            this.colCardType,
            this.colCardNo,
            this.colMobile});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPassenger.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPassenger.Location = new System.Drawing.Point(16, 206);
            this.dgvPassenger.Name = "dgvPassenger";
            this.dgvPassenger.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            this.dgvPassenger.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPassenger.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.dgvPassenger.RowTemplate.Height = 23;
            this.dgvPassenger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPassenger.Size = new System.Drawing.Size(651, 186);
            this.dgvPassenger.TabIndex = 18;
            // 
            // colCode
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.colCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCode.HeaderText = "代码";
            this.colCode.Name = "colCode";
            this.colCode.Visible = false;
            // 
            // colIdentity
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            this.colIdentity.DefaultCellStyle = dataGridViewCellStyle2;
            this.colIdentity.FillWeight = 20F;
            this.colIdentity.HeaderText = "序号";
            this.colIdentity.Name = "colIdentity";
            this.colIdentity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSeat
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F);
            this.colSeat.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSeat.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colSeat.FillWeight = 40F;
            this.colSeat.HeaderText = "席别";
            this.colSeat.Name = "colSeat";
            this.colSeat.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colType
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F);
            this.colType.DefaultCellStyle = dataGridViewCellStyle4;
            this.colType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colType.FillWeight = 40F;
            this.colType.HeaderText = "票种";
            this.colType.Name = "colType";
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colName
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            this.colName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colName.FillWeight = 40F;
            this.colName.HeaderText = "姓名";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCardType
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F);
            this.colCardType.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCardType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colCardType.FillWeight = 40F;
            this.colCardType.HeaderText = "证件类型";
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            this.colCardType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colCardNo
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F);
            this.colCardNo.DefaultCellStyle = dataGridViewCellStyle7;
            this.colCardNo.FillWeight = 80F;
            this.colCardNo.HeaderText = "证件号码";
            this.colCardNo.Name = "colCardNo";
            this.colCardNo.ReadOnly = true;
            this.colCardNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCardNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colMobile
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F);
            this.colMobile.DefaultCellStyle = dataGridViewCellStyle8;
            this.colMobile.FillWeight = 40F;
            this.colMobile.HeaderText = "手机号码";
            this.colMobile.Name = "colMobile";
            this.colMobile.ReadOnly = true;
            this.colMobile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMobile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // grpTrainInfo
            // 
            this.grpTrainInfo.Controls.Add(this.pnlTrainInfo);
            this.grpTrainInfo.Location = new System.Drawing.Point(16, 34);
            this.grpTrainInfo.Name = "grpTrainInfo";
            this.grpTrainInfo.Size = new System.Drawing.Size(651, 82);
            this.grpTrainInfo.TabIndex = 20;
            this.grpTrainInfo.TabStop = false;
            this.grpTrainInfo.Text = "列车信息（以下余票信息仅供参考）";
            // 
            // pnlTrainInfo
            // 
            this.pnlTrainInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTrainInfo.Controls.Add(this.lblTicketInfo);
            this.pnlTrainInfo.Controls.Add(this.lblTrainInfo);
            this.pnlTrainInfo.Location = new System.Drawing.Point(6, 20);
            this.pnlTrainInfo.Name = "pnlTrainInfo";
            this.pnlTrainInfo.Size = new System.Drawing.Size(639, 56);
            this.pnlTrainInfo.TabIndex = 0;
            // 
            // lblTicketInfo
            // 
            this.lblTicketInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTicketInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblTicketInfo.Location = new System.Drawing.Point(3, 27);
            this.lblTicketInfo.Name = "lblTicketInfo";
            this.lblTicketInfo.Size = new System.Drawing.Size(631, 23);
            this.lblTicketInfo.TabIndex = 0;
            this.lblTicketInfo.Text = "车票信息";
            this.lblTicketInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblTrainInfo
            // 
            this.lblTrainInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTrainInfo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTrainInfo.Location = new System.Drawing.Point(3, 3);
            this.lblTrainInfo.Name = "lblTrainInfo";
            this.lblTrainInfo.Size = new System.Drawing.Size(631, 23);
            this.lblTrainInfo.TabIndex = 0;
            this.lblTrainInfo.Text = "列车信息";
            this.lblTrainInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picCode
            // 
            this.picCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCode.Image = null;
            this.picCode.Location = new System.Drawing.Point(206, 403);
            this.picCode.Name = "picCode";
            this.picCode.Size = new System.Drawing.Size(78, 28);
            this.picCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCode.TabIndex = 21;
            this.picCode.TabStop = false;
            // 
            // FrmConfirmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(216)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(679, 455);
            this.Controls.Add(this.picCode);
            this.Controls.Add(this.grpTrainInfo);
            this.Controls.Add(this.grpPassengersPanel);
            this.Controls.Add(this.dgvPassenger);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResize = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfirmOrder";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 30);
            this.ShowIcon = false;
            this.ShowMenu = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "确认订单";
            this.grpPassengersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassenger)).EndInit();
            this.grpTrainInfo.ResumeLayout(false);
            this.pnlTrainInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.GroupBox grpPassengersPanel;
        private System.Windows.Forms.Panel pnlPassenger;
        private System.Windows.Forms.DataGridView dgvPassenger;
        private System.Windows.Forms.GroupBox grpTrainInfo;
        private System.Windows.Forms.Panel pnlTrainInfo;
        private System.Windows.Forms.Label lblTicketInfo;
        private System.Windows.Forms.Label lblTrainInfo;
        private UI.PictureBoxExt picCode;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentity;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSeat;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMobile;
    }
}
namespace Paway.Ticker
{
    partial class FrmMain2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnAuto = new System.Windows.Forms.Button();
            this.dtpTrainDate = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblTrainDate = new System.Windows.Forms.Label();
            this.lblToStation = new System.Windows.Forms.Label();
            this.lblFromStation = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvQuery = new System.Windows.Forms.DataGridView();
            this.colTrainNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLiShi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSWZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colButtonTextInfo = new Paway.Ticker.DataGridViewDisableButtonColumn();
            this.dataGridViewDisableButtonColumn1 = new Paway.Ticker.DataGridViewDisableButtonColumn();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnAuto);
            this.pnlTop.Controls.Add(this.dtpTrainDate);
            this.pnlTop.Controls.Add(this.btnQuery);
            this.pnlTop.Controls.Add(this.lblTrainDate);
            this.pnlTop.Controls.Add(this.lblToStation);
            this.pnlTop.Controls.Add(this.lblFromStation);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(942, 120);
            this.pnlTop.TabIndex = 0;
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(357, 68);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(91, 35);
            this.btnAuto.TabIndex = 5;
            this.btnAuto.Text = "刷票";
            this.btnAuto.UseVisualStyleBackColor = true;
            // 
            // dtpTrainDate
            // 
            this.dtpTrainDate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dtpTrainDate.Location = new System.Drawing.Point(558, 16);
            this.dtpTrainDate.Name = "dtpTrainDate";
            this.dtpTrainDate.Size = new System.Drawing.Size(172, 23);
            this.dtpTrainDate.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(745, 16);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(91, 24);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // lblTrainDate
            // 
            this.lblTrainDate.AutoSize = true;
            this.lblTrainDate.Location = new System.Drawing.Point(508, 21);
            this.lblTrainDate.Name = "lblTrainDate";
            this.lblTrainDate.Size = new System.Drawing.Size(53, 12);
            this.lblTrainDate.TabIndex = 0;
            this.lblTrainDate.Text = "出发日：";
            // 
            // lblToStation
            // 
            this.lblToStation.AutoSize = true;
            this.lblToStation.Location = new System.Drawing.Point(260, 21);
            this.lblToStation.Name = "lblToStation";
            this.lblToStation.Size = new System.Drawing.Size(53, 12);
            this.lblToStation.TabIndex = 0;
            this.lblToStation.Text = "目的地：";
            // 
            // lblFromStation
            // 
            this.lblFromStation.AutoSize = true;
            this.lblFromStation.Location = new System.Drawing.Point(12, 21);
            this.lblFromStation.Name = "lblFromStation";
            this.lblFromStation.Size = new System.Drawing.Size(53, 12);
            this.lblFromStation.TabIndex = 0;
            this.lblFromStation.Text = "出发地：";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvQuery);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 120);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(942, 470);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvQuery
            // 
            this.dgvQuery.AllowUserToAddRows = false;
            this.dgvQuery.AllowUserToDeleteRows = false;
            this.dgvQuery.AllowUserToResizeColumns = false;
            this.dgvQuery.AllowUserToResizeRows = false;
            this.dgvQuery.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuery.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuery.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQuery.ColumnHeadersHeight = 30;
            this.dgvQuery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrainNo,
            this.colStation,
            this.colTime,
            this.colLiShi,
            this.colSWZ,
            this.colTZ,
            this.colZY,
            this.colZE,
            this.colGR,
            this.colRW,
            this.colYW,
            this.colRZ,
            this.colYZ,
            this.colWZ,
            this.colQT,
            this.colButtonTextInfo});
            this.dgvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuery.Location = new System.Drawing.Point(0, 0);
            this.dgvQuery.MultiSelect = false;
            this.dgvQuery.Name = "dgvQuery";
            this.dgvQuery.ReadOnly = true;
            this.dgvQuery.RowHeadersVisible = false;
            this.dgvQuery.RowTemplate.Height = 23;
            this.dgvQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuery.Size = new System.Drawing.Size(942, 470);
            this.dgvQuery.TabIndex = 0;
            // 
            // colTrainNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colTrainNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTrainNo.FillWeight = 30F;
            this.colTrainNo.HeaderText = "车次";
            this.colTrainNo.Name = "colTrainNo";
            this.colTrainNo.ReadOnly = true;
            this.colTrainNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colStation
            // 
            this.colStation.FillWeight = 60F;
            this.colStation.HeaderText = "出发站-到达站";
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // colTime
            // 
            this.colTime.FillWeight = 45F;
            this.colTime.HeaderText = "出发时间到达时间";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colLiShi
            // 
            this.colLiShi.FillWeight = 30F;
            this.colLiShi.HeaderText = "历时";
            this.colLiShi.Name = "colLiShi";
            this.colLiShi.ReadOnly = true;
            // 
            // colSWZ
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSWZ.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSWZ.FillWeight = 26.74625F;
            this.colSWZ.HeaderText = "商务座";
            this.colSWZ.Name = "colSWZ";
            this.colSWZ.ReadOnly = true;
            // 
            // colTZ
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTZ.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTZ.FillWeight = 26.74625F;
            this.colTZ.HeaderText = "特等座";
            this.colTZ.Name = "colTZ";
            this.colTZ.ReadOnly = true;
            // 
            // colZY
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colZY.DefaultCellStyle = dataGridViewCellStyle5;
            this.colZY.FillWeight = 26.74625F;
            this.colZY.HeaderText = "一等座";
            this.colZY.Name = "colZY";
            this.colZY.ReadOnly = true;
            // 
            // colZE
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colZE.DefaultCellStyle = dataGridViewCellStyle6;
            this.colZE.FillWeight = 26.74625F;
            this.colZE.HeaderText = "二等座";
            this.colZE.Name = "colZE";
            this.colZE.ReadOnly = true;
            // 
            // colGR
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colGR.DefaultCellStyle = dataGridViewCellStyle7;
            this.colGR.FillWeight = 25F;
            this.colGR.HeaderText = "高级软卧";
            this.colGR.Name = "colGR";
            this.colGR.ReadOnly = true;
            // 
            // colRW
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colRW.DefaultCellStyle = dataGridViewCellStyle8;
            this.colRW.FillWeight = 26.74625F;
            this.colRW.HeaderText = "软卧";
            this.colRW.Name = "colRW";
            this.colRW.ReadOnly = true;
            // 
            // colYW
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colYW.DefaultCellStyle = dataGridViewCellStyle9;
            this.colYW.FillWeight = 26.74625F;
            this.colYW.HeaderText = "硬卧";
            this.colYW.Name = "colYW";
            this.colYW.ReadOnly = true;
            // 
            // colRZ
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colRZ.DefaultCellStyle = dataGridViewCellStyle10;
            this.colRZ.FillWeight = 26.74625F;
            this.colRZ.HeaderText = "软座";
            this.colRZ.Name = "colRZ";
            this.colRZ.ReadOnly = true;
            // 
            // colYZ
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colYZ.DefaultCellStyle = dataGridViewCellStyle11;
            this.colYZ.FillWeight = 26.74625F;
            this.colYZ.HeaderText = "硬座";
            this.colYZ.Name = "colYZ";
            this.colYZ.ReadOnly = true;
            // 
            // colWZ
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colWZ.DefaultCellStyle = dataGridViewCellStyle12;
            this.colWZ.FillWeight = 26.74625F;
            this.colWZ.HeaderText = "无座";
            this.colWZ.Name = "colWZ";
            this.colWZ.ReadOnly = true;
            // 
            // colQT
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQT.DefaultCellStyle = dataGridViewCellStyle13;
            this.colQT.FillWeight = 26.74625F;
            this.colQT.HeaderText = "其它";
            this.colQT.Name = "colQT";
            this.colQT.ReadOnly = true;
            // 
            // colButtonTextInfo
            // 
            this.colButtonTextInfo.FillWeight = 50.52069F;
            this.colButtonTextInfo.HeaderText = "备注";
            this.colButtonTextInfo.Name = "colButtonTextInfo";
            this.colButtonTextInfo.ReadOnly = true;
            this.colButtonTextInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.FillWeight = 50.52069F;
            this.dataGridViewDisableButtonColumn1.HeaderText = "备注";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisableButtonColumn1.Width = 93;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 590);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗口";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblFromStation;
        private System.Windows.Forms.Label lblToStation;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblTrainDate;
        private System.Windows.Forms.DateTimePicker dtpTrainDate;
        private System.Windows.Forms.DataGridView dgvQuery;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrainNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLiShi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSWZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQT;
        private DataGridViewDisableButtonColumn colButtonTextInfo;
        private DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
    }
}
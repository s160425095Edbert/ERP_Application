namespace ERP_Application
{
    partial class FormNota
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
            this.dataGridViewNota = new System.Windows.Forms.DataGridView();
            this.colNomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamabarang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBanyaknya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSatuan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNota)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewNota
            // 
            this.dataGridViewNota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNota.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNomor,
            this.colNamabarang,
            this.colBanyaknya,
            this.colSatuan,
            this.colHarga,
            this.colJumlah});
            this.dataGridViewNota.Location = new System.Drawing.Point(12, 145);
            this.dataGridViewNota.Name = "dataGridViewNota";
            this.dataGridViewNota.RowHeadersWidth = 51;
            this.dataGridViewNota.RowTemplate.Height = 24;
            this.dataGridViewNota.Size = new System.Drawing.Size(750, 207);
            this.dataGridViewNota.TabIndex = 0;
            this.dataGridViewNota.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNota_CellValueChanged);
            this.dataGridViewNota.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewNota_RowsAdded);
            this.dataGridViewNota.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewNota_RowsRemoved);
            // 
            // colNomor
            // 
            this.colNomor.HeaderText = "No.";
            this.colNomor.MinimumWidth = 6;
            this.colNomor.Name = "colNomor";
            this.colNomor.Width = 70;
            // 
            // colNamabarang
            // 
            this.colNamabarang.HeaderText = "NAMA BARANG";
            this.colNamabarang.MinimumWidth = 6;
            this.colNamabarang.Name = "colNamabarang";
            this.colNamabarang.Width = 125;
            // 
            // colBanyaknya
            // 
            this.colBanyaknya.HeaderText = "BANYAKNYA";
            this.colBanyaknya.MinimumWidth = 6;
            this.colBanyaknya.Name = "colBanyaknya";
            this.colBanyaknya.Width = 125;
            // 
            // colSatuan
            // 
            this.colSatuan.HeaderText = "SATUAN";
            this.colSatuan.MinimumWidth = 6;
            this.colSatuan.Name = "colSatuan";
            this.colSatuan.Width = 125;
            // 
            // colHarga
            // 
            this.colHarga.HeaderText = "HARGA";
            this.colHarga.MinimumWidth = 6;
            this.colHarga.Name = "colHarga";
            this.colHarga.Width = 125;
            // 
            // colJumlah
            // 
            this.colJumlah.HeaderText = "JUMLAH";
            this.colJumlah.MinimumWidth = 6;
            this.colJumlah.Name = "colJumlah";
            this.colJumlah.Width = 125;
            // 
            // FormNota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewNota);
            this.Name = "FormNota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormNota";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormNota_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNota)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamabarang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBanyaknya;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSatuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJumlah;
    }
}
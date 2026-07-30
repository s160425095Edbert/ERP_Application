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
            this.labelTanggal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNama = new System.Windows.Forms.TextBox();
            this.colNomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamabarang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBanyaknya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSatuan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewNota
            // 
            this.dataGridViewNota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dataGridViewNota.Size = new System.Drawing.Size(922, 272);
            this.dataGridViewNota.TabIndex = 0;
            this.dataGridViewNota.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNota_CellValueChanged);
            this.dataGridViewNota.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewNota_RowsAdded);
            this.dataGridViewNota.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewNota_RowsRemoved);
            // 
            // labelTanggal
            // 
            this.labelTanggal.AutoSize = true;
            this.labelTanggal.Location = new System.Drawing.Point(698, 13);
            this.labelTanggal.Name = "labelTanggal";
            this.labelTanggal.Size = new System.Drawing.Size(208, 16);
            this.labelTanggal.TabIndex = 1;
            this.labelTanggal.Text = "Tanggal 21 Agustus 2026 00:00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(698, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kepada YTH:";
            // 
            // textBoxNama
            // 
            this.textBoxNama.Location = new System.Drawing.Point(793, 48);
            this.textBoxNama.Name = "textBoxNama";
            this.textBoxNama.Size = new System.Drawing.Size(147, 22);
            this.textBoxNama.TabIndex = 3;
            this.textBoxNama.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNama_KeyDown);
            // 
            // colNomor
            // 
            this.colNomor.FillWeight = 57.23598F;
            this.colNomor.HeaderText = "No.";
            this.colNomor.MinimumWidth = 6;
            this.colNomor.Name = "colNomor";
            // 
            // colNamabarang
            // 
            this.colNamabarang.FillWeight = 160.4279F;
            this.colNamabarang.HeaderText = "NAMA BARANG";
            this.colNamabarang.MinimumWidth = 6;
            this.colNamabarang.Name = "colNamabarang";
            // 
            // colBanyaknya
            // 
            this.colBanyaknya.FillWeight = 95.58409F;
            this.colBanyaknya.HeaderText = "BANYAKNYA";
            this.colBanyaknya.MinimumWidth = 6;
            this.colBanyaknya.Name = "colBanyaknya";
            // 
            // colSatuan
            // 
            this.colSatuan.FillWeight = 95.58409F;
            this.colSatuan.HeaderText = "SATUAN";
            this.colSatuan.MinimumWidth = 6;
            this.colSatuan.Name = "colSatuan";
            // 
            // colHarga
            // 
            this.colHarga.FillWeight = 95.58409F;
            this.colHarga.HeaderText = "HARGA";
            this.colHarga.MinimumWidth = 6;
            this.colHarga.Name = "colHarga";
            // 
            // colJumlah
            // 
            this.colJumlah.FillWeight = 95.58409F;
            this.colJumlah.HeaderText = "JUMLAH";
            this.colJumlah.MinimumWidth = 6;
            this.colJumlah.Name = "colJumlah";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tanda Terima";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(694, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hormat Kami,";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 509);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "_______________";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(694, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "_______________";
            // 
            // buttonPrint
            // 
            this.buttonPrint.BackColor = System.Drawing.Color.IndianRed;
            this.buttonPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonPrint.Location = new System.Drawing.Point(779, 607);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(127, 39);
            this.buttonPrint.TabIndex = 8;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(39, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 102);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FormNota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 658);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTanggal);
            this.Controls.Add(this.dataGridViewNota);
            this.Name = "FormNota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormNota";
            this.Load += new System.EventHandler(this.FormNota_Load);
            this.Shown += new System.EventHandler(this.FormNota_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewNota;
        private System.Windows.Forms.Label labelTanggal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamabarang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBanyaknya;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSatuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJumlah;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
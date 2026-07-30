using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ERP_Application
{
    public partial class FormNota : Form
    {
        Form1 frmMain;
        public FormNota()
        {
            InitializeComponent();
        }
        private void UpdateRowNumbers()
        {
            for (int i = 0; i < dataGridViewNota.Rows.Count; i++)
            {
                if (!dataGridViewNota.Rows[i].IsNewRow)
                {
                    dataGridViewNota.Rows[i].Cells["colNomor"].Value = (i + 1).ToString();
                }
            }
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            frmMain = (Form1)this.Owner;
            labelTanggal.Text = "Tanggal " + DateTime.Now;
            dataGridViewNota.Columns[0].ReadOnly = true; // No.
            dataGridViewNota.Columns[5].ReadOnly = true; // JUMLAH
        }

        private void dataGridViewNota_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateRowNumbers();
        }

        private void dataGridViewNota_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks or invalid row indexes
            if (e.RowIndex < 0) return;

            var row = dataGridViewNota.Rows[e.RowIndex];

            // Check if the edited cell was either BANYAKNYA or HARGA
            string columnName = dataGridViewNota.Columns[e.ColumnIndex].Name;

            if (columnName == "colBanyaknya" || columnName == "colHarga")
            {
                // Safely parse quantity and price values
                decimal qty = 0;
                decimal harga = 0;

                if (row.Cells["colBanyaknya"].Value != null)
                    decimal.TryParse(row.Cells["colBanyaknya"].Value.ToString(), out qty);

                if (row.Cells["colHarga"].Value != null)
                    decimal.TryParse(row.Cells["colHarga"].Value.ToString(), out harga);

                // Auto-calculate Subtotal (JUMLAH)
                decimal total = qty * harga;
                row.Cells["colJumlah"].Value = total > 0 ? total.ToString("N0") : "";
            }
        }

        private void dataGridViewNota_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateRowNumbers();
        }

        private void textBoxNama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents the beep sound

                // Move focus directly to the DataGridView's first item cell (NAMA BARANG)
                dataGridViewNota.Focus();
                if (dataGridViewNota.Rows.Count > 0)
                {
                    dataGridViewNota.CurrentCell = dataGridViewNota.Rows[0].Cells[1]; // Column 1 = NAMA BARANG
                }
            }
        }

        private void FormNota_Shown(object sender, EventArgs e)
        {
            // Highlighting / focusing the Name text box immediately
            textBoxNama.Focus();
            textBoxNama.SelectAll();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // 1. Create a PrintDocument object
            PrintDocument printDoc = new PrintDocument();

            // 2. Attach the PrintPage event handler
            printDoc.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Show native Windows Print Dialog so cashier can choose printer & page size
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            printDialog.AllowSomePages = false;
            printDialog.ShowHelp = false;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontHeader = new Font("Arial", 14, FontStyle.Bold);
            Font fontSubHeader = new Font("Arial", 10, FontStyle.Regular);
            Font fontBody = new Font("Arial", 9, FontStyle.Regular);
            Font fontBodyBold = new Font("Arial", 9, FontStyle.Bold);
            Font fontWarning = new Font("Arial", 8, FontStyle.Italic);

            int startX = 40;
            int startY = 40;
            int offsetY = 0;

            // --- HEADER ---
            g.DrawString("NOTA PENJUALAN", fontHeader, Brushes.Black, startX, startY + offsetY);
            offsetY += 25;

            g.DrawString("Tanggal : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontSubHeader, Brushes.Black, startX, startY + offsetY);
            offsetY += 20;

            g.DrawString("Kepada YTH : " + textBoxNama.Text, fontSubHeader, Brushes.Black, startX, startY + offsetY);
            offsetY += 30;

            // --- TABLE HEADERS ---
            g.DrawString("NO.", fontBodyBold, Brushes.Black, startX, startY + offsetY);
            g.DrawString("NAMA BARANG", fontBodyBold, Brushes.Black, startX + 40, startY + offsetY);
            g.DrawString("BANYAKNYA", fontBodyBold, Brushes.Black, startX + 260, startY + offsetY);
            g.DrawString("SATUAN", fontBodyBold, Brushes.Black, startX + 360, startY + offsetY);
            g.DrawString("HARGA", fontBodyBold, Brushes.Black, startX + 440, startY + offsetY);
            g.DrawString("JUMLAH", fontBodyBold, Brushes.Black, startX + 540, startY + offsetY);

            offsetY += 15;
            g.DrawLine(Pens.Black, startX, startY + offsetY, startX + 650, startY + offsetY);
            offsetY += 10;

            // --- TABLE DATA ---
            decimal grandTotal = 0;

            for (int i = 0; i < dataGridViewNota.Rows.Count; i++)
            {
                var row = dataGridViewNota.Rows[i];
                if (row.IsNewRow) continue;

                string no = row.Cells[0].Value?.ToString() ?? "";
                string nama = row.Cells[1].Value?.ToString() ?? "";
                string banyak = row.Cells[2].Value?.ToString() ?? "";
                string satuan = row.Cells[3].Value?.ToString() ?? "";
                string harga = row.Cells[4].Value?.ToString() ?? "";
                string jumlah = row.Cells[5].Value?.ToString() ?? "";

                decimal rowTotal = 0;
                decimal.TryParse(jumlah.Replace(".", ""), out rowTotal);
                grandTotal += rowTotal;

                g.DrawString(no, fontBody, Brushes.Black, startX, startY + offsetY);
                g.DrawString(nama, fontBody, Brushes.Black, startX + 40, startY + offsetY);
                g.DrawString(banyak, fontBody, Brushes.Black, startX + 260, startY + offsetY);
                g.DrawString(satuan, fontBody, Brushes.Black, startX + 360, startY + offsetY);
                g.DrawString(harga, fontBody, Brushes.Black, startX + 440, startY + offsetY);
                g.DrawString(jumlah, fontBody, Brushes.Black, startX + 540, startY + offsetY);

                offsetY += 20;
            }

            // --- TOTAL ---
            g.DrawLine(Pens.Black, startX, startY + offsetY, startX + 650, startY + offsetY);
            offsetY += 10;

            g.DrawString("TOTAL: Rp " + grandTotal.ToString("N0"), fontBodyBold, Brushes.Black, startX + 440, startY + offsetY);
            offsetY += 40;

            // --- FOOTER: SIGNATURES & WARNING BOX ---
            int footerY = startY + offsetY;

            // 1. Left Signature
            g.DrawString("Tanda Terima,", fontBody, Brushes.Black, startX + 20, footerY);
            g.DrawString("( _______________ )", fontBody, Brushes.Black, startX + 10, footerY + 60);

            // 2. Middle Warning Box ("PERHATIAN...")
            int boxX = startX + 200;
            int boxY = footerY;
            int boxWidth = 220;
            int boxHeight = 65;

            // Draw outline rectangle box
            g.DrawRectangle(Pens.Black, boxX, boxY, boxWidth, boxHeight);

            // Draw wrapped warning text inside the box
            string warningText = "PERHATIAN:\nBarang yang telah dibeli tidak dapat dikembalikan atau ditukar.";
            RectangleF textRect = new RectangleF(boxX + 5, boxY + 5, boxWidth - 10, boxHeight - 10);
            g.DrawString(warningText, fontWarning, Brushes.Black, textRect);

            // 3. Right Signature
            g.DrawString("Hormat Kami,", fontBody, Brushes.Black, startX + 480, footerY);
            g.DrawString("( _______________ )", fontBody, Brushes.Black, startX + 460, footerY + 60);
        }
    }
}

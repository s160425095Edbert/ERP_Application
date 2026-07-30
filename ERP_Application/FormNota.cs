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
using MySql.Data.MySqlClient;

namespace ERP_Application
{
    public partial class FormNota : Form
    {
        Form1 frmMain;
        private string connString = "Server=localhost;Database=toko_db;Uid=root;Pwd=;";
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
        private bool SaveNotaToDatabase()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    // A. Generate Unique No Nota (e.g. NT-20260730-153022)
                    string noNota = "NT-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

                    // Calculate Grand Total
                    decimal grandTotal = 0;
                    foreach (DataGridViewRow row in dataGridViewNota.Rows)
                    {
                        if (row.IsNewRow) continue;
                        decimal subtotal = 0;
                        if (row.Cells[5].Value != null)
                            decimal.TryParse(row.Cells[5].Value.ToString().Replace(".", ""), out subtotal);
                        grandTotal += subtotal;
                    }

                    // B. Insert Header into 'penjualan'
                    string queryPenjualan = "INSERT INTO penjualan (no_nota, tanggal_nota, pembeli, total_harga) " +
                                            "VALUES (@noNota, @tanggal, @pembeli, @total)";

                    using (MySqlCommand cmdPenjualan = new MySqlCommand(queryPenjualan, conn, transaction))
                    {
                        cmdPenjualan.Parameters.AddWithValue("@noNota", noNota);
                        cmdPenjualan.Parameters.AddWithValue("@tanggal", DateTime.Now);
                        cmdPenjualan.Parameters.AddWithValue("@pembeli", string.IsNullOrWhiteSpace(textBoxNama.Text) ? "General" : textBoxNama.Text);
                        cmdPenjualan.Parameters.AddWithValue("@total", grandTotal);
                        cmdPenjualan.ExecuteNonQuery();
                    }

                    // C. Insert Details into 'detail_penjualan' & Deduct Stock in 'barang'
                    foreach (DataGridViewRow row in dataGridViewNota.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string namaBarang = row.Cells[1].Value?.ToString() ?? "";
                        int qty = 0;
                        int.TryParse(row.Cells[2].Value?.ToString(), out qty);

                        decimal hargaSatuan = 0;
                        decimal.TryParse(row.Cells[4].Value?.ToString().Replace(".", ""), out hargaSatuan);

                        decimal subtotal = qty * hargaSatuan;

                        // Obtain id_barang (either from row.Tag or search DB)
                        int idBarang = 0;
                        if (row.Tag != null)
                        {
                            idBarang = Convert.ToInt32(row.Tag);
                        }
                        else
                        {
                            // Fallback lookup if row.Tag wasn't set
                            string findIdQuery = "SELECT id_barang FROM barang WHERE nama_barang = @nama LIMIT 1";
                            using (MySqlCommand cmdFind = new MySqlCommand(findIdQuery, conn, transaction))
                            {
                                cmdFind.Parameters.AddWithValue("@nama", namaBarang);
                                object result = cmdFind.ExecuteScalar();
                                if (result != null) idBarang = Convert.ToInt32(result);
                            }
                        }

                        if (idBarang > 0)
                        {
                            // 1. Insert row detail
                            string queryDetail = "INSERT INTO detail_penjualan (no_nota, id_barang, banyaknya, harga_satuan, subtotal) " +
                                                 "VALUES (@noNota, @idBarang, @qty, @harga, @subtotal)";

                            using (MySqlCommand cmdDetail = new MySqlCommand(queryDetail, conn, transaction))
                            {
                                cmdDetail.Parameters.AddWithValue("@noNota", noNota);
                                cmdDetail.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdDetail.Parameters.AddWithValue("@qty", qty);
                                cmdDetail.Parameters.AddWithValue("@harga", hargaSatuan);
                                cmdDetail.Parameters.AddWithValue("@subtotal", subtotal);
                                cmdDetail.ExecuteNonQuery();
                            }

                            // 2. Reduce Stock in 'barang' table automatically!
                            string queryUpdateStock = "UPDATE barang SET stok = stok - @qty WHERE id_barang = @idBarang";
                            using (MySqlCommand cmdStock = new MySqlCommand(queryUpdateStock, conn, transaction))
                            {
                                cmdStock.Parameters.AddWithValue("@qty", qty);
                                cmdStock.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdStock.ExecuteNonQuery();
                            }
                        }
                    }

                    // Commit transaction
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save to database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            frmMain = (Form1)this.Owner;
            labelTanggal.Text = "Tanggal " + DateTime.Now;
            dataGridViewNota.Columns[0].ReadOnly = true; // No.
            dataGridViewNota.Columns[5].ReadOnly = true; // JUMLAH
            textBoxNama.Text = "CASH";
        }

        private void dataGridViewNota_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateRowNumbers();
        }

        
        private void dataGridViewNota_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewNota.Rows[e.RowIndex];
            string columnName = dataGridViewNota.Columns[e.ColumnIndex].Name;

            // 1. If NAMA BARANG changed, auto-fetch Satuan & Harga from DB
            if (columnName == "colNamaBarang" || e.ColumnIndex == 1)
            {
                string namaBarang = row.Cells[1].Value?.ToString();

                if (!string.IsNullOrEmpty(namaBarang))
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "SELECT id_barang, satuan, harga_jual FROM barang WHERE nama_barang = @nama LIMIT 1";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@nama", namaBarang);
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        // Store id_barang in Tag property (hidden data helper)
                                        row.Tag = reader["id_barang"];
                                        row.Cells[3].Value = reader["satuan"].ToString();   // SATUAN
                                        row.Cells[4].Value = Convert.ToDecimal(reader["harga_jual"]).ToString("N0"); // HARGA
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Fail silently or handle connection warning
                        }
                    }
                }
            }

            // 2. Recalculate JUMLAH whenever BANYAKNYA or HARGA changes
            decimal qty = 0;
            decimal harga = 0;

            if (row.Cells[2].Value != null)
                decimal.TryParse(row.Cells[2].Value.ToString(), out qty);

            if (row.Cells[4].Value != null)
                decimal.TryParse(row.Cells[4].Value.ToString().Replace(".", ""), out harga);

            decimal total = qty * harga;
            row.Cells[5].Value = total > 0 ? total.ToString("N0") : "";
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
            if (SaveNotaToDatabase())
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

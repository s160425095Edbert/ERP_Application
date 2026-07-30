using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}

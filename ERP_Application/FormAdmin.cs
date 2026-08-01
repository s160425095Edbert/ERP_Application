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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormData frm = new FormData();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void pembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPembelian frm = new FormPembelian();
            frm.Owner = this;
            frm.ShowDialog();
        }
    }
}

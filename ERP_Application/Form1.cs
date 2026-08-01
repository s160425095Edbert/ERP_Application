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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void CenterLabel()
        {
            // Calculate the X coordinate (middle of the form minus half of the label's width)
            int x = (this.ClientSize.Width - labelTime.Width) / 2;

            // Calculate the Y coordinate (middle of the form minus half of the label's height)
            int y = (this.ClientSize.Height - labelTime.Height) / 2;

            // Apply the new location to the label
            labelTime.Location = new Point(x, y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("dd MMMM yyyy");
            CenterLabel();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterLabel();
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Create the File Dialog instance
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 2. Set the window title
                openFileDialog.Title = "Select Background Image";

                // 3. Filter allowed file extensions (PNG, GIF, JPG)
                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif|All Files|*.*";

                // 4. Open the dialog box and wait for user to select a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 5. Load the selected image into memory
                    Image bgImage = Image.FromFile(openFileDialog.FileName);

                    // 6. Set the form's background image
                    this.BackgroundImage = bgImage;

                    // 7. Scale the image properly to cover the screen
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNota frm = new FormNota();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin frm = new FormAdmin();
            frm.Owner = this;
            frm.ShowDialog();
        }
    }
}

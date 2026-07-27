using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP_Application
{
    public class Barang
    {
        public int IdBarang { get; set; }
        public string NamaBarang { get; set; }
        public int Jumlah { get; set; }
        public string Satuan { get; set; } // pcs, pkg, dus, ktk, etc.
        public decimal HargaSatuan { get; set; }

        // Automatically calculates subtotal when Jumlah or HargaSatuan changes
        public decimal Subtotal => Jumlah * HargaSatuan;
    }
}
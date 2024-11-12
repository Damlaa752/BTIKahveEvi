using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTIKahveEvi
{
    public partial class SiparisEkrani : Form
    {
        public SiparisEkrani(int siparisAdedi, double toplamTutar)
        {
            InitializeComponent();
            lblToplamSiparis.Text=$"Toplam {siparisAdedi} Adedi Siparişiniz var ";
            lblToplamTutar.Text=$"{toplamTutar:F2} TL Tutarındadır.";

        }     

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}

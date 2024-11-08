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
            lblToplamSiparis.Text=$"Sipariş Adedi : {siparisAdedi}";
            lblToplamTutar.Text=$"Toplam Tutar :  {toplamTutar:F2} TL";

        }     

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

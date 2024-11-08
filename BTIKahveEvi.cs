using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace BTIKahveEvi
{
    public partial class BTIKahveEvi : Form
    {
        #region Diziler
        //public static string[][] icecekAdlari;
        //public static double[][] icecekFiyatlari;
        public static string[][] icecekAdlari = new string[][]
        {
            new string[] { "Misto = 4.5 TL ", "Americano = 5.75 TL ", "Bianco = 6 TL ", "Cappucino = 7.5 TL ", "Macchiato = 6.75 TL ", "Con Panna = 8 TL ", "Mocha = 7.75 TL " },
            new string[] { "Su = 5.5", "Meyve Suyu = 5.5" },
            new string[] { "Çay = 3 TL ", "Hot Chocolate = 4.5 TL ", "Chai Tea Latte = 6.5 TL " }
        };

        public static double[][] icecekFiyatlari = new double[][]
        {
            new double[] { 4.5, 5.75, 6, 7.5, 6.75, 8, 7.75 },
            new double[] { 5.5, 5.5 },
            new double[] { 3, 4.5, 6.5 }
        };
        //string[] kahveBoylari =
        //    {
        //    "Yağsız = + 0.5 TL ", 
        //    "Soya = + 0.5 TL ", 
        //    "Tall = x1 ",
        //    "Grande = x1.25 " ,
        //    "Venti = x1.75"
        //};

        #endregion

        public BTIKahveEvi()
        {
            InitializeComponent();
        }
        #region Classlar
        public class Siparis
        {
            public string UrunAdi { get; set; }
            public int Adet { get; set; }
            public double UrunBirimFiyati { get; set; }
            public double ToplamTutar => Adet* UrunBirimFiyati;
        }
        #endregion

        #region HesaplaVeEkleClaası

        private void BTIKahveEvi_Load(object sender, EventArgs e)
        {
            if (icecekAdlari == null)
            {
                Console.WriteLine("booşş Diziiiiii");
            }
            ComboBox[] combo = { cmbHotCoffee, cmbFrezeeDrink, cmbHotDrink };
            for (int i = 0; i < combo.Length; i++)
            {
                if (combo[i] != null && i < icecekAdlari.Length && icecekAdlari[i] != null)
                {
                    combo[i].Items.AddRange(icecekAdlari[i]);
                }
            }
        }
        #endregion

        private void buttonHesapla_Click(object sender, EventArgs e)
        {
            #region Textboxlar Üst GruopBox
            // SiparisListBox.Items.Add(txtName.Text);
            // SiparisListBox.Items.Add(txtPhone.Text);
            // SiparisListBox.Items.Add(txtAdress.Text);
            TextBox[] textBoxes = { txtName, txtPhone, txtAdress };

            foreach (TextBox txt in textBoxes)
            {
                if (txt.Text != "")
                {
                    SiparisListBox.Items.Add(txt.Text);
                }
            }
            #endregion

            #region Comboboxlar ve NumericUpDownlar

            ComboBox[] combobox = { cmbFrezeeDrink, cmbHotCoffee, cmbHotDrink };
            NumericUpDown[] num = {numHotCoffee, numericUpDown2,numericUpDown3};
            double genelToplam = 0;
            

            //SiparisListBox.Items.Clear();

            for (int i =0; i< combobox.Length; i++)
            {
                if (combobox[i].SelectedItem != null && num[i].Value > 0)
                {
                    string secilenUrun = combobox[i].SelectedItem.ToString();
                    int adet = Convert.ToInt32(num[i].Value);
                    double urunFiyati = 0;
                    bool urunBulundu = false;

                    for (int j = 0; j < icecekAdlari.Length && !urunBulundu; j++)
                    {
                        int indeks = Array.IndexOf(icecekAdlari[j], secilenUrun);
                        if (indeks != -1)
                        {
                            urunFiyati = icecekFiyatlari[j][indeks];
                            urunBulundu = true;
                        }
                    }
                    //toplam fiyatı hesapla
                    double toplamFiyat = urunFiyati * adet;

                    //listboxa yazdırma
                    SiparisListBox.Items.Add($"{secilenUrun}\n - Adet {adet}\n - Toplam\n {toplamFiyat:F2}\n--------------  ");
                    
                    //genel toplama ekleme işlemi
                    genelToplam += toplamFiyat;
                }
                else if (combobox[i].SelectedItem == null)
                {
                    MessageBox.Show("Herhangi bir içecek seçimiz bulunmamaktadır.");
                }
                else if (num[i].Value == -1)
                {
                    MessageBox.Show("Adet değer girilmedi");
                }
            }
            #endregion
            //Listboxa en son ki toplamı yazdırma
            SiparisListBox.Items.Add("-------------------");
            //SiparisListBox.Items.Add($"Sipariş Genel Toplam : {toplamFiyat:F2} TL");  
            SiparisListBox.Items.Add("-------------------");
            //En son ki toplam tutraı en alttaki labela yazdırdım
            lblToplam.Text =$"Toplam Tutar : {genelToplam:F2} TL";
        }

        private void buttonSiparisVer_Click(object sender, EventArgs e)
        {
            //Burda dedim ki siparisAdedini sipariş listesi içindeki index kadar yaz
            int siparisAdedi = siparisListesi.Count;
            //int siparisAdedi = siparisAdedi + siparisListesi.Count;
            //int siparisAdedi = siparisListesi.Sum(siparis => siparis.SiparisAdedi);
            double toplamTutar = siparisListesi.Sum(s => s.ToplamTutar);

            SiparisEkrani form2 = new SiparisEkrani(siparisAdedi, toplamTutar);
            form2.Show();
        }

        private void cmbHotCoffee_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
}

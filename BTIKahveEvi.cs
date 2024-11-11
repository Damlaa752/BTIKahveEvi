using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        public static int SiparisSayisi { get; set; }
        public static int SiparisToplam = 0;

        #region Diziler      
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
        #endregion

        public BTIKahveEvi()
        {
            InitializeComponent();

            chck1x.CheckedChanged += Shot_CheckChanges;
            chc2x.CheckedChanged += Shot_CheckChanges;
        }
        // ListBox için yatay kaydırma çubuğunu etkinleştirin ve en geniş öğeye göre ayarlayın
        private void Shot_CheckChanges(object sender, EventArgs e)
        {
            if (chck1x.Checked)
                chc2x.Checked = false;
            else if (chc2x.Checked)
                chck1x.Checked = false;
        }

        #region Classlar
        public class Siparisler
        {
            public string UrunAdi { get; set; }
            public int Adet { get; set; }
            public double UrunBirimFiyati { get; set; }
            public double BoyutKatSayisi { get; set; }
            public double EkstraFiyat { get; set; }
            //public double SiparisTutari { get; set; }
            public double ToplamTutar => (Adet * UrunBirimFiyati * BoyutKatSayisi) + (Adet * EkstraFiyat);

            public override string ToString()
            {
                return $"{UrunAdi} - Adet: {Adet}, Boyut Katsayısı: {BoyutKatSayisi}, Ekstra Ürün ücretleri: {EkstraFiyat}, Toplam: {ToplamTutar:F2} TL";
            }
        }

        #endregion

        #region HesaplaVeEkleClaası

        private void BTIKahveEvi_Load(object sender, EventArgs e)

        {
            //Control olayında iki farklı diziyi tek bir dizi yapamazsın sadece farklı türdeki tooları bir diziye alabilirsin o kadar.               
            Control[] cntrl = { chck1x, chc2x, radSoya, radYagsiz, radTall, radGrande, radVenti };
            foreach (Control item in cntrl)
            {
                item.Enabled=false;
            }

            if (icecekAdlari == null)
            {
                Console.WriteLine("booşş Diziiiiii");
            }
            //önce comboboxları dizi yaptık
            ComboBox[] combo = { cmbHotCoffee, cmbFrezeeDrink, cmbHotDrink };
            //Sonra dizinin içini dizi boyunca gez dedik
            for (int i = 0; i < combo.Length; i++)
            {
                //eğer combonun içindeki index değeri boş değil ve i icecekAdlari uzunluğundan küçükse, icecekadlarinda ki index değeride null değilse 
                if (combo[i] != null && i < icecekAdlari.Length && icecekAdlari[i] != null)
                {
                    //combonun indexini icecekAdlarının indexiyle doldur(Ekle)
                    combo[i].Items.AddRange(icecekAdlari[i]);
                }
            }
        }
        #endregion
        //Diğer formda da bunu yazdırıcağım için mecburen sınıfın üzerinde tanımladım
        List<Siparisler> siparisListesi = new List<Siparisler>();

        private void buttonHesapla_Click(object sender, EventArgs e)
        {
            #region Textboxlar Üst GruopBox
            //Textboxlardaki ismi listboxa ekleme
            TextBox[] textBoxes = { txtName, txtPhone, txtAdress };
            ComboBox[] combobox = { cmbHotCoffee, cmbFrezeeDrink, cmbHotDrink };
            NumericUpDown[] num = { numHotCoffee, numericUpDown2, numericUpDown3 };
            double genelToplam = 0;
            double toplamFiyat = 0;

            bool BosAlanVarMi = false;
            foreach (TextBox txt in textBoxes)
            {
                if (txt.Text != "")
                    SiparisListBox.Items.Add(txt.Text);
                else
                    BosAlanVarMi=true;
            }
            if (BosAlanVarMi)
                MessageBox.Show("Lütfen sizin için ayırılan alanları boş geçmeyiniz.!");

            #endregion

            #region Comboboxlar ve NumericUpDownlar Ekstra Ürünler
            for (int i = 0; i < combobox.Length; i++)
            {
                if (combobox[i].SelectedItem != null && num[i].Value > 0)
                {
                    string secilenUrun = combobox[i].SelectedItem.ToString();
                    int adet = Convert.ToInt32(num[i].Value);
                    double urunFiyati = 0;
                    bool urunBulundu = false;
                    double boyutKatsayisi = 1;
                    double ekstraFiyat = 0.5;
                    int siparisAdediHesapla = 0;

                    //fiyatlarla isimleri matchiyoruz aslına bakarsan 
                    for (int j = 0; j < icecekAdlari.Length && !urunBulundu; j++)
                    {
                        int indeks = Array.IndexOf(icecekAdlari[j], secilenUrun);
                        if (indeks != -1)
                        {
                            urunFiyati = icecekFiyatlari[j][indeks];
                            urunBulundu = true;
                        }
                    }
                    //seçilen ürün kahve ise alttaki zımbırtıları koşullara göre seçsin
                    if (icecekAdlari[0].Contains(secilenUrun))
                    {
                        #region RadioButton seçimlerine göre kahve boyutu kat sayısı artışı (tall-grande-venti)
                        //kat sayı çarpımı olcak çünkü küsüratlı olduğundan double dedim
                        if (radGrande.Checked)
                            boyutKatsayisi = 1.25;
                        else if (radVenti.Checked)
                            boyutKatsayisi = 1.75;

                        toplamFiyat = urunFiyati * adet * boyutKatsayisi;
                        #endregion

                        #region Chechkboxlara seçimine göre ekstra fiyat artışı (shotlar için)
                        //birden fazla seçim yapılamadığı için else if kullandım

                        if (chck1x.Checked)
                            ekstraFiyat=0.75 * adet;
                        else if (chc2x.Checked)
                            ekstraFiyat=0.75 * adet;

                        toplamFiyat += ekstraFiyat; 
                        #endregion

                        #region RadioButton Süt seçimlerine göre ekstra fiyat artışı (soya ve yagsiz)
                        if (radYagsiz.Checked)
                            toplamFiyat+= ekstraFiyat * adet;
                        if (radSoya.Checked)
                            toplamFiyat += ekstraFiyat * adet;

                        toplamFiyat += ekstraFiyat;
                        #endregion
                    }
                    //girilen değeri diğer sayfaya yollamak için classla matchliyoruz
                    Siparisler siparis = new Siparisler
                    {
                        UrunAdi = secilenUrun,
                        Adet = adet,
                        UrunBirimFiyati = urunFiyati,
                        BoyutKatSayisi = boyutKatsayisi,
                        EkstraFiyat=(cmbHotCoffee.SelectedIndex == 0) ? 0.5 : 0,
                    };

                    //sipariş listeye ekleme
                    siparisListesi.Add(siparis);

                    //listboxa yazdırma
                    SiparisListBox.Items.Add(siparis.ToString());

                }

                //genel toplama ekleme işlemi
                genelToplam=siparisListesi.Sum(s => s.ToplamTutar);

                if (combobox[i].SelectedItem != null && num[i].Value == 0)
                {
                    MessageBox.Show("Adet bilgilerini kontrol edin, girilmemiş değer var");
                }
            }
            // siparisToplam++;
            #endregion
            //Listboxa en son ki toplamı yazdırma
            SiparisListBox.Items.Add("-------------------");
            SiparisListBox.Items.Add("-------------------");
            //En son ki toplam tutraı en alttaki labela yazdırdım
            lblToplam.Text =$"Toplam Tutar : {genelToplam:F2} TL";
            SiparisSayisi = SiparisToplam++;
        }

        private void buttonSiparisVer_Click(object sender, EventArgs e)
        {
            //Burda dedim ki siparisAdedini sipariş listesi içindeki index kadar yaz

            double toplamTutar = siparisListesi.Sum(s => s.ToplamTutar);

            SiparisEkrani form2 = new SiparisEkrani(SiparisSayisi, toplamTutar);
            form2.Show();
        }

        private void cmbHotCoffee_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control[] cntrl = { chck1x, chc2x, radSoya, radYagsiz, radTall, radGrande, radVenti };
            if (cmbHotCoffee.SelectedIndex == null)
            {
                foreach (var control in cntrl)
                {
                    control.Enabled =false;
                }
            }
            else if ((cmbHotCoffee.SelectedIndex != null && cmbFrezeeDrink.SelectedIndex != null) || (cmbFrezeeDrink.SelectedIndex != null && cmbHotCoffee.SelectedIndex == null))
            {
                foreach (var control in cntrl)
                {
                    control.Enabled =true;
                }
            }
            else if ((cmbHotCoffee.SelectedIndex != null && cmbHotDrink.SelectedIndex != null) || (cmbHotDrink.SelectedIndex!= null && cmbHotCoffee.SelectedIndex==null))
            {
                foreach (var control in cntrl)
                {
                    control.Enabled =true;
                }
            }

            else if ((cmbHotDrink.SelectedIndex != null && cmbFrezeeDrink != null) || (cmbFrezeeDrink != null && cmbHotDrink.SelectedIndex != null))
            {
                foreach (var control in cntrl)
                {
                    control.Enabled=false;
                }
            }
            else
            {
                foreach (var control in cntrl)
                {
                    control.Enabled = true;
                }
            }
        }
        private void cmbFrezeeDrink_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Control olayında iki farklı diziyi tek bir dizi yapamazsın sadece farklı türdeki tooları bir diziye alabilirsin o kadar. 
            Control[] cntrl = { chck1x, chc2x, radSoya, radYagsiz, radTall, radGrande, radVenti };
            if (cmbFrezeeDrink.SelectedIndex == null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=false;
                }
            }
            else if (cmbFrezeeDrink.SelectedIndex == null && cmbHotCoffee.SelectedIndex !=null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=true;
                }
            }
            else if (cmbHotCoffee.SelectedIndex ==null && cmbFrezeeDrink.SelectedIndex != null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=false;
                }
            }         
            else if (cmbFrezeeDrink.SelectedIndex != null && cmbHotCoffee.SelectedIndex == null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=false;
                }
            }   
            else if (cmbHotCoffee.SelectedIndex != null && cmbHotDrink.SelectedIndex !=null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=true;
                }
            }
            else
            {
                foreach (var control in cntrl)
                {
                    control.Enabled = false;
                }
            }
        }
        private void cmbHotDrink_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Control olayında iki farklı diziyi tek bir dizi yapamazsın sadece farklı türdeki tooları bir diziye alabilirsin o kadar.               
            Control[] cntrl = { chck1x, chc2x, radSoya, radYagsiz, radTall, radGrande, radVenti };
            if (cmbHotDrink.SelectedIndex == null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=false;
                }
            }
            else if ((cmbHotDrink.SelectedIndex != null && cmbHotCoffee.SelectedIndex ==null) || (cmbHotCoffee.SelectedIndex == null && cmbHotDrink.SelectedIndex != null))
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=false;
                }
            }
            else if ((cmbHotDrink.SelectedIndex == null && cmbHotCoffee.SelectedIndex !=null) || (cmbHotCoffee.SelectedIndex !=null && cmbHotDrink.SelectedIndex == null))
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=true;
                }
            }     
            else if (cmbHotCoffee.SelectedIndex != null && cmbFrezeeDrink.SelectedIndex != null)
            {
                foreach (Control item in cntrl)
                {
                    item.Enabled=true;
                }
            }        
            else
            {
                foreach (var control in cntrl)
                {
                    control.Enabled = false;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GÖRÜNTÜ_İŞLEME
{
    public partial class Histogram : Form
    {
        int mov;
        int movx;
        int movy;
        public Histogram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filepath = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(filepath);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList DiziPiksel = new ArrayList();
            int OrtalamaRenk = 0; Color OkunanRenk; int R = 0, G = 0, B = 0; Bitmap GirisResmi; //Histogram için giriş resmi gri-ton olmalıdır.
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = GirisResmi.Height;
            int i = 0; //piksel sayısı tutulacak.
            for (int x = 0; x < GirisResmi.Width; x++)
            { 
                for (int y = 0; y < GirisResmi.Height; y++) 
                { 
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    OrtalamaRenk = (int)(OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3; //Griton resimde üç kanal rengi aynı değere sahiptir.
             DiziPiksel.Add(OrtalamaRenk); //Resimdeki tüm noktaları diziye atıyor.
                } 
            }
            int[] DiziPikselSayilari = new int[256]; for (int r = 0; r <= 255; r++) //256 tane renk tonu için dönecek.
            { int PikselSayisi=0; for (int s = 0; s < DiziPiksel.Count ; s++) //resimdeki piksel sayısınca dönecek. 
                { if (r == Convert.ToInt16(DiziPiksel[s]))
                        PikselSayisi++;
                } DiziPikselSayilari[r] = PikselSayisi;
            }
                                                                                    //Değerleri listbox'a ekliyor.
            int RenkMaksPikselSayisi = 0; //Grafikte y eksenini ölçeklerken kullanılacak.
            for (int k = 0; k <= 255; k++) 
            {
                listBox1.Items.Add("Renk:" + k + "=" + DiziPikselSayilari[k]); //Maksimum piksel sayısını bulmaya çalışıyor.
                if(DiziPikselSayilari[k]>RenkMaksPikselSayisi)
                { 
                    RenkMaksPikselSayisi=DiziPikselSayilari[k];
                } 
            }
                                                                                    //Grafiği çiziyor.
            Graphics CizimAlani;
            Pen Kalem1 = new Pen(System.Drawing.Color.Black, 1);
            Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
            CizimAlani = pictureBox2.CreateGraphics();
                pictureBox2.Refresh(); int GrafikYuksekligi = 300; 
            double OlcekY = RenkMaksPikselSayisi / GrafikYuksekligi;
            double OlcekX = 1.5;
            int X_kaydirma = 10;
            for (int x = 0; x <= 255; x++)
            {
                if (x % 50 == 0)
                        CizimAlani.DrawLine(Kalem2, (int)(X_kaydirma + x * OlcekX), 
                        GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), 0);
                        CizimAlani.DrawLine(Kalem1, (int)(X_kaydirma + x * OlcekX),
                        GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX),
                       (GrafikYuksekligi - (int)(DiziPikselSayilari[x] / OlcekY)));
                //Dikey kırmızı çizgiler.
            }
            textBox1.Text = "Maks.Piks=" + RenkMaksPikselSayisi.ToString();
            textBox1.ReadOnly = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movx = e.X;
            movy = e.Y;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movx, MousePosition.Y - movy);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

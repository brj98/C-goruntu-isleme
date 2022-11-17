using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GÖRÜNTÜ_İŞLEME
{
    public partial class Filtreler : Form
    {
        int mov;
        int movx;
        int movy;
        public Filtreler()
        {
            InitializeComponent();
        }

        private void Filtreler_Load(object sender, EventArgs e)
        {

        }
        private void btnYukle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filepath = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(filepath);
        }

        private void btnOrtalamaF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = Convert.ToInt32(numericUpDown1.Value); 
                int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R;
                                toplamG = toplamG + OkunanRenk.G;
                                toplamB = toplamB + OkunanRenk.B;
                            }
                        }
                        ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                        ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                        ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                        CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnGaussF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = 5; 
                int ElemanSayisi = SablonBoyutu * SablonBoyutu;
                int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
                int[] Matris = { 1, 4, 7, 4, 1, 4, 20, 33, 20, 4, 7, 33, 55, 33, 7, 4, 20, 33, 20, 4, 1, 4, 7, 4, 1 };
                int MatrisToplami = 1 + 4 + 7 + 4 + 1 + 4 + 20 + 33 + 20 + 4 + 7 + 33 + 55 + 33 + 7 + 4 + 20 +
               33 + 20 + 4 + 1 + 4 + 7 + 4 + 1;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) 
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;
                        int k = 0; 
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R * Matris[k];
                                toplamG = toplamG + OkunanRenk.G * Matris[k];
                                toplamB = toplamB + OkunanRenk.B * Matris[k];
                                k++;
                            }
                            ortalamaR = toplamR / MatrisToplami;
                            ortalamaG = toplamG / MatrisToplami;
                            ortalamaB = toplamB / MatrisToplami;
                            CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                        }
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnPrewittF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = 3;
                int ElemanSayisi = SablonBoyutu * SablonBoyutu;
                int x, y;
                Color Renk;
                int P1, P2, P3, P4, P5, P6, P7, P8, P9;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) 
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        Renk = GirisResmi.GetPixel(x - 1, y - 1);
                        P1 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y - 1);
                        P2 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y - 1);
                        P3 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x - 1, y);
                        P4 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y);
                        P5 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y);
                        P6 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x - 1, y + 1);
                        P7 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y + 1);
                        P8 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y + 1);
                        P9 = (Renk.R + Renk.G + Renk.B) / 3;
                        int Gx = Math.Abs(-P1 + P3 - P4 + P6 - P7 + P9); 
                        int Gy = Math.Abs(P1 + P2 + P3 - P7 - P8 - P9); 
                        int PrewittDegeri = 0;
                        PrewittDegeri = Gx;
                        PrewittDegeri = Gy;
                        PrewittDegeri = Gx + Gy; 
                        if (PrewittDegeri > 255) PrewittDegeri = 255;
                        CikisResmi.SetPixel(x, y, Color.FromArgb(PrewittDegeri, PrewittDegeri, PrewittDegeri));
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnGrayscalaF_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image==null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            { 
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color c = img.GetPixel(i, j);
                    int gray = (int)((c.R * 0.3) + (c.G * 0.6) + c.B * 0.1);
                    Color newC = Color.FromArgb(c.A, gray, gray, gray);
                    newImg.SetPixel(i, j, newC);
                }
                pictureBox2.Image = newImg;

            }
            }
        }

        private void btnMedyanF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = Convert.ToInt32(numericUpDown1.Value);
                int ElemanSayisi = SablonBoyutu * SablonBoyutu;
                int[] R = new int[ElemanSayisi];
                int[] G = new int[ElemanSayisi];
                int[] B = new int[ElemanSayisi];
                int[] Gri = new int[ElemanSayisi];
                int x, y, i, j;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        int k = 0;
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                R[k] = OkunanRenk.R;
                                G[k] = OkunanRenk.G;
                                B[k] = OkunanRenk.B;
                                Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114);
                                k++;
                            }
                        }
                        int GeciciSayi = 0;
                        for (i = 0; i < ElemanSayisi; i++)
                        {
                            for (j = i + 1; j < ElemanSayisi; j++)
                            {
                                if (Gri[j] < Gri[i])
                                {
                                    GeciciSayi = Gri[i];
                                    Gri[i] = Gri[j];
                                    Gri[j] = GeciciSayi;
                                    GeciciSayi = R[i];
                                    R[i] = R[j];
                                    R[j] = GeciciSayi;
                                    GeciciSayi = G[i];
                                    G[i] = G[j];
                                    G[j] = GeciciSayi;
                                    GeciciSayi = B[i];
                                    B[i] = B[j];
                                    B[j] = GeciciSayi;
                                }
                            }
                        }
                        CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) /
                       2], B[(ElemanSayisi - 1) / 2]));
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnSobelF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Bitmap GirisResmi, CikisResmiXY, CikisResmiX, CikisResmiY;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmiX = new Bitmap(ResimGenisligi, ResimYuksekligi);
                CikisResmiY = new Bitmap(ResimGenisligi, ResimYuksekligi);
                CikisResmiXY = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = 3;
                int ElemanSayisi = SablonBoyutu * SablonBoyutu;
                int x, y;
                Color Renk;
                int P1, P2, P3, P4, P5, P6, P7, P8, P9;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) 
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        Renk = GirisResmi.GetPixel(x - 1, y - 1);
                        P1 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y - 1);
                        P2 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y - 1);
                        P3 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x - 1, y);
                        P4 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y);
                        P5 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y);
                        P6 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x - 1, y + 1);
                        P7 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x, y + 1);
                        P8 = (Renk.R + Renk.G + Renk.B) / 3;
                        Renk = GirisResmi.GetPixel(x + 1, y + 1);
                        P9 = (Renk.R + Renk.G + Renk.B) / 3;
                        int Gx = Math.Abs(-P1 + P3 - 2 * P4 + 2 * P6 - P7 + P9);
                        int Gy = Math.Abs(P1 + 2 * P2 + P3 - P7 - 2 * P8 - P9);
                        int Gxy = Gx + Gy;
                        if (Gx > 255) Gx = 255;
                        if (Gy > 255) Gy = 255;
                        if (Gxy > 255) Gxy = 255;
                        CikisResmiX.SetPixel(x, y, Color.FromArgb(Gx, Gx, Gx));
                        CikisResmiY.SetPixel(x, y, Color.FromArgb(Gy, Gy, Gy));
                        CikisResmiXY.SetPixel(x, y, Color.FromArgb(Gxy, Gxy, Gxy));
                    }
                }
                pictureBox2.Image = CikisResmiXY;
            }
        }

        private void btnNegatifF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk, DonusenRenk;
                int R = 0, G = 0, B = 0;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int i = 0, j = 0;
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);
                        R = 255 - OkunanRenk.R;
                        G = 255 - OkunanRenk.G;
                        B = 255 - OkunanRenk.B;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnLaplaceF_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap bm1 = new Bitmap(bmp.Width, bmp.Height);
                Bitmap ciktiresmi = new Bitmap(bmp.Width, bmp.Height);
                int width = bmp.Width - 1;
                int height = bmp.Height - 1;
                for (int i = 1; i < width; i++)
                {
                    for (int j = 1; j < height; j++)
                    {
                        Color renk2, renk4, renk5, renk6, renk8;
                        renk2 = bmp.GetPixel(i, j - 1);
                        renk4 = bmp.GetPixel(i - 1, j);
                        renk5 = bmp.GetPixel(i, j);
                        renk6 = bmp.GetPixel(i + 1, j);
                        renk8 = bmp.GetPixel(i, j + 1);
                        int colorred = renk2.R + renk4.R + renk5.R * (-4) + renk6.R + renk8.R;
                        int colorgreen = renk2.G + renk4.G + renk5.G * (-4) + renk6.G + renk8.G;
                        int colorblue = renk2.B + renk4.B + renk5.B * (-4) + renk6.B + renk8.B;
                        int ort = ((colorred + colorgreen + colorblue) / 3);
                        if (ort > 255)
                        {
                            ort = 255;
                        }
                        if (ort < 0)
                        {
                            ort = 0;
                        }
                        bm1.SetPixel(i, j, Color.FromArgb(ort, ort, ort));
                    }
                }
                int R, G, B;
                Color OkunanRenk1, OkunanRenk2, DonusenRenk;
                for (int x = 1; x < width; x++)
                {
                    for (int y = 1; y < height; y++)
                    {
                        OkunanRenk1 = bmp.GetPixel(x, y);
                        OkunanRenk2 = bm1.GetPixel(x, y);
                        R = OkunanRenk1.R + OkunanRenk2.R;
                        G = OkunanRenk1.G + OkunanRenk2.G;
                        B = OkunanRenk1.B + OkunanRenk2.B;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;

                        DonusenRenk = Color.FromArgb(R, G, B);
                        ciktiresmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Image = bm1;
            }
        }

        private void hScrollBarKontrast_Click(object sender, EventArgs e)
        {
            int sayi = hScrollBarKontrast.Value;
            lbKontrast.Text = sayi.ToString();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                    Color OkunanRenk, DonusenRenk;
                    Bitmap GirisResmi, CikisResmi;
                    GirisResmi = new Bitmap(pictureBox1.Image);
                    int ResimGenisligi = GirisResmi.Width; 
                    int ResimYuksekligi = GirisResmi.Height;
                    CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); 
                    double C_KontrastSeviyesi = Convert.ToInt32(hScrollBarKontrast.Value);
                    double F_KontrastFaktoru = (259 * (C_KontrastSeviyesi + 255)) / (255 * (259 - C_KontrastSeviyesi));
                    for (int x = 0; x < ResimGenisligi; x++)
                    {
                        for (int y = 0; y < ResimYuksekligi; y++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x, y);
                            R = OkunanRenk.R;
                            G = OkunanRenk.G;
                            B = OkunanRenk.B;
                            R = (int)((F_KontrastFaktoru * (R - 128)) + 128);
                            G = (int)((F_KontrastFaktoru * (G - 128)) + 128);
                            B = (int)((F_KontrastFaktoru * (B - 128)) + 128);
                        //Renkler sınırların dışına çıktıysa, sınır değer alınacak
                        if (R > 255) R = 255;
                            if (G > 255) G = 255;
                            if (B > 255) B = 255;
                            if (R < 0) R = 0;
                            if (G < 0) G = 0;
                            if (B < 0) B = 0;
                            DonusenRenk = Color.FromArgb(R, G, B);
                            CikisResmi.SetPixel(x, y, DonusenRenk);
                        }
                    }

                    pictureBox2.Image = CikisResmi;
            }
        }

        private void hScrollBarParlaklik_Click(object sender, EventArgs e)
        {
            int sayi = hScrollBarParlaklik.Value;
            lbParlaklik.Text = sayi.ToString();

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int i = 0, j = 0; 
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    j = 0;
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);

                        R = OkunanRenk.R + hScrollBarParlaklik.Value;
                        G = OkunanRenk.G + hScrollBarParlaklik.Value;
                        B = OkunanRenk.B + hScrollBarParlaklik.Value;
                        //Renkler sınırların dışına çıktıysa, sınır değer alınacak
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(i, j, DonusenRenk);
                        j++;
                    }
                    i++;
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void hScrollBarEsikleme_Click(object sender, EventArgs e)
        {
            int sayi = hScrollBarEsikleme.Value;
            lbEsikleme.Text = sayi.ToString();

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini oluşturuyor. Boyutları giriş resmi ile aynı olur.
                int EsiklemeDegeri = hScrollBarEsikleme.Value;
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);
                        if (OkunanRenk.R >= EsiklemeDegeri)
                            R = 255;
                        else
                            R = 0;
                        if (OkunanRenk.G >= EsiklemeDegeri)
                            G = 255;
                        else
                            G = 0;
                        if (OkunanRenk.B >= EsiklemeDegeri)
                            B = 255;
                        else
                            B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnNetlestirmeF_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk; Bitmap GirisResmi, CikisResmi; GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width; int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = 3; int ElemanSayisi = SablonBoyutu * SablonBoyutu;
                int x, y, i, j, toplamR, toplamG, toplamB;
                int R, G, B; int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
                int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;
                        int k = 0; 
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        { for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R * Matris[k];
                                toplamG = toplamG + OkunanRenk.G * Matris[k];
                                toplamB = toplamB + OkunanRenk.B * Matris[k];
                                k++;
                            }
                        }
                        R = toplamR / MatrisToplami;
                        G = toplamG / MatrisToplami;
                        B = toplamB / MatrisToplami;
                        if (R > 255) R = 255; if (G > 255) G = 255; if (B > 255) B = 255;
                        if (R < 0) R = 0; if (G < 0) G = 0; if (B < 0) B = 0; 
                        CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                    }
                }
                pictureBox2.Image = CikisResmi;
            }

        }

        private void btnDondurme_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk; Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int Aci = Convert.ToInt16(hScrollBar1.Value);
                double RadyanAci = Aci * 2 * Math.PI / 360;
                double x2 = 0, y2 = 0;
                int x0 = ResimGenisligi / 2;
                int y0 = ResimYuksekligi / 2;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        x2 = Math.Cos(RadyanAci) * (x1 - x0) - Math.Sin(RadyanAci) * (y1 - y0) + x0;
                        y2 = Math.Sin(RadyanAci) * (x1 - x0) + Math.Cos(RadyanAci) * (y1 - y0) + y0;
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi) CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnTerscevir_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double Aci = 180;
                double RadyanAci = Aci * 2 * Math.PI / 360;
                double x2 = 0, y2 = 0;
                int x0 = ResimGenisligi / 2;
                int y0 = ResimYuksekligi / 2;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        x2 = Convert.ToInt16(x1);
                        y2 = Convert.ToInt16(-y1 + 2 * y0);
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnUzaklast_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int x2 = 0, y2 = 0;
                int KucultmeKatsayisi = 2;
                for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
                {
                    y2 = 0;
                    for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        DonusenRenk = OkunanRenk;
                        CikisResmi.SetPixel(x2, y2, DonusenRenk);
                        y2++;
                    }
                    x2++;
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width; int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double Aci = Convert.ToDouble(90); double RadyanAci = Aci * 2 * Math.PI / 360;
                double x2 = 0, y2 = 0;
                int x0 = ResimGenisligi / 2;
                int y0 = ResimYuksekligi / 2;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        double Delta = (x1 - x0) * Math.Sin(RadyanAci) - (y1 - y0) * Math.Cos(RadyanAci);
                        x2 = Convert.ToInt16(x1 + 2 * Delta * (-Math.Sin(RadyanAci)));
                        y2 = Convert.ToInt16(y1 + 2 * Delta * (Math.Cos(RadyanAci)));
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi) CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnOteleme_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double x2 = 0, y2 = 0;
                int Tx = 100;
                int Ty = 50;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        x2 = x1 + Tx;
                        y2 = y1 + Ty;
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void hScrollBar1_Click(object sender, EventArgs e)
        {
            int sayi = hScrollBar1.Value;
            labelll.Text = sayi.ToString();
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            Histogram histogram = new Histogram();
            histogram.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void hScrRED_Click(object sender, EventArgs e)
        {
            int sayi = hScrRED.Value;
            labelRED.Text = sayi.ToString();

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int i = 0, j = 0;
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    j = 0;
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);

                        R = OkunanRenk.R + hScrRED.Value;
                        G = OkunanRenk.G + hScrGREEN.Value;
                        B = OkunanRenk.B + hScrBLUE.Value;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(i, j, DonusenRenk);
                        j++;
                    }
                    i++;
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void hScrGREEN_Click(object sender, EventArgs e)
        {
            int sayi = hScrGREEN.Value;
            labelGREEN.Text = sayi.ToString();

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int i = 0, j = 0;
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    j = 0;
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);

                        R = OkunanRenk.R + hScrRED.Value;
                        G = OkunanRenk.G + hScrGREEN.Value;
                        B = OkunanRenk.B + hScrBLUE.Value;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(i, j, DonusenRenk);
                        j++;
                    }
                    i++;
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void hScrBLUE_Click(object sender, EventArgs e)
        {
            int sayi = hScrBLUE.Value;
            labelBLUE.Text = sayi.ToString();

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int i = 0, j = 0;
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    j = 0;
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);

                        R = OkunanRenk.R + hScrRED.Value;
                        G = OkunanRenk.G + hScrGREEN.Value;
                        B = OkunanRenk.B + hScrBLUE.Value;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(i, j, DonusenRenk);
                        j++;
                    }
                    i++;
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image==null)
            {
                MessageBox.Show("Kaydedilcek image yok..");
            }
          else
          {
               SaveFileDialog saveFileDialog1 = new SaveFileDialog();
               saveFileDialog1.Filter = "Jpeg Resmi|*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
               saveFileDialog1.Title = "Resmi Kaydet"; saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "") 
            { 
                FileStream DosyaAkisi = (FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1: pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case 2: pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case 3: pictureBox2.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Gif); break;
                }
                   DosyaAkisi.Close();
                    MessageBox.Show("Image Kaydedildi");
            }
          }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin..");
            }
            else
            {
            Bitmap SiyahBeyazResim = new Bitmap(pictureBox1.Image);
            int w = SiyahBeyazResim.Width;
            int h = SiyahBeyazResim.Height;

            BitmapData sd = SiyahBeyazResim.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            SiyahBeyazResim.UnlockBits(sd);
            int current = 0;
            double[] pn = new double[256];
            for (int p = 0; p < bytes; p += 4)
            {
                pn[buffer[p]]++;
            }
            for (int prob = 0; prob < pn.Length; prob++)
            {
                pn[prob] /= (w * h);
            }
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    current = y * sd.Stride + x * 4;
                    double sum = 0;
                    for (int i = 0; i < buffer[current]; i++)
                    {
                        sum += pn[i];
                    }
                    for (int c = 0; c < 3; c++)
                    {
                        result[current + c] = (byte)Math.Floor(255 * sum);
                    }
                    result[current + 3] = 255;
                }
            }
            Bitmap res = new Bitmap(w, h);
            BitmapData rd = res.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, rd.Scan0, bytes);
            res.UnlockBits(rd);
            pictureBox2.Image = res;
            }
        }

        private void btnYayma_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Lütfen Resim Ekleyin.");
            }
            else
            {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            Bitmap SiyahBeyazResim = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = SiyahBeyazResim.Width;
            int ResimYuksekligi = SiyahBeyazResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            GirisResmi = SiyahBeyazResim;
            int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int Esikleme = 0;
            try
            {
                Esikleme = Convert.ToInt16(textBox1.Text);
            }
            catch
            {
                Esikleme = 128;
            }
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) 

            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    if (OkunanRenk.R > Esikleme) 

                    {
                        bool KomsulardaSiyahVarMi = false;
                        
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                if (OkunanRenk.R < Esikleme) 
                                    KomsulardaSiyahVarMi = true;
                            }
                        }
                        if (KomsulardaSiyahVarMi == true) 
                        {
                            Color KendiRengi = GirisResmi.GetPixel(x, y);
                            CikisResmi.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else 
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            pictureBox2.Image = CikisResmi;
            }
        }

        private void btnPerspektif_Click(object sender, EventArgs e)
        {
            Perspektif perspektif = new Perspektif();
            perspektif.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbEsikleme_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Mayın_Tarlası
{
    public partial class Form1 : Form
    {
        Random Rasgele = new Random();

        byte BombaSayısı = 10;
        Button[] Duğmeler = new Button[64];
        DataTable MayınTarlası = new DataTable();
        

        public Form1()
        {

            başlat();

        }
        void başlat()
        {
            tarlaEkimi();

            InitializeComponent();

            TabloDoldur();

            sayıYerlestirme();

        }
        

        void tarlaEkimi()
        {
            int horizotal = 30;
            int vertical = 100;
            byte butonİsim = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int a = 0; a < 8; a++)
                {
                    Duğmeler[butonİsim] = new Button();
                    Duğmeler[butonİsim].Name = butonİsim.ToString();
                    Duğmeler[butonİsim].Size = new Size(30, 30);
                    Duğmeler[butonİsim].Location = new Point(horizotal, vertical);
                    Duğmeler[butonİsim].MouseDown += new MouseEventHandler(this.Duğmeler_MouseDown);
                    Duğmeler[butonİsim].BackColor = Color.Aqua;


                    horizotal += 30;
                    this.Controls.Add(Duğmeler[butonİsim]);

                    butonİsim++;
                }
                horizotal = 30;
                vertical += 30;
            }

        }

        void sayıYerlestirme()
        {
            byte bombaSayısı = 0;

            for (int SatırSırası = 1; SatırSırası < 9; SatırSırası++)
            {
                for (int SütunSırası = 1; SütunSırası < 9; SütunSırası++)
                {
                    if (MayınTarlası.Rows[SatırSırası][SütunSırası].ToString() != "255")
                    {

                        if (MayınTarlası.Rows[SatırSırası - 1][Convert.ToString(SütunSırası - 1)].ToString() == "255")
                            bombaSayısı += 1;
                        if (MayınTarlası.Rows[SatırSırası - 1][Convert.ToString(SütunSırası)].ToString() == "255")
                            bombaSayısı += 1;
                        if (MayınTarlası.Rows[SatırSırası - 1][Convert.ToString(SütunSırası + 1)].ToString() == "255")
                            bombaSayısı += 1;

                        if (MayınTarlası.Rows[SatırSırası][Convert.ToString(SütunSırası - 1)].ToString() == "255")
                            bombaSayısı += 1;
                        if (MayınTarlası.Rows[SatırSırası][Convert.ToString(SütunSırası + 1)].ToString() == "255")
                            bombaSayısı += 1;

                        if (MayınTarlası.Rows[SatırSırası + 1][Convert.ToString(SütunSırası - 1)].ToString() == "255")
                            bombaSayısı += 1;
                        if (MayınTarlası.Rows[SatırSırası + 1][Convert.ToString(SütunSırası)].ToString() == "255")
                            bombaSayısı += 1;
                        if (MayınTarlası.Rows[SatırSırası + 1][Convert.ToString(SütunSırası + 1)].ToString() == "255")
                            bombaSayısı += 1;

                        MayınTarlası.Rows[SatırSırası][SütunSırası] = bombaSayısı;

                        bombaSayısı = 0;
                    }

                }

            }
        }

        void TabloDoldur()
        {
            for (int i = 0; i < 10; i++)
            {
                MayınTarlası.Columns.Add(Convert.ToString(i), typeof(byte));
            }

            for (int i = 0; i < 10; i++)
            {

                DataRow dr = MayınTarlası.NewRow();

                dr[0] = 0;
                dr[1] = 0;
                dr[2] = 0;
                dr[3] = 0;
                dr[4] = 0;
                dr[5] = 0;
                dr[6] = 0;
                dr[7] = 0;
                dr[8] = 0;
                dr[9] = 0;

                MayınTarlası.Rows.Add(dr);

            }

            bombaYerleştir();
        }

        List<byte> bombasütunları = new List<byte>();
        List<byte> bombasatırları = new List<byte>();

        void bombaYerleştir()
        {
            byte bombaSatır;
            byte bombaSütun;

            for (int i = 0; i < BombaSayısı; i++)
            {
                bombaSatır = Convert.ToByte(Rasgele.Next(1, 9));
                bombaSütun = Convert.ToByte(Rasgele.Next(1, 9));

                MayınTarlası.Rows[bombaSatır][bombaSütun] = 255;
                bombasatırları.Add(bombaSatır);
                bombasütunları.Add(bombaSütun);
            }
        }

        public byte degerBul(byte sıra)
        {

            byte satır = 1;
            byte sütun = 1;
            byte tabloSıra = 0;

            for (byte s = 0; s < 8; s++)
            {

                for (byte sü = 0; sü < 8; sü++)
                {
                    tabloSıra += 1;
                    if (tabloSıra == sıra)
                    {
                        s++;
                        sü++;
                        satır = s;
                        sütun = sü;
                        break;
                    }
                }
                if (tabloSıra == sıra)
                {
                    break;
                }
            }

            if (MayınTarlası.Rows[satır][sütun].ToString() != "255")
            {
                Duğmeler[sıra - 1].Text = MayınTarlası.Rows[satır][sütun].ToString();

            }
            else
            {
                Duğmeler[sıra - 1].BackColor = Color.Black;


            }
            return Convert.ToByte(MayınTarlası.Rows[satır][sütun]);
        }
        void MayınaBastın()
        {
            for (byte i = 1; i < 64; i++)
            {
                if (degerBul(i) == 255)
                {
                    Duğmeler[i].BackColor = Color.Black;
                }
            }
            for (byte i = 1; i < 64; i++)
            {
                Duğmeler[i].Enabled = false;
            }

        }
        List<byte> sıfırAç(byte a)
        {
            List<byte> ÇevredekiSıfırlar = new List<byte>();

            if (a - 9 > 0 && a != 1 && a != 9 && a != 17 && a != 25 && a != 33 && a != 41 && a != 49 && a != 57)
                if (degerBul(Convert.ToByte(a - 9)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a - 9));
            if (a - 8 > 0)
                if (degerBul(Convert.ToByte(a - 8)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a - 8));
            if (a - 7 > 0 && a != 8 && a != 16 && a != 24 && a != 32 && a != 40 && a != 48 && a != 56 && a != 64)
                if (degerBul(Convert.ToByte(a - 7)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a - 7));
            if (a - 1 > 0 && a != 1 && a != 9 && a != 17 && a != 25 && a != 33 && a != 41 && a != 49 && a != 57)
                if (degerBul(Convert.ToByte(a - 1)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a - 1));


            if (a + 1 < 64 && a != 8 && a != 16 && a != 24 && a != 32 && a != 40 && a != 48 && a != 56 && a != 64)
                if (degerBul(Convert.ToByte(a + 1)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a + 1));
            if (a + 7 < 64 && a != 1 && a != 9 && a != 17 && a != 25 && a != 33 && a != 41 && a != 49 && a != 57)
                if (degerBul(Convert.ToByte(a + 7)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a + 7));
            if (a + 8 < 64)
                if (degerBul(Convert.ToByte(a + 8)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a + 8));
            if (a + 9 < 64 && a != 8 && a != 16 && a != 24 && a != 32 && a != 40 && a != 48 && a != 56 && a != 64)
                if (degerBul(Convert.ToByte(a + 9)) == 0)
                    ÇevredekiSıfırlar.Add(Convert.ToByte(a + 9));

            return (ÇevredekiSıfırlar);

        }

        void tümSıfırlarıAç(byte a)
        {
            List<byte> liste1 = new List<byte>();
            List<byte> liste2 = new List<byte>();

            liste1 = sıfırAç(a);
            for (int i = 0; i < 3; i++)
            {
                foreach (var item in liste1)
                {
                    foreach (var b in sıfırAç(item))
                    {
                        liste2.Add(b);
                    }
                }

                foreach (var item in liste2)
                {
                    foreach (var b in sıfırAç(item))
                    {
                        liste1.Add(b);
                    }
                }
            }

        }

        private void Duğmeler_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;

            if (e.Button == MouseButtons.Left)
            {
                if (btn.BackColor != Color.Red)
                {
                    byte a = Convert.ToByte(Convert.ToInt16(btn.Name) + 1);

                    if (degerBul(a) == 255)
                    {
                        MayınaBastın();
                    }

                    if (degerBul(a) == 0)
                    {
                        tümSıfırlarıAç(a);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (btn.BackColor != Color.Red)
                    btn.BackColor = Color.Red;
                else
                    btn.BackColor = Color.Aqua;

            }


        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Application.OpenForms[0] == this)//Uygulamanin main form'u
            {
                //uygulamanin ana formunu kapatirsaniz uygulama kapanir, ana formu yeniden baslatmak uygulamayi yeniden baslatmak demektir.
                Application.Restart();
            }
            else
            {
                Form2 f = new Form2();
                f.Show();
                this.Close();
            }
        }

    }
}

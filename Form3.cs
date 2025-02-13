using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesajProje
{
    public partial class Form3 : Form
    {
        public Form3(string Alicinumara)
        {
            //mesaj okuma sayfasını kapatınca anca sayfa tekrar açılıyor
            InitializeComponent();
            Form2 frm2 = new Form2();            
            this.FormClosing += (s, e) => frm2.Show();
            frm2.numara = Alicinumara;
        }

        public string kimden;
        public string konu;
        public string mesaj;
        public string Gnumara;
        public string ANumara;
        //mesaj okuma sayafası için gerekli bilgiler sayfaya taşınıyor
        private void Form3_Load(object sender, EventArgs e)
        {
            LblKimden.Text = kimden;
            LblKonu.Text = konu;
            RtxtMesaj.Text = mesaj;
        }
        //cevapla butonuna tıklayınca ana forma dönüp alıcı ve konu bilgilerini dolduruyor
        private void BtnCevapla_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.MTxtNumara.Text = Gnumara;
            form2.TxtBaslik.Text = "Re: " + konu;
            form2.numara = ANumara;
            form2.Show();
            this.Close();
        }
    }
}

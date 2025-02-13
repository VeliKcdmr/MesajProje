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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string numara;
        SqlConnection baglanti = new SqlConnection("Data Source=RAMPAGE\\SQLEXPRESS;Initial Catalog=DbMesaj;Integrated Security=True");
        void BilgiGetir()
        {
            LblNumara.Text = numara;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ad+' '+soyad from tblkisiler where numara=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    LblAdSoyad.Text = dr[0].ToString();
                }
            }
            baglanti.Close();
        }
        void GelenKutusu()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Gonderen,Ad+' '+Soyad as Gönderen,baslik,Icerik from tblmesajlar  Inner join TblKisiler on TblMesajlar.Gonderen=TblKisiler.Numara where alici=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();
        }
        void GidenKutusu()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select Ad+' '+Soyad as Alıcı,baslik,Icerik from tblmesajlar  Inner join TblKisiler on TblMesajlar.alici=TblKisiler.Numara where Gonderen=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BilgiGetir();
            GelenKutusu();
            GidenKutusu();

        }
        void temizle()
        {
            MTxtNumara.Clear();
            TxtBaslik.Clear();
            RTxtMesaj.Clear();
        }
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblmesajlar (gonderen,alici,baslik,Icerik) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", MTxtNumara.Text);
            komut.Parameters.AddWithValue("@p3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@p4", RTxtMesaj.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız Gönderildi");
            GidenKutusu();
            temizle();

        }
        public string kime;
        public string baslik;
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Form3 frm = new Form3(numara);
                int secilen = dataGridView1.SelectedRows[0].Index;
                frm.Gnumara = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                frm.kimden = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                frm.konu = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                frm.mesaj = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                frm.ANumara = numara;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen Satırın en başına tıklayınız.");
            }
        }
    }
}

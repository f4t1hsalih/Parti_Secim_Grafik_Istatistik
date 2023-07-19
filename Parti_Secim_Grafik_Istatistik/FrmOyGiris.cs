using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parti_Secim_Grafik_Istatistik
{
    public partial class FrmOyGiris : Form
    {
        public FrmOyGiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=krsDbSecimProje;Integrated Security=True");

        private void btnOyGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLILCE(ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtIlceAd.Text);
            komut.Parameters.AddWithValue("@p2", txtAParti.Text);
            komut.Parameters.AddWithValue("@p3", txtBParti.Text);
            komut.Parameters.AddWithValue("@p4", txtCParti.Text);
            komut.Parameters.AddWithValue("@p5", txtDParti.Text);
            komut.Parameters.AddWithValue("@p6", txtEParti.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Oy Girişi Başarıyla Gerçekleşti");
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler grafikler = new FrmGrafikler();
            grafikler.Show();
            this.Hide();
        }

        private void btnCıkısYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmOyGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

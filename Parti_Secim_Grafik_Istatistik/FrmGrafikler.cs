using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parti_Secim_Grafik_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=krsDbSecimProje;Integrated Security=True");
        int toplamoy;

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İlçe Adlarını Combobox'a Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();

            //Grafiğe Toplam Sonuçları Getirme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select Sum(APARTI), Sum(BPARTI), Sum(CPARTI), Sum(DPARTI), Sum(EPARTI) from TBLILCE", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A Parti", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B Parti", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C Parti", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D Parti", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E Parti", dr2[4]);
            }
            baglanti.Close();
        }

        private void FrmGrafikler_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmOyGiris oyGiris = new FrmOyGiris();
            oyGiris.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //İlçe Adlarını Combobox'a Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLILCE where ILCEAD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                //ProgressBar'ın maximum değerini bulma
                toplamoy = int.Parse(dr[2].ToString()) + int.Parse(dr[3].ToString()) + int.Parse(dr[4].ToString()) + int.Parse(dr[5].ToString()) + int.Parse(dr[6].ToString());

                progressBar1.Maximum = toplamoy;
                progressBar2.Maximum = toplamoy;
                progressBar3.Maximum = toplamoy;
                progressBar4.Maximum = toplamoy;
                progressBar5.Maximum = toplamoy;

                //ProgressBar'a değerleri atama
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                //Label'lara değerleri atama
                lblAParti.Text = dr[2].ToString();
                lblBParti.Text = dr[3].ToString();
                lblCParti.Text = dr[4].ToString();
                lblDParti.Text = dr[5].ToString();
                lblEParti.Text = dr[6].ToString();
            }
            baglanti.Close();
        }
    }
}

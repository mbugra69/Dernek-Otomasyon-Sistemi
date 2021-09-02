using Dernek_Otomasyon.Model;
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

namespace Dernek_Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@kullaniciAdi", SqlDbType.VarChar) { Value = txtKullaniciAdi.Text });
            parameters.Add(new SqlParameter("@sifre", SqlDbType.VarChar) { Value = txtSifre.Text });

            DataTable dt = IDataBase.DataToDataTable
                (
                "select * from kullanicilar where aktif=1 and kullaniciAdi = @kullaniciAdi and sifre = @sifre", parameters
                );

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    KullaniciBilgisi.userId = Convert.ToInt32(row["id"]);

                }
                MessageBox.Show("Giriş Başarılı. Kütüphane Otomasyon Sistemine Hoşgeldiniz!", "Hoşgeldiniz!");
                FormAna formAna = new FormAna();
                formAna.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifresi yanlış girildi.", "Uyarı!");
            }


        }
    }
}

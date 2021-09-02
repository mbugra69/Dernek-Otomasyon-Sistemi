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
    public partial class formm : Form
    {
        public formm()
        {
            InitializeComponent();
        }
        int uyeId = 0;
        private void UyeEkle_Load(object sender, EventArgs e)
        {
            dg.DataSource = IDataBase.DataToDataTable("select * from uyeler where aktif = 1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(uyeId > 0)
            {
                uyeGuncelle();
            }
            else
            {

           
            uyeEkle();
            }
        }

        void uyeGuncelle()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@adi", SqlDbType.VarChar) { Value = txtUyeAd.Text });
            parameters.Add(new SqlParameter("@soyadi", SqlDbType.VarChar) { Value = txtSoyAd.Text });
            parameters.Add(new SqlParameter("@cinsiyet", SqlDbType.VarChar) { Value = txtCinsiyet.Text });
            parameters.Add(new SqlParameter("@tc", SqlDbType.VarChar) { Value = txtTC.Text });
            parameters.Add(new SqlParameter("@telefon", SqlDbType.VarChar) { Value = txtTEL.Text });
            parameters.Add(new SqlParameter("@uyelikTarihi", SqlDbType.DateTime) { Value = dateTarih.Text });
            parameters.Add(new SqlParameter("@aidatOdemisMi", SqlDbType.VarChar) { Value = txtAidatOdemis.Text });
            parameters.Add(new SqlParameter("@aidatMiktar", SqlDbType.VarChar) { Value = txtMiktar.Text });
            parameters.Add(new SqlParameter("@unvan", SqlDbType.VarChar) { Value = txtUnvan.Text });
            parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = uyeId });
            IDataBase.executeNonQuery("update uyeler set adi=@adi, soyadi=@soyadi,cinsiyet=@cinsiyet,tc=@tc,telefon=@telefon,uyelikTarihi =@uyelikTarihi,aidatOdemisMi=@aidatOdemisMi,aidatMiktar = @aidatMiktar,unvan=@unvan where id = @id", parameters);
            uyeleriGetir();
            MessageBox.Show("Üye güncelleme işlemi başarıyla tamamlandı.", "Uyarı!");
        }
        void uyeEkle()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@adi", SqlDbType.VarChar) { Value = txtUyeAd.Text });
            parameters.Add(new SqlParameter("@soyadi", SqlDbType.VarChar) { Value = txtSoyAd.Text });
            parameters.Add(new SqlParameter("@cinsiyet", SqlDbType.VarChar) { Value = txtCinsiyet.Text });
            parameters.Add(new SqlParameter("@tc", SqlDbType.VarChar) { Value = txtTC.Text });
            parameters.Add(new SqlParameter("@telefon", SqlDbType.VarChar) { Value = txtTEL.Text });
            parameters.Add(new SqlParameter("@uyelikTarihi", SqlDbType.DateTime) { Value = dateTarih.Text });
            parameters.Add(new SqlParameter("@aidatOdemisMi", SqlDbType.VarChar) { Value = txtAidatOdemis.Text });
            parameters.Add(new SqlParameter("@aidatMiktar", SqlDbType.VarChar) { Value = txtMiktar.Text });
            parameters.Add(new SqlParameter("@unvan", SqlDbType.VarChar) { Value = txtUnvan.Text });


           
            IDataBase.executeNonQuery("insert into uyeler (adi,soyadi,cinsiyet,tc,telefon,uyelikTarihi,aidatOdemisMi,aidatMiktar,unvan) values (@adi,@soyadi,@cinsiyet,@tc,@telefon,@uyelikTarihi,@aidatOdemisMi,@aidatMiktar,@unvan)", parameters);

            uyeleriGetir();

        }
        void uyeleriGetir()
        {

            dg.DataSource = IDataBase.DataToDataTable("" +
                "select * from uyeler where aktif = 1 and adi+ ' ' +soyadi like @search",
                new SqlParameter("@search", SqlDbType.VarChar)
                {
                    Value = string.Format("%{0}%", txtArama.Text)
                }
                );
            dg.Columns["id"].Visible = false;

        }

        void uyeSil()
        {




            IDataBase.executeNonQuery("update uyeler set aktif = 0 where id = @id", new SqlParameter("@id", SqlDbType.Int) { Value = uyeId });

            uyeleriGetir();

        }

        void temizle()
        {
            uyeId = 0;
            foreach (var item in tableLayoutPanel1.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";

                }

                if (item is ComboBox)
                {
                    ((ComboBox)item).Text = "";

                }
           
                if(item is MaskedTextBox)
                {
                    ((MaskedTextBox)item).Text = "";
                }
            }

        }
        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            uyeleriGetir();
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                uyeId = Convert.ToInt32(dg.Rows[e.RowIndex].Cells["id"].Value);
                foreach (DataRow row in IDataBase.DataToDataTable("select * from uyeler where aktif = 1 and id = @id", new SqlParameter("@id", SqlDbType.Int) { Value = uyeId }).Rows)
                {
                    txtUyeAd.Text = row["adi"].ToString();
                    txtSoyAd.Text = row["soyadi"].ToString();
                    txtTC.Text = row["tc"].ToString();
                    txtTEL.Text = row["telefon"].ToString();
                    txtUnvan.Text = row["unvan"].ToString();
                    txtCinsiyet.Text = row["cinsiyet"].ToString();
                    txtAidatOdemis.Text = row["aidatOdemisMi"].ToString();
                    txtMiktar.Text = row["aidatMiktar"].ToString();
                    dateTarih.Text = row["uyelikTarihi"].ToString();
                    


                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(uyeId>0)
            { 
            DialogResult dialogResult2 = MessageBox.Show("Seçili üyeyi silmek istediğinize emin misiniz ? NOT: Silme işlemi kalıcıdır.", "Uyarı", MessageBoxButtons.YesNo);
            if(dialogResult2 == DialogResult.Yes)
            {
                MessageBox.Show("Üye başarıyla silindi.", "Uyarı!");
                uyeSil();
                temizle();
            }
            else
            {
                MessageBox.Show("Üye Silinmedi.", "Uyarı!");
            }
            }
            else
            {
                MessageBox.Show("Lütfen öncelikle bir üye seçiniz.", "Uyarı!");
            }

        }
    }
}

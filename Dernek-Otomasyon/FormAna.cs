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
    public partial class FormAna : Form
    {
        public FormAna()
        {
            InitializeComponent();
        }

        private void FormAna_Load(object sender, EventArgs e)
        {
            kitaplariGetir();
        }

        void kitaplariGetir()
        {

            dgUyeler.DataSource = IDataBase.DataToDataTable("select * from uyeler where aktif = 1");
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            formm uyeEkle = new formm();
            uyeEkle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormIstatistik formIstatistik = new FormIstatistik();
            formIstatistik.Show();
        }

        private void dgUyeler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }
    }
}

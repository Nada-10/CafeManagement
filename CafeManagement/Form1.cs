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
using System.Configuration;

namespace CafeManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            string cs = ConfigurationManager.ConnectionStrings["CafeManagement.Properties.Settings.cafeManagementNHConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(cs);

            string req = "select * from users where username=@nom and password=@pass";
            SqlCommand com = new SqlCommand(req, cn);
            com.Parameters.Clear();
            com.Parameters.AddWithValue("@nom", txtName.Text);
            com.Parameters.AddWithValue("@pass", txtPass.Text);
            cn.Open();

            SqlDataReader dr = com.ExecuteReader();
            if(dr.Read())
            {
                if(txtName.Text.ToLower() == "admin" && txtPass.Text.ToLower() == "admin123")
                {
                    AdminPage f = new AdminPage();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                else
                {
                   ClientPage f = new ClientPage();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                label4.Text = "Name or Password is not correct!";
                txtName.Clear();
                txtPass.Clear();
            }
            cn.Close();
            com = null;
            cn = null;
        }

        private void linkAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateAccount f = new CreateAccount();
            f.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "";
        }

       
    }
}

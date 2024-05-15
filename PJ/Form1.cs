using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.jet.OleDb.4.0;" +
                                                  @"Data Source=C:\Users\Koong\Desktop\Work\CPE363\PJ\DB\pjdb.mdb"); //สร้างการเชื่อมต่อไปยัง file database location ของเรา
        OleDbCommand cmd = new OleDbCommand();                                                                      //สร้าง object cmd เพือใช้คำสั่งในการ sql ไปยัง database 

        private void closetab_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void signup_label_Click(object sender, EventArgs e)
        {
            new register().Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Class1.uname = txtuser.Text;
            con.Open();
            string login = "SELECT * FROM table_users WHERE username = '" + txtuser.Text + "'AND password = '" + txtpassword.Text + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader(); //อ่านข้อมูลว่าสิ่งที่ SELECT แล้วเก็บในตัวแปร dr 


            if (dr.Read())
            {
                new Formoverall().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtuser.Text = "";
                txtpassword.Text = "";
            }
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }
    }
}

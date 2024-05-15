using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PJ
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.jet.OleDb.4.0;" +
                                                  @"Data Source=C:\Users\Koong\Desktop\Work\CPE363\PJ\DB\pjdb.mdb");
        OleDbCommand cmd = new OleDbCommand();

        private void closetab_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_regis_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || txtpassword.Text == "" || txtconpass.Text == "")
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtpassword.Text == txtconpass.Text)
            {
                try
                {
                    con.Open();
                    string register = "INSERT INTO table_users ([username], [password], [date]) VALUES ('" + txtuser.Text + "','" + txtpassword.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    cmd = new OleDbCommand(register, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Success");
                    

                    txtuser.Text = "";
                    txtpassword.Text = "";
                    txtconpass.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    new Form1().Show();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Password does not match, Please try again");
                txtpassword.Text = "";
                txtconpass.Text = "";
            }
        }


        private void signup_label_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpassword.PasswordChar = '\0';
                txtconpass.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
                txtconpass.PasswordChar = '•';
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void register_Load(object sender, EventArgs e)
        {

        }
    }
}

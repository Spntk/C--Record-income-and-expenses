using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PJ
{
    public partial class Formoverall : Form
    {
        public Formoverall()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.jet.OleDb.4.0;" +
                                  @"Data Source=C:\Users\Koong\Desktop\Work\CPE363\PJ\DB\pjdb.mdb");

        private void closetab_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Formoverall_Load(object sender, EventArgs e)
        {
            lblname.Text = Class1.uname;
            try
            {
                con.Open();

                // Calculate total income
                string sumIncomeQuery = "SELECT SUM(income) AS totalIncome FROM table_income";
                OleDbCommand sumIncomeCmd = new OleDbCommand(sumIncomeQuery, con);
                object incomeResult = sumIncomeCmd.ExecuteScalar();
                decimal totalIncome = (incomeResult != null && incomeResult != DBNull.Value) ? Convert.ToDecimal(incomeResult) : 0;

                // Calculate total expenses
                string sumExpensesQuery = "SELECT SUM(expenses) AS totalExpenses FROM table_expenses";
                OleDbCommand sumExpensesCmd = new OleDbCommand(sumExpensesQuery, con);
                object expensesResult = sumExpensesCmd.ExecuteScalar();
                decimal totalExpenses = (expensesResult != null && expensesResult != DBNull.Value) ? Convert.ToDecimal(expensesResult) : 0;

                decimal difference = totalIncome - totalExpenses;

                // Display total income and total expenses in labels
                label1.Text = "Total Income: " + totalIncome.ToString("N2");
                label2.Text = "Total Expenses: " + totalExpenses.ToString("N2");
                label4.Text = "Difference: " + difference.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while calculating totals: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Close connection
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Formincome().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void lblname_Click(object sender, EventArgs e)
        {

        }

        private void btnover_Click(object sender, EventArgs e)
        {
            new Formoverall().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Formexpenses().Show();
            this.Hide();
        }
    }
}

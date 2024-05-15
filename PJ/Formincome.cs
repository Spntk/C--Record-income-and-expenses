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
    public partial class Formincome : Form
    {
        public Formincome()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.jet.OleDb.4.0;" +
                          @"Data Source=C:\Users\Koong\Desktop\Work\CPE363\PJ\DB\pjdb.mdb");

        private void closetab_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Formincome_Load(object sender, EventArgs e)
        {
            lblname.Text = Class1.uname;

            string selectQuery = "SELECT category, item, income, date FROM table_income";
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, con);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private void btnover_Click(object sender, EventArgs e)
        {
            new Formoverall().Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    con.Open();

                    // Get the index of the selected row
                    int rowIndex = dataGridView1.SelectedRows[0].Index;

                    // Construct the delete query
                    string deleteQuery = "DELETE FROM table_income WHERE category = @category AND item = @item AND income = @income AND [date] = @date";
                    OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, con);
                    deleteCmd.Parameters.AddWithValue("@category", dataGridView1.Rows[rowIndex].Cells["category"].Value);
                    deleteCmd.Parameters.AddWithValue("@item", dataGridView1.Rows[rowIndex].Cells["item"].Value);
                    deleteCmd.Parameters.AddWithValue("@income", Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["income"].Value));
                    deleteCmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells["date"].Value));

                    // Execute the delete query
                    deleteCmd.ExecuteNonQuery();

                    // Update DataGridView to reflect changes
                    string selectQuery = "SELECT category, item, income, date FROM table_income";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, con);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close(); // Close connection
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Retrieve data from textboxes and combobox
                string category = txtcate.Text;
                string item = txtitem.Text;
                int income = Convert.ToInt32(txtincome.Text);
                DateTime selectedDate = dateTimePicker1.Value;

                // Insert data into database
                string insertQuery = "INSERT INTO table_income (category, item, income, [date]) VALUES (@category, @item, @income, @date)";
                OleDbCommand insertCmd = new OleDbCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@category", category);
                insertCmd.Parameters.AddWithValue("@item", item);
                insertCmd.Parameters.AddWithValue("@income", income);
                insertCmd.Parameters.AddWithValue("@date", selectedDate);
                insertCmd.ExecuteNonQuery();

                string selectQuery = "SELECT category, item, income, date FROM table_income";
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Close connection
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Formincome().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Formexpenses().Show();
            this.Hide();
        }
    }
}

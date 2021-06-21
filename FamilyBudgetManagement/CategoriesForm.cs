using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyBudgetManagement
{
    public partial class CategoriesForm : Form
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();

        public CategoriesForm()
        {
            InitializeComponent();

            initListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // insert category
            string insertQuery = "insert into family_budget_management.categories(category) " +
                "values('" + textBox1.Text + "');";

            MySqlConnection connection = databaseConnector.getConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            connection.Close(); 

            textBox1.Text = "";
        }

        private void initListView()
        {
            String sql = "select category from categories;";
            MySqlConnection connection = databaseConnector.getConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, databaseConnector.getConnection());
            MySqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                listView1.Items.Add(dataReader[0].ToString());
            }
            connection.Close();
        }
    }
}

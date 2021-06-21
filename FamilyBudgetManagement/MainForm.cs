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
    public partial class MainForm : Form
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();

        public MainForm()
        {
            InitializeComponent();

            label4.Text = getIncome().ToString();
            label5.Text = getConsumption().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageForm manageForm = new ManageForm();
            manageForm.Show();
        }

        public int getIncome()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '+';", databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        public int getConsumption()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '-';", databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }
}

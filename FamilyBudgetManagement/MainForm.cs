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

            label4.Text = getIncome().ToString() + "₽";
            label5.Text = getConsumption().ToString() + "₽";

            label6.Text = getBudget().ToString() + "₽";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageForm manageForm = new ManageForm();
            manageForm.Show();
        }

        public int getIncome()
        {
            DateTime today = DateTime.Today;
            String todayDate = today.ToString("yyyy-MM-dd");

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '+' AND date = '" + todayDate + "';",
                databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        public int getConsumption()
        {
            DateTime today = DateTime.Today;
            String todayDate = today.ToString("yyyy-MM-dd");

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '-' AND date = '" + todayDate + "';",
                databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        public int getBudget()
        {
            return sum() - min();
        }

        public int sum()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '+';",
                databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        public int min()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '-';",
                databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatsForm statsForm = new StatsForm();
            statsForm.Show();
        }
    }
}

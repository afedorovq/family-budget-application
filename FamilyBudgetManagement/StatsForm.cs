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
    public partial class StatsForm : Form
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();
        public StatsForm()
        {
            InitializeComponent();

            initComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = getIncome().ToString();
            label5.Text = getConsumption().ToString();
        }

        public int getIncome()
        {
            string start = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            string end = monthCalendar1.SelectionEnd.ToString("yyyy-MM-dd");

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '+' and date between '" + start + "' and '" + end + "';",
                databaseConnector.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return int.Parse(table.Rows[0][0].ToString());
        }

        public int getConsumption()
        {
            string start = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            string end = monthCalendar1.SelectionEnd.ToString("yyyy-MM-dd");

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            if (comboBox1.SelectedItem == null)
            {
                MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '-' and date between '" + start + "' and '" + end + "';",
                       databaseConnector.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                return int.Parse(table.Rows[0][0].ToString());

            }
            else
            {
                MySqlCommand command = new MySqlCommand("select sum(sum) from operations where type = '-' and date between '" + start + "' and '" + end + "' and category_type = '" + comboBox1.SelectedItem + "';",
                    databaseConnector.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                return int.Parse(table.Rows[0][0].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void initComboBox()
        {
            string sql = "select category from categories;";
            MySqlConnection connection = databaseConnector.getConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, databaseConnector.getConnection());
            MySqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                comboBox1.Items.Add(dataReader[0]);
            }
            connection.Close();
        }
    }
}

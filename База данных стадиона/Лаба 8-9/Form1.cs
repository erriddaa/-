using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Лаба_8_9.DataSet1TableAdapters;
using System.Security.Cryptography;

namespace Лаба_8_9
{
    public partial class Form1 : Form
    {
        private int RowIdCategory;
        private int RowIdMatches;
        private int RowIdTickets;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            oleDbConnection1.Open();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Sold_tickets". При необходимости она может быть перемещена или удалена.
            this.sold_ticketsTableAdapter.Fill(this.dataSet1.Sold_tickets);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Matches". При необходимости она может быть перемещена или удалена.
            this.matchesTableAdapter.Fill(this.dataSet1.Matches);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Categories_of_tickets". При необходимости она может быть перемещена или удалена.
            this.categories_of_ticketsTableAdapter.Fill(this.dataSet1.Categories_of_tickets);
            UpdateTables();
        }
        private void UpdateTables()
        {
            this.sold_ticketsTableAdapter.Update(dataSet1);
            this.matchesTableAdapter.Update(dataSet1);
            this.categories_of_ticketsTableAdapter.Update(dataSet1);
            oleDbDataAdapter1.Update(dataSet1);
            oleDbDataAdapter2.Update(dataSet1);
            oleDbDataAdapter3.Update(dataSet1);
            dataSet1.Clear();
            oleDbDataAdapter1.Fill(dataSet1.Categories_of_tickets);
            oleDbDataAdapter2.Fill(dataSet1.Matches);
            oleDbDataAdapter3.Fill(dataSet1.Sold_tickets);
            categories_of_ticketsTableAdapter.Fill(dataSet1.Categories_of_tickets);
            matchesTableAdapter.Fill(dataSet1.Matches);
            sold_ticketsTableAdapter.Fill(dataSet1.Sold_tickets);
            comboBox1.Items.Clear();
            foreach (DataRow row in dataSet1.Matches.Rows)
                comboBox1.Items.Add(row["match_id"]);
            comboBox1.Text = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text != "") && (textBox8.Text != "") && (textBox9.Text != ""))
            {
                try
                {
                    DataRow row = dataSet1.Sold_tickets.NewRow();
                    row["match_id"] = comboBox1.Text;
                    row["category_id"] = textBox8.Text;
                    row["quantity"] = textBox9.Text;
                    dataSet1.Sold_tickets.Rows.Add(row);
                    UpdateTables();
                }
                catch (Exception)
                {
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
            {
                try
                {
                    DataRow row = dataSet1.Matches.NewRow();
                    row["date_of_match"] = textBox1.Text;
                    row["location"] = textBox4.Text;
                    row["home_team"] = textBox5.Text;
                    row["away_team"] = textBox6.Text;
                    dataSet1.Matches.Rows.Add(row);
                    UpdateTables();
                }
                catch (Exception)
                {
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (textBox3.Text != ""))
            {
                try
                {
                    DataRow row = dataSet1.Categories_of_tickets.NewRow();
                    row["category_name"] = textBox2.Text;
                    row["price"] = textBox3.Text;
                    dataSet1.Categories_of_tickets.Rows.Add(row);
                    UpdateTables();
                }
                catch (Exception)
                {
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataRow row = dataSet1.Categories_of_tickets.Rows[RowIdCategory];
                row["category_name"] = textBox2.Text;
                row["price"] = textBox3.Text;
                UpdateTables();
            }
            else
                MessageBox.Show("Выберите строку для редактирования", "Ошибка");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataRow row = dataSet1.Matches.Rows[RowIdMatches];
                row["date_of_match"] = textBox1.Text;
                row["location"] = textBox4.Text;
                row["home_team"] = textBox5.Text;
                row["away_team"] = textBox6.Text;
                UpdateTables();
            }
            else
                MessageBox.Show("Выберите строку для редактирования",
                "Ошибка");
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                RowIdCategory = e.RowIndex;
                textBox2.Text = dataSet1.Categories_of_tickets.Rows[RowIdCategory]["category_name"].ToString();
                textBox3.Text = dataSet1.Categories_of_tickets.Rows[RowIdCategory]["price"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                RowIdMatches = e.RowIndex;
                textBox1.Text = dataSet1.Matches.Rows[RowIdMatches]["date_of_match"].ToString();
                textBox4.Text = dataSet1.Matches.Rows[RowIdMatches]["location"].ToString();
                textBox5.Text =
                dataSet1.Matches.Rows[RowIdMatches]["home_team"].ToString();
                textBox6.Text = dataSet1.Matches.Rows[RowIdMatches]["away_team"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                RowIdTickets = e.RowIndex;
                DataRow p = dataSet1.Sold_tickets.Rows[RowIdTickets];
                comboBox1.Text = p["match_id"].ToString();
                textBox8.Text = p["category_id"].ToString();
                textBox9.Text = p["quantity"].ToString();
                DataRow[] tickets = p.GetParentRows(dataSet1.Relations["match_id"]);
                comboBox1.SelectedItem = tickets[0]["match_id"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count != 0)
            {
                DataRow row = dataSet1.Sold_tickets.Rows[RowIdTickets];
                row["match_id"] = comboBox1.Text;
                row["category_id"] = textBox8.Text;
                row["quantity"] = textBox9.Text;
                UpdateTables();
            }
            else
                MessageBox.Show("Выберите строку для редактирования",
                "Ошибка");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //if (dataSet1.Sold_tickets.Rows[RowIdTickets]["category_id"].ToString() = textBox8)
                        {

                        }
                        dataSet1.Categories_of_tickets.Rows[RowIdCategory].Delete();
                    }
                    catch (Exception)
                    {
                    }
                    UpdateTables();
                }
            }
            else
                MessageBox.Show("Выберите строку для удаления", "Ошибка");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        dataSet1.Matches.Rows[RowIdMatches].Delete();
                    }
                    catch (Exception)
                    {
                    }
                    UpdateTables();
                }
            }
            else
                MessageBox.Show("Выберите строку для удаления", "Ошибка");
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (dataGridView3.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись ? ", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            dataSet1.Sold_tickets.Rows[RowIdTickets].Delete();
                        }
                        catch (Exception)
                        {
                        }
                        UpdateTables();
                    }
                }
                else
                    MessageBox.Show("Выберите строку для удаления", "Ошибка");
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //если установлен флажок и выбрана фамилия
            if (checkBox1.Checked && comboBox1.Text != " ")
            {
                try
                {
                    //получить выбранную фамилию
                    string match = comboBox1.SelectedItem.ToString();
                    //составить условие для поиска нужного человека 
                    //в таблице Contacts
                    string str = "match_id='" + match + "'";
                    DataRow[] matches = dataSet1.Sold_tickets.Select(str);
                    //составить условие для фильтра
                    str = "ticket_id=" + matches[0]["ticket_id"];
                    //применить фильтр
                    soldticketsBindingSource.Filter = str;
                }
                catch (Exception) 
                {
                }
            }
            else
                //отменить фильтрацию
                soldticketsBindingSource.Filter = "";

        }
    }
}


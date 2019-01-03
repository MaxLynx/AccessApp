using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessApp
{
    public partial class Form1 : Form
    {
        DAO dao;
        public Form1()
        {
            InitializeComponent();
            dao = new DAO();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable userTables = dao.GetAllTables();
            button1.Text = userTables.Rows[0][2].ToString();
            button2.Text = userTables.Rows[1][2].ToString();
            button3.Text = userTables.Rows[2][2].ToString();
            button4.Text = userTables.Rows[3][2].ToString();
            button5.Text = userTables.Rows[4][2].ToString();
            button6.Text = userTables.Rows[5][2].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button1.Text), button1.Text).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button2.Text), button2.Text).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button3.Text), button3.Text).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button4.Text), button4.Text).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button5.Text), button5.Text).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form2(dao.GetTableByName(button6.Text), button6.Text).Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String info = "";
            foreach(String warrior in dao.GetWarriors())
            {
                info += warrior + "\r\n";
            }
            MessageBox.Show(info);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String info = "";
            foreach (String youngster in dao.GetYoungsters())
            {
                info += youngster + "\r\n";
            }
            MessageBox.Show(info);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            String info = "";
            foreach (String cossack in dao.GetLonelyUkrainians())
            {
                info += cossack + "\r\n";
            }
            MessageBox.Show(info);
        }
    }
}

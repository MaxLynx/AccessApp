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
    public partial class Form2 : Form
    {

        DataTable table;
        DAO dao;

        public Form2(DataTable table, String tableName)
        {
            this.table = table;
            this.table.TableName = tableName;
            dao = new DAO();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            BindingSource source = new BindingSource();
            source.DataSource = table;
            dataGridView1.DataSource = source;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                MessageBox.Show("Виділіть рядок для видалення");
            else
            {
                dao.DeleteFromTableById(table.TableName, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                String tableName = table.TableName;
                table = dao.GetTableByName(tableName);
                table.TableName = tableName;
                Form2_Load(sender, e);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                List<String> newItems = new List<String>();
                foreach (DataGridViewCell cell in lastRow.Cells)
                {
                    newItems.Add(cell.Value.ToString());
                }
                dao.InsertToTable(table.TableName, newItems);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Додати даний запис до бази неможливо\r\n" + ex.Message);
            }
            String tableName = table.TableName;
            table = dao.GetTableByName(tableName);
            table.TableName = tableName;
            Form2_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                MessageBox.Show("Виділіть рядок для редагування");
            else
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                dao.DeleteFromTableById(table.TableName, row.Cells[0].Value.ToString());
                try
                {
                    List<String> newItems = new List<String>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        newItems.Add(cell.Value.ToString());
                    }
                    dao.InsertToTable(table.TableName, newItems);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Відредагувати даний запис у базі неможливо\r\n" + ex.Message);
                }
                String tableName = table.TableName;
                table = dao.GetTableByName(tableName);
                table.TableName = tableName;
                Form2_Load(sender, e);
            }
        }
    }
}

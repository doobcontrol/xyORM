using System.Data;
using xy.Db;
using xy.Db.PostgreSQL;
using xy.Db.SQLite64;
using xy.ORM;

namespace TestBench
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;

            loadData(dataGridView1, Company.i);
        }
        private async Task loadData(DataGridView dgv, KModel km)
        {
            DataTable dt = await km.SelectAll();
            if (dt != null)
            {
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                foreach (FieldDef fielddef in km.FieldList)
                {
                    int cIndex = dgv.Columns.Add(
                        fielddef.FieldCode, fielddef.FieldName);
                    if(fielddef.IsPrimaryKey)
                    {
                        dgv.Columns[cIndex].ReadOnly = true;
                    }
                    if (fielddef.IsForeignKey)
                    {
                        dgv.Columns[cIndex].ReadOnly = true;
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    int i = dgv.Rows.Add(row.ItemArray);
                    dgv.Rows[i].Tag = row;
                }
            }
        }
        private async Task loadEmployee(DataGridView dgv, string companyID)
        {
            DataTable dt = await Employee.i
                .SelectByField(Employee.CompanyID,companyID);
            if (dt != null)
            {
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                foreach (FieldDef fielddef in Employee.i.FieldList)
                {
                    int cIndex = dgv.Columns.Add(
                        fielddef.FieldCode, fielddef.FieldName);
                    if (fielddef.IsPrimaryKey)
                    {
                        dgv.Columns[cIndex].ReadOnly = true;
                    }
                    if (fielddef.IsForeignKey)
                    {
                        dgv.Columns[cIndex].ReadOnly = true;
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    int i = dgv.Rows.Add(row.ItemArray);
                    dgv.Rows[i].Tag = row;
                }
            }
        }
        private async Task loadDataRow(DataGridView dgv, DataRow dr)
        {
            int i = dgv.Rows.Add(dr.ItemArray);
            dgv.Rows[i].Tag = dr;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string rowID =
                    dataGridView1.SelectedRows[0].Cells[Company.fID]
                    .Value.ToString();
                loadEmployee(dataGridView2, rowID);
                dataGridView2.Tag = rowID;
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }

        private async void btnAddC_Click(object sender, EventArgs e)
        {
            var recordDic = new Dictionary<string, string>();
            string id = Guid.NewGuid().ToString();
            recordDic.Add(Company.fID, id);
            await Company.i.Insert(recordDic);
            DataRow dr = (await Company.i.SelectByPk(id)).Rows[0];
            loadDataRow(dataGridView1, dr);
        }

        private void btnDeleteC_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if(dataGridView2.Rows.Count > 0)
                {
                    MessageBox.Show("Please delete all employees first.");
                    return;
                }
                string rowID =
                    dataGridView1.SelectedRows[0].Cells[Company.fID]
                    .Value.ToString();
                Company.i.DeleteByPk(rowID);
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            else
            {
                MessageBox.Show("Please select one row to delete.");
            }
        }

        private async void btnAddE_Click(object sender, EventArgs e)
        {
            var recordDic = new Dictionary<string, string>();
            string id = Guid.NewGuid().ToString();
            recordDic.Add(Employee.fID, id);
            recordDic.Add(Employee.CompanyID, dataGridView2.Tag.ToString());
            await Employee.i.Insert(recordDic);
            DataRow dr = (await Employee.i.SelectByPk(id)).Rows[0];
            loadDataRow(dataGridView2, dr);
        }

        private void btnDeleteE_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                string rowID =
                    dataGridView2.SelectedRows[0].Cells[Employee.fID]
                    .Value.ToString();
                Employee.i.DeleteByPk(rowID);
                dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
            }
            else
            {
                MessageBox.Show("Please select one row to delete.");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            string cellValue =
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string rowID =
                dataGridView1.Rows[e.RowIndex].Cells[Company.fID]
                .Value.ToString();
            var recordDic = new Dictionary<string, string>();
            string id = Guid.NewGuid().ToString();
            recordDic.Add(colName, cellValue);
            Company.i.UpdateByPk(recordDic, rowID);
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
            string cellValue =
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string rowID =
                dataGridView2.Rows[e.RowIndex].Cells[Employee.fID]
                .Value.ToString();
            var recordDic = new Dictionary<string, string>();
            string id = Guid.NewGuid().ToString();
            recordDic.Add(colName, cellValue);
            Employee.i.UpdateByPk(recordDic, rowID);
        }
    }
}

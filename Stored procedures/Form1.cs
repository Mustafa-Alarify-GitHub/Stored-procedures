using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Stored_procedures
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"server=DESKTOP-DA5JGBQ\SQLEXPRESS; DataBase=TB_Test; Integrated Security =true");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dataTable = new DataTable();

        SqlDataAdapter adapter2;
        DataTable dataTable2 = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllData();
        }

     

        private void GetAllData()
        {
            try
            {
                dataTable.Clear();
                con.Open();
                cmd = new SqlCommand("GetAllData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                // ComboBox
                comboBox1.DataSource = dataTable;

                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "id";

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // Event New
        private void button1_Click(object sender, EventArgs e)
        {
            txt_name.Text = String.Empty;
            txt_price.Text = String.Empty;
         }

        // Event Insert
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("AddNewData", con);
                cmd.CommandType= CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[2];
           

                // var name
                param[0] = new SqlParameter("@name", SqlDbType.VarChar);
                param[0].Value = txt_name.Text;

                // var price
                param[1] = new SqlParameter("@price", SqlDbType.VarChar);
                param[1].Value = txt_price.Text;

                cmd.Parameters.AddRange(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MyMessage("Created");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }

        // Event Delete
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("DeleteData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                // var ID
                param = new SqlParameter("@id", SqlDbType.Int);
                param.Value = comboBox1.SelectedValue.ToString();

                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MyMessage("Deleted");

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }

        // UpdateData
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("UpdateData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[3];

                // var ID
                param[0] = new SqlParameter("@id",SqlDbType.Int);
                param[0].Value= comboBox1.SelectedValue.ToString();

                // var Name
                param[1] = new SqlParameter("@name",SqlDbType.VarChar);
                param[1].Value=txt_name.Text;

                // var price
                param[2] = new SqlParameter("@price",SqlDbType.VarChar);
                param[2].Value=txt_price.Text;

                cmd.Parameters.AddRange(param);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MyMessage("Updated");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }

        // My Message
        void MyMessage(string title)
        {
            GetAllData();
            MessageBox.Show($"The {title} is Successfully", $"{title} Data", MessageBoxButtons.OK, MessageBoxIcon.Information);     
        }

        // Event Get Data By ID
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Shose item")
                {
                    return;
                }
                if (con.State == ConnectionState.Closed) {
                    con.Open();
                }

                //cmd = new SqlCommand("GetDataById", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter param = new SqlParameter();

                ////var ID
                //param = new SqlParameter("@id", SqlDbType.Int);
                //param.Value = comboBox1.SelectedValue.ToString();

                //cmd.Parameters.Add(param);
                //adapter2 = new SqlDataAdapter("GetDataById", con);
                //adapter2.Fill(dataTable2);
                //con.Close();

                //txt_name.Text = dataTable2.Rows[0]["Name"].ToString();
                //txt_price.Text = dataTable2.Rows[0]["Price"].ToString();
            }
            catch (SqlException E)
            {
                MessageBox.Show(E.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}

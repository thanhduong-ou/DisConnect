using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Bai9_DisConnect
{
    public partial class frmCustomer : Form
    {
        string cnStr = "";
        SqlConnection cn;
        DataSet ds;

        public frmCustomer()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnStr);
            dgvCustomer.DataSource = GetCustomerDataset().Tables[0];
        }
        
        public DataSet GetCustomerDataset()
        {
            try
            {
                string sql = "SELECT * FROM KhachHang";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                cn.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(); //=>UPDATE
            
            //////////////INSERT
            //string ins = "INSERT INTO KhachHang(MaKH, TenKH, DiaChi, DienThoai, Fax) VALUES(@id, @name, @address, @phone, @fax)";
            string ins = "INSERT INTO KhachHang(MaKH) VALUES(@id)";                        
            SqlCommand cmd = new SqlCommand(ins, cn);
            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 4, "MaKH");
            //cmd.Parameters.Add("@id", SqlDbType.NVarChar, 4, "MaKH");
            da.InsertCommand = cmd;           
            
            /////////////DELETE
            string del = "DELETE FROM KhachHang WHERE MaKH = @id";
            cmd = new SqlCommand(del, cn);
            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 4, "MaKH");
            da.DeleteCommand = cmd;
            
            /////////////UPDATE
            //
            ////////////
            da.Update(ds);            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            ds.RejectChanges();
        }
    }
}

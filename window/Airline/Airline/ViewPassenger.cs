using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Airline
{
    public partial class ViewPassenger : Form
    {
        public ViewPassenger()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\THANH\OneDrive\Documents\MayBayDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()// hàm lỗi
        {
            //Mở DB
            if (Con.State == System.Data.ConnectionState.Closed)
            {
                Con.Open();
            }
            string a = "select * from Addpassenger ";
            //Khai báo 
            SqlDataAdapter Sda = new SqlDataAdapter(a, Con);
            SqlCommandBuilder CmdB = new SqlCommandBuilder(Sda);
            var ds = new DataSet();
            Sda.Fill(ds,"AssPassenger");
            dgvKhach.DataSource = ds.Tables["AssPassenger"];
            //Đóng DB
            Con.Close();
        }
        private void Update_Click(object sender, EventArgs e)
        {
            if (txtIDKhach.Text == "" || txtTenKhach.Text == "" || txtPassport.Text == "" || txtDiaChi.Text == "" || txtDienThoai.Text == "")
            {
                MessageBox.Show("Khong co thong tin de them");
            }
            else
            {

                try
                {
                    if (Con.State == System.Data.ConnectionState.Closed)
                    {
                        Con.Open();
                    }
                    string a = "update AddPassenger set TenKhach = N'" + txtTenKhach.Text + "',Passport = '" + txtPassport.Text + "',DiaChi = N'" + txtDiaChi.Text + "',QuocTich = N'" + cbQuocTich.SelectedItem.ToString() + "',GioiTinh = N'" + cbGioiTinh.SelectedItem.ToString() + "',DienThoai = " + txtDienThoai.Text + " where IDKhach = "+txtIDKhach.Text+";";
                    //Đối tượng thực thi truy vấn
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.CommandText = a;
                    //gửi truy vấn vào csdl
                    sqlcmd.Connection = Con;
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Sua thanh cong");
                    populate();
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Con.Close();
            }
        }
        private void ViewPassenger_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            AddPassenger n = new AddPassenger();
            n.Show();
            this.Hide();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (txtIDKhach.Text == "")
            {
                MessageBox.Show("Vui long nhap id de xoa !");
            }
            else
            {
                try
                {
                    //Mở DB
                    Con.Open();
                    //Câu lệnh truy vấn
                    string a = "delete from AddPassenger where IDKhach="+txtIDKhach.Text+";";
                    SqlCommand Cmd = new SqlCommand(a, Con);
                    Cmd.ExecuteNonQuery();
                    MessageBox.Show("Da xoa thanh cong");
                    populate();
                    //Đ DB
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dgvKhach_CellContentClick(object sender, DataGridViewCellEventArgs e)//hàm lỗi
        {
            dgvKhach.CurrentRow.Selected = true;
            DataGridViewRow row = dgvKhach.Rows[e.RowIndex];
            txtIDKhach.Text = row.Cells["IDKhach"].Value.ToString();
            txtTenKhach.Text = row.Cells["TenKhach"].Value.ToString();
            txtPassport.Text = row.Cells["Passport"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            cbQuocTich.SelectedItem = row.Cells["QuocTich"].Value.ToString();
            cbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
            txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();


        }

        private void Reset_Click(object sender, EventArgs e)
        {
            
            txtIDKhach.Text = "";
            txtTenKhach.Text = "";
            txtPassport.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }
    }
}

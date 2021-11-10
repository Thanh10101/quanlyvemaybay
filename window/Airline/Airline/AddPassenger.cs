using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Airline
{
    public partial class AddPassenger : Form
    {
        //Kết nối cơ sở dữ liệu
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\THANH\OneDrive\Documents\MayBayDB.mdf;Integrated Security=True;Connect Timeout=30");
        public AddPassenger()
        {
            InitializeComponent();
        }


        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Nút thêm thông tin khách
        private void button1_Click(object sender, EventArgs e)
        {
            //Kiểm tra khách hàng có điền đầy đủ thông tin
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
                    string a = "insert into AddPassenger values(" + txtIDKhach.Text + ",N'" + txtTenKhach.Text + "','" + txtPassport.Text + "',N'" + txtDiaChi.Text + "',N'" + cbQuocTich.SelectedItem.ToString() + "',N'" + cbGioiTinh.SelectedItem.ToString() + "'," + txtDienThoai.Text + ")";
                    //Đối tượng thực thi truy vấn
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.CommandText = a;
                    //gửi truy vấn vào csdl
                    sqlcmd.Connection = Con;
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Them thanh cong");
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Con.Close();

            }
        }
        //Nút gọi form ViewPassenger
        private void ViewPassenger_Click(object sender, EventArgs e)
        {
            ViewPassenger n = new ViewPassenger();
            n.Show();
            this.Hide();
        }
        //Nút đặt lại
        private void button2_Click(object sender, EventArgs e)
        {
            txtIDKhach.Text = "";
            txtTenKhach.Text = "";
            txtPassport.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取文本框中的值
            string username = this.textBox1.Text.Trim();
            string password = this.textBox2.Text.Trim();

            string testDB = ConfigurationManager.ConnectionStrings["testDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(testDB);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from T_login where username='"+username+"'and password='"+password+"'";
            //select * from T_login where username='admin' and password='123456'

            //sql注入语句 ' or 1=1 -- '
            //select * from T_login where username='' or 1=1 -- '' and password='' or 1=1 -- ''


            SqlDataReader dr = cmd.ExecuteReader();

            if (username.Equals("") || password.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                if (dr.Read())
                {
                    MessageBox.Show(username+ "登录成功");
                }
                else
                {
                    MessageBox.Show("登录失败");
                }
            }
            conn.Close();
            conn.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取文本框中的值
            string username = this.textBox1.Text.Trim();
            string password = this.textBox2.Text.Trim();

            string testDB = ConfigurationManager.ConnectionStrings["testDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(testDB);
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from T_login where username=@username and password=@password";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (username.Equals("") || password.Equals(""))//用户名或密码为空
                {
                    MessageBox.Show("用户名或密码不能为空");
                }
                else
                {
                    if (dr.Read())
                    {
                        MessageBox.Show(dr[0] + "登录成功");
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("登录失败");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //获取文本框中的值
            string username = this.textBox1.Text.Trim();
            string password = this.textBox2.Text.Trim();

            string testDB = ConfigurationManager.ConnectionStrings["testDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(testDB);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("up_login", conn);
                cmd.CommandType = CommandType.StoredProcedure; //通过存储过程的方式执行
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dr = cmd.ExecuteReader();
                if (username.Equals("") || password.Equals(""))//用户名或密码为空
                {
                    MessageBox.Show("用户名或密码不能为空");
                }
                else
                {
                    if (dr.Read())
                    {
                        MessageBox.Show(dr[0] + "登录成功");
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("登录失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

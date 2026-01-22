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


namespace ADO.NET_Connected_Architecture_Demo
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString);
        }

        //clear textbox meethod
        private void ClearFormFields()
        {
            txtid.Clear();
            txtName.Clear();
            txtSalary.Clear();
            txtEmail.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into employee values(@name,@email,@salary)";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if(res>=1)
                {
                    MessageBox.Show("Employee Added succesfully");
                    ClearFormFields();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                string qry = "Select name,email,salary from employee where empid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                conn.Open();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    if(dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtEmail.Text = dr["email"].ToString();
                        txtSalary.Text = dr["salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No record found");
                }

                    }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "Update employee set name=@name,email=@email,salary=@salary where empid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Employee Updated Succesfully");
                }
                else
                {
                    MessageBox.Show("An error occured");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from employee where empid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if(res>=1)
                {
                    MessageBox.Show("Employee Deleted Sucesfully");
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("No record found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from employee";
                cmd = new SqlCommand(qry, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select avg(salary) from employee";
                cmd = new SqlCommand(qry, conn);
                conn.Open();
                object avgsal = cmd.ExecuteScalar();
                if (avgsal!=null)
                {
                   txtavg.Text = avgsal.ToString();
                }
                else
                {
                    MessageBox.Show("no salary found");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    }


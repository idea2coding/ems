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

namespace Employee_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        SqlConnection con = new SqlConnection(@"Data Source=GWTN156-11;Initial Catalog=ems;Integrated Security=True");

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to exit...?","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            
            try
            {
                if(username=="" && password == "")
                {
                    MessageBox.Show("Username & Password Is Empty!!!","Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                }
                else
                {
                    con.Open();
                    string query_select = "SELECT * FROM login WHERE username = '" + username + "' AND Password = '" + password + "' ";
                    SqlCommand cmnd = new SqlCommand(query_select, con);
                    SqlDataReader row = cmnd.ExecuteReader();

                    if (row.HasRows)
                    {
                        this.Hide();
                        Registration obj = new Registration();
                        obj.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login credentials, Please check Username & Password and try again!", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            finally
            {
                con.Close();
            }
        }
    }
}

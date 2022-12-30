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
    public partial class Registration : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=GWTN156-11;Initial Catalog=ems;Integrated Security=True");
        public Registration()
        {
            InitializeComponent();
        }

       

        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtFName.Text;
                string lastName = txtLName.Text;
                dtpDob.Format = DateTimePickerFormat.Custom;
                dtpDob.CustomFormat = "yyyy/MM/dd";
                string gender;
                if (rbMale.Checked)
                {
                    gender = "Male"; 
                }
                else
                {
                    gender = "Female";
                }
                string address = txtAddress.Text;
                string email = txtEmail.Text;
                int mobilePhone = int.Parse(txtMobile.Text);
                int homePhone = int.Parse(txtHphone.Text);
                string departmentName = txtDName.Text;
                string designation = txtDesignation.Text;
                string employeeType = txtEtype.Text;
               
                
                con.Open();
                string query_insert = "INSERT INTO employee VALUES('" + firstName + "','" + lastName + "','" + dtpDob.Text + "','" + gender + "','" + address + "','" + email + "'," + mobilePhone + "," + homePhone + ",'" + departmentName + "','" + designation + "','" + employeeType + "')";
                SqlCommand cmnd = new SqlCommand(query_insert, con);
                cmnd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record added successfully!", "Registered Employee!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            
            string no = cmbReg.Text;

            try
            {
                

                if (no != "New Register")
                {
                    string firstName = txtFName.Text;
                    string lastName = txtLName.Text;
                    dtpDob.Format = DateTimePickerFormat.Custom;
                    dtpDob.CustomFormat = "yyyy/MM/dd";
                    string gender;
                    if (rbMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    string address = txtAddress.Text;
                    string email = txtEmail.Text;
                    int mobilePhone = int.Parse(txtMobile.Text);
                    int homePhone = int.Parse(txtHphone.Text);
                    string departmentName = txtDName.Text;
                    string designation = txtDesignation.Text;
                    string employeeType = txtEtype.Text;

                    con.Open();
                    string query_insert = "UPDATE employee SET firstName = '" + firstName + "',lastName='" + lastName + "',dateOfBirth= '" + dtpDob.Text + "',gender = '" + gender + "',address = '" + address + "',email = '" + email + "',mobilePhone = " + mobilePhone + ",homePhone = " + homePhone + ",departmentName ='" + departmentName + "',designation = '" + designation + "',employeeType = '" + employeeType + "' WHERE empNo = '" + no+"'"  ;
                    SqlCommand cmnd = new SqlCommand(query_insert, con);
                    cmnd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Update Successfully!", "Update Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbReg.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.CustomFormat = "yyyy/MM/dd";
            DateTime thisDay = DateTime.Today;
            dtpDob.Text = thisDay.ToString();

            rbMale.Checked = false;
            rbFemale.Checked = false;

            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtHphone.Text = "";
            txtDName.Text = "";
            txtDesignation.Text = "";
            txtEtype.Text = "";

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                var result = MessageBox.Show("Are you sure, Do you really want to delete this record...?", "Delet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string no = cmbReg.Text;
                   
                    con.Open();
                    string query_inset = "DELETE FROM employee WHERE empNo = " + no + "";
                    SqlCommand cmnd = new SqlCommand(query_inset, con);
                    cmnd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record deleted successfully!", "Deleted Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }
        private void linkLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Close();
        }

        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            con.Open();
            string query_select = "SELECT * FROM employee";
            SqlCommand cmnd = new SqlCommand(query_select, con);
            SqlDataReader row = cmnd.ExecuteReader();
            cmbReg.Items.Add("New Register");
            while (row.Read())
            {
                cmbReg.Items.Add(row[0].ToString());
            }
            con.Close();
        }

        private void cmbReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string no = cmbReg.Text;

            try{
                if (no != "New Register")
                {
                    con.Open();
                    string query_select = "SELECT * FROM employee WHERE empNo =" + no;
                    SqlCommand cmd = new SqlCommand(query_select, con);
                    SqlDataReader row = cmd.ExecuteReader(); 
                    while (row.Read())
                    {
                        txtFName.Text = row[1].ToString();
                        txtLName.Text = row[2].ToString();
                        dtpDob.Format = DateTimePickerFormat.Custom;
                        dtpDob.CustomFormat = "yyyy/MM/dd";
                        dtpDob.Text = row[3].ToString();
                        if (row[4].ToString() == "Male")
                        {
                            rbMale.Checked = true;
                            rbFemale.Checked = false;
                        }
                        else
                        {
                            rbMale.Checked = false;
                            rbFemale.Checked = true;
                        }
                        txtAddress.Text = row[5].ToString();
                        txtEmail.Text = row[6].ToString();
                        txtMobile.Text = row[7].ToString();
                        txtHphone.Text = row[8].ToString();
                        txtDName.Text = row[9].ToString();
                        txtDesignation.Text = row[10].ToString();
                        txtEtype.Text = row[11].ToString();
                    }
                    con.Close();
                    btnReg.Enabled = false;
                    btnUp.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    txtFName.Text = "";
                    txtLName.Text = "";
                    dtpDob.Format = DateTimePickerFormat.Custom;
                    dtpDob.CustomFormat = "yyyy/MM/dd";
                    DateTime thisDay = DateTime.Today;
                    dtpDob.Text = thisDay.ToString();

                    rbMale.Checked = false;
                    rbFemale.Checked = false;

                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtMobile.Text = "";
                    txtHphone.Text = "";
                    txtDName.Text = "";
                    txtDesignation.Text = "";
                    txtEtype.Text = "";

                    btnReg.Enabled = true;
                    btnUp.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch(Exception ex){
                MessageBox.Show(""+ ex);
            }
        }

        private void txtDepName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Residence_provision_system
{
    public partial class Previous_Bills : Form
    {
        
        
        public Previous_Bills()
        {
            InitializeComponent();

            rpsDataContext dbcon = new rpsDataContext();
            comboBox1.DataSource = dbcon.RENTERs;
            comboBox1.ValueMember = "rented_flat";

            load_data();

            //string s = comboBox1.SelectedText;

            if (login.user != "owner")
            {
                button1.Visible = false;
                button2.Visible = false;
                //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.SelectedIndex = comboBox1.FindStringExact(login.user);
                comboBox1.Enabled = false;
                //comboBox1.IsAccessible = false  ;
                //remove_flats_combo();
            }
            else
                comboBox1.SelectedIndex = comboBox1.FindStringExact(flat.flatselected);


        }

        private void load_data()
        {
            rpsDataContext dbcon = new rpsDataContext();
            
            //Bill_Table bill = new Bill_Table();
            var bills = from q in dbcon.Bill_Tables
                        where q.flat == comboBox1.SelectedValue.ToString()
                        select new
                        {
                            q.Bill_Date,
                            q.House_Rent, 
                            q.Electricity_Bill, 
                            q.Water_Bill,
                            q.Gas_Bill, 
                            q.Service_Charge
                        };

            bills = bills.OrderByDescending(x => x.Bill_Date);
            dataGridView1.DataSource = bills;
      
            RENTER r = dbcon.RENTERs.SingleOrDefault(x => x.rented_flat == comboBox1.SelectedValue.ToString());

            if (r != null)
            {
                label5.Text = r.r_name;
                label6.Text = r.rent_date.ToString(); ;
            }

             
 
        }
        private void remove_flats_combo()
        {

            comboBox1.Items.Clear();
            comboBox1.Items.Add(login.user);
            comboBox1.SelectedIndex = comboBox1.FindStringExact(login.user);

        }


        public static string billType = "";



        private void button1_Click(object sender, EventArgs e)
        {
            billType = "update";
            new New_Bill().ShowDialog();
           
        }

        private void Previous_Bills_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Current_Month_Bill().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Previous_Bills_Load(object sender, EventArgs e)
        {

        }
    }
}

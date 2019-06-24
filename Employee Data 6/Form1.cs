using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmpData;

namespace Employee_Data_6
{
    public partial class Form1 : Form
    {
        public string txtFile;
        public int showEmp = 3;
        public string searchFirstName = "";
        public string searchLastName = "";
        public string searchAddress = "";
        public int searchEmpInt = 0;
        public int maxEmp = 5;
        public int empCounter = 0;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open dialog box to get file
            openFileDialog1.ShowDialog();
            txtFile = openFileDialog1.FileName;

        }



        private void button2_Click(object sender, EventArgs e)
        {
            // send file and parse data

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Enter number of employees");
            }
            else if (string.IsNullOrWhiteSpace(txtFile))
            {
                MessageBox.Show("Select File");
            }


            else
            {
            maxEmp = Convert.ToInt32(textBox2.Text);
            empCounter = 0;
            ParseEmp2(txtFile);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // show all employees
            showEmp = 3;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // show developers only
            showEmp = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // show managers only
            showEmp = 1;
        }

        void ParseEmp2(string path)
        {
            List<Manager> managers = new List<Manager>();
            List<Developer> developers = new List<Developer>();

            DataTable table = new DataTable();
            DataView view;

            table.Columns.Add("First Name");
            table.Columns.Add("Last Name");
            table.Columns.Add("Address");
            table.Columns.Add("City");
            table.Columns.Add("State");
            table.Columns.Add("Zip");
            table.Columns.Add("Position");
            table.Columns.Add("Dev Type");
            table.Columns.Add("Cost Center");
            table.Columns.Add("Supervisor");
            table.Columns.Add("Tax");

            using (var reader = new StreamReader(path))
            {
                int lineCount = 0;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] values = line.Split('|');

                    if (values[6] == "Developer")
                    {
                        developers.Add(new Developer()
                        {
                            FirstName = values[0],
                            LastName = values[1],
                            Address = values[2],
                            City = values[3],
                            State = values[4],
                            Zip = values[5],
                            EmployeeType = values[6],
                            DevType = values[7],
                            Supervisor = values[9],
                            TaxType = values[10]
                        });
                    }

                    if (values[6] == "Manager")
                    {
                        managers.Add(new Manager()
                        {
                            FirstName = values[0],
                            LastName = values[1],
                            Address = values[2],
                            City = values[3],
                            State = values[4],
                            Zip = values[5],
                            EmployeeType = values[6],
                            CostCenter = values[7],
                            Supervisor = values[8],
                            TaxType = values[9]
                        });
                    }
                    lineCount++;
                }

                // search developers
                // search first name in developers
                if (searchEmpInt == 1)
                {
                    var first =
                        from e in developers
                        where e.FirstName == searchFirstName
                        select e;

                    foreach (var developer in first)
                    {
                        table.Rows.Add(developer.FirstName, developer.LastName, developer.Address, developer.City, developer.State, developer.Zip, developer.EmployeeType, developer.DevType, "", developer.Supervisor, developer.TaxType);
                    }
                }

                // search last name in developers
                else if (searchEmpInt == 2)
                {
                    var last =
                    from e in developers
                    where e.LastName == searchLastName
                    select e;

                    foreach (var developer in last)
                    {
                        table.Rows.Add(developer.FirstName, developer.LastName, developer.Address, developer.City, developer.State, developer.Zip, developer.EmployeeType, developer.DevType, "", developer.Supervisor, developer.TaxType);
                    }
                }

                else if (searchEmpInt == 3)
                {
                    // search address in developers
                    var address =
                    from e in developers
                    where e.Address == searchAddress
                    select e;

                    foreach (var developer in address)
                    {
                        table.Rows.Add(developer.FirstName, developer.LastName, developer.Address, developer.City, developer.State, developer.Zip, developer.EmployeeType, developer.DevType, "", developer.Supervisor, developer.TaxType);

                    }
                }
                // end search developers

                // search managers
                // search first name in managers
                if (searchEmpInt == 1)
                {
                    var first =
                        from e in managers
                        where e.FirstName == searchFirstName
                        select e;

                    foreach (var manager in first)
                    {
                        table.Rows.Add(manager.FirstName, manager.LastName, manager.Address, manager.City, manager.State, manager.Zip, manager.EmployeeType, "", manager.CostCenter, manager.Supervisor, "");
                    }
                }

                // search last name in developers
                else if (searchEmpInt == 2)
                {
                    var last =
                    from e in managers
                    where e.LastName == searchLastName
                    select e;

                    foreach (var manager in last)
                    {
                        table.Rows.Add(manager.FirstName, manager.LastName, manager.Address, manager.City, manager.State, manager.Zip, manager.EmployeeType, "", manager.CostCenter, manager.Supervisor, "");
                    }
                }

                else if (searchEmpInt == 3)
                {
                    // search address in developers
                    var address =
                    from e in managers
                    where e.Address == searchAddress
                    select e;

                    foreach (var manager in address)
                    {
                        table.Rows.Add(manager.FirstName, manager.LastName, manager.Address, manager.City, manager.State, manager.Zip, manager.EmployeeType, "", manager.CostCenter, manager.Supervisor, "");
                    }
                }

                if (showEmp == 3 || showEmp == 2 || showEmp == 1)
                {
                    if (showEmp == 2 || showEmp == 3)
                    {
                        foreach (var developer in developers)
                        {
                            if (maxEmp <= empCounter) { break; }
                            table.Rows.Add(developer.FirstName, developer.LastName, developer.Address, developer.City, developer.State, developer.Zip, developer.EmployeeType, developer.DevType, "", developer.Supervisor, developer.TaxType);
                            empCounter++;
                        }
                    }

                    if (showEmp == 1 || showEmp == 3)
                    {
                        foreach (var manager in managers)
                        {
                            if (maxEmp <= empCounter) { break; }
                            table.Rows.Add(manager.FirstName, manager.LastName, manager.Address, manager.City, manager.State, manager.Zip, manager.EmployeeType, "", manager.CostCenter, manager.Supervisor, "");
                            empCounter++;
                        }

                    }
                }
            }
            view = new DataView(table);
            dataGridView1.DataSource = view;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // search button
            ParseEmp2(txtFile);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            // search first name
            searchEmpInt = 1;
            searchFirstName = textBox1.Text;
            showEmp = 4;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            // search last name
            searchEmpInt = 2;
            searchLastName = textBox1.Text;
            showEmp = 4;
        }
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            // search address
            searchEmpInt = 3;
            searchAddress = textBox1.Text;
            showEmp = 4;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            // search text box
            searchFirstName = textBox1.Text;
            searchLastName = textBox1.Text;
            searchAddress = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // reset table to show all employees
            ParseEmp2(txtFile);
            showEmp = 3;
            empCounter = 0;
            textBox1.Text = string.Empty;
        }

        // Garbage below

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

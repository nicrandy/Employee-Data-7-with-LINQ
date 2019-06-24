using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employee_Data_6;

namespace EmpData
{
    public class Employee
    {
        public string EmployeeType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Supervisor { get; set; }
        public string TaxType { get; set; }
    }

    public class Developer : Employee
    {
        public string DevType { get; set; }
    }

    public class Manager : Employee
    {
        public string CostCenter { get; set; }
    }

    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


    }

}

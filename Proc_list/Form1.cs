using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Proc_list
{
    public partial class Form1 : Form
    {
        List<Process> list;
        public Form1()
        {
            InitializeComponent();
            list = new List<Process>();

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            list.Add(new Process());
            list.First().StartInfo = new ProcessStartInfo(comboBox.Text);
            list.First().Start();
            comboBox.Items.Clear();
            //comboBox.Items.AddRange(list);
            string item = list.First().ProcessName;
            item += $"(PID: {list.First().Id})";
            comboBox.Items.Add(item);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}

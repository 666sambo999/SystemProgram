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

namespace Process
{
    public partial class MyForm : Form
    {
        List<Proces> processes;
        public MyForm()
        {
            InitializeComponent();
            processes = new List<Proces>;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            comboBox.Items.Add(comboBox.Text);
        }
    }
}

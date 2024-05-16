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
using Microsoft.Win32;

namespace SystemProg
{
    public partial class MyForm : Form
    {
        Process process;
        public MyForm()
        {
            InitializeComponent();
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            //buttonStop.Enabled = false;
            richTextBox1.Text = "notepad";

            SetControl(true);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            process = new Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(richTextBox1.Text);
            process.Start();
            SetControl(false);
            labelInfo.Text = SetLableProcessInfo();

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            process.CloseMainWindow();
            process.Close();
            //process.Kill();
            SetControl(true);

        }
        private void SetControl(bool enableControl)
        {
            richTextBox1.Enabled = enableControl;
            buttonStart.Enabled = enableControl;
            buttonStop.Enabled = !enableControl;
            timer1.Enabled = !enableControl;
        }
        private string SetLableProcessInfo()
        {
            string info = "Process info:\n";
            info += $"PID:\t{process.Id.ToString()}\n";
            info += $"Name: \t{process.ProcessName}\n";
            info += $"Priority: \t{process.BasePriority}\n";
            info += $"Conteiner: \t{process.Container}\n";
            info += $"Handle: \t{process.HandleCount}\n";
            info += $"Virtual Memory: \t{process.VirtualMemorySize64/1024}\n";
            info += $"Priority Class: \t{process.PriorityClass}\n";
            info += $"Total Process Time: \t{process.TotalProcessorTime}\n";
            info += $"Session ID: \t{process.SessionId}\n";
            info += $"Threads: \t{process.Threads.Count}\n";
            info += $"User name: \t{process.StartInfo.UserName}\n";
            info += $"Working: \t{process.StartInfo.WorkingDirectory}\n";
            
            info += $"CPU: \t{process.WorkingSet64}\n";
            info += $": \t{process.WorkingSet64}\n";
            return info;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelInfo.Text = SetLableProcessInfo();
        }
    }
}          
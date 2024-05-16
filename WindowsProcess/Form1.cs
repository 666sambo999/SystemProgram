using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace WindowsProcess
{
    public partial class Form1 : Form
    {
        Process process;
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            //buttonStop.Enabled = false;
            richTextBox1.Text = "notepad";
            
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
            info += $"Virtual Memory: \t{process.VirtualMemorySize64 / 1024}\n";
            info += $"Virtual Memory: \t{process.VirtualMemorySize64}\n";
            info += $"Priority Class: \t{process.PriorityClass}\n";
            info += $"Total Process Time: \t{process.TotalProcessorTime}\n";
            info += $"Session ID: \t{process.SessionId}\n";
            info += $"Threads: \t{process.Threads.Count}\n";
            info += $"User name: \t{process.StartInfo.UserName}\n";
            info += $"Working: \t{process.StartInfo.WorkingDirectory}\n";
            info += $"CPU1: \t{process.PrivilegedProcessorTime}\n";

            info += $"CPU: \t{process.WorkingSet64}\n";
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            info += $"Name: \t{(currentUser.Name)}";
            
            return info;
       
        } 
               
        private void buttonStart_Click_1(object sender, EventArgs e)
        {
            process = new Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(richTextBox1.Text);
            process.Start();
            SetControl(false);
            labelInfo.Text = SetLableProcessInfo();
            
        }

        private void buttonStop_Click_1(object sender, EventArgs e)
        {
            process.CloseMainWindow();
            process.Close();
            //process.Kill();
            SetControl(true);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            labelInfo.Text = SetLableProcessInfo();
        }
         
      


    }
}
    


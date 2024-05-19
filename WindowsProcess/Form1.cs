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
using System.Runtime.InteropServices;

namespace WindowsProcess
{
    public partial class Form1 : Form
    {
        
        
        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            //buttonStop.Enabled = false;
            richTextBox1.Text = "notepad";
            labelInfo.Text = "No information";
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
            process.Refresh();
            if (!process.Responding) return "No active process";
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
            //info += $"User name: \t{process.StartInfo.UserName}\n";
            info += $"File name: \t{process.MainModule.FileName}\n";
            info += $"Working: \t{process.StartInfo.WorkingDirectory}\n";
            info += $"CPU1: \t{process.PrivilegedProcessorTime}\n";
            //PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor time", process.ProcessName, true);
            //double user = 0;
            //for (int i =0; i<5; i++)
            //{
            //    user= cpuCounter.NextValue();
            //    if (user != 0) break;
            //}
            //info += $"CPU usege:\t{CPU_user()}";
            info += $"CPU: \t{process.WorkingSet64}\n";
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            info += $"Name: \t{(currentUser.Name)}";
                        // 2 вариант 
            //IntPtr handle = IntPtr.Zero;
            //OpenProcessToken(process.Handle, 8, out handle);
            //WindowsIdentity b = new WindowsIdentity(handle);
            //info += $"UserName: \t {b.Name}\n";
            return info;
       
        } 
        //private async Task<int> CPU_user()
        //{
        //    PerformanceCounter Counter = new PerformanceCounter("Process", "% Processor time",process.ProcessName, true);
        //    await Task.Delay(500);
        //    return (int)Counter.NextValue();
        //}
               
        private void buttonStart_Click_1(object sender, EventArgs e)
        {
            //process = new Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(richTextBox1.Text);
            process.Start();
            SetControl(false);
            labelInfo.Text = SetLableProcessInfo();
            
        }

        private void buttonStop_Click_1(object sender, EventArgs e)
        {
            //process.CloseMainWindow();
            //process.Close();
            //process.Kill();
            if (!process.CloseMainWindow())
                process.Kill();
            SetControl(true);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            labelInfo.Text = SetLableProcessInfo();
        }
        private void process_Exited(object sender, EventArgs e)
        {
            SetControl(true);
            process.Close();
        }
        private int CPU_Time()
        {
            int time = 0;
            TimeSpan span = process.UserProcessorTime + process.PrivilegedProcessorTime;
            //TimeSpan 
            return time;
        }
        //[DllImport("advapi32.dll", SetLastError = true)]
        //private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr handle);
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool CloseHandle(IntPtr handle);


    }
}
    


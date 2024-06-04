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
using System.Security.Principal;
using System.Runtime.InteropServices;

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
            if (comboBox.Text == "")
            {
                MessageBox.Show("Вы забыли ввести имя процесса!", "Ошибка!");
                return;
            }
            list.Add(new Process());
            list.Last().StartInfo = new ProcessStartInfo(comboBox.Text);
            list.Last().Start();
            //comboBox.Items.Clear();
            //comboBox.Items.AddRange(list);
            list.Last().EnableRaisingEvents = true;
            list.Last().Exited += proc_close_handler;

            string item = list.Last().ProcessName;
            item += $"(PID: {list.Last().Id})";
            comboBox.Items.Add(item);
            
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private string SetLableProcessInfo(int i)
        {
            Process process = list[i];
            process.Refresh();
            //if (!process.Responding) return "No active process";
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex>=0)
                labelInfo.Text = SetLableProcessInfo(comboBox.SelectedIndex);

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            foreach(Process i  in list)
            {
                i.CloseMainWindow();
                i.Close();
            }
        }

        private void buttonstop_Click(object sender, EventArgs e)
        {
            
        }
        private void process_Exited(object sender, EventArgs e)
        {
            //Process process = list[comboBox.SelectedIndex];
            //int pid = ((Process)sender).Id;
            for (int i = 0; i <list.Count; i++)
            {
                //if (pid == list[i].Id)
                if (((Process)sender) == list[i])
                {
                    if (i == comboBox.SelectedIndex) labelInfo.Text = "Nein info";
                    list[i].Close();
                    list.RemoveAt(i);
                    comboBox.Items.RemoveAt(i);
                    if (i == 0) {comboBox.Text = "";}
                }
            }
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint uMsg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);
        
    }
}

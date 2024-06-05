﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace TaskWin
{
    public partial class MainForm : Form
    {
        List<Process> processes;
        Process[] a_processes;
        Dictionary<int, Process> d_processes;
        public MainForm()
        {
            InitializeComponent();
            listViewProc.Columns.Add("PID");
            listViewProc.Columns.Add("Name");
            processes = Process.GetProcesses().OfType<Process>().ToList();
            UpdateProc();
            LoadProcess();
            AllocConsole();
            RemoveCloseProc();
        }

        private void tabPage_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList()))
                UpdateProc();
            statusStrip1.Items["toolStripStatusLabel1"].Text = $"Total processes: {processes.Count}, displayed processes {listViewProc.Items.Count}";
        }
        void UpdateProc()
        {

            if (processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList())) 
                return;
            this.processes = Process.GetProcesses().OfType<Process>().ToList();
            Dictionary<int, Process> d_process = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
            this.d_processes = d_process;
            listViewProc.BeginUpdate();
            AddWiewProc();
            listViewProc.EndUpdate();
        }
        void RemoveCloseProc()
        {
            //AllocConsole();
            //Console.Clear();
            for (int i = 0; i < listViewProc.Items.Count; i++)
            {
                Console.Write(Convert.ToInt32(listViewProc.Items[i].Text) + "\t");
                if (!d_processes.ContainsKey(Convert.ToInt32(listViewProc.Items[i].Text)))
                {
                    listViewProc.Items.RemoveAt(i);
                }
            }
        }
        void AddWiewProc()
        {
            //Console.Clear();
            for (int i = 0; i < d_processes.Count; i++)
            {
                KeyValuePair<int, Process> pair = d_processes.ElementAt(i);
                //if (listViewProc.Items.ContainsKey(pair.Key.ToString()))
                if (listViewProc.FindItemWithText(pair.Key.ToString())==null)
                {
                    ListViewItem item = new ListViewItem(pair.Key.ToString());
                    //listViewProc.Items.Add(item);
                    item.SubItems.Add(pair.Value.ProcessName);
                    listViewProc.Items.Add(item);
                }
            }
            //foreach (ListViewItem i in listViewProc.Items)
            //{
            //    Console.WriteLine(Convert.ToInt32(i.Text) + "\t");
            //}
        }
        void LoadProcess()
        {
            listViewProc.Items.Clear();
            //processes = Process.GetProcesses().ToList();
            //for (int i = 0; i < processes.Count; i++)
            //{
            //    listViewProc.Items[i].Text = processes[i].ProcessName;
            //    listViewProc.Items[i].SubItems.Add(processes[i].Id.ToString());
            //}
            Dictionary<int, Process> d_process = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
            this.d_processes = d_process;
            for (int i = 0; i < processes.Count;i++)
            {
                KeyValuePair<int, Process> pair = d_process.ElementAt(i);
                listViewProc.Items.Add(pair.Key.ToString());
                listViewProc.Items[i].SubItems.Add(pair.Value.ProcessName);
            }
        }
        
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
    }
}

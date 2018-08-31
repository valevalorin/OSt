using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OSt
{
    public partial class Primary : Form
    {
        private EventHook ev;
        private Process soundAgentProcess;
        private Process hookActorProcess = null;
        private SupervisedJob job;
        private List<Assignment> assignments;
        private Configuration config;
        

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError=true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public Primary()
        {
            ev = new EventHook(WinEventProc, EventHook.WINEVENT_OUTOFCONTEXT, EventHook.EVENT_SYSTEM_FOREGROUND);
            job = new SupervisedJob();

            InitializeComponent();

            this.soundAgentProcess = new Process();
            ProcessStartInfo soundAgentProcessInfo = new ProcessStartInfo();
            soundAgentProcessInfo.CreateNoWindow = true;
            //soundAgentProcessInfo.RedirectStandardOutput = true;
            //soundAgentProcessInfo.UseShellExecute = false;
            soundAgentProcessInfo.FileName = "ost-sound-agent.exe";
            soundAgentProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this.soundAgentProcess.StartInfo = soundAgentProcessInfo;
            this.soundAgentProcess.Start();

            job.AddProcess(soundAgentProcess.Handle);

            //load config
            //LoadConfig();
            this.config = Configuration.Load();
            config.Current.crossFade.seconds = 1.3;
            config.SaveToDisk();
        }


        private void Primary_Load(object sender, EventArgs e)
        {

        }

        public void Close(object sender, EventArgs e)
        {
            Process[] mplayers = Process.GetProcessesByName("mplayer");
            foreach(Process player in mplayers)
            {
                player.Kill();
            }
            Application.Exit();
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            GetWindowThreadProcessId(hwnd, out uint processId);
            Process p = Process.GetProcessById((int)processId);
            Log.Text += p.ProcessName + "\r\n";

            //if (this.hookActorProcess != null)
            //{
            //    this.hookActorProcess.Kill();
            //}

            Process hookActorProcess = new Process();
            ProcessStartInfo hookActorProcessInfo = new ProcessStartInfo();
            hookActorProcessInfo.CreateNoWindow = true;
            hookActorProcessInfo.RedirectStandardOutput = true;
            hookActorProcessInfo.UseShellExecute = false;
            hookActorProcessInfo.FileName = "ost-hook-actor.exe";
            hookActorProcessInfo.Arguments = "" + p.ProcessName + "";
            hookActorProcess.StartInfo = hookActorProcessInfo;
            hookActorProcess.Start();

            this.hookActorProcess = hookActorProcess;
            hookActorProcess.OutputDataReceived += (sender, args) => Log.Text += args.Data.ToString() + "\r\n";
        }

        public void processSelected()
        {
            trackList.Items.Clear();
            ListViewItem selection = processList.SelectedItems[0];
            string processName = selection.Text;
            Assignment a = config.getAssignment(processName);
            foreach (Track track in a.Tracks)
            {
                trackList.Items.Add(track.ToString());
            }
        }

        public void addProcess()
        {
            
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}

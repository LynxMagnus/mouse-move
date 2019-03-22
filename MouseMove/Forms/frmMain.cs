using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMove.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.MaximizeBox = false;
            //this.MinimizeBox = false;

            Timer timer = new Timer();
            timer.Interval = 60000;
            timer.Tick += (sender, args) => { Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y + 1); };
            timer.Start();            
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            PreventSleepAndMonitorOff();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            AllowSleep();
        }

        // Prevent computer from entering sleep or hibernate (monitor not affected)
        private void PreventSleep()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
        }

        // Prevent the system from turning off monitor
        private void PreventMonitorOff()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_DISPLAY_REQUIRED);
        }

        // Prevent the system from entering sleep and turning off monitor
        private void PreventSleepAndMonitorOff()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_DISPLAY_REQUIRED);
        }

        // Clear EXECUTION_STATE flags to allow the system to sleep and turn off monitor normally
        private void AllowSleep()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);
        }
    }

    internal static class NativeMethods
    {
        // Import SetThreadExecutionState Win32 API and necessary flags
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
    }
        
}

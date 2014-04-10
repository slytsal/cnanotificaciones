using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int SW_SHOWMAXIMIZED = 1;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            SplashView splash = null;
            int val = this.ValidacionProceso();

            if (val == 0 || val == 1)
            {
                splash = new SplashView();
                splash.ShowDialog();

            }
            else
            {
                Process runProcess = this.ValidacionProcesoMaximeze();
                if (runProcess != null)
                    ShowWindow(runProcess.MainWindowHandle, SW_SHOWMAXIMIZED);

                Application.Current.Shutdown();
                Process.GetCurrentProcess().Kill();

            }

        }

        public int ValidacionProceso()
        {
            Process[] procesos;
            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProceso"];
            procesos = Process.GetProcessesByName(proceso);
            return procesos.Length;

        }

        public Process ValidacionProcesoMaximeze()
        {
            Process[] procesos;

            Process runProcess = null;

            string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProceso"];
            procesos = Process.GetProcessesByName(proceso);

            foreach (Process p in procesos)
            {
                if (p.ProcessName == proceso)
                {
                    runProcess = p;
                }

            }
            return runProcess;

        }

        public MainNotif GetParetWindows()
        {
            MainNotif res = null;
            try
            {
                object query = Application.Current.Windows[0];
                res = query as MainNotif;
            }
            catch (Exception)
            {
                ;
            }
            return res;
        }
    }
}

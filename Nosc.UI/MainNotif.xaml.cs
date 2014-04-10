using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Nosc.ViewModel;
using System.Configuration;
using System.Reflection;
using MahApps.Metro.Controls;
//using System.Windows.Forms;

namespace Nosc.UI
{                                       
    /// <summary>
    /// Lógica de interacción para MainNotif.xaml
    /// </summary>
    public partial class MainNotif : MetroWindow
    {
       public bool IsRight = true;
        public MainNotif()
        {
            InitializeComponent();
            ConfigurationManager.AppSettings["Assembly"] = int.Parse((Assembly.GetExecutingAssembly().GetName().Version.ToString()).Replace(".", "")).ToString();
            //this.DataContext = new MainNotifViewModel();

            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Top = 0;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 5;
            //this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight - 40;
        }

        public MainNotif(UserLoginViewModel userLogin)
        {
            InitializeComponent();
            ConfigurationManager.AppSettings["Assembly"] = int.Parse((Assembly.GetExecutingAssembly().GetName().Version.ToString()).Replace(".", "")).ToString();
            System.Windows.Threading.Dispatcher dispatcher = Application.Current.Dispatcher;
            this.DataContext = new MainNotifViewModel(userLogin,dispatcher);
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Top = 0;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 5;
            this.GetScreenActive();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.notifToolView1.wnd = this;
        }


        public void GetScreenActive()
        {
            if (IsRight)
                this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            else
                this.Left = 0;
        }

        public void GetScreenDesactive()
        {
            if (IsRight)
                this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 2;
            else
            {
                this.Left =0;
                this.Left =2-this.Width;
            }
            
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.GetScreenDesactive();
        }

        private void MetroWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                this.GetScreenActive();
            }
            
        }

    }
}

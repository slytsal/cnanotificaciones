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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Configuration;
using Nosc.ViewModel;
using System.ComponentModel;

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para NotifLowerView.xaml
    /// </summary>
    public partial class NotifLowerView : UserControl
    {
        DispatcherTimer dTimerUploadProcess;
        Storyboard _ImgSync;

        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

        public NotifLowerView()
        {
            InitializeComponent();

            int SyncTimer = Int32.Parse(ConfigurationManager.AppSettings["SyncTimer"].ToString());
            this._ImgSync = (Storyboard)this.FindResource("rotateImg");

            DTimerUploadProcess = new DispatcherTimer();
            DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            
            DTimerUploadProcess.Interval = new TimeSpan(0, 0, SyncTimer);
            DTimerUploadProcess.Start();

        }

        private MainNotifViewModel GetViewModel()
        {
            return this.DataContext as MainNotifViewModel;
        }

        private void imgConfiguracion_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());
        }

        private void imgUser_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            //Sincronizacion de catalogos             
            if (!SyncViewModel.IsRunning)
            {
                Recurrence recurrencia = new Recurrence();
                System.Windows.Threading.Dispatcher dispatcher = Application.Current.Dispatcher;
                string v = ConfigurationManager.AppSettings["Assembly"].ToString();
                MainNotifViewModel mainNotifViewModel = this.GetViewModel();                 
                SyncViewModel svm = new SyncViewModel(dispatcher, mainNotifViewModel,recurrencia,v);
                svm.PropertyChanged += delegate(object sndr, PropertyChangedEventArgs args)
                {
                    if (args.PropertyName.ToLower() == "jobdone")
                    {
                        if (!((SyncViewModel)sndr).JobDone)
                        {
                            Action a = () => this.ShowImgSync();
                            this.Dispatcher.BeginInvoke(a);
                             
                        }
                        else
                        {
                            Action ab = () => this.HideImgSync();
                            this.Dispatcher.BeginInvoke(ab);                            
                        }
                    }

                    if (args.PropertyName.ToLower() == "message")
                    {
                        Action ac = () => this.SetImgSyncMsg(((SyncViewModel)sndr).Message);
                        this.Dispatcher.BeginInvoke(ac);                        
                    }
                };
                svm.start();
            }
        }

        public void ShowImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Visible;
            this.cnvTmpRot2.Visibility = Visibility.Collapsed;
            _ImgSync.Begin(this);
        }

        public void HideImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Collapsed;
            this.cnvTmpRot2.Visibility = Visibility.Visible;
        }

        public void SetImgSyncMsg(string msg)
        {
            this.imgSyncFiles.ToolTip = msg;
            this.imgSyncFiles2.ToolTip = msg;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());
        }
    }
}

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

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para SplashView.xaml
    /// </summary>
    public partial class SplashView : Window
    {
        public SplashView()
        {
            InitializeComponent();
        }

        private SplashViewModel GetViewModel()
        {
            return this.DataContext as SplashViewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Init();
        }

        public void Init()
        {
            //Sincronizacion 
            if (!SyncViewModel.IsRunning)
            {
                System.Windows.Threading.Dispatcher dispatcher = Application.Current.Dispatcher;
                ShowWindows showWindows = new ShowWindows();

                this.DataContext = new SplashViewModel(dispatcher, showWindows);
                SplashViewModel svm = this.GetViewModel();
                svm.start();
            }
        }
    }
}

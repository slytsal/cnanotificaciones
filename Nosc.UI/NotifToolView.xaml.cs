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

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para NotifToolView.xaml
    /// </summary>
    public partial class NotifToolView : UserControl
    {
        public MainNotif wnd;
        public NotifToolView()
        {
            InitializeComponent();
        }

        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void MinimizeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (wnd!=null)
            {
                    wnd.IsRight = !wnd.IsRight;
                    wnd.GetScreenDesactive();
            }
        }
    }
}

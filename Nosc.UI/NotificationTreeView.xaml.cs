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
using Nosc.ViewModel;
using System.ComponentModel;
using Nosc.Model;

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para NotificationTreeView.xaml
    /// </summary>
    public partial class NotificationTreeView : UserControl
    {
        public NotificationTreeView()
        {
            InitializeComponent();
        }

        private MainNotifViewModel GetViewModel()
        {
            return this.DataContext as MainNotifViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender != null)
            {
                MainNotifViewModel notificacionViewModel = this.GetViewModel();

                TreeView dg = sender as TreeView;
                NotificationViewModel notificacion = dg.SelectedItem as NotificationViewModel;

                if (notificacionViewModel != null && notificacion != null)
                {
                    notificacionViewModel.NotificationTreeView.IDesactiveNotification(notificacion.Notification);

                    notificacionViewModel.LoadInfo(1);
                }
            }
        }
    }
}

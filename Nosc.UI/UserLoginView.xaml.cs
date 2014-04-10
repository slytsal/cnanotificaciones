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
using System.ComponentModel;
using Nosc.ViewModel;
using MahApps.Metro.Controls;
using System.Diagnostics;

namespace Nosc.UI
{
    /// <summary>
    /// Lógica de interacción para UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : MetroWindow
    {
        private bool loaded;
        public UserLoginView()
        {
            this.loaded = false;
            InitializeComponent();

            this.Loaded += delegate
            {
                FocusManager.SetFocusedElement(this, this.tbUser);
                Keyboard.Focus(this.tbUser);
                this.tbUser.SelectAll();

            };
            
        }

        private UserLoginViewModel GetViewModel()
        {
            return this.DataContext as UserLoginViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.loaded)
            {
                if (!DesignerProperties.GetIsInDesignMode(this))
                {
                    UserLoginViewModel ulvm = new UserLoginViewModel();
                    if (ulvm.UserSet())
                    {
                        this.DataContext = ulvm;
                        this.LoginSuccess();
                    }

                    ulvm.PropertyChanged += delegate(object s, PropertyChangedEventArgs args)
                    {
                        if (args.PropertyName == "User")
                        {
                            if (ulvm.User != null && ulvm.User.IsActive)
                            {
                                if ((s as UserLoginViewModel).UserSet())
                                    this.LoginSuccess();
                            }
                        }
                    };
                    this.DataContext = ulvm;
                } 
            }
            this.loaded = true;
        }

        public void LoginSuccess()
        {
            (new MainNotif(this.GetViewModel())).Show();  
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Process.GetCurrentProcess().Kill();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Notepad_Mobile.ViewModels;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Notepad_Mobile
{
    public partial class UserRegistrationUC : UserControl
    {
        public UserRegistrationUC()
        {
            InitializeComponent();

            this.DataContext = NotepadViewModel.viewModel;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            NotepadViewModel.viewModel.LogIn(textBoxUserName.Text, passwordBox1.Password);
        }

        private void buttonNewUser_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame)
                .Navigate(new Uri("/RegistrationPage.xaml", UriKind.Relative));

            NotepadViewModel.viewModel.ErrorMsg = null;
        }
    }
}

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
using Microsoft.Phone.Controls;
using Notepad_Mobile.ViewModels;

namespace Notepad_Mobile
{
    public partial class RegistrationPage : PhoneApplicationPage
    {
        public RegistrationPage()
        {
            InitializeComponent();

            this.DataContext = NotepadViewModel.viewModel;
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBoxUserPass.Password == passwordBoxConfirm.Password)
            {
                NotepadViewModel.viewModel.AddUser(
                    textBoxUserName.Text,
                    passwordBoxUserPass.Password,
                    "User: " + textBoxUserName.Text.GetHashCode()
                    );
            }
            else
            {
                NotepadViewModel.viewModel.ErrorMsg = "Hasła się nie zgadzają!";
            }
        }
    }
}
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

namespace Notepad_Mobile.UserControls
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();

            this.DataContext = NotepadViewModel.viewModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NotepadViewModel.viewModel.
                SaveNote(tbxNoteTitle.Text, tbxNoteText.Text);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NotepadViewModel.viewModel.DeleteNote();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NotepadViewModel.viewModel.SelectedNote = null;
            NotepadViewModel.viewModel.ShowNoteList = false;
        }

        private void btnViewEdit_Click(object sender, RoutedEventArgs e)
        {
            if (NotepadViewModel.viewModel.Notes.Count > 0)
            {
                NotepadViewModel.viewModel.ShowNoteList = true;
            }
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            NotepadViewModel.viewModel.LogOut();
        }
    }
}

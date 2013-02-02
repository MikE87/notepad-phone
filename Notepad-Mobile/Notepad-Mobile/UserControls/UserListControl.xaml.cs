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
using Notepad_Mobile.NotepadServiceProxy;

namespace Notepad_Mobile
{
    public partial class NoteListControl : UserControl
    {
        public NoteListControl()
        {
            InitializeComponent();

            this.DataContext = NotepadViewModel.viewModel;
        }

        private void noteListBox_SelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            if(e.AddedItems != null && e.AddedItems.Count > 0)
            {
                NotepadViewModel.viewModel.SelectedNote =
                    (NoteDC)e.AddedItems[0];
                this.userListBox.SelectedItem = null;
                NotepadViewModel.viewModel.ShowNoteList = false;
            }
        }

        private void noteListBox_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

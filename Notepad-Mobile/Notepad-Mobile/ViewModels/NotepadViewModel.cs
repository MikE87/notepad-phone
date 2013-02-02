using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Notepad_Mobile;
using Notepad_Mobile.NotepadServiceProxy;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Notepad_Mobile.ViewModels
{
    public sealed class NotepadViewModel: INotifyPropertyChanged
    {
        public static NotepadViewModel viewModel = new NotepadViewModel();
        private NotepadClient _client = new NotepadClient();

        public NotepadViewModel()
        {
            this._client.AddUserCompleted += 
                new System.EventHandler<AddUserCompletedEventArgs>
                    (_client_AddUserCompleted);
            this._client.AddNoteCompleted += 
                new System.EventHandler<AddNoteCompletedEventArgs>
                    (_client_AddNoteCompleted);
            this._client.UpdateNoteCompleted += 
                new System.EventHandler<AsyncCompletedEventArgs>
                    (_client_UpdateNoteCompleted);
            this._client.GetUserNotesCompleted += 
                new System.EventHandler<GetUserNotesCompletedEventArgs>
                    (_client_GetUserNotesCompleted);
            this._client.DeleteNoteCompleted +=
                new System.EventHandler<AsyncCompletedEventArgs>
                    (_client_DeleteNoteCompleted);
            this._client.CheckUserCompleted += 
                new System.EventHandler<CheckUserCompletedEventArgs>
                    (_client_CheckUserCompleted);

            if (this.NeedUserID)
            {
                this.Notes = new ObservableCollection<NoteDC>();
            }
            else
            {
                this.ReloadData();
            }
        }

        #region Event Handling

        void _client_CheckUserCompleted(object sender, CheckUserCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                if (e.Result != null)
                {
                    this.UserID = e.Result.UserId;
                    this.UserPass = e.Result.Pass;
                    this.ReloadData();
                    this.ShowMenu = true;
                    this.ErrorMsg = null;   
                }
                else
                {
                    this.UserID = -1;
                    this.UserPass = null;
                    this.ErrorMsg = "Błędne dane logowania!";
                }
            }
            else
            {
                this.UserID = -1;
                this.UserPass = null;
                this.ErrorMsg = e.Error.Message;
            }
        }

        void _client_AddUserCompleted(object sender, 
            AddUserCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                this.UserID = e.Result.UserId;
                this.UserPass = e.Result.Pass;
                this.SelectedNote = null;
                this.Notes = null;
                this.ShowMenu = true;
                this.ErrorMsg = null;

                (Application.Current.RootVisual as PhoneApplicationFrame)
                    .GoBack();
            }
            else
            {
                this.UserID = -1;
                this.UserPass = null;
                this.SelectedNote = null;
                this.Notes = null;
                this.ErrorMsg = e.Error.Message;
            }
        }

        void _client_AddNoteCompleted(object sender, 
            AddNoteCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                this.SelectedNote = e.Result;
                this.ReloadData();
                this.ErrorMsg = null;
            }
            else
            {
                this.ErrorMsg = e.Error.Message;
            }
        }

        void _client_UpdateNoteCompleted(object sender,
            AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.ReloadData();
                this.ErrorMsg = null;
            }
            else
            {
                this.ErrorMsg = e.Error.Message;
            }
        }

        void _client_DeleteNoteCompleted(object sender,
            AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.SelectedNote = null;
                this.ReloadData();
                this.ErrorMsg = null;
                this.ShowNoteList = true;
            }
            else
            {
                this.ErrorMsg = e.Error.Message;
            }
        }

        void _client_GetUserNotesCompleted(object sender, 
            GetUserNotesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.Notes = e.Result;
                this.ErrorMsg = null;
            }
            else
            {
                this.ErrorMsg = e.Error.Message;
            }
        }

        public void ReloadData()
        {
            this._client.GetUserNotesAsync(this.UserID);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Powiadomienie o zmianie właściwości, 
        /// potrzebne do odświeżania widoku.
        /// </summary>
        /// <param name="PropertyName">Nazwa właściwości ulegającej zmianie</param>
        private void RaisePropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;

            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion // Event Handling

        #region Properties

        /// <summary>
        /// Właściwość przechowywująca wiadomości błędów.
        /// Wartość null oznacza brak błędów.
        /// </summary>
        private string _errorMsg = null;
        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                this._errorMsg = value;

                this.RaisePropertyChanged("ErrorMsg");
            }
        }

        /// <summary>
        /// Identyfikator użytkownika przechowywany w obszarze izolowanym
        /// urządzenia mobilnego - ustawieniach aplikacji.
        /// </summary>
        public int UserID
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("UserID"))
                {
                    return (int)IsolatedStorageSettings.ApplicationSettings["UserID"];
                }
                else 
                {
                    return -1; // brak użytkownika
                }
            }
            set
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("UserID"))
                {
                    IsolatedStorageSettings.ApplicationSettings["UserID"] = value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add("UserID", value);
                }

                this.RaisePropertyChanged("UserID");
                this.RaisePropertyChanged("NeedUserID");
            }
        }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        public string UserPass
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("UserPass"))
                {
                    return (string)IsolatedStorageSettings.ApplicationSettings["UserPass"];
                }
                else
                {
                    return null; // brak użytkownika
                }
            }
            set
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("UserPass"))
                {
                    IsolatedStorageSettings.ApplicationSettings["UserPass"] = value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add("UserPass", value);
                }

                this.RaisePropertyChanged("UserPass");
            }
        }

        /// <summary>
        /// Właściwość określająca czy konieczne jest stworzenie nowego
        /// konta użytkownika.
        /// </summary>
        public bool NeedUserID
        {
            get
            {
                if(!DesignerProperties.IsInDesignTool)
                {
                return !IsolatedStorageSettings
                    .ApplicationSettings.Contains("UserID") ||
                    (((int)IsolatedStorageSettings
                    .ApplicationSettings["UserID"]) == -1);
                }

                return true;
            }
        }

        /// <summary>
        /// Właściwość informująca o wyświetlaniu listy notatek.
        /// </summary>
        private bool _showNoteList;
        public bool ShowNoteList
        {
            get
            {
                return this._showNoteList;
            }
            set
            {
                this._showNoteList = value;
                this.ShowMenu = !value;
                this.RaisePropertyChanged("ShowNoteList");
            }
        }

        /// <summary>
        /// Właściwość określająca wyświetlanie menu
        /// </summary>
        private bool _showMenu;
        public bool ShowMenu
        {
            get 
            {
                return this._showMenu;
            }
            set 
            {
                this._showMenu = value;
                this.RaisePropertyChanged("ShowMenu");
            }
        }

        /// <summary>
        /// Wybrana notatka.
        /// </summary>
        private NoteDC _note;
        public NoteDC SelectedNote
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
                this.RaisePropertyChanged("SelectedNote");
            }
        }

        /// <summary>
        /// Wszystkie notatki danego użytkownika.
        /// </summary>
        private ObservableCollection<NoteDC> _notes;
        public ObservableCollection<NoteDC> Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
                this.RaisePropertyChanged("Notes");
            }
        }

        #endregion // Properties

        #region Methods

        public void LogIn(string userName, string userPass)
        {
            if (userName.Length > 3 && userPass.Length > 5)
            {
                this._client.CheckUserAsync(userName, userPass);
            }
            else
            {
                this.ErrorMsg = "Podaj poprawne dane!";
            }
        }

        public void LogOut()
        {
            this.UserID = -1;
            this.UserPass = null;
            this.ShowMenu = false;
            this.SelectedNote = null;
            this.Notes = null;
            this.ErrorMsg = null;
        }

        public void AddUser(string userName, string userPass, string userDesc)
        {
            if (this.NeedUserID && userName.Length > 3 && userPass.Length > 5)
            {
                this._client.AddUserAsync(userName, userPass, userDesc);
            }
            else if (userName.Length < 4)
            {
                this.ErrorMsg = "Nazwa użytkownika min 4 znaki.";
            }
            else if (userPass.Length < 6)
            {
                this.ErrorMsg = "Hasło min 6 znaków.";
            }
        }

        public void SaveNote(string noteDesc, string noteText)
        {
            if (noteDesc.Length > 2)
            {
                if (this.SelectedNote != null)
                {
                    var note = this.Notes.SingleOrDefault(
                    n => n.NoteId.Equals(this.SelectedNote.NoteId)
                    );

                    this._client.UpdateNoteAsync(note.NoteId, noteText);
                }
                else
                {
                    this._client.AddNoteAsync(this.UserID, noteDesc, noteText);
                }
            }
            else
            {
                this.ErrorMsg = "Opis notatki min 3 znaki.";
            }
        }

        public void DeleteNote()
        {
            if (this.SelectedNote != null)
            {
                this._client.DeleteNoteAsync(this.SelectedNote.NoteId);
            }
        }

        #endregion // Methods
    }
}

using System;
using System.Windows;
using System.Windows.Input;
using TaringCompare.Commands;
using TaringCompare.Models;

namespace TaringCompare.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private User _user;
        public ICommand LoginCommand { get; }

        public LoginVM()
        {
            _user = new User();
            LoginCommand = new RelayCommand(param => LoggedIn(param));
        }

        private void LoggedIn(object param)
        {
            MessageBox.Show($"Logged in successfully as {param}");
        }

        public string UserName
        {
            get { return _user.UserName; }
            set
            {
                _user.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}

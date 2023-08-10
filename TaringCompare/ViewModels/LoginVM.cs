using TaringCompare.Models;

namespace TaringCompare.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private User _user = new User();

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

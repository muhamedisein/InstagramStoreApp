using WinInstagram.InstagramAPI.Models;
using System.Threading.Tasks;
using WinInstagram.Common;
using WinInstagram.InstagramAPI.Endpoints;
using WinInstagram.InstagramAPI.OAuth;
using System;

namespace WinInstagram.ViewModel
{
    class UserViewModel : BindableBase
    {
        private User _user;
        public User User
        {
            get { return _user; }
            private set { SetProperty(ref _user, value); }
        }

        public UserViewModel(string userId = null)
        {
            User = new User() { Id = userId };
        }

        public async Task RaedData()
        {
            try
            {
                User = await UsersEndpoint.User(OAuthResponse.OAuthData, User.Id);

            }
            catch (Exception)
            {
                User = null;
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.Common;
namespace WinInstagram.ViewModel
{
    class UserCollectionVm : BindableBase
    {


        public UserCollectionVm()
        {
            Users = new ObservableCollection<UserInfo>();
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        private ObservableCollection<UserInfo> _users;
        public ObservableCollection<UserInfo> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public async Task ReadData(Func<Task<ObservableCollection<UserInfo>>> loadData)
        {
            try
            {
               Users = await loadData.Invoke();

            }
            catch (Exception)
            {
                Users = null;
            }
           
        }
    }
}

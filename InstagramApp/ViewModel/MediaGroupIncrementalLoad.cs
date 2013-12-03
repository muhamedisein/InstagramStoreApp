using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using WinInstagram.Common;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.InstagramAPI.Endpoints;
using System.Threading.Tasks;
using WinInstagram.Utilites;
using System.ComponentModel;
using WinInstagram.ViewModel.Helpers;
using Windows.UI.Xaml.Data;

namespace WinInstagram.ViewModel
{

    public class MediaGroupIncrementalLoad : BindableBase
    {

        private uint _maxData;
        private string _userId;
        private IncrementalLoadCollection<Media> _items;
        public IncrementalLoadCollection<Media> Items
        {
            get { return _items; }
            private set { SetProperty(ref _items, value); }
        }

        private string _groupTitle;
        public String GroupTitle
        {
            get { return _groupTitle; }
            set { SetProperty(ref _groupTitle, value); }
        }

        public MediaGroupIncrementalLoad(string groupTitle, string userId, uint maxData)
        {

            GroupTitle = groupTitle;
            _maxData = maxData;
            _userId = userId;
            LoadFunction();

        }

        private void LoadFunction()
        {
            Items = new IncrementalLoadCollection<Media>(_maxData, _userId, async (uid, pagination) =>
            {

                if (Items != null && Items.Count == 0)
                {
                    try
                    {
                        switch (GroupTitle)
                        {
                            case "Recent Media":
                                return await UsersEndpoint.MediaRecent(null, OAuthResponse.OAuthData, uid);
                            case "Home":
                                return await UsersEndpoint.UserFeed(null, OAuthResponse.OAuthData);
                            case "Popular":
                                return await MediaEndpoint.Popular(null, OAuthResponse.OAuthData);
                            default:
                                return null;
                        }
                    }
                    catch (Exception )
                    {
                        return null;
                    }

                }
                if (pagination != null && pagination.NextUrl != null)
                {
                    try
                    {

                        switch (GroupTitle)
                        {
                            case "Recent Media":
                                return await UsersEndpoint.MediaRecent(pagination.NextUrl);
                            case "Home":
                                return await UsersEndpoint.UserFeed(pagination.NextUrl);
                            default:
                                return null;
                        }

                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return null;


            });
        }

        public async Task RefreshData(uint count)
        {

            while (Items.IsBusy())
            {
                await Task.Delay(20);
            }
            Items.clearItems();
            await Task.Delay(10);
            //this is only for windows 8
            //in windows 8.1 this line caouses for the first 16 items to be duplicate
            //await Items.LoadMoreItemsAsync(16);
            OnPropertyChanged("Items");
        }




    }
}
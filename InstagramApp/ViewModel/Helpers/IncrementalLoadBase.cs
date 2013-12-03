
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Endpoints;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinInstagram.ViewModel.Helpers
{
    public abstract class IncrementalLoadingBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {

        public delegate void CollectionChangingHandler();

        #region ISupportIncrementalLoading

        public bool HasMoreItems
        {
            get { return HasMoreItemsOverride(); }
           
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_busy)
            {
              //  throw new InvalidOperationException("Only one operation in flight at a time");
            }

            _busy = true;

            return AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
        }


        public bool IsBusy()
        {
            return _busy == true;
        }

        #endregion

        #region INotifyCollectionChanged

        public new event NotifyCollectionChangedEventHandler CollectionChanged;

        public event CollectionChangingHandler CollectonChanging;

        #endregion

        #region Private methods

        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                NotiftyCollectionChanging();
                var items = await LoadMoreItemsOverrideAsync(c, count);
                var baseIndex = Count;

                if (items != null)
                {

                    foreach (var item in items)
                    {
                        Add(item);
                    }

                    NotifyOfInsertedItems(baseIndex, items.Count);
                    return new LoadMoreItemsResult {Count = (uint) items.Count};
                }
                return new LoadMoreItemsResult {Count = (uint) 0};
            }
            catch (Exception)
            {
                return new LoadMoreItemsResult { Count = (uint) 0 };
            }
            finally
            {
                _busy = false;
            }
        }

        void NotifyOfInsertedItems(int baseIndex, int count)
        {
            if (CollectionChanged == null || count == 0)
            {
                return;
            }

          /*  for (int i = 0; i < count; i++)
            {
              // var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, this[i + baseIndex], i + baseIndex);
                
                CollectionChanged(this, args);
            }*/
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,this[0],0);

            CollectionChanged(this, args);
        }

        void NotiftyCollectionChanging()
        {
            if (CollectonChanging != null)
            {
                CollectonChanging();
            }
        }


        #endregion

        #region Overridable methods

        protected abstract Task<ObservableCollection<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);
        protected abstract bool HasMoreItemsOverride();

        #endregion

        #region State

        bool _busy = false;

        #endregion
    }
}

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.Endpoints;

namespace WinInstagram.ViewModel.Helpers
{
    public class IncrementalLoadCollection<T> : IncrementalLoadingBase<T>
    {
        private uint DownloadedCount { get; set; }
        private uint MaxCount { get; set; }
        private Pagination Pagination { get; set; }
        private string Param { get; set; }
        private readonly Func<string, Pagination, Task<Envelope<ObservableCollection<T>>>> _load;

        public IncrementalLoadCollection(uint maxCount, string param, Func<string, Pagination, Task<Envelope<ObservableCollection<T>>>> load)
        {
            MaxCount = maxCount;
            DownloadedCount = 0;
            Param = param;
            _load = load;

        }

        public void clearItems()
        {
            DownloadedCount = 0;
            Pagination = null;
            Clear();
        }

        protected async override Task<ObservableCollection<T>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            try
            {
                Envelope<ObservableCollection<T>> result = await _load.Invoke(Param, Pagination);
                if (result != null)
                {

                    Pagination = result.Pagination;

                    DownloadedCount += (uint)result.Data.Count;
                    if (Pagination == null || Pagination.NextUrl == null)
                    {
                        MaxCount = DownloadedCount;
                    }
                    return result.Data;
                }
                MaxCount = DownloadedCount;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override bool HasMoreItemsOverride()
        {
            return DownloadedCount < MaxCount;
        }
    }
}

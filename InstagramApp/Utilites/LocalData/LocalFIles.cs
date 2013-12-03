using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WinInstagram.Utilites.LocalData
{
    /// <summary>
    /// Helper class for interaction with local files
    /// </summary>
    internal class LocalFIles
    {
        public static async Task<string> GetFileContent(string fileName)
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.GetFileAsync(fileName);

                string result = await FileIO.ReadTextAsync(storageFile);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static async Task<bool> SetFileContent<T>(string fileName, T data)
        {
            string content = await JsonConvert.SerializeObjectAsync(data);

            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile =
                    await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, content);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public static async Task checkACFolder()
        {
            var package = Windows.ApplicationModel.Package.Current;
            var installedLocation = package.InstalledLocation;
            var list = await installedLocation.GetFoldersAsync();
            if (list != null)
            {
               var x = list;
            }
            await ApplicationData.Current.ClearAsync();
  
        }
    }
}
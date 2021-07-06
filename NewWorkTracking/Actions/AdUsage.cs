using System;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Windows;

namespace NewWorkTracking.Actions
{
    /// <summary>
    /// Contains the logic for finding employees in the active directory 
    /// </summary>
    class AdUsage
    {
        public delegate void UserSearch(string m);
        /// <summary>
        /// Search query display event 
        /// </summary>
        public event UserSearch UserSearchEvent;

        /// <summary>
        /// User object Active Directors 
        /// </summary>
        private DirectoryEntry entry;
        /// <summary>
        /// Search result for Active Directory objects
        /// </summary>
        private DirectorySearcher searcher;

        public AdUsage()
        {
            entry = new DirectoryEntry("");
            searcher = new DirectorySearcher(entry);
        }

        /// <summary>
        /// Retrieves Active Directory objects 
        /// </summary>
        /// <param name="searchObject"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetAdUsers(string searchObject, string searchName)
        {
            UserSearchEvent?.Invoke($@"Поиск...");

            var userList = new ObservableCollection<string>();

            // Filtering by entered data
            searcher.Filter = $"(&(objectClass={searchObject})(Name=*{searchName}*))";

            try
            {
                foreach (SearchResult result in searcher.FindAll())
                {
                    var name = result.GetDirectoryEntry().Properties["DisplayName"].Value;

                    if (name != null)
                    {
                        Application.Current.Dispatcher.Invoke(() => userList.Add(name.ToString()));
                    }

                    UserSearchEvent?.Invoke($@"Найдено...{userList.Count}");
                }
            }
            catch (NotSupportedException e)
            {
                throw new NotSupportedException(e.Message);
            }

            userList = new ObservableCollection<string>(userList.OrderBy(x => x));

            UserSearchEvent?.Invoke($@"Найдено {userList.Count} совпадений. Выберите из списка.");

            return userList;
        }
    }
}

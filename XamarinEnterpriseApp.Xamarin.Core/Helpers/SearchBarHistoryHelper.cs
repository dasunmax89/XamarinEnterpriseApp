using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Models;
namespace XamarinEnterpriseApp.Xamarin.Core.Helpers
{
    public static class SearchBarHistoryHelper
    {
        private const int HistoryRecordLimit = 20;

        private static ILocalDbContextService _localDbContext;

        public static void AddOrUpdateSearchBarHistoryEntry(string searchText, string searchType)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    return;
                }

                GetLocalDbContext();

                // Check if there is already existing record for the search text
                SearchBarHistory result = GetSearchBarHistoryEntryByText(searchText);

                if (result == null)
                {

                    // Save the new search text to history
                    SaveSearchBarHistoryEntry(PopulateSearchBarHistoryObject(searchText, searchType));

                    // Remove oldest history records to keep the limit to 20 records
                    List<SearchBarHistory> historyEntries = GetAllSearchBarHistoryEntries();

                    if (historyEntries.Count > HistoryRecordLimit)
                    {
                        // Get the olderst record by search date
                        SearchBarHistory searchBarHistory = historyEntries.OrderBy(o => o.SearchDate).Take(1).FirstOrDefault();

                        DeleteSearchBarHistoryEntry(searchBarHistory);
                    }

                }
                else
                {
                    // Update search time incase if the same text searched
                    result.SearchDate = DateTime.Now;
                    SaveSearchBarHistoryEntry(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while creating a new search bar history entry", ex);

                Debug.WriteLine($"Exception occured while creating a new search bar history entry {0}", ex);
            }
        }

        public static List<SearchBarHistory> GetAllSearchBarHistoryEntries()
        {
            var historyEntries = new List<SearchBarHistory>();
            try
            {
                GetLocalDbContext();

                historyEntries = _localDbContext.GetEntityList<SearchBarHistory>().OrderByDescending(x => x.SearchDate).ToList();

            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while retrieving all search bar history entries", ex);
                Debug.WriteLine($"Exception occured while retrieving all search bar history entries {0}", ex);
            }

            return historyEntries;
        }

        public static void DeleteSearchBarHistoryEntry(SearchBarHistory historyEntry)
        {
            try
            {
                GetLocalDbContext();

                _localDbContext.DeleteEntity(historyEntry);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while deleting search bar history entry", ex);
                Debug.WriteLine($"Exception occured while deleting search bar history entry {0}", ex);
            }
        }

        public static void SaveSearchBarHistoryEntry(SearchBarHistory searchBarHistory)
        {
            try
            {
                GetLocalDbContext();

                _localDbContext.SaveEntity(searchBarHistory);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while saving the search bar history entry", ex);
                Debug.WriteLine($"Exception occured while saving the search bar history entry {0}", ex);
            }
        }

        private static void GetLocalDbContext()
        {
            if (_localDbContext == null)
            {
                _localDbContext = DependencyResolver.Resolve<ILocalDbContextService>();
            }
        }

        private static SearchBarHistory PopulateSearchBarHistoryObject(string searchText, string searchType)
        {
            // Create SearchBarHistoryEntry new record
            SearchBarHistory searchBarHistory = new SearchBarHistory();

            searchBarHistory.CreatedDate = DateTime.Now;
            searchBarHistory.SearchDate = DateTime.Now;
            searchBarHistory.SearchBarType = searchType;
            searchBarHistory.SearchText = searchText;

            return searchBarHistory;
        }

        private static SearchBarHistory GetSearchBarHistoryEntryByText(string searchText)
        {
            SearchBarHistory historyEntry = null;

            try
            {
                GetLocalDbContext();

                List<SearchBarHistory> historyEntries = _localDbContext.FilterEntity<SearchBarHistory>(e => e.SearchText.Equals(searchText));

                if (historyEntries != null && historyEntries.Any())
                {
                    historyEntry = historyEntries.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while retrieving search bar history by search text", ex);
                Debug.WriteLine($"Exception occured while retrieving search bar history by search text {0}", ex);
            }

            return historyEntry;
        }
    }
}

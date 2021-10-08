using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class PageHistoryState
    {
        private List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }

        public void AddPageToHistory(string PageName)
        {
            previousPages.Add(PageName);
        }

        public string GetGoBackPage()
        {
            if(previousPages.Count > 1)
            {
                // you add page on intialized() so you need to retrun the 2en from  the last.
                return previousPages.ElementAt(previousPages.Count - 2);
            }
            //cant go back couz you didn't navigate enough.
            return previousPages.FirstOrDefault();
        }

        public bool CanGoBack()
        {
            return previousPages.Count > 1 ;
        }
    }
}

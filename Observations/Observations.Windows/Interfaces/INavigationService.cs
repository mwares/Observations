using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observations.WindowsRT.Interfaces
{
    public interface INavigationService
    {
        bool CanGoBack
        {
            get;
        }

        Type CurrentPageType
        {
            get;
        }

        void GoBack();
        void GoForward();
        void GoHome();
        void Navigate(Type sourcePageType);
        void Navigate(Type sourcePageType, object parameter);
    }
}

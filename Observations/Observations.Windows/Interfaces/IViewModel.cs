using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Observations.WindowsRT.Interfaces
{
    public interface IViewModel
    {
        void OnNavigatedTo(NavigationEventArgs e);

        void OnNavigatedFrom(NavigationEventArgs e);
    }
}

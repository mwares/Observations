using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Observations.WindowsRT.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Observations.ViewModel;
using Observations.Entities;
using Observations.Parse;
using Observations.WindowsRT.DesignerViewModel;
//using Observations.WindowsRT.Temp.SampleData.SampleDataSource;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace Observations.WindowsRT.Views
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class PupilListView : LayoutAwarePage
    {
        public PupilListView()
        {
            this.InitializeComponent();

            //LoadData();
        }

        private async void LoadData()
        {
            //var viewModel = new PupilViewModel();
            //((PupilListDesignerViewModel)this.DataContext).
            PupilListDesignerViewModel viewModel = new PupilListDesignerViewModel();
            await viewModel.LoadData();
            this.DataContext = viewModel;
            //var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
            //((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if(navigationParameter != null)
            {
                ObservationViewModel observationViewModel = (ObservationViewModel)navigationParameter;
                foreach (var item in observationViewModel.Observation.Learners)
                {
                    
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreatePersonView));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreatePersonView), itemGridView.SelectedItem);
        }
    }
}
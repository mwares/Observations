using Observations.Entities;
using Observations.ViewModel;
using Observations.WindowsRT.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Observations.WindowsRT.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CreateObservationView : LayoutAwarePage
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        ObservationViewModel viewModel = new ObservationViewModel();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public CreateObservationView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

            if (e.NavigationParameter == null)
            {
                await viewModel.LoadDataAsync();

            }
            else
            {
                viewModel = (ObservationViewModel)e.NavigationParameter;
            }
            this.DataContext = viewModel;
            
            var collectionGroups = selectedLearners.View.CollectionGroups;
            ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;


        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            MeanuHeader lbi = (MeanuHeader)lb.SelectedItem;
            
            //TODO: Hide other controls

            switch (lbi.Header)
            {
                case "Learners":
                    //ctl_ObjectiveLearners.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
           
            
            Observation observation = new Observation();
            observation.ObservationDate = DateTime.Now.Date;
            observation.Notes = "Hello World";
            Learner l1 = new Learner();
            l1.Id = "T0lPrHk6DU";
            Learner l2 = new Learner();
            l2.Id = "VdMTaQGAoR";
            List<Learner> learners = new List<Learner>();
            learners.Add(l1);
            learners.Add(l2);
            observation.Learners = learners;
            await viewModel.Save(observation);
        }

        private void AddLearners_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PupilListView), viewModel);
        }
    }

    public class MeanuHeader
    {
        public string Header { get; set; }

        public MeanuHeader(string header)
        {
            Header = header;
        }
    }

    public class MenuHeaders : ObservableCollection<MeanuHeader>
    {
        public MenuHeaders()
        {
            Add(new MeanuHeader("Overview"));
            Add(new MeanuHeader("Learners"));
            Add(new MeanuHeader("Notes"));
            Add(new MeanuHeader("Objectives"));
            Add(new MeanuHeader("Photos"));
            Add(new MeanuHeader("Videos"));
            Add(new MeanuHeader("Audios"));
        }
    }
}

using Observations.WindowsRT.Common;
using System;
using System.Collections.Generic;
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
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Observations.ViewModel;
using Parse;
using Observations.Entities;
using Windows.Storage.Pickers;
using Observations.Parse;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Observations.WindowsRT.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CreatePersonView : LayoutAwarePage
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        StorageFile file;

        MainPage rootPage = MainPage.Current;

        PupilViewModel pupilsViewModel;

        public string ParseObjectId { get; set; }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public CreatePersonView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            pupilsViewModel = new PupilViewModel();
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
            if (e.NavigationParameter != null)
            {
                if (((Learner)e.NavigationParameter).Image.Url != null)
                {
                    BitmapImage image;
                    image = new BitmapImage(((Learner)e.NavigationParameter).Image.Url);
                    Photo.Source = image;
                }
                ParseObjectId = ((Learner)e.NavigationParameter).Id;
                Forename.Text = ((Learner)e.NavigationParameter).Forename;
                Surname.Text = ((Learner)e.NavigationParameter).Surname;
                DOB.Date = ((Learner)e.NavigationParameter).DateOfBirth;
            }
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

        private async void TakePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CameraCaptureUI dialog = new CameraCaptureUI();
                Size aspectRatio = new Size(7, 5);
                dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

                file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (file != null)
                {
                    DisplayPhoto(file);
                }
                else
                {
                    rootPage.NotifyUser("No photo captured.", NotifyType.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                rootPage.NotifyUser(ex.Message, NotifyType.ErrorMessage);
            }
        }

        private async void DisplayPhoto(StorageFile file)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                bitmapImage.SetSource(fileStream);
            }
            Photo.Source = bitmapImage;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            ImageToByteArrayConverter imageConverter = new ImageToByteArrayConverter();
            Learner pupil = new Learner();
            if (file != null)
            {
                ParseFile pf = new ParseFile(file.Name, await imageConverter.GetByteArray(file));
                pupil.Image = pf;
            }
            pupil.Id = ParseObjectId;
            pupil.Forename = Forename.Text;
            pupil.Surname = Surname.Text;
            pupil.DateOfBirth = DOB.Date.Date;
            await pupilsViewModel.SavePupil(pupil);

            if (this.Frame != null)
            {
                if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
            }
        }

        private async void AddImage_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Launch file open picker and caller app is suspended and may be terminated if required
            file = await openPicker.PickSingleFileAsync();
            if(file != null)
                DisplayPhoto(file);
        }

        private async void Photo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}

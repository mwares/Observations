using Observations.Entities;
using Observations.Parse;
using Observations.ViewModel;
using Observations.WindowsRT.Views;
using Parse;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Observations.WindowsRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int count = 1;
        public static MainPage Current;

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            //MyButton.Click += delegate { MyButton.Content = string.Format(Observations.App.GetMainPageLabel(), count++); };

        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            UserManagement p = new UserManagement();

            var user = new ParseUser()
            {
                Username = "mwares",
                Password = "password",
                Email = "markrwares@gmail.com"
            };

            p.CreateUser(user);
        }

        private async void LoginUser_Click(object sender, RoutedEventArgs e)
        {
            UserManagement um = new UserManagement();
            ParseLogin pl = await um.CheckUserDetails("mwares", "password");

        }

        private async void LoginUserFailure_Click(object sender, RoutedEventArgs e)
        {
            UserManagement um = new UserManagement();
            ParseLogin pl = await um.CheckUserDetails("mwares", "password123");
        }

        private async void GetUser_Click(object sender, RoutedEventArgs e)
        {
            PupilViewModel pm = new PupilViewModel();
            Learner p = await pm.GetPupil("TYfQ7Q9qUz");
        }

        private void AddPupil_Click(object sender, RoutedEventArgs e)
        {
            Learner p = new Learner();
            p.Id = Id.Text;
            p.Forename = Forename.Text;
            p.Surname = Surname.Text;
            p.DateOfBirth = new DateTime(DOB.Date.Year, DOB.Date.Month, DOB.Date.Day);
            PupilViewModel pm = new PupilViewModel();
            pm.SavePupil(p);
        }

        private void DeletePupil_Click(object sender, RoutedEventArgs e)
        {
            Learner pupil = (Learner)ListOfPupils.SelectedItem;
            PupilViewModel pm = new PupilViewModel();
            pm.DeletePupil(pupil);
        }

        private async void GetAllPupil_Click(object sender, RoutedEventArgs e)
        {
            PupilViewModel pm = new PupilViewModel();
            List<Learner> pupils = await pm.GetAllPupilsByClass("xxx");

            ListOfPupils.ItemsSource = pupils;
            ListOfPupils.DisplayMemberPath = "Forename";
            //ListOfPupils.f
        }

        private void ListOfPupils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Learner p = (Learner)ListOfPupils.SelectedItem;
            if (p != null)
            {
                Id.Text = p.Id;
                Forename.Text = p.Forename;
                Surname.Text = p.Surname;
                DOB.Date = p.DateOfBirth;
            }
        }

        private void ShowPupils_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PupilListView));
        }

        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                // Use the status message style.
                case NotifyType.StatusMessage:
                    //StatusBlock.Style = Resources["StatusStyle"] as Style;
                    break;
                // Use the error message style.
                case NotifyType.ErrorMessage:
                    //StatusBlock.Style = Resources["ErrorStyle"] as Style;
                    break;
            }
            //StatusBlock.Text = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            //if (StatusBlock.Text != String.Empty)
            //{
            //    StatusBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //}
            //else
            //{
            //    StatusBlock.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //}
        }

        private async void GetObjectivesGrouped_Click(object sender, RoutedEventArgs e)
        {
            //ObjectiveViewModel ovm = new ObjectiveViewModel();
            //List<ObjectivesGrouped> obs = await ovm.GetAllObjectiviesGrouped();
            this.Frame.Navigate(typeof(ObjectiveCategoryView));
        }

        private void AddObservation_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateObservationView));
        }

    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
}

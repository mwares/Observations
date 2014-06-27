using Observations.Entities;
using Observations.Parse;
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
        public MainPage()
        {
            this.InitializeComponent();
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
            PupilManagement pm = new PupilManagement();
            Pupil p = await pm.GetPupil("TYfQ7Q9qUz");
        }

        private void AddPupil_Click(object sender, RoutedEventArgs e)
        {
            Pupil p = new Pupil();
            p.Id = Id.Text;
            p.Forename = Forename.Text;
            p.Surname = Surname.Text;
            p.DateOfBirth = new DateTime(DOB.Date.Year, DOB.Date.Month, DOB.Date.Day);
            PupilManagement pm = new PupilManagement();
            pm.SavePupil(p);
         }

        private void DeletePupil_Click(object sender, RoutedEventArgs e)
        {
            Pupil pupil = (Pupil)ListOfPupils.SelectedItem;
            PupilManagement pm = new PupilManagement();
            pm.DeletePupil(pupil);
        }

        private async void GetAllPupil_Click(object sender, RoutedEventArgs e)
        {
            PupilManagement pm = new PupilManagement();
            List<Pupil> pupils = await pm.GetAllPupilsByClass("xxx");

            ListOfPupils.ItemsSource = pupils;
            ListOfPupils.DisplayMemberPath = "Forename";
            //ListOfPupils.f
        }

        private void ListOfPupils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pupil p = (Pupil)ListOfPupils.SelectedItem;
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


    }
}

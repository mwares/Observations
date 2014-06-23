using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;



namespace Observations.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        int count = 1;
        public MainPage()
        {
            InitializeComponent();

            //MyButton.Click += delegate { MyButton.Content = string.Format(Observations.App.GetMainPageLabel(), count++); };

            //Content = Observations.App.GetMainPage().ConvertPageToUIElement(this);
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Observations.Parse.UserManagement p = new Parse.UserManagement();

            var user = new ParseUser()
            {
                Username = "mwaresWinPhone",
                Password = "password",
                Email = "markrwaresWinPhone@gmail.com"
            };

            p.CreateUser(user);
        }
    }
}

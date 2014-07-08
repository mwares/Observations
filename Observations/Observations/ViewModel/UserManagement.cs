using System;
using System.Collections.Generic;
using System.Text;
using Parse;
using System.Threading.Tasks;

namespace Observations.Parse
{
    public class UserManagement
    {
        public async void CreateUser(ParseUser user)
        {
            try
            {
                await user.SignUpAsync();
            }
            catch (ParseException ex)
            {
                throw;
            }
        }

        public async Task<ParseLogin> CheckUserDetails(string username, string password)
        {
            ParseLogin pl = new ParseLogin();
            try
            {
                await ParseUser.LogInAsync(username, password);
                pl.LoginSuccessful = true;
                return pl;
            }
            catch (ParseException ex)
            {
                pl.LoginSuccessful = false;
                pl.loginFailureMessage = ex.Message;
                return pl;
            }
        }
    }

    public class ParseLogin
    {
        public bool LoginSuccessful { get; set; }

        public string loginFailureMessage { get; set; }
    }
}

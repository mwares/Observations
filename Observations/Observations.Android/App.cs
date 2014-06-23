using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace ParseAndroidStarterProject
{
    [Application]
    public class App : Application
    {
        public App(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Initialize the parse client with your Application ID and .NET Key found on
            // your Parse dashboard
            ParseClient.Initialize("DS9IBWUmKJHmMfd0ehzfMkjRIled7zvmgRFWMYrJ",
                                   "hX0olhR8ZIpBgTvVbBpZmRBG68wgkO68DDvtEOew");
        }
    }
}
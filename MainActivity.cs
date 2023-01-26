using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Ads;

namespace KalkulatorMasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        Button blachaButton, profilButton, ruraButton,kontownikButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            blachaButton = FindViewById<Button>(KalkulatorMasy.Resource.Id.blachaButton);
            profilButton = FindViewById<Button>(KalkulatorMasy.Resource.Id.profilButton);
            ruraButton = FindViewById<Button>(KalkulatorMasy.Resource.Id.ruraButton);
            kontownikButton = FindViewById<Button>(KalkulatorMasy.Resource.Id.katownikButton);
            blachaButton.Click += BlachaButton_Click;
            profilButton.Click += ProfilButton_Click;
            ruraButton.Click += RuraButton_Click;
            kontownikButton.Click += KontownikButton_Click;
            var id = "ca-app-pub-2646013949580292~3858727808";
            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, id);
            var adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
        }

        private void KontownikButton_Click(object sender, System.EventArgs e)
        {
            var intend = new Intent(this, typeof(Kontownik));
            StartActivity(intend);
        }

        private void RuraButton_Click(object sender, System.EventArgs e)
        {
            var intend = new Intent(this, typeof(RuraActivity));
            StartActivity(intend);
        }

        private void ProfilButton_Click(object sender, System.EventArgs e)
        {
            var intend = new Intent(this, typeof(ProfilActivity));
            StartActivity(intend);
        }

        private void BlachaButton_Click(object sender, System.EventArgs e)
        {
            var intend = new Intent(this, typeof(BlachaActivity));
            StartActivity(intend);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Gms.Ads;

namespace KalkulatorMasy
{
    [Activity(Label = "RuraActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class RuraActivity : Activity
    {
        decimal gestoscMetalu = 7.85M;

        Button backButton, liczButton;
        RadioGroup gestoscMetaluRadioGroup;
        TextView szerokoscTextView, dlugoscTextView, gruboscTextView, wynikTextView;
        NumberFormatInfo myInv = CultureInfo.CurrentCulture.NumberFormat;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Rura);

            // Create your application here
            backButton = FindViewById<Button>(Resource.Id.backButton);
            liczButton = FindViewById<Button>(Resource.Id.liczButton);
            szerokoscTextView = FindViewById<TextView>(Resource.Id.szerokoscTextField);
            dlugoscTextView = FindViewById<TextView>(Resource.Id.dlugoscTextField);
            gruboscTextView = FindViewById<TextView>(Resource.Id.gruboscTextField);
            wynikTextView = FindViewById<TextView>(Resource.Id.wynikLabel);
            gestoscMetaluRadioGroup = FindViewById<RadioGroup>(Resource.Id.materialRadioGroup);

            backButton.Click += BackButton_Click;
            gestoscMetaluRadioGroup.CheckedChange += GestoscMetaluRadioGroup_CheckedChange;
            liczButton.Click += LiczButton_Click;
            var id = "ca-app-pub-2646013949580292~3858727808";
            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, id);
            var adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
        }
        private void LiczButton_Click(object sender, EventArgs e)
        {
            decimal x = 0;
            int y = 0;
            decimal z = 0M;
            decimal xm2 = 0;
            decimal ym2 = 0;
            string gruboscBlachyDoZmiany = gruboscTextView.Text.Replace(".", myInv.NumberDecimalSeparator);
            string srednicaRuryDoZmiany = szerokoscTextView.Text.Replace(".", myInv.NumberDecimalSeparator);
            if (Decimal.TryParse(srednicaRuryDoZmiany, out x) && x > 0)
            {
                xm2 = (decimal)x / 1000;

            }
            else
            {
                Toast.MakeText(this, "Błędna wartość średnicy", ToastLength.Long).Show();
                return;
            }
            if (Int32.TryParse(dlugoscTextView.Text, out y) && y > 0)
            {
                ym2 = (decimal)y / 1000;

            }
            else
            {
                Toast.MakeText(this, "Błędna wartość Długosci", ToastLength.Long).Show();
                return;
            }
            if (Decimal.TryParse(gruboscBlachyDoZmiany, out z) && z > 0)
            {


            }
            else
            {
                Toast.MakeText(this, "Błędna wartość Grubości", ToastLength.Long).Show();
                return;
            }

            xm2 -= (z/1000);
            decimal obwodKola = (xm2 / 2) * 3.14M * 2M;


            decimal MasaArkusza = (decimal)obwodKola * ym2 * z * gestoscMetalu;
            wynikTextView.Text = MasaArkusza.ToString("0.00") + "kg";
            wynikTextView.SetTextColor(Color.Orange);



        }

        /// <summary>
        /// Zmiana rodzaju materialu EVENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestoscMetaluRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var wartosc = FindViewById<RadioButton>(gestoscMetaluRadioGroup.CheckedRadioButtonId).Text;
            if (wartosc == "Stal")
            {
                gestoscMetalu = 7.85M;
            }
            else if (wartosc == "Inox")
            {
                gestoscMetalu = 8M;
            }
            else if (wartosc == "Aluminium")
            {
                gestoscMetalu = 2.7M;
            }
        }
        /// <summary>
        /// Przycisk Powrotu Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            var intend = new Intent(this, typeof(MainActivity));
            StartActivity(intend);
        }

    }
}
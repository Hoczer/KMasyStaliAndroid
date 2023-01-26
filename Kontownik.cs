using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.PM;
using Android.Gms.Ads;

namespace KalkulatorMasy
{
    [Activity(Label = "Kontownik", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Kontownik : Activity
    {
        decimal gestoscMetalu = 7.8M;
        Button backButton, liczButton;
        RadioGroup gestoscMetaluRadioGroup;
        TextView szerokoscTextView, dlugoscTextView, gruboscTextView, wynikTextView, wysokoscTextView;
        NumberFormatInfo myInv = CultureInfo.CurrentCulture.NumberFormat;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Katownik);
            backButton = FindViewById<Button>(Resource.Id.backButton);
            liczButton = FindViewById<Button>(Resource.Id.liczButton);
            szerokoscTextView = FindViewById<TextView>(Resource.Id.szerokoscTextField);
            dlugoscTextView = FindViewById<TextView>(Resource.Id.dlugoscTextField);
            gruboscTextView = FindViewById<TextView>(Resource.Id.gruboscTextField);
            wysokoscTextView = FindViewById<TextView>(Resource.Id.wysokoscTextField);
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

            // Create your application here
        }

        private void LiczButton_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            decimal z = 0M;
            int a = 0;
            decimal xm2 = 0;
            decimal ym2 = 0;
            decimal am2 = 0;
            string gruboscBlachyDoZmiany = gruboscTextView.Text.Replace(".", myInv.NumberDecimalSeparator);
            if (Int32.TryParse(szerokoscTextView.Text, out x) && x > 0)
            {
                xm2 = (decimal)x / 1000;

            }
            else
            {
                Toast.MakeText(this, "Błędna wartość Szerokości", ToastLength.Long).Show();
                return;
            }
            if (Int32.TryParse(wysokoscTextView.Text, out a) && a > 0)
            {
                am2 = (decimal)a / 1000;

            }
            else
            {
                Toast.MakeText(this, "Błędna wartość wysokosci", ToastLength.Long).Show();
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

            decimal odjeteGrubosci = ((z * 2 * 3.14M) / 4) / 1000;
            decimal obwodProfila = xm2 + am2;
            obwodProfila -= odjeteGrubosci;



            decimal MasaArkusza = (decimal)obwodProfila * ym2 * z * gestoscMetalu;
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
                gestoscMetalu = 7.8M;
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
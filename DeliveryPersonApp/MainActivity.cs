using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using DeliveryApp1.Model;
using Android.Content;
using DeliveryApp1;
using System;

namespace DeliveryPersonApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText emailEditText, passwordEditText;
        Button signinButton, registerButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passswordEditText);
            signinButton = FindViewById<Button>(Resource.Id.signInButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            signinButton.Click += SigninButton_Click;
            registerButton.Click += RegisterButton_Click;

        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            intent.PutExtra("email", emailEditText.Text);
            StartActivity(intent);
        }

        private async void SigninButton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text;
            var password = passwordEditText.Text;

            var userId = await DeliveryPerson.Login(email, password);
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    Toast.MakeText(this, "Login Successful, Welcome!!", ToastLength.Long).Show();
                    var intent = new Intent(this, typeof(TabsActivity));
                    intent.PutExtra("userId", userId);
                    //var intent = new Intent(this, typeof(NewDeliveryActivity));
                    StartActivity(intent);

                    //Prevent from moving back to main_activity page(login page) after sign in
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Couldnot login, try again", ToastLength.Long).Show();
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, "Login Credintal not correct, try again", ToastLength.Long).Show();
            }
        }
    }
}
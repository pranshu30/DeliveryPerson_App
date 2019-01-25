using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveryApp1.Model;

namespace DeliveryPersonApp
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText emailEditText, passwordEditText, confirmpasswordEditText;
        Button registerButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            emailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            confirmpasswordEditText = FindViewById<EditText>(Resource.Id.ConfirmPasswordEditText);


            registerButton = FindViewById<Button>(Resource.Id.registerUserbutton);

            registerButton.Click += RegisterButton_Click;

            //Take the value of email from the main activity
            string email = Intent.GetStringExtra("email");
            emailEditText.Text = email;
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            var result = await DeliveryPerson.Register(emailEditText.Text, passwordEditText.Text, confirmpasswordEditText.Text);

            if (result)
            {
                Toast.MakeText(this, "Success", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Could not Register, Try Again", ToastLength.Long).Show();
            }
        }
    }
}
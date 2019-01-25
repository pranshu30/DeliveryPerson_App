using Android.App;
using System.Linq;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using DeliveryApp1.Model;

namespace DeliveryApp1.Andriod
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        EditText emailEditText, passwordEditText;
        Button signinbutton, registerbutton;
        Button tabs;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById <EditText>(Resource.Id.passwordEditText);

            signinbutton = FindViewById<Button>(Resource.Id.signinbutton);
            registerbutton = FindViewById<Button>(Resource.Id.registerbutton);

            tabs = FindViewById<Button>(Resource.Id.tabs);
            tabs.Click += Tabs_Click;


            signinbutton.Click += Signinbutton_Click;
            registerbutton.Click += Registerbutton_Click;
        }

        private void Tabs_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(TabsActivity));
            StartActivity(intent);
        }

        private async void Signinbutton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text;
            var password = passwordEditText.Text;

            var result = await User.Login(email, password);

            /*
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Email and Password cannot be empty", ToastLength.Long).Show();
            }
            else
            {
                var user = (await MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
                if (user.Password == password)
                {
                    Toast.MakeText(this, "Login Successful, Welcome!!", ToastLength.Long).Show();
                    var intent = new Intent(this, typeof(TabsActivity));
                    StartActivity(intent);

                    //Prevent from moving back to main_activity page(login page) after sign in
                    Finish();
                }

                else
                    Toast.MakeText(this, "Incorrect Password", ToastLength.Long).Show();


            }
            */

            if (result)
            {
                Toast.MakeText(this, "Login Successful, Welcome!!", ToastLength.Long).Show();
                var intent = new Intent(this, typeof(TabsActivity));
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

        private void Registerbutton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            intent.PutExtra("email", emailEditText.Text);
            StartActivity(intent);

        }


        /*
        nameEditText = FindViewById<EditText>(Resource.Id.editText1);
        helloButton = FindViewById<Button>(Resource.Id.hellobutton);

        helloButton.Click += HelloButton_Click;

    }

    private void HelloButton_Click(object sender, System.EventArgs e)
    {
        Toast.MakeText(this, $"Hello {nameEditText.Text}", ToastLength.Long).Show();
    }
    */
    }
}
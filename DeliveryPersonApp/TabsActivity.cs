using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace DeliveryPersonApp
{
    [Activity(Label = "TabsActivity")]
    public class TabsActivity : Android.Support.V4.App.FragmentActivity
    {
        TabLayout tabLayout;
        public string userId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Tabs);
            userId = Intent.GetStringExtra("userId");
             
            tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            tabLayout.TabSelected += TabLayout_TabSelected;

            TabNavigate(new DeliveringFragment());


        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            switch (e.Tab.Position)
            {
                case 0: TabNavigate(new DeliveringFragment());
                    break;
                case 1:
                    TabNavigate(new WaitingFragment());
                    break;

                case 2:
                    TabNavigate(new DeliveredFragment());
                    break;
            }
        }

        private void TabNavigate(Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}
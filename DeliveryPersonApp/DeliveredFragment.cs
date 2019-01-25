using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DeliveryApp1.Model;

namespace DeliveryPersonApp
{
    public class DeliveredFragment : global::Android.Support.V4.App.ListFragment
    {
        List<Delivery> deliveries;
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            deliveries = new List<Delivery>();


            var userId = (Activity as TabsActivity).userId;

            deliveries = await Delivery.GetDelivered(userId);
            ListAdapter = new ArrayAdapter(Activity, global::Android.Resource.Layout.SimpleListItem1, deliveries);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}
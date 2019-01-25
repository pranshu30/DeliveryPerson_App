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
    public class WaitingFragment : global::Android.Support.V4.App.ListFragment
    {
        List<Delivery> deliveries;
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            deliveries = new List<Delivery>();


            var userId = (Activity as TabsActivity).userId;

            deliveries = await Delivery.GetWaiting(userId);
            ListAdapter = new ArrayAdapter(Activity, global::Android.Resource.Layout.SimpleListItem1, deliveries);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            var selecteddelivery = deliveries[position];
            var userId = (Activity as TabsActivity).userId;

            Intent intent = new Intent(Activity, typeof(PickupActivity));
            intent.PutExtra("latitude", selecteddelivery.OriginLatitude);
            intent.PutExtra("longitude", selecteddelivery.OriginLongitude);
            intent.PutExtra("userId", userId);
            intent.PutExtra("deliveryId", selecteddelivery.Id);
            StartActivity(intent);
        }
    }
}
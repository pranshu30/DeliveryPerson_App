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

namespace DeliveryApp1.Andriod
{
    public class DeliveredFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            var delivered = await Delivery.GetDelivered();
            //ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, delivered);
            ListAdapter = new DeliveryAdapter(Activity,delivered);
        }
        /*
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            return inflater.Inflate(Resource.Layout.Delivered, container, false);         
        }
        */
    }
}
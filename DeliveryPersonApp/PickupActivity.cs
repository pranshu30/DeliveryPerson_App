using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DeliveryApp1.Model;

namespace DeliveryPersonApp
{
    [Activity(Label = "PickupActivity")]
    public class PickupActivity : Activity, IOnMapReadyCallback
    {
        MapFragment mapFragment;
        Button button;

        double lat, lng;
        string deliveryId,userId;

        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(lat, lng));
            marker.SetTitle("Pickup here");
            googleMap.AddMarker(marker);

            //Centering the map
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(lat, lng), 10));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Pickup);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickupMapFragment);
            button = FindViewById<Button>(Resource.Id.pickUpbutton);
            button.Click += Button_Click;
            deliveryId = Intent.GetStringExtra("deliveryId");
            userId = Intent.GetStringExtra("userId");
               
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                lat = Intent.GetDoubleExtra("latitude", 0);
                lng = Intent.GetDoubleExtra("longitude", 0);
                mapFragment.GetMapAsync(this);
            }
            else
            {
                Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            }

       }

        private async void Button_Click(object sender, EventArgs e)
        {
           await Delivery.MarkAsPickedUp(deliveryId,userId);
        }
    }
}
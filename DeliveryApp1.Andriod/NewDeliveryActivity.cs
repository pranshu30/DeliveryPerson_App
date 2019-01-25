using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Compat;
using Android.Support.V4.Content;
using Android.Widget;
using DeliveryApp1.Model;
using Plugin.Permissions;

namespace DeliveryApp1.Andriod
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity,IOnMapReadyCallback, ILocationListener
    {
        Button saveButton;
        EditText packageNameEditText;
        MapFragment mapFragment, destinationMapFragment;
        double latitude, longitude;
        LocationManager locationManager;

       
        public void OnLocationChanged(Location location)
        {
            latitude = location.Latitude;
            longitude = location.Longitude;
            mapFragment.GetMapAsync(this);
            destinationMapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(latitude, longitude));
            marker.SetTitle("Your Location");
            googleMap.AddMarker(marker);

            //Centering the map
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(latitude, longitude), 10));
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDelivery);

            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);
            destinationMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.destinationmapFragment);
            saveButton.Click += SaveButton_Click;

           
        }

        protected override void OnResume()
        {
            base.OnResume();
            // mapFragment.GetMapAsync(this); //we donot await this beacuse this java object not C# object
            locationManager = GetSystemService(Context.LocationService) as LocationManager;
            string provider = LocationManager.GpsProvider;

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {

                if (locationManager.IsProviderEnabled(provider))
                {
                    locationManager.RequestLocationUpdates(provider, 5000, 1, this);
                }

                // var location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                var location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
                latitude = location.Latitude;
                longitude = location.Longitude;
                mapFragment.GetMapAsync(this);
                destinationMapFragment.GetMapAsync(this);

            }
            else
            {
                Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            }
            
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            
#pragma warning disable CS0618 // Type or member is obsolete
            LatLng originLocation = mapFragment.Map.CameraPosition.Target;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            LatLng destinationLocation = destinationMapFragment.Map.CameraPosition.Target;
#pragma warning restore CS0618 // Type or member is obsolete
            Delivery delivery = new Delivery()
            {
                Name = packageNameEditText.Text,
                Status = 0,
                OriginLatitude = originLocation.Latitude,
                OriginLongitude = originLocation.Longitude,
                DestinationLatitude = destinationLocation.Latitude,
                DestinationLongitude = destinationLocation.Longitude
            };

            await Delivery.InsertDelivery(delivery);
            Toast.MakeText(this, "Success", ToastLength.Long).Show();
        }
    }
}
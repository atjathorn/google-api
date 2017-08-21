using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace App3
{
    [Activity(Label = "googleapi", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        private static string urlAddress = "http://192.168.175.2/test/select.php";
        private ListView lv;
        
        GoogleMap _map;
        protected override void OnCreate(Bundle bundel)
        {
            base.OnCreate(bundel);
            SetContentView(Resource.Layout.Main);

            lv = FindViewById<ListView>(Resource.Id.lv);
            Button btnTerrain = FindViewById<Button>(Resource.Id.btnTerrain);
            Button btnSatellite = FindViewById<Button>(Resource.Id.btnSatellite);
            Button btnNormal = FindViewById<Button>(Resource.Id.btnNormal);
            Button btnHybrid = FindViewById<Button>(Resource.Id.btnHybrid);
            Button show = FindViewById<Button>(Resource.Id.btn);

            show.Click += Show_Click;

            btnNormal.Click += delegate
            {
                _map.MapType = GoogleMap.MapTypeNormal;
            };
            btnTerrain.Click += delegate
            {
                _map.MapType = GoogleMap.MapTypeTerrain;
            };
            btnSatellite.Click += delegate
            {
                _map.MapType = GoogleMap.MapTypeSatellite;
            };
            btnHybrid.Click += delegate
            {
                _map.MapType = GoogleMap.MapTypeHybrid;
            };

            showmap();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            new Downloader(this, urlAddress, lv).Execute();
        }

        private void showmap()
        {
            if (_map == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap map)
        {
           // double lat=DataParser.lat, lng= 100.54290;
            _map = map;
            LatLng l1 = new LatLng(13.73167, 100.54290);
            //LatLng l2 = new LatLng(lat,lng);
           

            MarkerOptions m1 = new MarkerOptions();
            m1.SetPosition(l1);
            m1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));
            m1.SetTitle("suanlum");
            m1.SetSnippet("suansatarana");
            _map.AddMarker(m1);
            /*
            MarkerOptions m2 = new MarkerOptions();
            m2.SetPosition(l2);
            m2.SetTitle("suanlum1");
            m2.SetSnippet("suansatarana1");
            _map.AddMarker(m2);
            */
            CameraUpdate c = CameraUpdateFactory.NewLatLngZoom(l1, 10);
            _map.MoveCamera(c);
            //_map.MyLocationEnabled = true;
            _map.UiSettings.ZoomControlsEnabled = true;
            _map.UiSettings.CompassEnabled = true;
        }


       
}
}


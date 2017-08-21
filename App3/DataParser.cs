using System;
using Org.Json;
using Object = Java.Lang.Object;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace App3
{

    class DataParser : AsyncTask
    {
        private Context c;
        private string jsonData;
        private ListView lv;
        string lat;
        private ProgressDialog pd;
        private JavaList<string> spacecrafts;

        public DataParser(Context c, string jsonData, ListView lv)
        {
            this.c = c;
            this.jsonData = jsonData;
            this.lv = lv;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            pd = new ProgressDialog(c);
            pd.SetTitle("Parse Data");
            pd.SetMessage("Parsing..Please wait");
            pd.Show();
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return ParseData();
        }

        protected override void OnPostExecute(Object isParsed)
        {
            base.OnPostExecute(isParsed);

            pd.Dismiss();

            if ((bool)isParsed)
            {
                //BIND TO LISTVIEW
                lv.Adapter = new ArrayAdapter(c, Android.Resource.Layout.SimpleListItem1, spacecrafts);
                lv.ItemClick += lv_ItemClick;
            }
            else
            {
                Toast.MakeText(c, "Unable To Parse", ToastLength.Short).Show();
            }

        }

        void lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(c, spacecrafts[e.Position], ToastLength.Short).Show();
        }

        public Boolean ParseData()
        {
            try
            {
                JSONArray ja = new JSONArray(jsonData);
                JSONObject jo;

                spacecrafts = new JavaList<string>();

                for (int i = 0; i < ja.Length(); i++)
                {
                    jo = ja.GetJSONObject(i);

                    int id = jo.GetInt("id");
                    string name = jo.GetString("name");
                    string pass = jo.GetString("pass");
                    spacecrafts.Add(name + "_" + pass);
                    name = lat;

                }

                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return false;

        }
    }

}
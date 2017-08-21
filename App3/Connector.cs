using System;
using Java.Net;

namespace App3
{

    class Connector
    {
        public static HttpURLConnection connect(string urlAddress)
        {
            try
            {
                URL url = new URL(urlAddress);
                HttpURLConnection con = (HttpURLConnection)url.OpenConnection();

                //SET PROPERTIES
                con.RequestMethod = "GET";
                con.ConnectTimeout = 15000;
                con.ReadTimeout = 15000;
                con.DoInput = true;

                return con;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }
    }


}
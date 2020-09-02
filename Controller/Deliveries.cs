using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Model;

namespace HatidPress
{
    public class Deliveries
    {
        #region Fields
        /// <summary>
        /// Instance of Deliveries Class with accept, cancel, complete_cancel, list, coordinates, eta deliveries and eta veicle method.
        /// </summary>
        private static Deliveries instance;
        public static Deliveries Instance
        {
            get
            {
                if (instance == null)
                    instance = new Deliveries();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Deliveries()
        {
            client = new HttpClient();
        }
        #endregion

        #region Accept Method
        public async void Accept(string wp_id, string session_key, string fee, string odid, string vehicle, string destination, string origin, string lat, string lon, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("fee", fee);
                dict.Add("odid", odid);
                dict.Add("vehicle", vehicle);
                dict.Add("destination", destination);
                dict.Add("origin", origin);
                dict.Add("lat", lat);
                dict.Add("lon", lon);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/accept", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region Cancel Method
        public async void Cancel(string wp_id, string session_key, string odid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("odid", odid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/cancel", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region Complete_Cancel Method
        public async void Complete_Cancel(string wp_id, string session_key, string odid, string stage, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("odid", odid);
                dict.Add("stage", stage);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/complete/cancel", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region List Method
        public async void List(string wp_id, string session_key, string del_id, string odid, string vehicle, string date_stage, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("del_id", del_id);
                dict.Add("odid", odid);
                dict.Add("vehicle", vehicle);
                dict.Add("date_stage", date_stage);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/listing", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region Coordinates Method
        public async void Coordinates(string wp_id, string session_key, string lat, string lon, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("lat", lat);
                dict.Add("long", lon);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/rider/insert", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region ETA_Delivery Method
        public async void ETA_Delivery(string wp_id, string session_key, string rider_id, string stid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("rider_id", rider_id);
                dict.Add("stid", stid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/eta/delivery", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion

        #region ETA_Vehicle Method
        public async void ETA_Vehicle(string wp_id, string session_key, string lat, string lon, string travel, string traffic, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("lat", lat);
                dict.Add("long", lon);
                dict.Add("travel", travel);
                dict.Add("traffic", traffic);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/deliveries/eta/vehicle", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion
    }
}

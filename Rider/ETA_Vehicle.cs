using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struct;

namespace HatidPress.Rider
{
    public class ETA_Vehicle
    {
        #region Fields
        /// <summary>
        /// Instance of ETA Vehicle Class.
        /// </summary>
        private static ETA_Vehicle instance;
        public static ETA_Vehicle Instance
        {
            get
            {
                if (instance == null)
                    instance = new ETA_Vehicle();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ETA_Vehicle()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Vehicle(string wp_id, string session_key, string lat, string lon, string travel, string traffic, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("lat", lat);
            dict.Add("long", lon);
            dict.Add("travel", travel);
            dict.Add("traffic", traffic);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/deliveries/eta/vehicle", content);
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

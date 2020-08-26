using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struct;

namespace HatidPress.Rider
{
    public class Coordinates
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Coordinates of Rider Class.
        /// </summary>
        private static Coordinates instance;
        public static Coordinates Instance
        {
            get
            {
                if (instance == null)
                    instance = new Coordinates();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Coordinates()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Insert(string wp_id, string session_key, string lat, string lon, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("lat", lat);
            dict.Add("long", lon);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/rider/insert", content);
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

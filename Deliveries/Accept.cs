using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struck;

namespace HatidPress.Deliveries
{
    public class Accept
    {
        #region Fields
        /// <summary>
        /// Instance of Rider Accept the Job Class.
        /// </summary>
        private static Accept instance;
        public static Accept Instance
        {
            get
            {
                if (instance == null)
                    instance = new Accept();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Accept()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Job(string wp_id, string session_key, string fee, string odid, string vehicle, string destination, string origin, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("fee", fee);
            dict.Add("odid", odid);
            dict.Add("vehicle", vehicle);
            dict.Add("destination", destination);
            dict.Add("origin", origin);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/deliveries/accept", content);
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

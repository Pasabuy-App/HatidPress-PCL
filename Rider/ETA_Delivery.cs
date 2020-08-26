using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struck;

namespace HatidPress.Rider
{
    public class ETA_Delivery
    {
        #region Fields
        /// <summary>
        /// Instance of ETA Delivery Class.
        /// </summary>
        private static ETA_Delivery instance;
        public static ETA_Delivery Instance
        {
            get
            {
                if (instance == null)
                    instance = new ETA_Delivery();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ETA_Delivery()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Delivery(string wp_id, string session_key, string rider_id, string stid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("rider_id", rider_id);
            dict.Add("stid", stid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/deliveries/eta/delivery", content);
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

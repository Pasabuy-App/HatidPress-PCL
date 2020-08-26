using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struct;

namespace HatidPress.Deliveries
{
    public class Complete_Cancel
    {
        #region Fields
        /// <summary>
        /// Instance of Complete or Cancel the delivery Class.
        /// </summary>
        private static Complete_Cancel instance;
        public static Complete_Cancel Instance
        {
            get
            {
                if (instance == null)
                    instance = new Complete_Cancel();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Complete_Cancel()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Delivery(string wp_id, string session_key, string odid, string stage, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("odid", odid);
            dict.Add("stage", stage);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/deliveries/complete/cancel", content);
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

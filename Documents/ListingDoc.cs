using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struck;

namespace HatidPress.Documents
{
    public  class ListingDoc
    {
        #region Fields
        /// <summary>
        /// Instance of Listing Documents for Rider Class.
        /// </summary>
        private static ListingDoc instance;
        public static ListingDoc Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListingDoc);
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ListingDoc()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Listing(string wp_id, string session_key, string status, string type, string docid, string app_status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            if (status != "")
            {
                dict.Add("status", status);
            }
            if (type != "")
            {
                dict.Add("type", type);
            }
            if (docid != "")
            {
                dict.Add("docid", docid);
            }
            if (app_status != "")
            {
                dict.Add("app_status", app_status);
            }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/documents/list", content);
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

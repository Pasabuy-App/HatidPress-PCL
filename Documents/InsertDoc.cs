using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struck;

namespace HatidPress.Documents
{
    public class InsertDoc
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Documents for Rider Class.
        /// </summary>
        private static InsertDoc instance;
        public static InsertDoc Instance
        {
            get
            {
                if (instance == null)
                    instance = new InsertDoc();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public InsertDoc()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Insert(string wp_id, string session_key, string preview, string type, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("preview", preview);
            dict.Add("type", type);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/documents/insert", content);
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

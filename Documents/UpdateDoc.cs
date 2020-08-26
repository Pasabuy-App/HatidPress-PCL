using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Deliveries.Struck;

namespace HatidPress.Documents
{
    public class UpdateDoc
    {
        #region Fields
        /// <summary>
        /// Instance of Update Documents for Rider Class.
        /// </summary>
        private static UpdateDoc instance;
        public static UpdateDoc Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateDoc();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateDoc()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Update(string wp_id, string session_key, string docid, string type, string preview, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("docid", docid);
            dict.Add("type", type);
            dict.Add("preview", preview);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/hatidpress/v1/documents/update", content);
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

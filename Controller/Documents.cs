using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using HatidPress.Model;

namespace HatidPress
{
    public class Documents
    {
        #region Fields
        /// <summary>
        /// Instance of Documents Class with approve, delete, insert, list and update method.
        /// </summary>
        private static Documents instance;
        public static Documents Instance
        {
            get
            {
                if (instance == null)
                    instance = new Documents();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Documents()
        {
            client = new HttpClient();
        }
        #endregion

        #region Approve Method
        public async void Approve(string wp_id, string session_key, string docid, string rider_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("docid", docid);
                dict.Add("rider_id", rider_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/documents/approve", content);
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

        #region Delete Method
        public async void Delete(string wp_id, string session_key, string docid, string rider_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("docid", docid);
                dict.Add("rider_id", rider_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/documents/delete", content);
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

        #region Insert Method
        public async void Insert(string wp_id, string session_key, string preview, string type, string rider_id, string exp, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("preview", preview);
                dict.Add("type", type);
                dict.Add("rider_id", rider_id);
                if (exp != "" ) { dict.Add("exp", exp); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/documents/insert", content);
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
        public async void List(string wp_id, string session_key, string status, string type, string docid, string app_status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                if (status != "") { dict.Add("status", status); }
                if (type != "") {  dict.Add("type", type); }
                if (docid != "") { dict.Add("docid", docid); }
                if (app_status != "") { dict.Add("app_status", app_status); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/documents/list", content);
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

        #region Update Method
        public async void Update(string wp_id, string session_key, string docid, string rider_id, string preview, string exp, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("docid", docid);
                dict.Add("rider_id", rider_id);
                dict.Add("preview", preview);
                if (exp!= "") { dict.Add("exp", exp); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(HPHost.Instance.BaseDomain + "/hatidpress/v1/documents/update", content);
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

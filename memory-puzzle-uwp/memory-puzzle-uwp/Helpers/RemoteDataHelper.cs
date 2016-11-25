using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using memory_puzzle_uwp.Models;
using Newtonsoft.Json;

namespace memory_puzzle_uwp.Helpers
{
    class RemoteDataHelper
    {
        const string REMOTE_HOST = "http://localhost:4000/score";
        protected static string collectionPattern = "/%s";

        public static async Task<bool> GetScores() {
            string jsonString = "";
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage httpResponse = await client.GetAsync(new Uri(REMOTE_HOST + String.Format(collectionPattern, "default")));
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    jsonString = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                //No internet or api is not running
            }

            ScoreModel[] remoteScores = JsonConvert.DeserializeObject<ScoreModel[]>(jsonString);

            DatabaseHelper db = new DatabaseHelper();
            db.InsertScores(remoteScores);

            return true;
        }

        public static async Task<bool> StoreScore(ScoreModel score) {
            try
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(score), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await client.PostAsync(REMOTE_HOST, content);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch {
                 //No internet or api is not running
            }

            return false;
        }
    }
}

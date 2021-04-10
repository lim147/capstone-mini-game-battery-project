using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Storage
{
    /// <summary>
    /// This module implements [ElasticsearchStorageManager Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#elasticsearchstoragemanager-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module implements IStorage by storing the BatterySession data
    /// in Elasticsearch.
    ///
    /// <div class="WARNING">
    ///     <h5>ASSUMPTION</h5>
    ///     <p> The Elasticsearch endpoint is running and there is already
    ///         an index with name <c>ELASTICSEARCH_INDEX_NAME</c> set up.
    ///         If this index is not set up, one can do so using a PUT request to
    ///         the endpoint using Postman or Kibana.
    ///     </p>
    /// </div>
    ///
    /// Why doesn't this module use the .NET Elasticsearch library? Short answer:
    /// the .NET Elasticsearch library uses dependencies (particularly those
    /// which are network based) which are broken in WebGL due to WebGL's
    /// application sandboxing. To get around this, we need to use Unity's
    /// UnityWebRequest directly.
    /// </summary>
    public class ElasticsearchStorageManager : MonoBehaviour, IStorage
    {
        private const string ELASTICSEARCH_ENDPOINT = "https://ludusminigamebattery-1683149612.us-east-1.bonsaisearch.net:443";
        private const string ELASTICSEARCH_INDEX_NAME = "battery-session";
        private const string ELASTICSEARCH_USERNAME = "fsy7eehz0o";
        private const string ELASTICSEARCH_PASSWORD = "egngizumly";

        /// <summary>
        /// Store the battery session information to the Elasticsearch backend.
        /// </summary>
        /// <param name="batterySession">Battery session object to be stored</param>
        public void Store(BatterySessionStorage batterySession, Action onStorage)
        {
            StartCoroutine(UploadBatterySession(batterySession, onStorage));
        }

        /// <summary>
        /// Retrieve the battery session information from the Elasticsearch backend.
        /// </summary>
        public void Retrieve(Guid batterySessionId,
            Action<BatterySessionStorage> onRetrieval)
        {
            StartCoroutine(DownloadBatterySessionStorage(batterySessionId, onRetrieval));
        }

        /// <summary>
        /// Uploads BatterySessionStorage data to Elasticsearch.
        /// 
        /// The logic is:
        /// 1. BatterySessionStorage C# Object
        /// 2. JSON representation of BatterySessionStorage
        /// 3. Make a POST request to Elasticsearch Index API with this JSON
        ///    as the payload.
        /// </summary>
        /// <param name="batterySession">The data to store in Elasticsearch.</param>
        /// <param name="onStorage">An action to perform once the battery session is successfully stored.
        /// </param>
        /// <returns>IEnumerator so the uploading happens in the background.</returns>
        /// <remarks>
        /// In the JSON representation of BatterySessionStorage, C# enum values
        /// are represented by their string representations rather than by
        /// integers to avoid ambiguity when new abilities are added (e.g. today
        /// ability #3 could be selective visual, but tomorrow another
        /// ability added to the enum could have value #3).
        /// </remarks>
        private IEnumerator UploadBatterySession(BatterySessionStorage batterySession,
            Action onStorage)
        {
            // We're looking to store a *new* BatterySession in the index.
            // The ID of the Elasticsearch document will be the same as
            // the BatterySessionId to make retrieval of the stored data easier.
            Uri uri = new Uri(string.Join("/",
                            ELASTICSEARCH_ENDPOINT, ELASTICSEARCH_INDEX_NAME, "_doc",
                            batterySession.BatterySessionId));

            // POSTing JSON usng UnityWebRequest requires dealing with raw bytes of JSON
            // See: https://forum.unity.com/threads/posting-raw-json-into-unitywebrequest.397871/#post-4693238
            string jsonifedBatterySession = JsonConvert.SerializeObject(batterySession);
            byte[] rawJson = Encoding.UTF8.GetBytes(jsonifedBatterySession);

            var request = new UnityWebRequest(uri, "POST",
                new DownloadHandlerBuffer(), new UploadHandlerRaw(rawJson));
            request.SetRequestHeader("Content-Type", "application/json");
            AuthenticateRequest(request);

            // Return to caller while waiting for BatterySession to be uploaded.
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                Debug.LogError(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Successfully uploaded Battery Session to Elasticsearch.");
                onStorage?.Invoke();
            }
        }

        /// <summary>
        /// Obtains a saved battery session from Elasticsearch.
        /// </summary>
        /// <param name="batterySessionId">The ID of the battery session to obtain.</param>
        /// <param name="onRetrieval">The action to perform once the battery session is
        /// successfully obtained.</param>
        /// <returns></returns>
        public IEnumerator DownloadBatterySessionStorage(Guid batterySessionId,
            Action<BatterySessionStorage> onRetrieval)
        {
            // We're looking to obtain a BatterySession from the index.
            Uri uri = new Uri(string.Join("/",
                            ELASTICSEARCH_ENDPOINT, ELASTICSEARCH_INDEX_NAME, "_doc",
                            batterySessionId));

            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                AuthenticateRequest(request);
                // Request and wait for the desired page.
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    Debug.LogError(request.downloadHandler.text);
                }
                else
                {
                    onRetrieval(ConvertToBatterySessionStorage(request.downloadHandler.text));
                }
            }
        }

        /// <summary>
        /// Converts the JSON returned from an Elasticsearch GET document
        /// request into a battery session storage object.
        /// </summary>
        /// <param name="elasticsearchResponseJson">The JSON returned from the
        /// GET document request.</param>
        /// <returns>The battery session storage contained within the JSON.
        /// </returns>
        private BatterySessionStorage ConvertToBatterySessionStorage(string elasticsearchResponseJson)
        {
            // The JSON returned from Elasticsearch has several fields, including
            // version, time took, etc
            IDictionary<string, object> elasticsearchResponseDict =
                JsonConvert.DeserializeObject<Dictionary<string, object>>
                (elasticsearchResponseJson);

            // The battery session storage payload is located in the "_source"
            // field.
            elasticsearchResponseDict.TryGetValue("_source",
                out object batterySessionStorageJson);

            // Now we can convert the JSON representation of the battery
            // session storage object into the C# representation.
            return JsonConvert.DeserializeObject<BatterySessionStorage>(batterySessionStorageJson.ToString());
        }

        /// <summary>
        /// Authenticates the request to the Elasticsearch endpoint by
        /// using <c>ELASTICSEARCH_USERNAME</c> and <c>ELASTICSEARCH_PASSWORD</c>.
        /// </summary>
        /// <param name="webRequest">The request to be authenticated.</param>
        private void AuthenticateRequest(UnityWebRequest webRequest)
        {
            // could also use "US-ASCII" or "ISO-8859-1" encoding
            string encoding = "UTF-8";
            string base64 = Convert.ToBase64String(
                Encoding.GetEncoding(encoding)
                    .GetBytes(ELASTICSEARCH_USERNAME + ":" + ELASTICSEARCH_PASSWORD)
            );

            webRequest.SetRequestHeader("Authorization", "Basic " + base64);
        }
    }
}

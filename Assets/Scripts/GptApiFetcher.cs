using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Net;
using System.IO;
using UnityEngine.Scripting;

public class GptApiFetcher : MonoBehaviour
{
    private const string API_ENDPOINT = "https://api.openai.com/v1/chat/completions";
    private const string API_KEY = "YourAPIKey";

    public string FetchGPTResponse(string Input)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_ENDPOINT);
        request.Method = "POST";
        request.Headers.Add("Authorization", $"Bearer {API_KEY}");
        request.ContentType = "application/json";
        request.Timeout = 5 * 60 * 1000;
        var requestBody = new JObject
        {
            ["model"] = "gpt-4",
            ["messages"] = new JArray
            {
                new JObject
                {
                    ["role"] = "user",
                    ["content"] = Input
                }
            }
        };
        Debug.Log(requestBody.ToString());
        byte[] data = Encoding.UTF8.GetBytes(requestBody.ToString());
        using (Stream reqStream = request.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
        }
        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = reader.ReadToEnd();
                    Debug.Log(responseBody);
                    JObject jsonResponse = JObject.Parse(responseBody);
                    if (jsonResponse?["choices"]?[0]?["message"]?["content"] != null)
                    {
                        return jsonResponse["choices"][0]["message"]["content"].ToString();
                    }
                    else
                    {
                        Debug.Log("Error");
                        return null;
                    }
                }
                else
                {
                    Debug.Log($"Error: {response.StatusCode} - {response.StatusDescription}");
                    return null;
                }
            }
        }
        catch (WebException ex)
        {
            Debug.Log($"WebException: {ex.Message}");
            return null;
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

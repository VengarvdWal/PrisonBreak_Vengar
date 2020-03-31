using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class LoadAndParseAPI : MonoBehaviour
{
    private string apiCallUrl = "https://api.nationalize.io/?name=Wessel";

    //public Text textBox;
    private Dictionary<string, string> CountryNamesDictionary = new Dictionary<string, string>(); 
    
    void Start()
    {
        CountryNamesDictionary["NL"] = "Netherlands";
        CountryNamesDictionary["ZA"] = "South Africa";
        StartCoroutine(GetRequest(apiCallUrl));        

    }  

    private void JSONParse(string jsonStr)
    {
        //Debug.Log("_json = " + jsonStr);
        JSONNode jsonObj = JSON.Parse(jsonStr);
        //JSONNode nameNode = jsonObj["name"]; 

        List<float> probabilities = new List<float>();

        for (int i = 0; i <jsonObj["country"].Count; i++)
        {
            float myFloatValue = (float)jsonObj["country"][i]["probability"];
            probabilities.Add(myFloatValue);

            //Debug.Log("Country id: " + jsonObj["country"][i]["country_id"]);
            JSONNode countryID = jsonObj["country"][i]["country_id"];
            string countryIDString = (string)countryID;            

            if(CountryNamesDictionary.ContainsKey(countryIDString))
            {
                Debug.Log(CountryNamesDictionary[countryIDString]);
                
            }            
        }                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + "Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + "Received: " + webRequest.downloadHandler.text);
                JSONParse(webRequest.downloadHandler.text);
            }
        }
    }


}

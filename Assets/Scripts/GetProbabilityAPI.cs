using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GetProbabilityAPI : GetWebData
{

    private Dictionary<string, string> CountryNamesDictionary = new Dictionary<string, string>();
    private string apiCallUrl = "https://api.nationalize.io/?name=Wessel";

    public override void Start()
    {        
        StartCoroutine(GetRequest(apiCallUrl));  
    }
    

    protected override void ParseJSON(string jsonString)
    {
        currentJSONObj = JSON.Parse(jsonString);        
        List<float> probabilities = new List<float>();

        for (int i = 0; i <currentJSONObj["country"].Count; i++)
        {
            float probabilityValue = (float)currentJSONObj["country"][i]["probability"];
            probabilities.Add(probabilityValue);           
            JSONNode countryID = currentJSONObj["country"][i]["country_id"];
            string countryIDString = (string)countryID;            

            if(CountryNamesDictionary.ContainsKey(countryIDString))
            {
                Debug.Log(CountryNamesDictionary[countryIDString]);
                
            }            
        }             
    }

    public void AddCountryNames()
    {
        CountryNamesDictionary["NL"] = "Netherlands";
        CountryNamesDictionary["ZA"] = "South Africa";
    }
    
}

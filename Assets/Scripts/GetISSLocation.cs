using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GetISSLocation : GetWebData
{

    private string apiCallUrl = "http://api.open-notify.org/iss-now.json";


	public override void Start()
    {
        StartCoroutine(GetRequest(apiCallUrl));
    }


    protected override void ParseJSON(string jsonString)
    {
        currentJSONObj = JSON.Parse(jsonString);

		JSONNode ISSLat = currentJSONObj["iss_position"]["latitude"];
		JSONNode ISSLon = currentJSONObj["iss_position"]["longitude"];

		float floatISSLat = (float)ISSLat;
		float floatISSLon = (float)ISSLon;

		SphericalProjection.instance.CalculateISSPosition(floatISSLat, floatISSLon);

		Debug.Log("ISS Position || Latitude : " + floatISSLat + " Longitude : " +floatISSLon);

    }

}

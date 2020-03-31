using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GetIPData : GetWebData
{
	private string apiCurlUrl = "http://ip-api.com/json/195.169.188.129?fields=lat,lon,timezone";

	public override void Start()
	{
		StartCoroutine(GetRequest(apiCurlUrl));
	}

	protected override void ParseJSON(string jsonString)
	{
		currentJSONObj = JSON.Parse(jsonString);

		
		JSONNode IPlat = currentJSONObj["lat"];
		JSONNode IPLon = currentJSONObj["lon"];
		JSONNode IPTimezone = currentJSONObj["timezone"];

		float floatIPLat = (float)IPlat;
		float floatIPLon = (float)IPLon;

		SphericalProjection.instance.CalculateIPPosition(floatIPLat, floatIPLon);

		Debug.Log("IP Position || Latitude : " + floatIPLat + " Longitude : " + floatIPLon);	

	}

}

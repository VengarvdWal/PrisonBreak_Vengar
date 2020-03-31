using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using UnityEngine.UI;
using TMPro;

public class IPGeoISSTime : GetWebData
{
    private enum apiCallState
	{
		IP,
		GET_LOCATION,
		ISS_FLYBYS,
		ISS_LOCATION
	}

	private string CURRENT_IP = "https://api.ipify.org?format=json";
	private string IP_LOCATION = "http://ip-api.com/json/";
	private string ISS_LOCATION = "http://api.open-notify.org/iss-now.json";
	private string ISS_PASS = "http://api.open-notify.org/iss-pass.json";
	//public GameObject canvasUI;
	public GameObject[] textObjects;

	private string timezoneID;

	private apiCallState apiEnumState;

	public override void Start()
	{
		apiEnumState = apiCallState.IP;
		StartCoroutine(GetRequest(CURRENT_IP));

		
	}

	protected override void ParseJSON(string jsonString)
	{
		currentJSONObj = JSON.Parse(jsonString);


		switch (apiEnumState)
		{
			case apiCallState.IP:

				string ip = currentJSONObj["ip"];

				apiEnumState = apiCallState.GET_LOCATION;
				StartCoroutine(GetRequest(IP_LOCATION + ip));

				break;

			case apiCallState.GET_LOCATION:				

				float fLat = (float)currentJSONObj["lat"];
				float fLon = (float)currentJSONObj["lon"];				

				string IPlat = currentJSONObj["lat"];
				string IPlon = currentJSONObj["lon"];				

				timezoneID = currentJSONObj["timezone"];				

				apiEnumState = apiCallState.ISS_FLYBYS;
				StartCoroutine(GetRequest(ISS_PASS + "?lat=" + GetFormattedStringLat(GetFormattedFloatLat(fLat)) + "&lon=" + GetFormattedStringLon(GetFormattedFloatLon(fLon)) + "&n=5"));

				SphericalProjection.instance.CalculateIPPosition(GetFormattedFloatLat(fLat), GetFormattedFloatLon(fLon));

				break;
			case apiCallState.ISS_FLYBYS:				

				for (int i = 0; i < currentJSONObj["response"].Count; i++)
				{
					int epocInt = int.Parse(currentJSONObj["response"][i]["risetime"]);					
					DateTime utcTime = UnixTimeStampToDateTime(epocInt).ToLocalTime();					

					textObjects[i].GetComponent<TextMeshProUGUI>().text = utcTime.ToString("dddd, dd MMMMM yyyy HH:mm");
 				}
								
				apiEnumState = apiCallState.ISS_LOCATION;
				StartCoroutine(UpdateISSLocation());

				break;
			case apiCallState.ISS_LOCATION:


				float fISSLat = (float)currentJSONObj["iss_position"]["latitude"];
				float fISSLon = (float)currentJSONObj["iss_position"]["longitude"];
				Debug.Log("ISS Latitude : " + fISSLat + " " + fISSLon);
				
				SphericalProjection.instance.CalculateISSPosition(GetFormattedFloatLat(fISSLat), GetFormattedFloatLon(fISSLon));
				break;
			default:
				break;
		}		

	}

	public bool shouldUpdateISSLocation = true;
	private IEnumerator UpdateISSLocation()
	{
		while(shouldUpdateISSLocation)
		{
			StartCoroutine(GetRequest(ISS_LOCATION));
			yield return new WaitForSecondsRealtime(5f);
		}
	}

	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
		return dtDateTime;
	}
}


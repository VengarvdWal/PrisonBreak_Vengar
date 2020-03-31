using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public abstract class GetWebData : MonoBehaviour
{

    protected JSONNode currentJSONObj;

	//public bool isRefreshing;
    protected virtual void ParseJSON(string JSONString) { } 
    protected virtual void ReceivedTextureHandler(Texture2D texture2D) { }

    protected IEnumerator GetRequest(string url)
    {		
			using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
			{
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
					ParseJSON(webRequest.downloadHandler.text);

				}
			}
		
    }


    protected IEnumerator GetTexture(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                ReceivedTextureHandler(DownloadHandlerTexture.GetContent(uwr));
				
            }
        }
    }

	protected float GetFormattedFloatLat(float lat)
	{
		if(Mathf.Abs(lat) > 90f)
		{
			lat /= 10000f;
		}
		GetFormattedStringLat(lat);
		return lat;
	}

	protected float GetFormattedFloatLon(float lon)
	{
		if (Mathf.Abs(lon) > 180f)
		{
			lon /= 10000f;
		}
		GetFormattedStringLon(lon);
		return lon;
	}

	protected string GetFormattedStringLat(float lat)
	{
		string sfLat = lat.ToString().Replace(',', '.');

		return sfLat;
	}

	protected string GetFormattedStringLon(float lon)
	{
		string sfLon = lon.ToString().Replace(',', '.');

		return sfLon;
	}

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}

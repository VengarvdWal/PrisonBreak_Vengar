using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class LoadAndParseXKCD : MonoBehaviour
{
    private string apiCallUrl; // = "https://xkcd.com//info.0.json";
    private int comicNumber;
    public RawImage myRawImage;
    public Text myText;    
    public float desiredImageHeight;
    
    
    void Start()
    {   
        comicNumber = Random.Range(1, 2266);     
        apiCallUrl = "https://xkcd.com/" + comicNumber + "/info.0.json";
        StartCoroutine(GetRequest(apiCallUrl));     
             
    } 
    private JSONNode currentJSONObje;
    private void JSONParse(string jsonStr)
    {        
        currentJSONObje = JSON.Parse(jsonStr); 

        JSONNode imgName = currentJSONObje["img"];

        string imgURL = (string)imgName;
        

        Debug.Log(imgURL);

        StartCoroutine(GetTexture(imgURL));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ReceivedTextureHandler(Texture2D texture2D)
    {
        myRawImage.texture = texture2D;
        
        float ratio = (float)texture2D.width / (float)texture2D.height;

        myRawImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, desiredImageHeight);
        myRawImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, desiredImageHeight * ratio);

        myText.text = currentJSONObje["title"];
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

    IEnumerator GetTexture(string url)
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class GetXKCDComics : GetWebData
{
    public RawImage comicImage;
    public Text myText;
    public float desiredImageHeight;
    private int comicNumber;
    private string apiCallUrl;


    public override void Start()
    {        
        GetRandomNumber();    
        apiCallUrl = "https://xkcd.com/" + comicNumber + "/info.0.json";
        StartCoroutine(GetRequest(apiCallUrl));
    }

    protected override void ParseJSON(string jsonString)
    {
        currentJSONObj = JSON.Parse(jsonString); 
        JSONNode imgName = currentJSONObj["img"];
        string imgURL = (string)imgName;        

        Debug.Log(imgURL);
        StartCoroutine(GetTexture(imgURL));
    }

    protected override void ReceivedTextureHandler(Texture2D texture2D)
    {
        comicImage.texture = texture2D;
        
        float ratio = (float)texture2D.width / (float)texture2D.height;

        comicImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, desiredImageHeight);
        comicImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, desiredImageHeight * ratio);

        myText.text = currentJSONObj["title"];
    }

    private float GetRandomNumber()
    {
        comicNumber = Random.Range(1, 2266);
        return comicNumber;
    }
}

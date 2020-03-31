using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SphericalProjection : MonoBehaviour
{
	public static SphericalProjection instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad((gameObject));
		}
		else
		{
			Destroy(this);
		}
	}

	public bool isInRange = false;

	//public float latitude;
    //public float longitude;

    private float _radius;
    public GameObject IPArea;
	public GameObject ISSPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _radius = gameObject.GetComponentInChildren<SphereCollider>().radius * gameObject.transform.lossyScale.z;
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public bool InRadius()
	{
		isInRange = true;	

		return isInRange;

	}

	public bool NotInRadius()
	{
		isInRange = false;

		return isInRange;
	}

    public void CalculateIPPosition(float latitude, float longitude)
    {

		latitude = latitude * Mathf.Deg2Rad;
		longitude = longitude * Mathf.Deg2Rad;

		float xPos = (_radius) * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float zPos = (_radius) * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float yPos = (_radius) * Mathf.Sin(latitude);

        //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(1581949737);

		//Debug.Log(dateTimeOffset.ToString());


        GameObject currentObj = Instantiate(IPArea, new Vector3(xPos, yPos, zPos), Quaternion.identity);
		//Destroy(currentObj, 5f);
    }

	public void CalculateISSPosition(float latitude, float longitude)
	{

		Debug.Log(latitude + " " + longitude);
		latitude *= Mathf.Deg2Rad;
		longitude *= Mathf.Deg2Rad;

		float xPos = (_radius) * Mathf.Cos(latitude) * Mathf.Cos(longitude);
		float zPos = (_radius) * Mathf.Cos(latitude) * Mathf.Sin(longitude);
		float yPos = (_radius) * Mathf.Sin(latitude);		

		//Make var ISS target 
		//Update ISS with the target

		GameObject currentObj = Instantiate(ISSPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
		currentObj.transform.SetParent(this.gameObject.transform);
		Destroy(currentObj, 5.5f);
	}
}

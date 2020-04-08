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
			//DontDestroyOnLoad((gameObject));
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
	private GameObject currentObj;
    // Start is called before the first frame update
    void Start()
    {
        _radius = gameObject.GetComponent<SphereCollider>().radius * gameObject.transform.lossyScale.z;
		currentObj = Instantiate(ISSPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity ,this.gameObject.transform);		

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

	public bool ReturnBool()
	{
		return isInRange;
	}

    public void CalculateIPPosition(float latitude, float longitude)
    {

		latitude = latitude * Mathf.Deg2Rad;
		longitude = longitude * Mathf.Deg2Rad;

		float xPos = (_radius) * Mathf.Cos(latitude) * Mathf.Cos(longitude) + transform.position.x;
        float zPos = (_radius) * Mathf.Cos(latitude) * Mathf.Sin(longitude) + transform.position.z;
        float yPos = (_radius) * Mathf.Sin(latitude) + transform.position.y;		  


        GameObject currentObj = Instantiate(IPArea, new Vector3(xPos, yPos, zPos), Quaternion.identity, this.transform);
		
		
	}

	public void CalculateISSPosition(float latitude, float longitude)
	{

		
		latitude *= Mathf.Deg2Rad;
		longitude *= Mathf.Deg2Rad;
		//Debug.Log(latitude + " " + longitude);

		float xPos = (_radius) * Mathf.Cos(latitude) * Mathf.Cos(longitude);
		float zPos = (_radius) * Mathf.Cos(latitude) * Mathf.Sin(longitude);
		float yPos = (_radius) * Mathf.Sin(latitude);

		//Make var ISS target 
		//Update ISS with the target
		currentObj.transform.position = new Vector3(xPos + transform.position.x, yPos + transform.position.y, zPos + transform.position.z);

		



	}
}

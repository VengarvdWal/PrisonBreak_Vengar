using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISSDOOR : MonoBehaviour
{
	//public bool open = false;
	private float initialRotation;

	void Start()
    {
		initialRotation = transform.rotation.eulerAngles.y;
	}
    
    void Update()
    {
		if (SphericalProjection.instance.ReturnBool() && transform.rotation.eulerAngles.y < initialRotation + 90)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation + 90, 0), 5);
		}
		else if (!SphericalProjection.instance.ReturnBool() && transform.rotation.eulerAngles.y > initialRotation)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation, 0), 5);
		}
	}
}

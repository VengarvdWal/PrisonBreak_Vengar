using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisionISS : MonoBehaviour
{



	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "IPArea")
		{
			SphericalProjection.instance.InRadius();
			Debug.LogWarning(SphericalProjection.instance.InRadius());
		}
	}
	private void OnTriggerExit(Collider other)
	{
		
		SphericalProjection.instance.NotInRadius();
		Debug.LogWarning(SphericalProjection.instance.NotInRadius());
	}

	
}

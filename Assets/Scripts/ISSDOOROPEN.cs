using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ISSDOOROPEN : MonoBehaviour, IInteractable
{
	private bool access = false;
	public bool isTrigger = false;
	private bool active = false;
	

    void Update()
    {

		if (isTrigger)
		{
			active = true;
		}
		else
		{
			if (access)
			{
				SceneManager.LoadScene("PrisonBreak");
			}
		}
		
	}

	public void Action()
	{		
		Open();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (active)
		{
			if (other.gameObject.tag == "Player")
			{
				SceneManager.LoadScene("PrisonBreak");
			}
		}
		
		
	}

	public void Open()
	{
		access = !access;
	}
}

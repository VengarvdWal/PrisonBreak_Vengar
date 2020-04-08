using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RaftAccess : MonoBehaviour, IInteractable
{
	public bool raftAccess = false;

	public GameObject winCanvas;
	public void AccessRaft()
	{
		if (Inventory.instance.RaftPartsCollected())
		{
			raftAccess = true;
		}
	}

	public void Start()
	{
		StartCoroutine("Restart");	
	}
	public void Update()
	{
		if (raftAccess)
		{
			Debug.Log("You have access to the raft");
			winCanvas.SetActive(true);
		}
		else
			Debug.Log("You have NO access to the raft");
		
	}

	IEnumerator Restart()
	{
		while (winCanvas.activeSelf)
		{
			yield return new WaitForSeconds(3f);
			Debug.Log("Waited 3 seconds");
			SceneManager.LoadScene("SampleScene");
		}
		
	}

	public void Action()
	{
		AccessRaft();
	}
}

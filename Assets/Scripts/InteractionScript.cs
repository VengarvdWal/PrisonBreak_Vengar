using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionScript : MonoBehaviour
{
    public float raycastRange = 5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastInteract();
        }

        if(Input.GetButtonDown("OpenInventory"))
        {
            OpenInventory(!InventoryUI.instance.gameObject.activeSelf);
        }
    }

    public void OpenInventory(bool value)
    {
        InventoryUI.instance.gameObject.SetActive(value);
        GetComponentInParent<FirstPersonController>().enabled = !value;
        Cursor.visible = value;                 
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        
    }

	void RaycastInteract()
	{
		if(!InventoryUI.instance.gameObject.activeSelf)
		{
			Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hit;

			int ignoreLayer = ~LayerMask.GetMask("Player");

			if (Physics.Raycast(r, out hit, raycastRange, ignoreLayer))
			{
				IInteractable i = hit.collider.gameObject.GetComponent<IInteractable>();
				if (i != null)
				{
					i.Action();
				}
			}
		}
		
	}
}

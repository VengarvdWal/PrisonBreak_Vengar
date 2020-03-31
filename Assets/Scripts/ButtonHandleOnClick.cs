using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandleOnClick : MonoBehaviour
{

    private InteractionScript player;
	// Start is called before the first frame update
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InteractionScript>();
    }

	// Update is called once per frame
	public void HandleClick()
    {
		Inventory.instance.RemoveItemByName(transform.GetComponentInChildren<TextMeshProUGUI>().text);        
        player.OpenInventory(false);
    }
}

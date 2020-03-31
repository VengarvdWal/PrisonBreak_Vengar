using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickUp : MonoBehaviour, IInteractable
{

    public string itemName;
    public float weight;
    public  Sprite image;
    private GameObject inventoryObject; 


    private void Start()
    {
        InventoryUI.instance.RegisterPickUpItem(this);    
    }

    public void Action()
    {
        if(Inventory.instance.AddItem(CreateItem()))
        {
            gameObject.SetActive(false);
        }
    }    

    public bool isInInventory()
    {
        return inventoryObject != null;
    }

    public void AddInventoryObject(GameObject go)
    {
        inventoryObject = go;
    }

    public void RemoveInventoryItem()
    {
        Destroy(inventoryObject);
        inventoryObject = null;
    }

    public void Close()
    {
        RemoveInventoryItem();
        transform.position = Camera.main.transform.position + Camera.main.transform.forward;
        gameObject.SetActive(true);
    }

    protected abstract Item CreateItem();
}

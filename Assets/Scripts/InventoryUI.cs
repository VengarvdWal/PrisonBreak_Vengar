using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUI : MonoBehaviour
{

    public static InventoryUI instance;

    public GameObject itemPrefab;
    private Dictionary<string, PickUp> items;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        items = new Dictionary<string, PickUp>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void RegisterPickUpItem(PickUp i)
    {
        if(!items.ContainsKey(i.itemName))
        {            
            items.Add(i.itemName, i);                        
        }
    }

    public void Add(Item i)
    {   
               
        if(items.ContainsKey(i.name) && !items[i.name].isInInventory())
        {     
            GameObject go = Instantiate(itemPrefab, transform);                       
            go.GetComponentInChildren<Image>().sprite = items[i.name].image;
            go.transform.parent = gameObject.GetComponentInChildren<VerticalLayoutGroup>().transform;			
            go.transform.GetComponentInChildren<TextMeshProUGUI>().text = i.name;   
            items[i.name].AddInventoryObject(go);
        }
    }

	public void RemoveItem(Item i)
   {
       if(items.ContainsKey(i.name))
       {
           items[i.name].Close();
       }
   }   
}

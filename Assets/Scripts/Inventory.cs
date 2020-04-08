using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    //Properties
    private List<Item> items;	
    public float maxWeight = 10f;
    private float currentWeight = 0;
	int partsCollected = 0;


	private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad((gameObject));
        }
        else
        {
            Destroy(this);
        }

        items = new List<Item>();
        currentWeight = 0;
    }

	void RaftParts()
	{

	}
	
    public bool AddItem(Item i)
    {
        if (currentWeight + i.weight > maxWeight)
        {
            return false;
        }
        else
        {
			
			items.Add(i);
			if (i is RaftPartItem)
			{
				foreach (RaftPartItem item in items)
				{
					int partsCollected = 0;
				}
			}			
					
			InventoryUI.instance.Add(i);            
            currentWeight += i.weight;
            return true;
        }
    }

    public void RemoveItem(Item i)
    {
        if(items.Remove(i))
        {
			InventoryUI.instance.RemoveItem(i);
            currentWeight -= i.weight;
        }

    }

    public void RemoveItemByName(string name)
    {
        foreach(Item i in items)
        {
            if(i.name == name)
            {
                RemoveItem(i);
                break;
            }
        }
    } 

	public bool RaftPartsCollected()
	{
		
		for (int i = 0; i < items.Count; i++)
		{

			if (partsCollected != 3)
			{
				if (items[i] is RaftPartItem)
				{
					RaftPartItem rp = (RaftPartItem)items[i];
					partsCollected++;										
					return false;



				}
			}			
			
		}		
		return true;
		
	}

    public bool Opens(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is AccessItem)
            {
                AccessItem ai = (AccessItem) items[i];
                if (ai.doorID == id)
                {                    
                    return true;                    
                }
            }
        }

        return false;
    }

    public float ReturnCurrentWeight()
    {
        return currentWeight;
    }

    public int Count()
    {
        return items.Count;
    }    
}

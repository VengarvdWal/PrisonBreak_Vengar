//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Test : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        Item testItem = new BonusItem("testItem", 12.5f, 100);
//        Debug.Log("Item is " + testItem.name + " and weighs " + testItem.weight);

//        if(testItem is BonusItem)
//        {
//            Debug.Log("testItem is a bonus item");
//        }
//        else
//        {
//            Debug.Log("testItem is not a bonus item");
//        }

//        Item key = new AccessItem("Key of Doom", 1.0f, 1);
//        Debug.Log("Item is " + key.name + " and weighs " + key.weight);

//        //Here we transform the Item into a BonusItem
//        BonusItem b = (BonusItem) testItem;
//        Debug.Log(testItem.name + " gives you " + b.score + " points");

//        AccessItem k = (AccessItem) key;
//        Debug.Log(key.name + " opens door number " + k.doorID);     
        
//        if(Inventory.instance.AddItem(b))
//        {
//            Debug.Log("Inventory has " + Inventory.instance.Count() + " items");
//        }
//        else
//        {
//            Debug.Log("Could not add item because it is to heavy");
//        }

//        if(Inventory.instance.AddItem(k))
//        {
//            Debug.Log("Inventory has " + Inventory.instance.Count() + " items");
//        }
//        else
//        {
//            Debug.Log("Could not add item because it is to heavy");
//        }

//        // Inventory.instance.RemoveItem(b);

//        // Debug.Log("Removed " + b.name + " from your inventory");
//        // Inventory.instance.RemoveItem(k);

//        // Debug.Log("Removed " + k.name + " from your inventory");


//        // Debug.Log("Inventory has " + Inventory.instance.Count() + " items");        
//    }    
//}

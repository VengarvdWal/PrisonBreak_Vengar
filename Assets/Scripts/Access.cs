using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Access : PickUp
{
    public int door;
    
    protected override Item CreateItem()
    {
        return new AccessItem(itemName, weight, door);
    }
}
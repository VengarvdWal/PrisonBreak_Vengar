using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessItem : Item
{
    //Properties
    public int doorID;

    //Constructor
    public AccessItem(string name, float weight, int doorID) : base(name, weight)
    {        
        this.doorID = doorID;
    }    

}

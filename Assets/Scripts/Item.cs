using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
     //Properties
    public string name;
    public float weight;
   
    //Constructor    
    public Item(string name, float weight) 
    {
        this.name = name;
        this.weight = weight;
    }


    //Methods  
}

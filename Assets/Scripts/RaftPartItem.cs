using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftPartItem : Item
{
    //Properties
    public int score;

    //Constructor
    public RaftPartItem(string name, float weight, int score) : base(name, weight)
    {
        this.score = score;
    }

    //Methods
    
}

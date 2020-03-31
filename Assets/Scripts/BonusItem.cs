using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : Item
{
    //Properties
    public int score;

    //Constructor
    public BonusItem(string name, float weight, int score) : base(name, weight)
    {
        this.score = score;
    }

    //Methods
    
}

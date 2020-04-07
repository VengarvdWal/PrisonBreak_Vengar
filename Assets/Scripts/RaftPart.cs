using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftPart : PickUp
{
    public int points;

    protected override Item CreateItem()
    {
        return new RaftPartItem(name, weight, points);
    }
}

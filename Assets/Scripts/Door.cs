﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int id;
    public bool open = false;
    private float initialRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && transform.rotation.eulerAngles.y < initialRotation + 90)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation + 90, 0), 5);
        } else if (!open && transform.rotation.eulerAngles.y > initialRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation, 0), 5);
        }        
    }  

    public void Open()
    {
        if (Inventory.instance.Opens(id)) //id == -1  || 
        {
            open = !open;
        }
		else if (SphericalProjection.instance.isInRange)
		{
			open = !open;
		}
    }

    public void Action()
    {
        Open();
    }
}
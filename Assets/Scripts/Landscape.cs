using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Landscape : MonoBehaviour
{
    private void Start()
    {
        Init();
    }
    protected void Init()
    {
        ProceduralManager.instance.regenerate.AddListener(Generate);
        Generate();
		
    }
    public virtual void Generate()
    {
        Debug.Log("this should not display");
    }
    public virtual void Clean()
    {
        
    }
}

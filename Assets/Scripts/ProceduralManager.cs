using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProceduralManager : MonoBehaviour
{

	public static ProceduralManager instance;
	private int seed;
	private float perlinSeed;
	public ProceduralWorld world;
	public UnityEvent regenerate;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(obj: this);
		}
		regenerate = new UnityEvent();
		world.Init();
	}


	void OnValidate()
	{
		if (instance != null)
		{
			regenerate.Invoke();
		}

	}
	// Start is called before the first frame update

	public void SetSeed(int seed)
	{
		this.seed = seed;
		Random.InitState(seed);
		perlinSeed = Random.Range(-100000f, 100000f);
	}

	public float GetPerlinSeed()
	{
		return perlinSeed;
	}

}	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProceduralWorld
{
    public enum GenType
    {
        RandomBased,
        PerlinBased,
        Sign
    };


    [SerializeField]
    public GameObject[] raftPartPrefabs = new GameObject[3];
	public GameObject[] raftPrefab = new GameObject[1];
	public List<GameObject> rockPrefabs;
	[SerializeField]
    private float maxHeight = 1;
    [SerializeField]
    private float minHeight = 0;
    [SerializeField]
    private int size= 10;
    [SerializeField]
    private float detail = 10f;
	[SerializeField]
	private float rockProbability;
	[SerializeField]
    private int seed = 0;
    [SerializeField]
    private GenType type;
    [SerializeField]
    public float[,] heights;
	public List<Vector3Int> rocks;
    public List<Vector3Int> raftParts;
	public List<Vector3Int> raftPosition;
    public int Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
            Init();
        }
    }

    public ProceduralWorld(float minHeight, float maxHeight, int size, float detail, int seed, GenType type)
    {
        Debug.Log(message: "constructor of the world called. ");
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.size = size;
        this.detail = detail;
        this.seed = seed;
        this.type = type;

        heights = new float[size, size];
    }
    public void Generate()
    {
		

        Debug.Log("Generating World");
        for (int x = 0; x < heights.GetLength(dimension: 0); x++)
        {
            for (int z = 0; z < heights.GetLength(dimension: 1); z++)
            {
                float height = 0;
                switch (type)
                {
                    case GenType.RandomBased:
                        height = UnityEngine.Random.Range(minHeight, maxHeight);
                        break;
                    case GenType.PerlinBased:
						float perlinX = ProceduralManager.instance.GetPerlinSeed() + x / (float)size * detail;
						float perlinY = ProceduralManager.instance.GetPerlinSeed() + z / (float)size * detail;
						height = (Mathf.PerlinNoise(perlinX, perlinY) - minHeight) * maxHeight;
                        break;
                    case GenType.Sign:
                        height = Mathf.Sin(x / detail + z) + Mathf.Cos(z /detail) + UnityEngine.Random.Range(minHeight, maxHeight);
                        break;
                }               
                heights[x, z] = height;

				float rockRand = UnityEngine.Random.value;
				if (rockRand < rockProbability * MathUtils.Map(height, 0, maxHeight, 0f, 1f))
				{
					int t = UnityEngine.Random.Range(0, rockPrefabs.Count);
					Vector3Int rock = new Vector3Int(x, z, t);
					rocks.Add(rock);
										
				}

				

			}
        }

		for (int i = 0; i < raftPartPrefabs.Length; i++)
		{
			int x = UnityEngine.Random.Range(0, heights.GetLength(dimension: 0));
			int z = UnityEngine.Random.Range(0, heights.GetLength(dimension: 1));
			Vector3Int part = new Vector3Int(x, z, i);
			raftParts.Add(part);
		}

		for (int i = 0; i < raftPrefab.Length; i++)
		{
			int x = UnityEngine.Random.Range(0, heights.GetLength(dimension: 0));
			int z = UnityEngine.Random.Range(0, heights.GetLength(dimension: 1));
			Vector3Int raft = new Vector3Int(x, z, i);
			raftPosition.Add(raft);

		}

		Debug.Log(message: "world generated");
    }

	
    public void Regenerate()
    {
        heights = new float[size, size];
        ProceduralManager.instance.SetSeed(seed);        
        rocks.Clear();
		raftParts.Clear();
        Generate();
	}
    public void Init()
    {
		ProceduralManager.instance.regenerate.AddListener(Regenerate);
        Regenerate();
    }
}

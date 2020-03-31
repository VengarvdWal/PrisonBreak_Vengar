using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : MonoBehaviour
{
	public GameObject prefab;

	[Header("Terrain Settings")]
	[Tooltip("Change the minimum height of the terrain. Between 0 and 1.")]
	[Range(0f, 1f)]
	public float minHeight = 0f;
	[Tooltip("Change the maximum height of the terrain. Between 1 and 5.")]
	[Range(1f, 5f)]
	public float maxHeight = 1f;

	[Header("Grid Size")]
	[Tooltip("Change the grid size. Between 0 and 50.")]
	[Range(0f, 50f)]
	public float gridSize = 10.0f;

	[Header("Perlin Zoom")]
	[Tooltip("Change to perlin zoom. Between 1 and 25.")]
	[Range(1, 25f)]
	public float detail =  10.0f;

    // Start is called before the first frame update
    void Start()
    {
		ProceduralManager.instance.SetSeed(10);
		Generate(); 
    }

   	private void Generate()
	{

		for (int x = 0; x < gridSize; x++)
		{
			for (int z = 0; z < gridSize; z++)
			{
				//float r = Random.Range(minHeight, maxHeight); //Random Generation

				float perlinX = x / detail + ProceduralManager.instance.GetPerlinSeed();
				float perlinY = z / detail + ProceduralManager.instance.GetPerlinSeed();

				float r = (Mathf.PerlinNoise(perlinX,perlinY)-minHeight) * maxHeight;

				Vector3 pos = new Vector3(x, r, z);
				Instantiate(prefab, Vector3.zero + pos, Quaternion.identity, this.gameObject.transform);
			}
		}
		
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TextureModifier : MonoBehaviour
{
	public static TextureModifier instance;
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
		
	}
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class TextureAttributes
	{

		public string name;
		public int index;
		public bool defaultTexture = false;
		[Range(0.0f, 1.0f)]
		public float minSteepness;
		[Range(0.0f, 1.0f)]
		public float maxSteepness;
		[Range(0.0f, 1.0f)]
		public float minAltitude;
		[Range(0.0f, 1.0f)]
		public float maxAltitude;
	}

	public List<TextureAttributes> listTextures = new List<TextureAttributes>();
	private Terrain terrain;
	private TerrainData terrainData;
	private int indexOfDefaultTexture;


	public void TextureMapping()
	{

		
		terrain = GetComponent<Terrain>();

		
		terrainData = terrain.terrainData;
				
		int nbTextures = terrainData.alphamapLayers;
				
		float maxHeight = GetMaxHeight(terrainData, terrainData.heightmapResolution);				
		float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
				
		for (int i = 0; i < listTextures.Count; i++)
		{
			if (listTextures[i].minAltitude > listTextures[i].maxAltitude)
			{
				float temp = listTextures[i].minAltitude;
				listTextures[i].minAltitude = listTextures[i].maxAltitude;
				listTextures[i].maxAltitude = temp;
			}

			if (listTextures[i].minSteepness > listTextures[i].maxSteepness)
			{
				float temp2 = listTextures[i].minSteepness;
				listTextures[i].minSteepness = listTextures[i].maxSteepness;
				listTextures[i].maxSteepness = temp2;
			}
		}
				
		for (int i = 0; i < listTextures.Count; i++)
		{
			if (listTextures[i].defaultTexture)
			{
				indexOfDefaultTexture = listTextures[i].index;
			}
		}


		for (int y = 0; y < terrainData.alphamapHeight; y++)
		{
			for (int x = 0; x < terrainData.alphamapWidth; x++)
			{
				float y_01 = (float)y / (float)terrainData.alphamapHeight;
				float x_01 = (float)x / (float)terrainData.alphamapWidth;
								
				float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapResolution), Mathf.RoundToInt(x_01 * terrainData.heightmapResolution));
								
				float normHeight = height / maxHeight;
				float steepness = terrainData.GetSteepness(y_01, x_01);
				float normSteepness = steepness / 90.0f;
								
				for (int i = 0; i < terrainData.alphamapLayers; i++)
				{
					splatmapData[x, y, i] = 0.0f;
				}

				float[] splatWeights = new float[terrainData.alphamapLayers];

				for (int i = 0; i < listTextures.Count; i++)
				{										
					if (normHeight >= listTextures[i].minAltitude && normHeight <= listTextures[i].maxAltitude && normSteepness >= listTextures[i].minSteepness && normSteepness <= listTextures[i].maxSteepness)
					{
						splatWeights[listTextures[i].index] = 1.0f;
					}
				}
								
				float z = splatWeights.Sum();

				if (Mathf.Approximately(z, 0.0f))
				{
					splatWeights[indexOfDefaultTexture] = 1.0f;
				}

				
				for (int i = 0; i < terrainData.alphamapLayers; i++)
				{										
					splatWeights[i] /= z;
										
					splatmapData[x, y, i] = splatWeights[i];
				}
			}
		}
				
		terrainData.SetAlphamaps(0, 0, splatmapData);
	}
		
	private float GetMaxHeight(TerrainData tData, int heightmapWidth)
	{

		float maxHeight = 0f;

		for (int x = 0; x < heightmapWidth; x++)
		{
			for (int y = 0; y < heightmapWidth; y++)
			{
				if (tData.GetHeight(x, y) > maxHeight)
				{
					maxHeight = tData.GetHeight(x, y);
				}
			}
		}
		return maxHeight;
	}
}

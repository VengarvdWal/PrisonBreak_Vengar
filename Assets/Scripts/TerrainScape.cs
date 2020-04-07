using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScape : Landscape
{
	private Terrain t;

	public void Start()
	{
		t = GetComponent<Terrain>();

		if (t == null)
		{
			Debug.LogError("please put the terrainscape script on a terrain");
		}
		Init();
	}

	public override void Clean()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Rock");
		for (int i = 0; i < gos.Length; i++)
		{
			Destroy(gos[i]);
		}

	}

	public override void Generate()
	{
		Clean();
		Debug.Log("adding heights");
		t.terrainData.heightmapResolution = ProceduralManager.instance.world.Size;
		t.terrainData.alphamapResolution = ProceduralManager.instance.world.Size;
		t.terrainData.SetHeights(0, 0, ProceduralManager.instance.world.heights);
			

		for (int r = 0; r < ProceduralManager.instance.world.rocks.Count; r++)
		{
			Vector3Int rock = ProceduralManager.instance.world.rocks[r];

			Vector3 worldPosition = new Vector3(
				MathUtils.Map(
					rock.x,
					0,
					ProceduralManager.instance.world.Size,
					t.GetPosition().x,
					t.GetPosition().x + t.terrainData.size.x),
				0.0f,
				MathUtils.Map(
					rock.y,
					0,
					ProceduralManager.instance.world.Size,
					t.GetPosition().z,
					t.GetPosition().z + t.terrainData.size.z)
				);


			worldPosition.y = t.SampleHeight(worldPosition);

			Instantiate(ProceduralManager.instance.world.rockPrefabs[rock.z], worldPosition, Quaternion.identity, this.transform);
			
			
		}

		for (int p = 0; p < ProceduralManager.instance.world.raftParts.Count; p++)
		{
			Vector3Int part = ProceduralManager.instance.world.raftParts[p];

			Vector3 worldPosition = new Vector3(
				MathUtils.Map(
					part.x,
					0,
					ProceduralManager.instance.world.Size,
					t.GetPosition().x,
					t.GetPosition().x + t.terrainData.size.x),
				0.0f,
				MathUtils.Map(
					part.y,
					0,
					ProceduralManager.instance.world.Size,
					t.GetPosition().z,
					t.GetPosition().z + t.terrainData.size.z)
				);

			worldPosition.y = t.SampleHeight(worldPosition);

			Instantiate(ProceduralManager.instance.world.raftPrefabs[part.z], worldPosition, Quaternion.identity, this.transform);

		}
		TextureModifier.instance.TextureMapping(t);
	}
}



using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class RandomPlaneCreator : MonoBehaviour {

	public int size_x = 100;
	public int size_z = 50;
	public float tileSize = 1.0f;
	public Texture2D terrainTiles;
	public int tileResolution;
	public int[] tileChance;

	// Use this for initialization
	void Start () {
		BuildMesh ();

	}

	public void SetTileChances() {

		tileChance = new int[ChopUpTiles().Length];

		for (int c=0; c<ChopUpTiles().Length; c++) {
			tileChance[c] = 1;
		}

	}

	Color[][] ChopUpTiles() {

		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;

		Color[][] tiles = new Color[numTilesPerRow*numRows][];

		for (int y=0; y<numRows; y++) {
			for (int x=0; x<numTilesPerRow; x++) {
				tiles[y*numTilesPerRow + x] = terrainTiles.GetPixels(x*tileResolution,y*tileResolution,tileResolution,tileResolution);
			}
		}

		return tiles;
	}
	void Builtexture () {

		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;

		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;
		Texture2D texture = new Texture2D(texWidth,texHeight);

		//maak Color array aan met tiles, tiles met hoge kans worden vaker in array geplaatst
		Color[][] tiles = ChopUpTiles();
		Color[][] tilesChances = new Color[tileChance.Sum ()][];
//		Debug.Log ("lengte tileschances" + tilesChances.Length);
		int counter = 0;

		for (int i = 0; i<tileChance.Length; i++) {
			int chance = tileChance[i];
			for (int j = 0; j<chance ; j++){
				int temp = i+ counter;
//				Debug.Log ("i " +i);
//				Debug.Log ("counter " + counter);
//				Debug.Log ("i+counter " + temp);
//				Debug.Log ("j " +j);
				tilesChances[i+counter+j] = tiles[i];
			}
			counter = counter + chance - 1;
		}
		for (int y=0; y<size_z; y++) {
			for (int x=0; x<size_x; x++) {
				Color[] p = tilesChances[Random.Range(0,tileChance.Sum ())];
				texture.SetPixels(x*tileResolution,y*tileResolution,tileResolution,tileResolution,p);
			}
		}
		texture.filterMode = FilterMode.Bilinear;
		texture.Apply ();

		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;
	}

	public void BuildMesh() {

		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;
		
		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;
		
		// Generate the mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		
		int[] triangles = new int[ numTris * 3 ];
		
		int x, z;
		for(z=0; z < vsize_z; z++) {
			for(x=0; x < vsize_x; x++) {
				vertices[ z * vsize_x + x ] = new Vector3( x*tileSize, 0, z*tileSize );
				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / size_x, (float)z / size_z );
			}
		}
		
		for(z=0; z < size_z; z++) {
			for(x=0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;
				
				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + x + 		   1;
			}
		}

		
		// Create a new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// Assign our mesh to our filter/renderer/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;

		Builtexture();
	}

}

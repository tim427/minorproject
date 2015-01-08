using UnityEngine;
using System.Collections;
using System.Linq;

public class WallProceduralTexturing : MonoBehaviour {
	
	
	public int numTilesX;
	public int numTilesZ;
	public Texture2D wallTiles;
	public int tilePixelWidth;
	public int tilePixelHeight;
	public int tileOffset;
	public int[] tileChance;
	private MeshFilter mf;
	private Mesh mesh;
	private Vector2[] uvs;
	private float frontbound;
	private float sidebound;
	
	
	// Use this for initialization
	public void Start () {
		BuildTexture();
		
		UVMapping();
	}
	
	public void SetTileChances() {
		
		tileChance = new int[ChopUpTiles().Length];
		
		for (int c=0; c<ChopUpTiles().Length; c++) {
			tileChance[c] = 1;
		}
		
	}
	
	Color[][] ChopUpTiles() {
		
		int numTilesPerRow = wallTiles.width / tilePixelWidth;
		int numRows = wallTiles.height / tilePixelHeight;
		
		Color[][] tiles = new Color[numTilesPerRow*numRows][];
		
		for (int y=0; y<numRows; y++) {
			for (int x=0; x<numTilesPerRow; x++) {
				tiles[y*numTilesPerRow + x] = wallTiles.GetPixels(x*tilePixelWidth,y*tilePixelHeight,tilePixelWidth,tilePixelHeight);
			}
		}
		
		return tiles;
	}
	
	public void BuildTexture () {
		
		int numTilesPerRow = wallTiles.width / tilePixelWidth;
		int numRows = wallTiles.height / tilePixelHeight;
		
		int texWidth = numTilesX * tilePixelWidth;
		int texHeight = numTilesZ * tilePixelHeight;
		Texture2D texture = new Texture2D(texWidth,texHeight);
		
		//maak Color array aan met tiles, tiles met hoge kans worden vaker in array geplaatst
		Color[][] tiles = ChopUpTiles();
		Color[][] tilesChances = new Color[tileChance.Sum ()][];
		int counter = 0;
		
		for (int i = 0; i<tileChance.Length; i++) {
			int chance = tileChance[i];
			for (int j = 0; j<chance ; j++){
				int temp = i+ counter;
				tilesChances[i+counter+j] = tiles[i];
			}
			counter = counter + chance - 1;
		}
		
		// plak texture tegel voor tegel op cube
		
		
		int offset = 0;
		for (int y=0; y<numTilesZ; y++) {
			
			// eerste kolom invullen
			Color[] p = tilesChances[Random.Range(0,tileChance.Sum ())];
			texture.SetPixels(0,y*tilePixelHeight,tilePixelWidth,tilePixelHeight,p);
			
			
			offset = offset + tileOffset;
			if (offset>tilePixelWidth){
				offset = offset-tilePixelWidth;
			}
			
			// laatste kolom invullen
			int randx = Random.Range(0,numTilesPerRow-1);
			int randy = Random.Range(0,numRows-1);
			p = wallTiles.GetPixels(randx*tilePixelWidth,randy*tilePixelHeight,offset,tilePixelHeight);
			texture.SetPixels((numTilesX)*tilePixelWidth-offset,y*tilePixelHeight,offset,tilePixelHeight,p);
			
			// middelste kolommen invullen
			for (int x=1; x<numTilesX; x++) {
				p = tilesChances[Random.Range(0,tileChance.Sum ())];
				texture.SetPixels(x*tilePixelWidth-offset,y*tilePixelHeight,tilePixelWidth,tilePixelHeight,p);
				
			}
			
			
		}
		
		
		// benodigde texture instellingen
		texture.filterMode = FilterMode.Bilinear;
		texture.Apply ();
		
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;
		
		
		
	}
	
	public void UVMapping(){
		
		
		mf = GetComponent<MeshFilter> ();
		if (mf != null){
			mesh = mf.sharedMesh;
		}
		
		frontbound=1.0f;
		sidebound=1.0f;
		
		
		
		if (transform.localScale.x<transform.localScale.z){
			frontbound = transform.localScale.x/transform.localScale.z;
		}
		
		if (transform.localScale.z<transform.localScale.x){
			sidebound = transform.localScale.z/transform.localScale.x;
		}
		
		
		if (mesh == null || mesh.uv.Length != 24) {
			print ("script needs toMainMenu Behaviour attached toMainMenu built-int Cube");
			return;
		}
		
		uvs = mesh.uv;
		
		
		//front
		uvs[0]  = new Vector2(0.0f, 0.0f);
		uvs[1]  = new Vector2(frontbound, 0.0f);
		uvs[2]  = new Vector2(0.0f, 1.0f);
		uvs[3]  = new Vector2(frontbound, 1.0f);
		
		// Back
		uvs[10] = new Vector2(0.0f, 0.0f);
		uvs[11] = new Vector2(frontbound, 0.0f);
		uvs[6]  = new Vector2(0.0f, 1.0f);
		uvs[7]  = new Vector2(frontbound, 1.0f);
		
		// Left
		uvs[16] = new Vector2(0.0f, 0.0f);
		uvs[18] = new Vector2(sidebound, 0.0f);
		uvs[19] = new Vector2(0.0f, 1.0f);
		uvs[17] = new Vector2(sidebound, 1.0f);    
		
		// Right        
		uvs[20] = new Vector2(0.0f, 0.0f);
		uvs[22] = new Vector2(sidebound, 0.0f);
		uvs[23] = new Vector2(0.0f, 1.0f);
		uvs[21] = new Vector2(sidebound, 1.0f);
		
		mesh.uv = uvs;
	}
}

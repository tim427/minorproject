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


	// Use this for initialization
	void Start () {
		BuildTexture();
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

}

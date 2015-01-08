#pragma strict

var uvAnimationTileX = 24; //The number of colums of you sheet
var uvAnimationTileY = 1; // The number of rows of your sheet

var framesPerSecond = 10.0;

function Update () {

	// Calculate index
	var index : int = Time.time * framesPerSecond;
	// Repeat when exhausting all frames
	index = index  % (uvAnimationTileX * uvAnimationTileY);
	
	// Size of every tile
	var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
	
	// Split into horizontal and vertical index
	var uIndex = index % uvAnimationTileX;
	var vIndex = index / uvAnimationTileX;
	
	// Build offset
	// v coordinate is the bottom of the image in opengl so we need to invert
	var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
	
	renderer.material.SetTextureOffset ("_MainTex", offset);
	renderer.material.SetTextureScale ("_MainTex", size);
}
using Godot;
using System;

public partial class map_generation : Node
{
	// 3D model paths:
	private string floorModelPath = "res://_Assets/floor.glb";
	
	
	public int floorXCount = 8;
	public int floorYCount = 8;
	public int floorCount = 256;
	//public PackedScene Floorscene{ get; set; }
	private void MakeFloor(){
		//loads the floor 3d model by creating a scene
		var FloorScene = GD.Load<PackedScene>(floorModelPath);
		
		for(int i = 0; i < floorCount; i++){
			var FloorInstance = FloorScene.Instantiate(); 
			FloorInstance.Name = i.ToString();
			GetNode("Floor").AddChild(FloorInstance);
		}

	}
	private bool floorgeneration = true;
	private int floorindex = 0;
	private void UpdateFloor(){
		
		//positions the generated 64 tiles along a 8x8 grid
		if (floorgeneration && floorindex < floorCount){
			for(int y = -floorYCount; y < floorYCount; y++){
				for(int x = -floorXCount; x < floorXCount; x++){
					GetNode("Floor").GetChild<Node3D>(floorindex).Position = new Vector3(x, 0 , y);
					floorindex ++;
				}
			}
			floorgeneration = false; floorindex = 0;
		} 
		
		// Calculate y-position based on a sine wave
		for (int x = -floorYCount; x < floorYCount; x++)
		{
			float y = Mathf.Sin(floorindex * 0.5f) * 2.0f; // Adjust the frequency and amplitude as needed
			
			for (int z = -floorXCount; z < floorXCount; z++)
			{
				// Set the position of the tile
				GetNode("Floor").GetChild<Node3D>(floorindex).Position = new Vector3(x, y, z);
				floorindex++;
				if(floorindex >= floorCount){floorindex = 0;}

			}
		}

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		MakeFloor();
		
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateFloor();
		
	}
}



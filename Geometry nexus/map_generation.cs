using Godot;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class map_generation : Node
{
	// 3D model paths:
	private string floorModelPath = "res://_Assets/floor.glb";
	
	//dimensions of the board
	public int floorXCount = 8;
	public int floorYCount = 8;
	public int floorCount = 256;
  
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
	
	private Timer timer;
	private async void Wait(float delay)
	{
		while (true)  // Use a condition to stop the loop if needed
		{
			

			// Wait for 1 second
			await ToSignal(GetTree().CreateTimer(delay), "timeout");
		}
	}
	private async void UpdateFloor(){
		
		//positions the generated 64 tiles along a 8x8 grid
		if (floorgeneration && floorindex < floorCount){
			for(int y = -floorYCount; y < floorYCount; y++){
				for(int x = -floorXCount; x < floorXCount; x++){
					//await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
					Node3D Currentnode = GetNode("Floor").GetChild<Node3D>(floorindex);
					
					Currentnode.Position = new Vector3(x, -10 , y);
					
					Tween tween = GetTree().CreateTween();
					tween.TweenProperty(Currentnode, "position", new Vector3(x, 0, y), 1.0f);
					//await ToSignal(GetTree().CreateTimer(0.01f), Timer.SignalName.Timeout);
					
					floorindex ++;
					
					
					// Wait for 1 second

				}
				
			}
			floorgeneration = false; floorindex = 0;
		} 
		
			
			

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		timer = GetNode<Timer>("Timer");
		MakeFloor();
		
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateFloor();
		
	}
}



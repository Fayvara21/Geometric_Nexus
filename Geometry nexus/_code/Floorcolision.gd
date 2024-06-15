extends Area3D

func _ready():
	set_process_input(true)
	
func _input_event(camera, event, position, normal, shape_idx):
	if (event is InputEventMouseButton && event.pressed):
		if mouse_entered:
			print('Clicked', get_parent().name)
	
		
			

		

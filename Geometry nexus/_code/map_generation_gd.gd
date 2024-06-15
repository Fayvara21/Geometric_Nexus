extends Node3D

#taille du tableau
var mapSize = 16
var offset = 0.5
func load_mp3(mp3):
	await get_tree().create_timer(4.5).timeout
	if !$AudioStreamPlayer.is_playing():
		$AudioStreamPlayer.stream = mp3
		$AudioStreamPlayer.play()

func MapInit():
	#charge les modeles 3d
	var Floorscene = preload("res://_Assets/floor.tscn")
	var Floorsound = preload("res://_Assets/card shuffle.mp3")
	
	load_mp3(Floorsound)
	
	#charge les scripts
	await get_tree().create_timer(1.0).timeout
	#crée et charge les Nodes3D des tuiles
	for i in range(mapSize * mapSize):
		#rajoute des noeuds enfants
		var instance = Floorscene.instantiate()
		instance.name = str(i)
		instance.visible = false
		instance.scale = Vector3(0.5,0.5,0.5)
		var floorarea = Area3D.new()
		floorarea.input_ray_pickable = false
		floorarea.set_script(load("res://_code/Floorcolision.gd"))
		var floorcollision = CollisionShape3D.new()
		var floorshape = BoxShape3D.new()
		floorshape.size = Vector3(2.0,0.1,2.0)
		floorcollision.shape = floorshape
		floorarea.add_child(floorcollision)
		instance.add_child(floorarea)
		get_node("Floor").add_child(instance)
		
		
	#positionne et espace les tuiles au centre du terrain
	var floorindex = 0;
	for d in range(mapSize*2 - 1):
		for y in range(mapSize):
			for x in range(mapSize):
				if y+x == d:
					await get_tree().create_timer(0.006).timeout
					var tween = get_tree().create_tween()
					var node = get_node("Floor").get_child(floorindex)
					node.position = Vector3(x - (mapSize/2.0) + offset, -30, y - (mapSize/2.0) + offset)
					node.visible = true
					tween.tween_property(node, "position", Vector3(x - (mapSize/2.0) + offset, 0, y - (mapSize/2.0) + offset), 4.5).set_trans(Tween.TRANS_QUAD)
					floorindex += 1



# Called when the node enters the scene tree for the first time.
func _ready():
	MapInit();


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

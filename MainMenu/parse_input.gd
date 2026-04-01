extends TextureRect

@onready var vp: SubViewport = $"../../../SubViewport"

func _gui_input(event: InputEvent) -> void:
	
	if not is_inside_tree():
		return
	if vp == null or not is_instance_valid(vp) or not vp.is_inside_tree():
		return
	
	if event is InputEventMouse:
		vp.push_input(event)

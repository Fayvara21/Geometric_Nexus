shader_type spatial;

uniform float colorstrength : hint_range(0.0,1.0);
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
METALLIC = 0.5;
ROUGHNESS = 0.7;

//ALBEDO = vec3(0.73 * colorstrength , 1.0 * colorstrength , 0.78 * colorstrength );
}

//void light() {
 ////Called for every pixel for every light affecting the material.
 ////Uncomment to replace the default light processing function with this one.
//
//}

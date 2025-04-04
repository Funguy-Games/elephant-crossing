shader_type canvas_item;

uniform float intensity = 1.0;
uniform float blinkSpeed = 5.0f;
uniform bool isBlinking = false;
void vertex() {
	// Called for every vertex the material is visible on.
	vec3 rgb = COLOR.rgb + intensity * sin(TIME * blinkSpeed) * float(isBlinking);
	COLOR.rgb = rgb;
}

void fragment() {

}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

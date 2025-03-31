shader_type canvas_item;

uniform float fillAmount;

vec3 rgb_to_gray(vec3 rgb) {
	return vec3(rgb.r * 0.299 + rgb.g * 0.587 + rgb.b * 0.114);
}

void fragment() {
	float fill = 1.0f - fillAmount;
	if(UV.g < fill) {
		COLOR.rgb = rgb_to_gray(COLOR.rgb);
	} else if (UV.g < fill + 0.02) {
		COLOR.rgb = vec3(1, 0.69, 0.11);
	}
}



//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

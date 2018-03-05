uniform float uv_fade;
uniform float uv_alpha;

in float dataDate;
out vec4 color;
uniform sampler2D panorama;
uniform sampler2D co2;
uniform vec4 ColorMultiplier;
uniform float displayFraction;
uniform float yearStart;
uniform int	uv_simulationtimeDays;

void main()
{	
	if ((uv_simulationtimeDays/365.25 + 1970.)<dataDate) {
		discard;
	}
	if (dataDate<yearStart) {
		discard;
	}
	color =ColorMultiplier;
	color.a *= uv_fade*uv_alpha;
	//gl_FragColor = color;
}
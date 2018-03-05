uniform float uv_fade;
uniform float uv_alpha;

in vec2 TexCoord;
out vec4 color;
uniform sampler2D panorama;
uniform sampler2D co2;
uniform vec4 ColorMultiplier;
uniform float displayFraction;
uniform int	uv_simulationtimeDays;

void main()
{	

	color =ColorMultiplier;
	color.a *= uv_fade*uv_alpha;
	//gl_FragColor = color;
}
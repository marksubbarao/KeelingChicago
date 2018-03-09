uniform float uv_fade;
uniform float uv_alpha;

in float dataDate;
in float dTf;
out vec4 color;
uniform sampler2D panorama;
uniform sampler2D co2;
uniform sampler2D cmap;
uniform float trailLength;
uniform float trailMin;
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
	color = texture(cmap,vec2(0.5+dTf,0.5));
	color.a *= uv_fade*uv_alpha;
	float fadeTarget=0.1;
	if ((uv_simulationtimeDays/365.25 + 1970.)-dataDate > trailLength) {
		color.a*=trailMin;
	} else {
	  color.a = 1.0- (1.0-trailMin)*((uv_simulationtimeDays/365.25 + 1970.)-dataDate )/trailLength ;
	}

	//gl_FragColor = color;
}
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
	//Read the color from the texture and apply the appropreate color and alpha multipliers
	color = texture2D(panorama,TexCoord);
	float yearFrag = uv_simulationtimeDays/365.25 - 4.0;// time starts in 1970, our image texture in 1974
	float xSample=yearFrag/44.;
	float ySample=fract(yearFrag);
	vec4 dataColor=textureGrad(co2,vec2(xSample,1-ySample),vec2(0.),vec2(0.));
	float co2Value=300.+256.*dataColor.r+dataColor.g;//unpacking the texture encoding	
	float co2fac=(co2Value-300.)/100.;
	// OK' Lets Turn the Blue Sky Red
	float blueFac=clamp(4.0*(color.b-0.75*color.r-0.25*color.g),0,3.0);
	if (TexCoord[0]<displayFraction&&blueFac>0.5&&xSample>0.0&&xSample<1.0){
		color = mix(color,color.bgra,10.*co2fac);	
		//color=vec4(vec3(co2fac),1);
		//color=color.bgra;
	}
	//color *= vec4(0.0,0.0,blueFac,1.0);
	color.a *= uv_fade*uv_alpha;
	//gl_FragColor = color;
}
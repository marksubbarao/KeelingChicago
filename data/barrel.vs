in vec3 vertex;
uniform vec4 uv_cameraPos;
uniform mat4 uv_modelViewInverseMatrix;
uniform mat4 uv_modelViewProjectionMatrix;
uniform mat4 uv_scene2ObjectMatrix;
uniform float Scale;
uniform vec3 RotationAxis;
uniform float RotationAngle;
uniform float spiralRadius;
uniform float spiralOffset;
uniform float spiralAngle;
in float year;
in float month;
out float dataDate;
in float dT;
out float dTf;
const float PI = 3.1415926535897932384626433;
const float DEG2PI = PI / 180.0;

out vec2 TexCoord;

mat4 getRotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    return mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
}

void main()
{  
  //Calculate the distance between the camera and the center of the panorama to fad out the panorama
  //float cameraDistance = length((uv_modelViewInverseMatrix * vec4(0.0, 0.0, 0.0, 1.0)).xyz) / Scale; 
  //DistanceFade = smoothstep(1, 0, cameraDistance);  
  vec3 center =   (uv_scene2ObjectMatrix * uv_cameraPos).xyz;
  float theta = spiralAngle*vertex[1]+spiralOffset;
  float x = spiralRadius*cos(vertex[0])*cos(theta);
  float y = spiralRadius*sin(vertex[0])*cos(theta);
  float z = spiralRadius*sin(theta);
  vec3 pos = 0.01*vec3(x,z,y);
  //Rotate the sphere the specified angle about the specified axis
  gl_Position = uv_modelViewProjectionMatrix *(Scale *(getRotationMatrix(RotationAxis, RotationAngle)*vec4(pos,0.0))+vec4(center, 1.0));    		  
  dataDate=year+(month-1)/12.;
  dTf=dT;
}
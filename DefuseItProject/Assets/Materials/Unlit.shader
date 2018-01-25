// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Outlined/Unlit" {
     Properties {
         _Color ("Main Color", Color) = (.5,.5,.5,1)
         _OutlineColor ("Outline Color", Color) = (0,0,0,1)
         _Outline ("Outline width", Range (0.0, 0.1)) = .005
     }
  
 CGINCLUDE
 #include "UnityCG.cginc"
  
 struct appdata {
     float4 vertex : POSITION;
     float3 normal : NORMAL;
 };
  
 struct v2f {
     float4 pos : POSITION;
     float4 color : COLOR;
 };
  
 uniform float _Outline;
 uniform float4 _OutlineColor;
  
 v2f vert(appdata v) {
     // just make a copy of incoming vertex data but scaled according to normal direction
     v2f o;
     o.pos = UnityObjectToClipPos(v.vertex);
  
     float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
     float2 offset = TransformViewToProjection(norm.xy);
  
     o.pos.xy += offset * o.pos.z * _Outline;
     o.color = _OutlineColor;
     return o;
 }
 ENDCG
  
     SubShader {
         Tags {"Queue" = "Transparent"}
  
         // note that a vertex shader is specified here but its using the one above
         Pass {
             Name "OUTLINE"
             Tags { "LightMode" = "Always" }
             Cull Off
             ZWrite Off
             ZTest Always
             ColorMask RGB // alpha not used
  
             // you can choose what kind of blending mode you want for the outline
             Blend SrcAlpha OneMinusSrcAlpha // Normal
             //Blend One One // Additive
             //Blend One OneMinusDstColor // Soft Additive
             //Blend DstColor Zero // Multiplicative
             //Blend DstColor SrcColor // 2x Multiplicative
  
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             
             half4 frag(v2f i) :COLOR {
                 return i.color;
             }
             ENDCG
         }
  
         CGPROGRAM
           #pragma surface surf NoLighting noambient
   		   #include "UnityCG.cginc"

   		   fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		    {
		        fixed4 c;
		        c.rgb = s.Albedo; 
		        c.a = s.Alpha;
		        return c;
		    }

           fixed4 _Color;
   
           struct Input {
               fixed4 _Color;
           };
   
           void surf (Input IN, inout SurfaceOutput o) {
               o.Albedo = _Color;
           }
         ENDCG
     }
  
     
 }
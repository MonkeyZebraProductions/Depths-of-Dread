Shader "FX/Hologram Shader"
{
	Properties
	{
		_Color("Color", Color) = (0, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_AlphaTexture("Alpha Mask (R)", 2D) = "white" {}
		//Alpha Mask Properties
		_Scale("Alpha Tiling", Float) = 3
		_ScrollSpeedV("Alpha scroll Speed", Range(0, 5.0)) = 1.0
			// Glow
			_GlowIntensity("Glow Intensity", Range(0.01, 1.0)) = 0.5
			// Glitch
			_GlitchSpeed("Glitch Speed", Range(0, 50)) = 50.0
			_GlitchIntensity("Glitch Intensity", Range(0.0, 0.1)) = 0

	    [Enum(UnityEngine.Rendering.BlendMode)]
		_SrcBlend("SrcFactor", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)]
		_DstBlend("DstFactor", Float) = 1

	    _Sections("Sections", Range(2, 200)) = 100
	    _AlphaAmount("AlphaAmount", Range(0, 0.99)) = 0
	}

		SubShader
		{
			Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

			Pass
			{
				Lighting Off
				ZWrite On
				Blend[_SrcBlend][_DstBlend]
				ZTest Always
				

				CGPROGRAM

					#pragma vertex vertexFunc
					#pragma fragment fragmentFunc

					#include "UnityCG.cginc"

					struct appdata {
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
						float3 normal : NORMAL;
					};

					struct v2f {
						float4 position : SV_POSITION;
						float2 uv : TEXCOORD0;
						float3 grabPos : TEXCOORD1;
						float3 viewDir : TEXCOORD2;
						float3 worldNormal : NORMAL;
					};

					fixed4 _Color, _MainTex_ST;
					sampler2D _MainTex, _AlphaTexture;
					half _Scale, _ScrollSpeedV, _GlowIntensity, _GlitchSpeed, _GlitchIntensity;
					float _Sections,_AlphaAmount;

					v2f vertexFunc(appdata IN) {
						v2f OUT;

						//Glitch
						IN.vertex.z += sin(_Time.y * _GlitchSpeed * 5 * IN.vertex.y) * _GlitchIntensity;

						OUT.position = UnityObjectToClipPos(IN.vertex);
						OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

						OUT.grabPos = UnityObjectToViewPos(IN.vertex);

						OUT.worldNormal = UnityObjectToWorldNormal(IN.normal);
						OUT.viewDir = normalize(UnityWorldSpaceViewDir(OUT.grabPos.xyz));

						return OUT;
					}

					fixed4 fragmentFunc(v2f IN) : SV_Target{

						half dirVertex = (dot(IN.grabPos, 1.0) + 1) / 2;

						//fixed4 alphaColor = tex2D(_AlphaTexture,  IN.grabPos.xy * _Scale);
						float4 tanCol = clamp(_AlphaAmount, abs(tan((IN.uv.y - ((_Time.x) * _ScrollSpeedV)) * _Sections)), 1);
						tanCol *= _Color;
						fixed4 pixelColor = tex2D(_MainTex, IN.uv) * tanCol;
						//pixelColor.w = alphaColor.w;

						// Rim Light
						half rim = 1.0 - saturate(dot(IN.viewDir, IN.worldNormal));

						return pixelColor * _Color * (rim + _GlowIntensity);
					}
				ENDCG
			}
		}
}
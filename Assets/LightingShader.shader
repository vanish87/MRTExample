Shader "Custom/LightingShader" {
	Properties {
		
	}
	CGINCLUDE
		#include "UnityCG.cginc"

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		struct PixelOutput {
			float4 col0 : SV_Target0;
			float4 col1 : SV_Target1;
		};

		sampler2D _MainTex;
		sampler2D _Tex0;
		sampler2D _Tex1;

		v2f vert(appdata_img v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.texcoord.xy;
			return o;
		}
		
		float4 fragShowMRT(v2f pixelData) : COLOR0
		{
			//return tex2D(_Tex0, pixelData.uv);
			//return tex2D(_Tex1, pixelData.uv);

			float4 normalWS = tex2D(_Tex1, pixelData.uv);
			float4 col = float4(0.1,0.1,0.1,1);
			if (normalWS.x == 0 && normalWS.y == 0 && normalWS.z == 0) return col;
			normalWS.w = 0;
			float4 snowDirection = float4(0, 1, 0, 0);
			float4 SnowColor = float4(1,1,1,1);

			half difference = dot(normalWS.xyz, snowDirection.xyz);
			difference = saturate(difference / 0.3);
			col.rgb = difference*SnowColor.rgb + (1 - difference) *col;
			//col = normalWS;
			col.a = 1;

			return col;
		}

	ENDCG
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass{
			Cull Off ZWrite Off ZTest Always
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment fragShowMRT
			#pragma target 3.0
			ENDCG
		}
	}
	FallBack "Diffuse"
}

// Default Sprite Shader combined with an indexed color texture (Palette)
Shader "Custome/JoppeTest"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "white" {}
		_Palette ("Palette Texture", 2D) = "white" {}
		_Tint ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM

			//Run Shader programs
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			//Define Structs used by the shader programs
			struct appdata
			{
				float4 vertex   : POSITION;
				float3 normal : NORMAL;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float3 normal : NORMAL;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 tangent : TANGENT;
			};
			
			fixed4 _Tint;
			sampler2D _MainTex;
			sampler2D _Palette;
			sampler2D _NormalMap;
			float4 _Palette_ST; 

			v2f vert(appdata IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				// Removed the tinting here, since it would tint the palette map
				//OUT.color = IN.color;


				//OUT.color.xyz = IN.normal;
				float3 bitangent = cross( IN.normal, IN.tangent.xyz ) * IN.tangent.w;
				OUT.color.xyz = bitangent * 0.5 + 0.5;
				OUT.color.w = 1.0;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 paletteMapColor = tex2D(_MainTex, IN.texcoord) * IN.color;
				
				// The alpha channel of the palette map points to UVs in the palette key.
				float paletteX = paletteMapColor.a;
				float2 paletteUV = float2(paletteX, 0.0f);
				// Get the palette's UV accounting for texture tiling and offset
				float2 paletteUVTransformed = TRANSFORM_TEX(paletteUV, _Palette);
				
				// Get the color from the palette key
				fixed4 outColor = tex2D(_Palette, paletteUVTransformed);
				
				// Apply the tint to the final color
				outColor *= _Tint;
				return outColor;
			}
		ENDCG
		}

	}
}

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Spelonko/IceCrystal"
{
	Properties
	{
		_MainTex("Albdeo", 2D) = "white" {}
		_OffsetTex("Refraction Map", 2D) = "white" {}
		_AmbientTex("Ambient Reflections", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
			"DisableBatching" = "True"
		}

		Pass
		{
			ZWrite Off
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				half4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 screenuv : TEXCOORD1;
				half4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenuv = ((o.vertex.xy / o.vertex.w) + 1) * 0.5;
				o.color = v.color;
				return o;
			}

			float2 safemul(float4x4 M, float4 v)
			{
				float2 r;

				r.x = dot(M._m00_m01_m02, v);
				r.y = dot(M._m10_m11_m12, v);

				return r;
			}

			sampler2D _MainTex;
			sampler2D _OffsetTex;
			sampler2D _AmbientTex;

			uniform sampler2D _GlobalRefractionTex;
			uniform float _GlobalVisibility;
			uniform float _GlobalRefractionMag;

			float4 frag(v2f i) : SV_Target
			{
				return tex2D(_GlobalRefractionTex, i.screenuv);
			}
			ENDCG
		}
	}

	Fallback "Sprites/Default"
}
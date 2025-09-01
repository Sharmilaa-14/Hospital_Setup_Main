Shader "Custom/ColumnGlowBlink"
{ 
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_Thickness("Thickness", Range(0, 1)) = 0.5
		_FadeStart("Fade Start", Range(0, 1)) = 0.5
		_FadeEnd("Fade End", Range(-1, 1)) = 0.5
		_Intensity("Base Intensity", Range(0, 5)) = 1.0
		_BlinkSpeed("Blink Speed", Range(0, 10)) = 2.0
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent" }
		Blend SrcAlpha One
		Cull Off Lighting Off ZWrite On

		LOD 0

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 origPosition : POSITION1;
				float3 eyeDir : DIRECTION;
			};

			fixed4 _TintColor;
			float _Thickness;
			float _FadeStart;
			float _FadeEnd;
			float _Intensity;
			float _BlinkSpeed;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = normalize(mul(UNITY_MATRIX_IT_MV, v.normal).xyz);
				o.origPosition = v.vertex;
				o.eyeDir = -normalize(mul(UNITY_MATRIX_MV, v.vertex).xyz);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float d = dot(i.normal, i.eyeDir);

				// Shape intensity
				float p = smoothstep(0, _Thickness, d) * smoothstep(_FadeStart, _FadeEnd, i.origPosition.y);

				// Blinking effect with sin wave (0..1)
				float blink = 0.5 + 0.5 * sin(_Time.y * _BlinkSpeed);

				// Final color with blink applied
				return float4((p * _TintColor.rgb * _Intensity * blink), p);
			}
			ENDCG
		}
	}
}

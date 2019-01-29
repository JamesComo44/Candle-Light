Shader "Candle/ToonRamp" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Colour("Colour", Color) = (1, 1, 1, 1)
		_RampTex("Ramp Texture", 2D) = "white"{}
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" }

		CGPROGRAM

		#pragma surface surf ToonRamp

		float4 _Colour;
		sampler2D _RampTex;
		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
		};

		float4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float diff = dot(s.Normal, lightDir);
			float h = diff * 0.5 + 0.5;
			float2 rh = h;
			float3 ramp = tex2D(_RampTex, rh).rgb;
			float4 c;
			c.rgb = s.Albedo * _LightColor0 * (ramp);
			c.a = s.Alpha;
			return c;
		}
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgba * _Colour.rgba;
			//o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
	}
		ENDCG
	}
		FallBack "Diffuse"
}


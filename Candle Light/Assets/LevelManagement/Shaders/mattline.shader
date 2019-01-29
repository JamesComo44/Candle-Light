Shader "Custom/mattline" {


	Properties{
	 _MainTex("Texture", 2D) = "white" {}
	 _InnerColor("Inner Color", Color) = (0,0,0,1)
	 _OutlineColor("Outline Color", Color) = (0,0,0,1)
	 _Outline("Outline Width", Range(0, 0.001)) = .005
	}

		SubShader{
		   Tags { "Queue" = "Transparent" }
		   ZWrite off
		   CGPROGRAM
			   #pragma surface surf Lambert vertex:vert
			   struct Input {
				   float2 uv_MainTex;
			   };
			   float _Outline;
			   float4 _OutlineColor;
			   float4 _InnerColor;
			   void vert(inout appdata_full v) {
				   v.vertex.xyz += v.normal * _Outline;
			   }
			   sampler2D _MainTex;
			   void surf(Input IN, inout SurfaceOutput o)
			   {
				   o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				   o.Emission = _OutlineColor.rgb;
				   
			   }
		   ENDCG

		   ZWrite on

		   CGPROGRAM
			   #pragma surface surf Lambert
			   struct Input {
				   float2 uv_MainTex;
			   };
			   float4 _OutlineColor;
			   float4 _InnerColor;
			   sampler2D _MainTex;
			   void surf(Input IN, inout SurfaceOutput o) {
				   o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				   o.Emission = _InnerColor.rgb;
			   }
		   ENDCG

	 }
		 Fallback "Diffuse"
}



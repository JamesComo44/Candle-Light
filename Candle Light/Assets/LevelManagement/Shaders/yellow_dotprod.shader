Shader "Custom/yellow_dotprod" {
	
	SubShader {
		

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		struct Input {
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			half dotp = dot(IN.viewDir, o.Normal);
			o.Albedo = float3(1-dotp,1-dotp,0);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

Shader "Custom/DiffuseColor" {
	Properties
	{
		_Color("Color", Color) = (1.0, 0.6, 0.6, 1.0)
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("Normal", 2D) = "bump" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
#pragma surface surf Lambert finalcolor:mycolor
		struct Input {
			float2 uv_Albedo;
			float2 uv_NormalMap;
		};

		fixed4 _Color;
		void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color *= _Color;
		}

		sampler2D _Albedo;
		sampler2D _NormalMap;
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_Albedo, IN.uv_Albedo).rgb;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
		}
	ENDCG
	}
	Fallback "Diffuse"
}
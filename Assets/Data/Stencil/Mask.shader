Shader "Custom/Mask" {
	
	
	SubShader{
		Tags{ "RenderType" = "Transparent" }
		Tags{ "ForceNoShadowCasting" = "True" }
		Stencil
		{
			Ref 1
			Comp Always
			Pass Replace
		}

		Tags{ "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
		ColorMask 0 // Don't write to any colour channels
		ZWrite Off // Don't write to the Depth buffer

		CGPROGRAM
#pragma surface surf Lambert noshadow alpha

		struct Input {
		fixed3 Albedo;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = fixed3(1, 1, 1);
		o.Alpha = 0;
	}
	ENDCG
	}
		FallBack "Diffuse"
}

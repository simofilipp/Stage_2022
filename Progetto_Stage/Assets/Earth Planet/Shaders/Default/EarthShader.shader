// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EarthShader"
{
	Properties
	{
		_EarthMap("EarthMap", 2D) = "white" {}
		_NightMap("NightMap", 2D) = "white" {}
		_NightMult("NightMult", Range( 0 , 10)) = 0.85
		_DayNightContrast("DayNight Contrast", Range( 0 , 50)) = 7
		_WaterMask("WaterMask", 2D) = "white" {}
		_EarthNormalMap("EarthNormalMap", 2D) = "bump" {}
		_WaterNormal("WaterNormal", Range( 0 , 1)) = 0.03
		_LandNormal("LandNormal", Range( 0 , 1)) = 0.1
		_WaterSmoothness("WaterSmoothness", Range( 0 , 1)) = 0.7
		_LandSmoothness("LandSmoothness", Range( 0 , 1)) = 0.4
		_AtmosphereColor("AtmosphereColor", Color) = (0.2392157,0.6392158,1,1)
		_AtmospherePower("AtmospherePower", Range( 0 , 10)) = 1.2
		_AtmosphereScale("AtmosphereScale", Range( 0 , 10)) = 1.75
		_AtmosphereDarkSide("AtmosphereDarkSide", Range( 0 , 1)) = 0.423
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _EarthNormalMap;
		uniform float4 _EarthNormalMap_ST;
		uniform float _LandNormal;
		uniform float _WaterNormal;
		uniform sampler2D _WaterMask;
		uniform float4 _WaterMask_ST;
		uniform sampler2D _NightMap;
		uniform float4 _NightMap_ST;
		uniform float _NightMult;
		uniform sampler2D _EarthMap;
		uniform float4 _EarthMap_ST;
		uniform float _DayNightContrast;
		uniform float _AtmosphereScale;
		uniform float _AtmospherePower;
		uniform float4 _AtmosphereColor;
		uniform float _AtmosphereDarkSide;
		uniform float _LandSmoothness;
		uniform float _WaterSmoothness;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color3 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
			float2 uv_EarthNormalMap = i.uv_texcoord * _EarthNormalMap_ST.xy + _EarthNormalMap_ST.zw;
			float3 tex2DNode23 = UnpackNormal( tex2D( _EarthNormalMap, uv_EarthNormalMap ) );
			float4 lerpResult49 = lerp( color3 , float4( tex2DNode23 , 0.0 ) , _LandNormal);
			float4 lerpResult47 = lerp( color3 , float4( tex2DNode23 , 0.0 ) , _WaterNormal);
			float2 uv_WaterMask = i.uv_texcoord * _WaterMask_ST.xy + _WaterMask_ST.zw;
			float4 tex2DNode24 = tex2D( _WaterMask, uv_WaterMask );
			float4 lerpResult51 = lerp( lerpResult49 , lerpResult47 , tex2DNode24);
			o.Normal = lerpResult51.rgb;
			float2 uv_NightMap = i.uv_texcoord * _NightMap_ST.xy + _NightMap_ST.zw;
			float4 temp_output_18_0 = ( tex2D( _NightMap, uv_NightMap ) * _NightMult );
			float2 uv_EarthMap = i.uv_texcoord * _EarthMap_ST.xy + _EarthMap_ST.zw;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult17 = dot( ase_worldlightDir , ase_worldNormal );
			float4 temp_cast_3 = (dotResult17).xxxx;
			float4 clampResult43 = clamp( CalculateContrast(_DayNightContrast,temp_cast_3) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult4 = lerp( temp_output_18_0 , tex2D( _EarthMap, uv_EarthMap ) , clampResult43);
			o.Albedo = lerpResult4.rgb;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV57 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode57 = ( 0.0 + _AtmosphereScale * pow( 1.0 - fresnelNdotV57, _AtmospherePower ) );
			float4 temp_output_63_0 = ( fresnelNode57 * _AtmosphereColor );
			float fresnelNdotV96 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode96 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV96, 1.0 ) );
			float4 lerpResult65 = lerp( ( temp_output_63_0 * fresnelNode96 ) , ( dotResult17 * temp_output_63_0 ) , ( 1.0 - _AtmosphereDarkSide ));
			float4 clampResult92 = clamp( lerpResult65 , float4( 0,0,0,1 ) , float4( 1,1,1,1 ) );
			o.Emission = ( ( ( 1.0 - clampResult43 ) * temp_output_18_0 ) + clampResult92 ).rgb;
			o.Metallic = 0.0;
			float lerpResult54 = lerp( _LandSmoothness , _WaterSmoothness , tex2DNode24.r);
			o.Smoothness = lerpResult54;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
-1830;320;1718;878;-158.3905;11.92218;1.3;True;False
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;15;-194.0422,-744.1138;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;16;-185.1312,-555.8136;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;60;966.2259,-224.6997;Inherit;False;Property;_AtmospherePower;AtmospherePower;11;0;Create;True;0;0;0;False;0;False;1.2;1.2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;964.7723,-354.7121;Inherit;False;Property;_AtmosphereScale;AtmosphereScale;12;0;Create;True;0;0;0;False;0;False;1.75;1.75;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;17;69.87811,-664.0692;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;57;1301.078,-359.563;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;62;1154.534,-105.8389;Inherit;False;Property;_AtmosphereColor;AtmosphereColor;10;0;Create;True;0;0;0;False;0;False;0.2392157,0.6392158,1,1;0.2392157,0.6392158,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-0.2740768,-411.0257;Float;False;Property;_DayNightContrast;DayNight Contrast;3;0;Create;True;0;0;0;False;0;False;7;7;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;8;385.9743,-672.3835;Inherit;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;1611.177,-262.7995;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;66;1455.66,43.70599;Inherit;False;Property;_AtmosphereDarkSide;AtmosphereDarkSide;13;0;Create;True;0;0;0;False;0;False;0.423;0.423;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;96;1617.283,-504.2598;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;524.4537,-990.3504;Inherit;True;Property;_NightMap;NightMap;1;0;Create;True;0;0;0;False;0;False;-1;14abb0596b07d1f41b3c28fc41d0b5a7;14abb0596b07d1f41b3c28fc41d0b5a7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;1875.871,-93.69704;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;67;1749.704,36.71761;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;43;743.6908,-622.3689;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;2159.76,-297.7076;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;530.6058,-771.3463;Float;False;Property;_NightMult;NightMult;2;0;Create;True;0;0;0;False;0;False;0.85;0.85;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;587.9959,367.6404;Float;False;Property;_WaterNormal;WaterNormal;6;0;Create;True;0;0;0;False;0;False;0.03;0.03;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;21;1015.843,-614.9991;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;50;596.3315,638.3582;Float;False;Property;_LandNormal;LandNormal;7;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;764.3363,126.7477;Float;False;Constant;_Color0;Color 0;0;0;Create;True;0;0;0;False;0;False;0,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;930.5309,-942.4436;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;23;571.0692,444.9096;Inherit;True;Property;_EarthNormalMap;EarthNormalMap;5;0;Create;True;0;0;0;False;0;False;-1;1db77afdc9b714c49a4728890de35977;1db77afdc9b714c49a4728890de35977;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;65;2543.467,-115.0418;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;92;2776.585,-175.7076;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,1;False;2;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;1414.003,-620.4317;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;49;1009.415,540.4409;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;47;1017.316,358.902;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.567;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;999.311,796.8622;Float;False;Property;_WaterSmoothness;WaterSmoothness;8;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;1001.935,719.7249;Float;False;Property;_LandSmoothness;LandSmoothness;9;0;Create;True;0;0;0;False;0;False;0.4;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;616.6603,-1265.538;Inherit;True;Property;_EarthMap;EarthMap;0;0;Create;True;0;0;0;False;0;False;-1;286b6e6f63cf3dc479d05fdb0cb7a020;286b6e6f63cf3dc479d05fdb0cb7a020;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;24;985.348,868.2402;Inherit;True;Property;_WaterMask;WaterMask;4;0;Create;True;0;0;0;False;0;False;-1;cf3f504ff7142c74bb1a1cff47525087;cf3f504ff7142c74bb1a1cff47525087;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;1445.393,-1033.135;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;54;1431.687,725.0565;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;51;1418.806,322.2994;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;2953.219,-319.8319;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;79;2870.215,41.74731;Inherit;False;Constant;_Metallic;Metallic;14;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;98;3463.423,-204.1288;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;EarthShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;57;2;59;0
WireConnection;57;3;60;0
WireConnection;8;1;17;0
WireConnection;8;0;22;0
WireConnection;63;0;57;0
WireConnection;63;1;62;0
WireConnection;64;0;17;0
WireConnection;64;1;63;0
WireConnection;67;0;66;0
WireConnection;43;0;8;0
WireConnection;97;0;63;0
WireConnection;97;1;96;0
WireConnection;21;0;43;0
WireConnection;18;0;9;0
WireConnection;18;1;19;0
WireConnection;65;0;97;0
WireConnection;65;1;64;0
WireConnection;65;2;67;0
WireConnection;92;0;65;0
WireConnection;20;0;21;0
WireConnection;20;1;18;0
WireConnection;49;0;3;0
WireConnection;49;1;23;0
WireConnection;49;2;50;0
WireConnection;47;0;3;0
WireConnection;47;1;23;0
WireConnection;47;2;48;0
WireConnection;4;0;18;0
WireConnection;4;1;10;0
WireConnection;4;2;43;0
WireConnection;54;0;53;0
WireConnection;54;1;25;0
WireConnection;54;2;24;0
WireConnection;51;0;49;0
WireConnection;51;1;47;0
WireConnection;51;2;24;0
WireConnection;61;0;20;0
WireConnection;61;1;92;0
WireConnection;98;0;4;0
WireConnection;98;1;51;0
WireConnection;98;2;61;0
WireConnection;98;3;79;0
WireConnection;98;4;54;0
ASEEND*/
//CHKSM=261FB8D7C1B87D3CD14C57BE888526CCD8EB0655
﻿Shader "Hidden/HeatCameraEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement texture", 2D) = "white"{}
		_Magnitude("Magnitude", Range(0,0.1)) = 1
		_Velocity("Magnitude", Range(0,1)) = .5
    }
    SubShader
    {
       

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _DisplaceTex;
			float _Magnitude;
			float _Velocity;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 distuv = float2(i.uv.x + _Time.x * _Velocity, i.uv.y + _Time.y * _Velocity);
				//Almacenamos el mapa UV de la imagen de DisplaceTex
                float2 disp = tex2D(_DisplaceTex, distuv).xy;
				//Cambiamos el rango de 0:1 a -1:1
				disp = ((disp * 2) - 1)* _Magnitude; 

                //Almacenamos la textura principal añadiendole el desplazamiento
				float4 col = tex2D(_MainTex, i.uv + disp);
                return col;
            }
            ENDCG
        }
    }
}

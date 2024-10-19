Shader "Custom/XenonLightShader"
{
    Properties
    {
        _Color ("Light Color", Color) = (0.8, 0.9, 1, 1)   // Color frío, cercano al azul
        _ExternalIntensity ("External Intensity", Range(0.0, 1.0)) = 1.0 // Intensidad controlada por el script
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _ExternalIntensity;  // Valor que será controlado por el script
            float4 _Color;             // Color de la luz

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Multiplicamos el color de la luz por la intensidad que llega desde el script
                return _Color * _ExternalIntensity;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

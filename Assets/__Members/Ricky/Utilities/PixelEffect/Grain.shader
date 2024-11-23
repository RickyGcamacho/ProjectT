Shader "Custom/GrainEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GrainIntensity ("Grain Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _GrainIntensity;

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

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture
                float4 color = tex2D(_MainTex, i.uv);

                // Generate grain noise
                float grain = rand(i.uv * _Time.y * 100.0);

                // Mix grain with the original color
                color.rgb += (grain - 0.5) * _GrainIntensity;

                return color;
            }
            ENDCG
        }
    }
}

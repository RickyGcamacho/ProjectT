Shader "Custom/VHSEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _ScanlineTex ("Scanline Texture", 2D) = "white" {}
        _VerticalLinesTex ("Vertical Lines Texture", 2D) = "white" {}
        _HorizontalLinesTex ("Horizontal Lines Texture", 2D) = "white" {} // Textura de líneas horizontales
        _DistortionAmount ("Distortion Amount", Float) = 0.1
        _ScanlineSpeed ("Scanline Speed", Float) = 0.5
        _NoiseAmount ("Noise Amount", Float) = 0.1
        _LineAmplitude ("Line Amplitude", Float) = 0.2 // Amplitud del parpadeo de líneas verticales
        _HorizontalLineAmplitude ("Horizontal Line Amplitude", Float) = 0.2 // Amplitud del parpadeo de líneas horizontales
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

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _ScanlineTex;
            sampler2D _VerticalLinesTex; // Textura de líneas verticales
            sampler2D _HorizontalLinesTex; // Textura de líneas horizontales
            float _DistortionAmount;
            float _ScanlineSpeed;
            float _NoiseAmount;
            float _LineAmplitude; // Amplitud del parpadeo de líneas verticales
            float _HorizontalLineAmplitude; // Amplitud del parpadeo de líneas horizontales

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

            // Función para generar un valor aleatorio basado en las coordenadas UV
            float random(float2 uv)
            {
                return frac(sin(dot(uv.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Distorsión de la señal
                uv.x += sin(uv.y * 10.0) * _DistortionAmount;

                fixed4 col = tex2D(_MainTex, uv);

                // Añadir ruido configurable
                float noise = tex2D(_NoiseTex, uv * 10.0 + _Time.y * 0.5).r * _NoiseAmount;
                col += noise;

                // Añadir líneas de escaneo en movimiento
                float scanline = tex2D(_ScanlineTex, float2(uv.x, frac(uv.y * 100.0 + _Time.y * _ScanlineSpeed))).r;
                col -= scanline * 0.05;

                // Calcular la frecuencia de las líneas verticales y horizontales aleatorias
                float verticalFrequency = 5.0 + random(uv + _Time.xy) * 5.0; // Valor aleatorio entre 5.0 y 10.0
                float horizontalFrequency = 5.0 + random(uv + _Time.xy * 0.5) * 5.0; // Valor aleatorio entre 5.0 y 10.0

                // Calcular el parpadeo de las líneas verticales
                float verticalLineFactor = abs(sin(_Time.y * verticalFrequency)) * _LineAmplitude;

                // Añadir líneas verticales
                float verticalLines = tex2D(_VerticalLinesTex, uv).r * verticalLineFactor;
                col += verticalLines * 0.5; // Ajusta el valor para controlar la intensidad de las líneas

                // Calcular el parpadeo de las líneas horizontales
                float horizontalLineFactor = abs(sin(_Time.y * horizontalFrequency)) * _HorizontalLineAmplitude;

                // Añadir líneas horizontales
                float horizontalLines = tex2D(_HorizontalLinesTex, float2(frac(uv.x * 100.0), uv.y)).r * horizontalLineFactor;
                col += horizontalLines * 0.5; // Ajusta el valor para controlar la intensidad de las líneas horizontales

                // Desenfoque simple
                fixed4 blurColor = (tex2D(_MainTex, uv + float2(-1.0, 0.0) * _DistortionAmount) +
                                    tex2D(_MainTex, uv + float2(1.0, 0.0) * _DistortionAmount) +
                                    tex2D(_MainTex, uv + float2(0.0, -1.0) * _DistortionAmount) +
                                    tex2D(_MainTex, uv + float2(0.0, 1.0) * _DistortionAmount)) * 0.25;

                col = lerp(col, blurColor, 0.2); // Mezcla el color original con el desenfoque

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
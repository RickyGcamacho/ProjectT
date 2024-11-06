//
//  OutlineFill.shader
//  QuickOutline
//
//  Created by Chris Nolet on 2/21/18.
//  Modificado para mejorar el efecto de contorno.
//

Shader "Custom/OutlineShaderNoCullFront"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1, 0, 0, 1) // Color del contorno
        _OutlineWidth("Outline Width", Range(0.01, 0.2)) = 0.05 // Grosor del contorno
    }

    SubShader
    {
        Tags { "Queue" = "Overlay+10" } // Asegura que el contorno se dibuje después del objeto
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            // No culling, dibuja ambas caras
            Cull Off
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            CGPROGRAM
            #pragma surface surf Lambert
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };

            uniform float _OutlineWidth; // Grosor del contorno
            uniform fixed4 _OutlineColor; // Color del contorno

            // Función de vértices: Desplaza los vértices para crear el contorno
            v2f vert(appdata v)
            {
                v2f o;
                // Calculamos la normal en el espacio mundial para desplazar los vértices
                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                // Desplazamos los vértices hacia afuera para crear el contorno
                o.pos = UnityObjectToClipPos(v.vertex + worldNormal * _OutlineWidth);
                o.color = _OutlineColor;
                return o;
            }

            // Fragment shader: Aplica el color del contorno
            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }

    // Fallback en caso de que el shader no se pueda usar
    FallBack "Diffuse"
}
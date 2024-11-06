//
//  OutlineMask.shader
//  QuickOutline
//
//  Created by Chris Nolet on 2/21/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

Shader "Custom/Outline Mask" {
  Properties {
    [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
  }

  SubShader {
    Tags {
      "Queue" = "Overlay+100"  // Asegúrate de que la máscara se renderice primero
      "RenderType" = "Transparent"
    }

    Pass {
      Name "Mask"
      Cull Off
      ZWrite Off
      ZTest [_ZTest]
      ColorMask 0  // No escribe color, solo en el stencil buffer

      Stencil {
        Ref 1
        Comp Always  // Esto asegura que marque todas las áreas
        Pass Replace  // Reemplaza el valor en el stencil buffer con 1
      }
    }
  }
}


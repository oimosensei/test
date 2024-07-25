// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/SpritesHighlight"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _HighlightIntensity ("Highlight Intensity", Range(0, 10)) = 1.0
        _HighlightThreshold ("Highlight Threshold", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            Stencil {  
                Ref 2  
                Comp always  
                Pass replace  
            }
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag_Custom
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            // プロパティの宣言
            sampler2D _MainTex_Custom;
            float _HighlightIntensity;
            float _HighlightThreshold;

            // フラグメントシェーダーの改造
            fixed4 SpriteFrag_Custom(v2f IN) : SV_Target
            {
                // テクスチャの色をサンプル
	fixed4 c = SampleSpriteTexture(IN.texcoord);

                // 明るい部分を強調する
                float brightness = dot(c.rgb, fixed3(0.2126, 0.7152, 0.0722));
                if (brightness > _HighlightThreshold)
                {
                    c.rgb *= 1.0 + _HighlightIntensity ;
                }

                // アルファ値を考慮した色の計算
                c.rgb *= c.a;
                return c;
            }

            ENDCG
        }
    }
}

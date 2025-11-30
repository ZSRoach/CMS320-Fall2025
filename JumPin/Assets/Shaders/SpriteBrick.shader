Shader "Custom/SpriteBrick"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _BrickTex("Brick Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _BrickScale("Brick Scale", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "IgnoreProjector"="True"
        }

        Blend One OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color  : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color  : COLOR;
                float2 uv     : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _BrickTex;
            float4 _BrickTex_ST;

            fixed4 _Color;
            float _BrickScale;

            v2f vert (appdata_t IN)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(IN.vertex);
                o.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);
                o.color = IN.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Original sprite (we only care about its alpha for shape)
                fixed4 spriteSample = tex2D(_MainTex, i.uv);

                // Brick texture, tiled by BrickScale
                float2 brickUV = i.uv * _BrickScale;
                fixed4 brickSample = tex2D(_BrickTex, brickUV);

                fixed4 col;
                // RGB from brick, tinted
                col.rgb = brickSample.rgb * i.color.rgb;
                // Alpha from original sprite shape (and brick alpha & tint)
                col.a = spriteSample.a * i.color.a * brickSample.a;

                return col;
            }
            ENDCG
        }
    }
}

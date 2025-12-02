Shader "Custom/SpriteBrick"
{
    Properties
    {
        // The original sprite texture (Unity fills this from the SpriteRenderer)
        _MainTex("Sprite", 2D) = "white" {}

        // The brick pattern texture
        _BrickTex("Brick Texture", 2D) = "white" {}

        // Color tint you apply in the material
        _Color("Tint", Color) = (1,1,1,1)

        // Brick tiling on X (horizontal) and Y (vertical)
        // Higher values = more bricks = sharper look
        _BrickTiling("Brick Tiling (X Y)", Vector) = (5,5,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"        // Renders with transparent objects
            "RenderType"="Transparent"   // Treated as transparent for batching
            "CanUseSpriteAtlas"="False"   // Allows sprite atlas usage
            "IgnoreProjector"="True"     // Avoids unwanted projector effects
        }

        // Standard 2D sprite transparency blending
        Blend One OneMinusSrcAlpha

        Cull Off       // Don’t cull any faces (2D sprites have no back)
        Lighting Off   // 2D sprites don't need lighting
        ZWrite Off     // Don’t write to depth buffer (fixes sorting issues)
        Fog { Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert       // Use "vert" for vertex processing
            #pragma fragment frag     // Use "frag" for pixel processing
            #include "UnityCG.cginc"  // Includes Unity helper functions

            // Input from the mesh (Unity supplies sprite vertices)
            struct appdata_t
            {
                float4 vertex : POSITION;   // Vertex position
                float4 color  : COLOR;      // Vertex color
                float2 texcoord : TEXCOORD0; // Sprite UV coordinates
            };

            // Output to the fragment shader
            struct v2f
            {
                float4 vertex : SV_POSITION; // Screen position
                fixed4 color  : COLOR;       // Color passed to pixel
                float2 uv     : TEXCOORD0;   // UV for the original sprite
            };

            // Textures and shader properties
            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _BrickTex;
            float4 _BrickTex_ST;

            fixed4 _Color;           // Tint from material
            float4 _BrickTiling;     // X/Y tiling values

            // Vertex shader: prepares data for the fragment shader
            v2f vert (appdata_t IN)
            {
                v2f o;

                // Convert object space → screen space
                o.vertex = UnityObjectToClipPos(IN.vertex);

                // Transform UV with Unity’s built-in tiling/offset
                o.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);

                // Multiply vertex color by material tint
                o.color = IN.color * _Color;

                return o;
            }

            // Pixel shader: determines the final color of each pixel
            fixed4 frag (v2f i) : SV_Target
            {
                //---------------------------------------------------------
                // 1. Sample the original sprite (for alpha shape)
                //---------------------------------------------------------
                fixed4 spriteSample = tex2D(_MainTex, i.uv);
                // spriteSample.a = sprite’s transparency
                // spriteSample.rgb = original colors (ignored here)

                //---------------------------------------------------------
                // 2. Sample the brick texture, using tiling for sharpness
                //---------------------------------------------------------
                float2 brickUV = i.uv * _BrickTiling.xy;
                fixed4 brickSample = tex2D(_BrickTex, brickUV);

                //---------------------------------------------------------
                // 3. Combine brick pattern with sprite alpha
                //---------------------------------------------------------
                fixed4 col;

                // RGB comes from brick texture multiplied by tint
                col.rgb = brickSample.rgb * i.color.rgb;

                // Alpha comes from original sprite → keeps your shape
                col.a = spriteSample.a * i.color.a * brickSample.a;

                return col;
            }
            ENDCG
        }
    }
}


Shader "Buddha/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HairTex ("Hair", 2D) = "white" {}
        _FaceTex ("Face", 2D) = "white" {}
        _ClothesTex ("Clothes", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 
          "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"
        }

        LOD 100

        Cull Off         //关闭背面剔除
        Lighting Off     //关闭灯光
        ZWrite Off       //关闭Z缓冲
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 color  : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;           
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {            
                fixed4 col = tex2D(_MainTex, i.uv); 

                fixed a = col.a;
                fixed total_mask = 1 - step(a, 0.5);
                fixed hair_mask = 1 - step(a, 0.95);
                fixed face_mask = step(a, 0.95f) * (1 - step(a, 0.85f));
                fixed clothes_mask = step(a, 0.85);

                fixed4 color = fixed4(i.color.r * hair_mask, i.color.g * face_mask, i.color.b * clothes_mask, 1);
                fixed4 hair_color = fixed4(color.r, frac(color.r * 10), frac(color.r * 100), 1);              
                fixed4 face_color = fixed4(color.g, frac(color.g * 10), frac(color.g * 100), 1);             
                fixed4 clothes_color = fixed4(color.b, frac(color.b * 10), frac(color.b * 100), 1);              
                col = col * (hair_color + face_color + clothes_color);
                return fixed4(col.r, col.g, col.b, 1 * total_mask);
            }
            ENDCG
        }
    }
}

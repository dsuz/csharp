// https://teratail.com/questions/168277
Shader "Unlit/ShapeFill"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 1)
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        CGINCLUDE

        struct appdata
        {
            float4 vertex: POSITION;
        };

        struct v2f
        {
            float4 vertex: SV_POSITION;
        };

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }

        ENDCG

        Pass // ステンシルパス...描画するべき部分をマークする
        {
            Name "Shape Stencil"
            Cull Off
            ZWrite Off
            ColorMask 0 // カラーバッファへの書き込みは不要
            Stencil
            {
                WriteMask 1 // 一番下の1ビットだけを使う...ステンシルバッファに加算するたび、1と0を繰り返す
                Pass IncrWrap // 描画が行われたら、ステンシルバッファ上の値を1増やす
            }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 frag(v2f i) : SV_Target
            {
                return 0;
            }

            ENDCG
        }

        Pass // 塗りつぶしパス...描画するべき部分だけに色を塗る
        {
            Name "Shape Fill"
            Cull Off
            ZWrite Off
            Stencil
            {
                ReadMask 1 // 一番下の1ビットだけ見ればよい
                Ref 1
                Comp Equal // ステンシルバッファ上の値が1のフラグメント(奇数回重なっている部分)だけに描画する
            }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment fragFill
            #include "UnityCG.cginc"

            float4 _Color;

            fixed4 fragFill(v2f i) : SV_Target
            {
                return _Color;
            }

            ENDCG
        }
    }
}
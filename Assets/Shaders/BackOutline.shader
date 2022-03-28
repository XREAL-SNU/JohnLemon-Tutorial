Shader "Unlit/BackOutline"
{
    Properties
    {
        [Header(Outline)][Space(5)]  //outline
        _OtlColor("Color", COLOR) = (0,0,0,1)
        _OtlWidth("Width", Range(0,10)) = 1
    }

        SubShader
        {
            Pass
            {
                Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
                Blend Off
                Cull Front
                ZWrite Off
                ZTest Always

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                float4 _OtlColor;
                float _OtlWidth;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = v.vertex;
                    //o.pos.xyz += normalize(v.normal.xyz) * _OtlWidth * 0.008;
                    o.pos = UnityObjectToClipPos(o.pos);
                    float3 norm = TransformViewToProjection(normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal)));
                    o.pos.xyz += norm * _OtlWidth * 0.008;

                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    //clip(-negz(_OtlWidth));
                    return _OtlColor;
                    //return fixed4(0.2, 0.2, 0.2, 1.0);
                }

                ENDCG
            }

            UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        }
}

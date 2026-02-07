sampler uImage0 : register(s0);
float3 uColor;
float uTime;
float uIntensity;
float uProgress;

float4 LobotomyScreen(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    return color + float4(sin(uTime), sin(uTime + 3.1415 / 2), sin(uTime - 3.1415 / 2), 1) * uIntensity;
}

technique Technique1
{
    pass LobotomyScreen
    {
        PixelShader = compile ps_2_0 LobotomyScreen();
    }
}
sampler uImage0 : register(s0);
float3 uColor;
float uTime;
float uIntensity;
float uProgress;

float4 LobotomyWave(float2 coords : TEXCOORD0) : COLOR0
{
    float2 waveCoords = coords;
    waveCoords.y += sin(coords.x * 10.0 + uTime * 3.0) * 0.04 * uIntensity;
    waveCoords.x += cos(coords.y * 10.0 + uTime * 2.5) * 0.02 * uIntensity;
    
    return tex2D(uImage0, saturate(waveCoords));
}

technique Technique1
{
    pass LobotomyWave
    {
        PixelShader = compile ps_2_0 LobotomyWave();
    }
}
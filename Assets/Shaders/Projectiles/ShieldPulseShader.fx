sampler uImage0 : register(s0);
float uProgress;
float2 uTargetPosition;
float2 uScreenResolution;

sampler noiseTexture : register(s1);
float noiseTimeX;
float noiseTimeY;
float uTime;
float time;
float speed;
float4 color;
float colorMultiplier;
bool alwaysVisible;

float4 ShieldPulseShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 noiseColorBase = tex2D(noiseTexture, coords + float2(uTime / 100.0, uTime / 100.0));
    float4 noiseColor = tex2D(noiseTexture, coords + float2(uTime / 100.0, uTime / 100.0));
    float2 centeredCoords = coords * 2.0 - 1.0;
    float coordsLength = length(centeredCoords);
    if (coordsLength > frac(time * speed))
    {
        return 0;
    }
    float fadeOut = 1 - frac(time * speed);
    if (alwaysVisible)
    {
        fadeOut = 1;
    }
    return fadeOut * color * noiseColor.r * coordsLength * colorMultiplier;
}

technique Tech1
{
    pass ShieldPulseShader
    {
        PixelShader = compile ps_2_0 ShieldPulseShader();
    }
}
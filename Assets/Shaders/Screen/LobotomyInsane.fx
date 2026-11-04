sampler uImage0 : register(s0);
float3 uColor;
float uTime;
float uIntensity;
float uProgress;

float4 LobotomyInsane(float2 coords : TEXCOORD0) : COLOR0
{
    float2 centered = coords - 0.5;
    float distSq = dot(centered, centered) * 4.0;

    float shatterMask = saturate(distSq * uIntensity * 2.2 - 0.25);

    float2 warped = centered * uColor.xy + uProgress;
    float2 axis1 = warped.x * float2(1.4, -0.9) + warped.y * float2(0.8, 1.3);
    float2 axis2 = warped.x * float2(0.6, 1.5) + warped.y * float2(-1.5, 0.5);

    float2 sTrunk = axis1 + abs(axis2) * 0.42;
    float2 trunk = abs(sTrunk);

    float2 split1 = saturate(abs(axis1) + abs(axis2) - 0.26) * 0.28;
    float2 sBranch = trunk - split1;
    float2 branch = abs(sBranch);

    float2 paneA = saturate(sTrunk * 200.0);
    float2 paneB = saturate(sBranch * 200.0);

    float2 panelShift = (paneA - paneB * 0.6 - 0.2) * 0.045;

    float4 color = tex2D(uImage0, coords + panelShift * shatterMask);

    float3 lcdBars = frac(float3(coords.x * 160.0, coords.x * 430.0, coords.y * 110.0) + paneA.x * 0.5);
    float brokenZone = saturate(paneB.x - paneA.y) * shatterMask * 0.65;
    color.rgb = lerp(color.rgb, lcdBars, brokenZone);

    float2 branch2 = abs(branch - trunk * 0.45 - 0.06);

    float2 lineTrunk = saturate(1.0 - trunk * 140.0);
    float2 lineBranch = saturate(1.0 - branch * 170.0);
    float2 lineSub = saturate(1.0 - branch2 * 200.0);
    float2 allLines = lineTrunk + lineBranch + lineSub * 0.85;
    float cracks = saturate(allLines.x + allLines.y) * shatterMask;

    float inkBleed = saturate(1.0 - branch.x * 35.0) * paneA.x * shatterMask;
    float darkness = saturate((distSq - 0.2) * uIntensity * 1.6 + inkBleed);

    color.rgb = color.rgb * (1.0 - darkness) + cracks * 1.15;
    return color;
}

technique Technique1
{
    pass LobotomyInsane
    {
        PixelShader = compile ps_3_0 LobotomyInsane();
    }
}
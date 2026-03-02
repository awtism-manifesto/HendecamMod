using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories.VapeDyes;

public abstract class VapeDye
{
    public abstract string Name { get; }
    public virtual float MixWeight => 1.0f;
    public abstract Color Color { get; }

    
    public virtual Color GetMixedColor(List<VapeDye> activeDyes) => Color;
}

// Define your dyes as instances
public static class VapeDyes
{
    public static VapeDye Red40 = new RedVapeDye();
    public static VapeDye Yellow5 = new YellowVapeDye();

    // Add new dyes here easily
    public static VapeDye Blue1 = new BlueVapeDye();
    public static VapeDye Green3 = new GreenVapeDye();

    public static List<VapeDye> AllDyes = new List<VapeDye>
    {
        Red40,
        Yellow5,
        Blue1,
        Green3
        // Add new dyes here
    };
}

// Individual dye implementations
public class RedVapeDye : VapeDye
{
    public override string Name => "Red 40";
    public override float MixWeight => 0.8f;
    public override Color Color => new Color(255, 67, 67);
}

public class YellowVapeDye : VapeDye
{
    public override string Name => "Yellow 5";
    public override float MixWeight => 1.66f;
    public override Color Color => new Color(255, 230, 38);
}

public class BlueVapeDye : VapeDye
{
    public override string Name => "Blue 1";
    public override float MixWeight => 0.66f;
    public override Color Color => new Color(110, 95, 255);
}

public class GreenVapeDye : VapeDye
{
    public override string Name => "Green 3";
    public override float MixWeight => 0.95f;
    public override Color Color => new Color(58, 255, 64);
}
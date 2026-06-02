using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace HendecamMod.Common.Systems;

public class HendecamConfig : ModConfig
{

    public override ConfigScope Mode => ConfigScope.ServerSide;



    [Header("Reworks")]

    [DefaultValue(true)]


    public bool ProgressionBasedEnemyPowercreep;

    [DefaultValue(true)]

    [ReloadRequired]
    public bool BootsTreeRework;

    [DefaultValue(true)]

    [ReloadRequired]
    public bool VanillaWeaponClassChanges;

    [DefaultValue(true)]

    [ReloadRequired]
    public bool VanillaWeaponStatBuffs;

    [DefaultValue(true)]

    [ReloadRequired]
    public bool MiscVanillaWeaponChanges;

    [Header("ExperimentalFeatures(serverside)")]


    [Label("All Projectiles Fire Radially")]
    [Tooltip("Causes all projectiles to duplicate themselves and fire in a full circle")]
    [DefaultValue(false)]
    
    public bool Enabled { get; set; }

    [Label("Radial Projectile Count")]
    [Tooltip("Number of projectiles fired in a full circle (including the original). WARNING: THIS CAN GET VERY LAGGY VERY FAST")]
    [Range(2, 20)]
    
    [DefaultValue(6)]
    public int ProjectileCount { get; set; }


}
public class HendecamClientConfig : ModConfig

{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    [Header("ExperimentalFeatures(clientside)")]



    [DefaultValue(false)]

    public bool EverythingMakesCritNoise;


}

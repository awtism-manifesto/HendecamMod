using HendecamMod.Content.Buffs;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace HendecamMod.Common.Systems;

public class HendecamConfig : ModConfig
{

    public override ConfigScope Mode => ConfigScope.ServerSide;



    [Header("PrimaryConfig")]

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

    [DefaultValue(false)]

   
    public bool EnableExperimentalFeatures;



}
public class HendecamClientConfig : ModConfig

{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    [Header("ExperimentalFeatures(clientside)")]



    [DefaultValue(false)]

    public bool EverythingMakesCritNoise;


}

public class HendecamExperimentalConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

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

    [Label("(Almost) All Projectiles are bouncy")]
    [Tooltip("Causes almost all projectiles to bounce off walls (does not work with certain projectiles that have ignored/special collision logic)")]
    [DefaultValue(false)]
   
    public bool Bouncy { get; set; }

    [Label("DeathEnabled")]
    [Tooltip("Whether or not all players have a chance to instantly die")]
    [DefaultValue(false)]
    public bool DeathEnabled { get; set; }

    [Label("Instant Death Chance")]
    [Tooltip("Chances of instantly dying")]
    [Range(1, 200000)]
    [DefaultValue(10000)]
    public int DeathChance { get; set; }


    public override void OnChanged()
    {
        var enabledConfig = ModContent.GetInstance<HendecamConfig>();

        
        if (!enabledConfig.EnableExperimentalFeatures)
        {
            Enabled = false;
            Bouncy = false;
            DeathEnabled = false;
           
        }
    }

  
    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
    {
        var enabledConfig = ModContent.GetInstance<HendecamConfig>();

        if (!enabledConfig.EnableExperimentalFeatures)
        {
            message = "Cannot modify experimental features - enable them in the primary config!";
            return false;
        }

        return true;
    }
}


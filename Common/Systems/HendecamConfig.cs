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


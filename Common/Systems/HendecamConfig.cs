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


}
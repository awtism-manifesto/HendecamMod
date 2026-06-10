using HendecamMod.Content.Buffs;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace HendecamMod.Common.Systems;


public class ZeGogEnablers : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [Label("Gog Enabler 1")]
    [Tooltip("WARNING: GOG BLOCKS CAN SPREAD TO ANY TILE AND TAKE A STUPIDLY LONG TIME TO MINE")]
    [DefaultValue(false)]

    public bool GogEnabler1 { get; set; }
    [Label("Gog Enabler 2")]
    [Tooltip("WARNING: THE SPREAD OF GOG BLOCKS IS SPED UP FURTHER AND FURTHER THE MORE YOU PROGRESS")]
    [DefaultValue(false)]

    public bool GogEnabler2 { get; set; }

    [Label("Gog Enabler 3")]
    [Tooltip("WARNING: BY ENABLING ALL FIVE OF THESE CONFIGS, YOU AGREE THAT THE HENDECAM TEAM IS NOT RESPONSIBLE FOR DAMAGE TO YOUR WORLD")]
    [DefaultValue(false)]

    public bool GogEnabler3 { get; set; }
    [Label("Gog Enabler 4")]
    [Tooltip("WARNING: IF YOU ENABLE THESE ENABLERS IN MULTIPLAYER WITHOUT LETTING THE HOST KNOW, YOU ARE A MASSIVE PIECE OF SHIT.")]
    [DefaultValue(false)]

    public bool GogEnabler4 { get; set; }

    [Label("Gog Enabler 5")]
    [Tooltip("Have fun watching your world get taken over by the Gog!!!")]
    [DefaultValue(false)]
    [ReloadRequired]


    public bool GogEnabler5 { get; set; }


    [Label("One Random Gog Block Spawns on boss kill")]
    [Tooltip("This is a WONDERFUL idea!!!")]
    [DefaultValue(false)]

    public bool GogRandomSpawns { get; set; }

    public override void OnChanged()
    {
        var expConfig = GetInstance<HendecamExperimentalConfig>();


        if (!expConfig.EnableGogEnablers)
        {
           GogEnabler1 = false;
            GogEnabler2 = false;
            GogEnabler3 = false;
            GogEnabler4 = false;
            GogEnabler5 = false;

        }
    }


    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
    {
        var expConfig = GetInstance<HendecamExperimentalConfig>();

        if (!expConfig.EnableGogEnablers)
        {
            message = "Cannot modify gog enablers now!";
            return false;
        }

        return true;
    }
}
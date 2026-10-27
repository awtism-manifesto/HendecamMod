using HendecamMod.Common.Systems.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Map;

namespace HendecamMod.Common.Utilities
{
    public static class Helpers
    {
        public static HendecamPlayer GetHCPlayer(this Player player) => player.GetModPlayer<HendecamPlayer>();

        public static bool IsLocalPlayer(this Player player) => player.whoAmI == Main.myPlayer;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendecamMod.Common.Utils
{
    public static class Helpers
    {
        public static bool IsLocalPlayer(this Player player) => player.whoAmI == Main.myPlayer;
    }
}

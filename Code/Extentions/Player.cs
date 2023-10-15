using ItemShops.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoppyScyyeGameModes.Extentions
{
    public static class PlayerExtentions
    {
        public static void AddSkillPoints(this Player player, int amount)
        {
            player.GetAdditionalData().bankAccount.Deposit(new Dictionary<string, int> { { "Skill Points", amount } });
        }

        public static int GetSkillPoints(this Player player)
        {
            return player.GetAdditionalData().bankAccount.Money.GetValueOrDefault("Skill Points", 0);
        }
    }
}

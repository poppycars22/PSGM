using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace PoppyScyyeGameModes.Cards
{
    internal abstract class SkillPointCard : CustomCard
    {
        public abstract override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health,
            Gravity gravity, Block block, CharacterStatModifiers characterStats);

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                GetStat()
            };
        }

        protected abstract override string GetTitle();
        protected abstract CardInfoStat GetStat();
    }
}

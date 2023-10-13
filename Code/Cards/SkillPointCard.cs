using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace PoppyScyyeGameModes.Cards
{
    internal abstract class SkillPointCard : CustomCard
    {
        public abstract override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block);

        protected override GameObject GetCardArt()
        {
            return null;
        }

        public override string GetModName()
        {
            return "Skill Points";
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

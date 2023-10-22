using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace PoppyScyyeGameModes.Cards
{
    internal abstract class SkillPointCard : CustomCard
    {
        public static List<SkillPointCard> Cards = new List<SkillPointCard>();
        public abstract override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block);

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            Cards.Add(this);
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }
        public override bool GetEnabled()
        {
            return false;
        }

        public override string GetModName()
        {
            return GetCost() + " point" + (GetCost()>1?"s":"");
        }

        protected override string GetDescription()
        {
            return GetLimit()==0?"":"Limit of "+GetLimit();
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
            return new[]{GetStat()};
        }

        protected override string GetTitle()
        {
            return GetStat().stat;
        }
        protected abstract CardInfoStat GetStat();
        public virtual int GetCost()
        {
            return 1;
        }
        public virtual int GetLimit()
        {
            return 0;
        }
        public virtual string GetCategory()
        {
            return "Gun";
        }
    }
}

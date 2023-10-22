using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class PercentDamageSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.percentageDamage = 0.1f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+10%",
                positive = true,
                stat = "Percentage Damage"
            };
        }
        public override int GetCost()
        {
            return 5;
        }
        public override int GetLimit()
        {
            return 5;
        }
    }
}

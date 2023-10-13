using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class DamageSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage = 1.15f;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                positive = true,
                stat = "Damage"
            };
        }
    }
}

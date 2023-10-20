using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class HealthSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 1.15f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                positive = true,
                stat = "Health"
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

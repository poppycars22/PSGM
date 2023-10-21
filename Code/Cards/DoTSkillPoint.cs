using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class DoTSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.secondsToTakeDamageOver = 1;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1 second",
                positive = true,
                stat = "DoT"
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

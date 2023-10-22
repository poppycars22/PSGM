using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class SizeSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.sizeMultiplier = 0.9f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "-10%",
                positive = true,
                stat = "Size"
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
        public override int GetLimit()
        {
            return 7;
        }
    }
}

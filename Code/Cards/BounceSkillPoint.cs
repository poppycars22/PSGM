using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class BounceSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.reflects = 2;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+2",
                positive = true,
                stat = "Bounces"
            };
        }
    }
}

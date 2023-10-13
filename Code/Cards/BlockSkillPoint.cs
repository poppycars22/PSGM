using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class BlockSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.additionalBlocks = 1;
            //COSTS 5*********
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                positive = true,
                stat = "Block"
            };
        }
    }
}

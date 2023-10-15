using System;
using System.Collections.Generic;
using System.Text;

namespace PoppyScyyeGameModes.Cards
{
    internal class RegenerationSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.regen = 1;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                positive = true,
                stat = "Regeneration"
            };
        }
    }
}

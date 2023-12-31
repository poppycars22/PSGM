﻿namespace PoppyScyyeGameModes.Cards
{
    internal class MoveSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.movementSpeed = 1.15f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                stat = "Movement Speed",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

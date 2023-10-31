namespace PoppyScyyeGameModes.Cards
{
    internal class UpwardsBlockForceSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.forceToAddUp = 3;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+3",
                stat = "Upwards Block Force",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Block";
        }
    }
}

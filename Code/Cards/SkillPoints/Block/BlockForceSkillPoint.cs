namespace PoppyScyyeGameModes.Cards
{
    internal class BlockForceSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.forceToAdd = 3;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+3",
                stat = "Block Force",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Block";
        }
    }
}

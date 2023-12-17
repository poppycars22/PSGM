namespace PoppyScyyeGameModes.Cards
{
    internal class BlockCooldownSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.cdMultiplier = 0.95f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "-5%",
                stat = "Block Cooldown",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Block";
        }
        public override int GetCost()
        {
            return 2;
        }
    }
}

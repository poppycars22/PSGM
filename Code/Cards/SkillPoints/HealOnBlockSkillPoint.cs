namespace PoppyScyyeGameModes.Cards
{
    internal class HealOnBlockSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.healing = 3;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+3",
                stat = "Healing on Block",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Block";
        }
    }
}

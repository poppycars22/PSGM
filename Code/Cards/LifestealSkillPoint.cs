namespace PoppyScyyeGameModes.Cards
{
    internal class LifestealSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.lifeSteal = 0.1f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+10%",
                stat = "Lifesteal",
                positive = true
            };
        }
    }
}

namespace PoppyScyyeGameModes.Cards
{
    internal class SlowSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.slow = 0.1f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+10%",
                stat = "Slow",
                positive = true
            };
        }
    }
}

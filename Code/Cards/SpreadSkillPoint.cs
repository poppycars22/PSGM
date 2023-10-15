namespace PoppyScyyeGameModes.Cards
{
    internal class SpreadSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.spread = -0.05f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "-5%",
                positive = true,
                stat = "Spread"
            };
        }
    }
}

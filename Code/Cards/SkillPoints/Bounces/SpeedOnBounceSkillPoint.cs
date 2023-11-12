namespace PoppyScyyeGameModes.Cards
{
    internal class SpeedOnBounceSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.speedMOnBounce = 1.1f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+10%",
                positive = true,
                stat = "Bullet Speed on Bounce"
            };
        }
        public override string GetCategory()
        {
            return "Bounces";
        }
    }
}

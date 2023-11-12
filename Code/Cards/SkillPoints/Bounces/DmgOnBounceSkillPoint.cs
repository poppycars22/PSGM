namespace PoppyScyyeGameModes.Cards
{
    internal class DmgOnBounceSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.dmgMOnBounce = 1.1f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+10%",
                positive = true,
                stat = "Damage on Bounce"
            };
        }
        public override string GetCategory()
        {
            return "Bounces";
        }
    }
}

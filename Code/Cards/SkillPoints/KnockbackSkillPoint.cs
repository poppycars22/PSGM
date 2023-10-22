namespace PoppyScyyeGameModes.Cards
{
    internal class KnockbackSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.knockback = 1.25f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+25%",
                stat = "Knockback",
                positive = true
            };
        }
    }
}

namespace PoppyScyyeGameModes.Cards
{
    internal class ProjectileSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSpeed = 1.15f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                stat = "Projectile Speed",
                positive = true
            };
        }
    }
}

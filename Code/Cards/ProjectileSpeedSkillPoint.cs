namespace PoppyScyyeGameModes.Cards
{
    internal class ProjectileSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSpeed = 1.25f;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+25%",
                stat = "Projectile Speed",
                positive = true
            };
        }
    }
}

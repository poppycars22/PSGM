namespace PoppyScyyeGameModes.Cards
{
    internal class ProjectileSimSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectielSimulatonSpeed = 1.25f;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+25%",
                stat = "Projectile Simulation Speed",
                positive = true
            };
        }
    }
}

namespace PoppyScyyeGameModes.Cards
{
    internal class ProjectileSimSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectielSimulatonSpeed = 1.15f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                stat = "Projectile Simulation Speed",
                positive = true
            };
        }
    }
}

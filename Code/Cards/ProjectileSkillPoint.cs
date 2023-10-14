namespace PoppyScyyeGameModes.Cards
{
    internal class ProjectileSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.numberOfProjectiles = 1;
            gun.multiplySpread = 1.05f;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                positive = true,
                stat = "Projectile"
            };
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                GetStat(),
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Spread",
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        public override int GetCost()
        {
            return 4;
        }
    }
}

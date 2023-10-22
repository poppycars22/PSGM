namespace PoppyScyyeGameModes.Cards
{
    internal class BurstSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.bursts = 1;
            Cards.Add(this);
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (gun.bursts <= 1)
            {
                gun.bursts += 1;
            }
            if (gun.timeBetweenBullets == 0)
            {
                gun.timeBetweenBullets = 0.1f;
            }
        }
        public override int GetCost()
        {
            return 3;
        }
        public override int GetLimit()
        {
            return 10;
        }
        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                stat = "Burst",
                positive = true
            };
        }
    }
}

namespace PoppyScyyeGameModes.Cards
{
    internal class BulletGravSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.gravity = 0.9f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "-10%",
                stat = "Bullet Gravity",
                positive = true
            };
        }
    }
}

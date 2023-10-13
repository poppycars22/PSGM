namespace PoppyScyyeGameModes.Cards
{
    internal class AmmoSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.ammo = 5;
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+5",
                stat = "Ammo",
                positive = true
            };
        }
    }
}

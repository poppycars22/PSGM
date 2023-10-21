namespace PoppyScyyeGameModes.Cards
{
    internal class ReloadSpeedSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.reloadTime = 0.85f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                stat = "Reload Speed",
                positive = true
            };
        }
    }
}

namespace PoppyScyyeGameModes.Cards
{
    internal class RespawnsSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.respawns = 1;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                positive = true,
                stat = "Respawn"
            };
        }
        public override int GetCost()
        {
            return 15;
        }
        public override int GetLimit()
        {
            return 2;
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

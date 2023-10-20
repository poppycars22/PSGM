namespace PoppyScyyeGameModes.Cards
{
    internal class JumpSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.numberOfJumps = 1;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+1",
                stat = "Jump",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

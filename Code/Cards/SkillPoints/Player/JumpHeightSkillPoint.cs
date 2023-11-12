namespace PoppyScyyeGameModes.Cards
{ 
    internal class JumpHeightSkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.jump = 1.05f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+5%",
                stat = "Jump Height",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

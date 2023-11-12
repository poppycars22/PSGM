namespace PoppyScyyeGameModes.Cards
{ 
    internal class PlayerGravitySkillPoint : SkillPointCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.gravity = 0.95f;
            Cards.Add(this);
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "-5%",
                stat = "Player Gravity",
                positive = true
            };
        }
        public override string GetCategory()
        {
            return "Player";
        }
    }
}

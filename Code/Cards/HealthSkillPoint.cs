using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class HealthSkillPoint : SkillPointCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            throw new NotImplementedException();
        }

        protected override CardInfoStat GetStat()
        {
            return new CardInfoStat()
            {
                amount = "+15%",
                positive = true,
                stat = "Health"
            };
        }

        protected override string GetTitle()
        {
            return "Health";
        }
    }
}

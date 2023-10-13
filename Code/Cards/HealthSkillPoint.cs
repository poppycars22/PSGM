using System;

namespace PoppyScyyeGameModes.Cards
{
    internal class HealthSkillPoint : SkillPointCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            data.health += 15;
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

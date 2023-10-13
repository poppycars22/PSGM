using PoppyScyyeGameModes.Gamemodes;
using UnityEngine;

namespace PoppyScyyeGameModes.Monos
{
    public class SkillPointMono : MonoBehaviour
    {
        public int skillPoints { get; private set; }

        public void Start()
        {
            skillPoints = 10;
        }

        public void AddSkillPoints(int amount)
        {
            skillPoints += amount;
        }
        public void RemoveSkillPoints(int amount)
        {
            skillPoints -= amount;
        }
    }
}

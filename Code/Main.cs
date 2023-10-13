using BepInEx;
using HarmonyLib;
using PoppyScyyeGameModes.Gamemodes;
using UnboundLib;
using UnboundLib.GameModes;

namespace PoppyScyyeGameModes
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("io.olavim.rounds.rwf", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.itemshops", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "dev.scyyepoppy.rounds.gamemodes";
        private const string ModName = "Poppys And Scyyes Gamemodes";
        private const string Version = "1.0.0"; // What version are we on (major.minor.patch)?

        /*
         * TODO: Make the gamemode work properly :"D
         */

        public static Main instance { get; private set; }

        void Awake()
        {
            instance = this;
            Unbound.RegisterCredits("Poppys And Scyyes Gamemodes", new string[] { "Poppycars", "Scyye" }, new string[] { "GitHub", "Poppycars", "Scyye" },
                new string[] {"https://github.com/poppycars22/PSGM", "https://github.com/poppycars22", "https://github.com/Scyye"});

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            GameModeManager.AddHandler<SkillPointGM>(SkillPointHandler.GameModeID, new SkillPointHandler());
            GameModeManager.AddHandler<SkillPointGM>(SkillPointTeamHandler.GameModeID, new SkillPointTeamHandler());
        }

    }
}

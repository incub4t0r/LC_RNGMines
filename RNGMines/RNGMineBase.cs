using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RNGMines.Patches;

namespace RNGMines
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class RNGMineBase : BaseUnityPlugin
    {
        private const string modGUID = "f0ur3y3s.RNGMine";
        private const string modName = "RNGMine";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static RNGMineBase Instance;
        static ManualLogSource logger;
        internal static ManualLogSource GetLogger()
        {
            return logger;
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(modName);
            string icon = @"
                                              
                                              
                                ██    ██      
                    ██████      ██  ██        
                  ██      ██                  
                ██          ████░░    ████    
                ██                            
              ██████            ██  ██        
              ██████            ██    ██      
          ██████████████                      
        ██████░░░░░░░░▓▓██                    
      ██████░░░░░░░░  ▓▓▓▓██                  
      ██████▓▓▓▓▓▓▓▓    ▓▓██                  
    ████████▓▓▓▓▓▓▓▓▓▓  ▓▓▓▓██                
    ████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██                
    ████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██                
      ████████▓▓▓▓▓▓▓▓▓▓▓▓██                  
      ████████████▓▓▓▓▓▓████                  
        ██████████████████                    
          ██████████████                      
              ██████                          
";
            logger.LogWarning(icon);
            logger.LogInfo(" RNGMine Loaded!");
            logger.LogInfo("Patching RNGMineBase");
            harmony.PatchAll(typeof(RNGMineBase));
            logger.LogInfo("Patching PlayerItemSlots");
            harmony.PatchAll(typeof(MinePatch));
        }
    }
}

using robotManager.Helpful;
using System.Drawing;
using wManager;
using wManager.Wow.ObjectManager;

namespace NodeBlackListEx
{
  class Helpers
  {
    public static void Log(string message)
    {
      Logging.Write("[NodeBlackListEx] " + message, Logging.LogType.Normal, Color.ForestGreen);
    }

    public static void CheckNodeLevel()
    {
      ObjectManager.GetObjectWoWGameObject().ForEach(o =>
      {
        if (Data.MiningRequirements.ContainsKey(o.Name) && Data.MiningRequirements[o.Name] > Main.CurrentMining && !wManager.wManagerSetting.IsBlackListed(o.Guid))
        {
          Log("Blacklisting " + o.Name + " because required skill " + Data.MiningRequirements[o.Name] + " is lower than current skill " + Main.CurrentMining.ToString());
          wManagerSetting.AddBlackList(o.Guid, wManagerSetting.CurrentSetting.BlacklistUnitDefaultTimeMs, true);
        }

        if (Data.HerbalismRequirements.ContainsKey(o.Name) && Data.HerbalismRequirements[o.Name] > Main.CurrentHerbalism && !wManager.wManagerSetting.IsBlackListed(o.Guid))
        {
          Log("Blacklisting " + o.Name + " because required skill " + Data.HerbalismRequirements[o.Name] + " is lower than current skill " + Main.CurrentHerbalism.ToString());
          wManagerSetting.AddBlackList(o.Guid, wManagerSetting.CurrentSetting.BlacklistUnitDefaultTimeMs, true);
        }
      });
    }
  }
}

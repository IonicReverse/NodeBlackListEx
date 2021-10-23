using System;
using System.Collections.Generic;
using System.Threading;
using NodeBlackListEx;
using wManager.Plugin;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;

public class Main : IPlugin
{
  public bool _isLaunched;
  public static int CurrentMining = 0;
  public static int CurrentHerbalism = 0;

  public void Initialize()
  {
    _isLaunched = true;
    SetGatherSkill();
    EventsLuaWithArgs.OnEventsLuaStringWithArgs += EventsLuaWithArgsGather;
    Helpers.Log("[NodeBlacklist] Started");
    Pulse();
  }

  public void Pulse()
  {
    try
    {
      while (_isLaunched)
      {
        if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
          Helpers.CheckNodeLevel();
        Thread.Sleep(500);
      }
    }
    catch (Exception ex)
    {
      Helpers.Log("Failed " + ex);
    }
  }

  public void Dispose()
  {
    _isLaunched = false;
    Helpers.Log("Stopped");
  }

  public void Settings()
  {
    Helpers.Log("No Settings"); 
  }

  private void SetGatherSkill()
  {
    CurrentMining = Skill.GetValue(SkillLine.Mining);
    CurrentHerbalism = Skill.GetValue(SkillLine.Herbalism);
    Helpers.Log("CurrentMining : " + CurrentMining);
    Helpers.Log("CurrentHerbalism : " + CurrentHerbalism);
  }

  private void EventsLuaWithArgsGather(string id, List<string> args)
  {
    if (id == "CHAT_MSG_SKILL")
      SetGatherSkill();
      if (CurrentMining % 5 == 0 || CurrentHerbalism % 5 == 0)
        wManager.wManagerSetting.ClearBlacklistOfCurrentProductSession();
  }
}

using System;
using System.IO;
using SweetSugar.Scripts.System;
using SweetSugar.Scripts.TargetScripts.TargetSystem;
using UnityEngine;

public static class LoadingManager
{
    private static LevelData levelData;
    static string levelPath = "Assets/SweetSugar/Resources/Levels/";

    public static LevelData LoadForPlay(int currentLevel, LevelData levelData)
    {
        levelData = new LevelData(Application.isPlaying, currentLevel);
        levelData = LoadlLevel(currentLevel, levelData).DeepCopyForPlay(currentLevel);
        levelData.LoadTargetObject();
        levelData.InitTargetObjects();
        return levelData;
    }

    public static LevelData LoadlLevel(int currentLevel, LevelData levelData)
    {
        levelData = ScriptableLevelManager.LoadLevel(currentLevel);
        LevelData.THIS = levelData;
        levelData.LoadTargetObject();
        levelData.InitTargetObjects();

        return levelData;
    }


    public static int GetLastLevelNum()
    {
        FileInfo[] fis = new DirectoryInfo(levelPath).GetFiles(@"Level_*.asset");
        return fis.Length;
    }
}


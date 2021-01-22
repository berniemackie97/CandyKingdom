using System.Collections.Generic;
using UnityEngine;

public static class ColorGenerator
{
    public static int GenColor(Square square, int maxColors = 6, int exceptColor = -1, bool onlyNONEType = false)
    {
        
        List<int> exceptColors = new List<int>();
        List<int> remainColors = new List<int>();
        var thisColorLimit = LevelManager.THIS.levelData.colorLimit - ((LevelManager.THIS?.gameStatus == GameState.RegenLevel) ? 1 : 0);
        for (int i = 0; i < LevelManager.THIS.levelData.colorLimit; i++)
        {
            bool canGen = true;
            if (square.GetMatchColorAround(i) > 1)
            {
                exceptColors.Add(i);
            }
        }

        int randColor = 0;
        do
        {
            randColor = Random.Range(0, thisColorLimit);
            
        } while (exceptColors.Contains(randColor) && exceptColors.Count < thisColorLimit );
        if (remainColors.Count > 0)
            randColor = remainColors[Random.Range(0, remainColors.Count)];
        if (exceptColor == randColor)
            randColor = Mathf.Clamp( randColor++,0 , thisColorLimit);
        return randColor;
    }
}
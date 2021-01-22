using System.Collections;
using UnityEngine;

/// <summary>
/// block expands constantly until you explode one
/// </summary>
public class ThrivingBlock : Square
{
    static bool blockCreated;
    int lastMoveID = -1;
    void OnEnable()
    {
        LevelManager.OnTurnEnd += OnTurnEnd;
    }

    void OnDisable()
    {
        LevelManager.OnTurnEnd -= OnTurnEnd;
        LevelManager.THIS.thrivingBlockDestroyed = true;

    }

    private void OnTurnEnd()
    {
        if (LevelManager.THIS.moveID == lastMoveID) return;
        lastMoveID = LevelManager.THIS.moveID;
        var field = LevelManager.THIS.field.fieldData;
        if (LevelManager.THIS.thrivingBlockDestroyed || blockCreated) return;
        blockCreated = true;
        StartCoroutine(blockCreatedCD());
        LevelManager.THIS.thrivingBlockDestroyed = false;
        var thrivingBlockSelected = false;
        for (var col1 = 0; col1 < field.maxCols; col1++)
        {
            if (thrivingBlockSelected)
                break;
            for (var row1 = field.maxRows - 1; row1 >= 0; row1--)
            {
                if (thrivingBlockSelected)
                    break;
                if (LevelManager.THIS.GetSquare(col1, row1) == null) continue;
                if (LevelManager.THIS.GetSquare(col1, row1).type != SquareTypes.ThrivingBlock) continue;
                var sqList = LevelManager.THIS.GetSquare(col1, row1).GetAllNeghborsCross();
                foreach (var sq in sqList)
                {
                    if (!sq.CanGoInto() || Random.Range(0, 1) != 0 ||
                        sq.type != SquareTypes.EmptySquare || sq.Item?.currentType != ItemsTypes.NONE) continue;
                    if (sq.Item == null) continue;
                    if (sq.Item.currentType != ItemsTypes.NONE) continue;
                    sq.CreateObstacle(SquareTypes.ThrivingBlock, 1);
                    Destroy(sq.Item.gameObject);
                    thrivingBlockSelected = true;
                    break;
                }
            }
        }
    }

    IEnumerator blockCreatedCD()
    {
        yield return new WaitForSeconds(1);
        blockCreated = false;
    }

}

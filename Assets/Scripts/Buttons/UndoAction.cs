using System.Collections.Generic;
using UnityEngine;

public class UndoAction : MonoBehaviour
{
    public void undoAction()
    {
        undo(GameHistory.getLastMove());
    }

    public void resetTurn()
    {
        foreach(Action action in GameHistory.getCurrentTurnActions())
        {
            undo(action);
        }
    }

    private void undo(Action action)
    {
        if (action != null && GameHistory.removeAction(action))
        {
            GameObject cardObject = GameObject.Find(action.card.name);
            cardObject.transform.SetParent(action.previousPosition.transform);
        }
    }
}

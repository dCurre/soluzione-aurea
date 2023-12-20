using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameHistory
{
    public static int currentTurn = 1;

    private static List<Action> gameHistory = new List<Action>();

    public static void SaveHistory(Action action)
    {
        gameHistory.Insert(0, action);
    }

    public static Action getLastMove()
    {
        if (gameHistory.Count < 1){
            return null;
        }

        return gameHistory.First();
    }

    public static bool removeAction(Action action)
    {
        if(action == null || action.turn != currentTurn)
        {
            return false;
        }

        gameHistory.Remove(action);

        return true;
    }

    public static List<Action> getCurrentTurnActions()
    {
        return gameHistory.Where(item => item.turn == currentTurn).ToList();
    }
}

public class Action
{
    public GameObject card;
    public GameObject previousPosition;
    public GameObject currentPosition;
    public int turn;

    public Action(GameObject card, GameObject previousPosition, GameObject lastPosition, int turn)
    {
        this.card = card;
        this.previousPosition = previousPosition;
        this.currentPosition = lastPosition;
        this.turn = turn;
    }
}
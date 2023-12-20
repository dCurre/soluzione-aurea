using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public TextMeshProUGUI turnCounter;
    void Update()
    {
        updateTurnCounter();
    }
    private void updateTurnCounter()
    {
        turnCounter.text = "Turn: " + GameHistory.currentTurn.ToString();
    }
}

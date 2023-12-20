using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownPassTurn;
    public GameObject ActivePlayer;
    public Game Game;
    public ErrorMessage errorMessage;
    public GameObject dropZoneArea;

    private ActivePlayer activePlayer;
    private CardManager cardManager;

    private readonly Dictionary<int, string> dropdownOptions = new() {
        { 0, "" },
        { 1, "Draw 1" },
        { 2, "Draw 2" },
        { 3, "Draw 3" }
    };

    void Start()
    {
        activePlayer = ActivePlayer.GetComponent<ActivePlayer>();
        cardManager = Game.GetComponent<CardManager>();
        setupDropdownOptions();
    }

    private void Update()
    {
        setupDropdownOptions();
    }

    public void onTurnPass()
    {
        if (!Utils.isFieldStable())
        {
            return;
        }
        cardManager.draw(dropdownPassTurn.value, activePlayer.getActivePlayer());
        activePlayer.switchActivePlayer();
        GameHistory.currentTurn += 1;
    }

    private void setupDropdownOptions()
    {
        int cardsInHand = activePlayer.getActivePlayer().GetComponent<AreaPlayer>().cardsInHand;

        dropdownPassTurn.ClearOptions();

        int cardsToDraw = GameConstants.MAX_HAND - cardsInHand;

        if(cardsToDraw > 3)
        {
            cardsToDraw = 3;
        }

        dropdownPassTurn.AddOptions(
            dropdownOptions.Where(item => item.Key < (cardsToDraw + 1))
            .ToDictionary(item => item.Key, item => item.Value)
            .Values.ToList());
    }
}

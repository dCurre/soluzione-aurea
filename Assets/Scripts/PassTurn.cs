using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownPassTurn;
    public GameObject ActivePlayer;
    public Game Game;

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
        cardManager.draw(dropdownPassTurn.value, activePlayer.getActivePlayer(), activePlayer.getActivePlayer().GetComponent<AreaPlayer>());
        activePlayer.switchActivePlayer();
    }
    private void setupDropdownOptions()
    {
        int cardsInHand = activePlayer.getActivePlayer().GetComponent<AreaPlayer>().cardsInHand;

        dropdownPassTurn.ClearOptions();

        dropdownPassTurn.AddOptions(
            dropdownOptions.Where(item => item.Key < (GameConstants.MAX_HAND-cardsInHand+1))
            .ToDictionary(item => item.Key, item => item.Value)
            .Values.ToList());
    }
}

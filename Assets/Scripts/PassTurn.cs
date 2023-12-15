using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
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
    private List<GameObject> dropZones;

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
        dropZones = GameObject.FindGameObjectsWithTag("Dropzone").ToList();
        setupDropdownOptions();
    }

    private void Update()
    {
        setupDropdownOptions();
    }

    public void onTurnPass()
    {
        if (!isFieldStable())
        {
            errorMessage.setTextAndShow("Table not stable");
            return;
        }
        cardManager.draw(dropdownPassTurn.value, activePlayer.getActivePlayer(), activePlayer.getActivePlayer().GetComponent<AreaPlayer>());
        activePlayer.switchActivePlayer();
    }

    private bool isFieldStable()
    {
        foreach(GameObject dropZone in dropZones)
        {
            if (!isDropZoneStable(dropZone))
            {
                return false;
            }
        }

        return true;
    }

    private bool isDropZoneStable(GameObject dropZone)
    {
        /* REGOLE:
         *  1. La zona deve essere vuota
         *  2. Se ci sono carte --> almeno 3 carte
         *  3. Se ci sono carte --> devono essere scale o dello stesso valore
         */
        if (dropZone.transform.childCount == 0)
        {
            return true;
        }

        List<GameObject> children = Utils.GetAllChildrenGameObjectsFromGameObject(dropZone);

        if (children.Count > 2)
        {

            Debug.Log("Children Count: " + children.Count);

            return false;
        }

        return false;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayer : MonoBehaviour
{
    public TextMeshProUGUI textActivePlayer;
    [SerializeField]
    private Player activePlayer = Player.Player1;
    public GameObject AreaPlayer;
    public GameObject AreaPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        updateTextActivePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        updateTextActivePlayer();
    }
    public GameObject getActivePlayer()
    {
        return activePlayer switch
        {
            Player.Player1 => AreaPlayer,
            Player.Player2 => AreaPlayer2,
            _ => null
        };
    }
    public void switchActivePlayer()
    {
        activePlayer = (Player)((int)(activePlayer + 1) % GameConstants.PLAYERS_NUMBER);
    }

    private void updateTextActivePlayer()
    {
        textActivePlayer.text = activePlayer.ToString();
    }

}
public enum Player
{
    Player1,
    Player2
}

using TMPro;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    public TextMeshProUGUI textActivePlayer;
    [SerializeField]
    private Player activePlayer = Player.Player1;
    public GameObject AreaPlayer;
    public GameObject AreaPlayer2;

    void Start()
    {
        updateTextActivePlayer();
    }

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

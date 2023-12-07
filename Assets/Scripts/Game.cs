using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject AreaPlayer;
    public GameObject AreaPlayer2;
    public CardManager CardManager;

    private AreaPlayer AreaPlayerScript;
    private AreaPlayer AreaPlayer2Script;
    
    [SerializeField]
    private Player activePlayer = Player.Player1;

    void Start()
    {
        AreaPlayerScript = AreaPlayer.GetComponent<AreaPlayer>();
        AreaPlayer2Script = AreaPlayer2.GetComponent<AreaPlayer>();
        CardManager.populateDeck();

        CardManager.draw(8, AreaPlayer, AreaPlayerScript);
        CardManager.draw(8, AreaPlayer2, AreaPlayer2Script);
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
        activePlayer = (Player) ((int)(activePlayer + 1) % PLAYERS_NUMBER);
    }

    public static int MAX_HAND = 8;
    public static int PLAYERS_NUMBER = 2;

}

public enum Player
{
    Player1,
    Player2
}

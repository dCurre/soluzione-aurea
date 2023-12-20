using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject AreaPlayer;
    public GameObject AreaPlayer2;
    public CardManager CardManager;

    void Start()
    {
        CardManager.populateDeck();
        CardManager.draw(GameConstants.MAX_HAND, AreaPlayer);
        CardManager.draw(GameConstants.MAX_HAND, AreaPlayer2);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject AreaPlayer;
    public GameObject AreaPlayer2;
    public CardManager CardManager;

    private AreaPlayer AreaPlayerScript;
    private AreaPlayer AreaPlayer2Script;

    void Start()
    {
        AreaPlayerScript = AreaPlayer.GetComponent<AreaPlayer>();
        AreaPlayer2Script = AreaPlayer2.GetComponent<AreaPlayer>();
        CardManager.populateDeck();
        CardManager.draw(GameConstants.MAX_HAND, AreaPlayer, AreaPlayerScript);
        CardManager.draw(GameConstants.MAX_HAND, AreaPlayer2, AreaPlayer2Script);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public Game Game;
    public void OnClick(){

        CardManager CardManager = Game.GetComponent<CardManager>();
        GameObject AreaPlayer = Game.getActivePlayer();
        AreaPlayer AreaPlayerScript = AreaPlayer.GetComponent<AreaPlayer>();

        if(AreaPlayerScript.cardsInHand < Game.MAX_HAND)
        {
            CardManager.draw(1, AreaPlayer, AreaPlayerScript);
            Game.switchActivePlayer();
        }


    }
}

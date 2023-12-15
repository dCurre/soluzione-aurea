using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AreaPlayer : MonoBehaviour
{
    public int cardsInHand;
    public Player player;

    public void Start()
    {
        cardsInHand = 0;
    }

    private void Update()
    {
        cardsInHand = transform.childCount;
    }
}

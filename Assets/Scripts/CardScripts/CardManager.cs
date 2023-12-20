using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<GameObject> deck = new List<GameObject>();

    public void populateDeck()
    {
        deck.AddRange(generateCardsBySeed(Seed.Blue));
        deck.AddRange(generateCardsBySeed(Seed.Green));
        deck.AddRange(generateCardsBySeed(Seed.Red));
        deck.AddRange(generateCardsBySeed(Seed.Yellow));

        shuffle();
    }
    public List<GameObject> getDeck()
    {
        return deck;
    }
    public void draw(int times, GameObject areaPlayer)
    {
        for (int i = 0; i < times; i++){
            if (deck[0].transform.parent != null)
            {
                deck[1].transform.SetParent(deck[0].transform.parent);
            }
            deck[0].transform.SetParent(areaPlayer.transform, false);
            deck[0].GetComponent<Card>().setDrawn(true);
            deck.RemoveAt(0);
        }
    }

    public void shuffle()
    {
        var count = deck.Count;
        for (var i = 0; i < count - 1; ++i){
            var r = Random.Range(i, count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }

        //Placing the cards in the deck area
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i].transform.SetParent((i == 0) ? GameObject.Find("DeckArea").transform.GetChild(0) : deck[i - 1].transform);
        }
    }

    private List<GameObject> generateCardsBySeed(Seed seed)
    {
        List<GameObject> cardList = new List<GameObject>();
        GameObject cardResource = Resources.Load<GameObject>("Card");

        for(int i = 1; i < GameConstants.CARDS_BY_SEED+1; i++)
        {
            GameObject card = Instantiate(cardResource);
            card.name = string.Format("{0} of {1}", i, seed);
            card.GetComponent<Card>().seed = seed;
            card.GetComponent<Card>().value = i;
            card.GetComponent<Card>().setDrawn(false);
            cardList.Add(card);
        }

        return cardList;
    }
}

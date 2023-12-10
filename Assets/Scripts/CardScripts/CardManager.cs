using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<GameObject> RedCards;
    public List<GameObject> BlueCards;
    public List<GameObject> YellowCards;
    public List<GameObject> GreenCards;

    private List<GameObject> deck = new List<GameObject>();
    public void populateDeck()
    {
        addAllCard(RedCards);
        addAllCard(BlueCards);
        addAllCard(YellowCards);
        addAllCard(GreenCards);

        shuffle();
    }
    public List<GameObject> getDeck()
    {
        return deck;
    }

    public void addCard(GameObject card)
    {
        deck.Add(card);
    }
    public void addAllCard(List<GameObject> cards)
    {
        deck.AddRange(cards);
    }
    public void draw(int times, GameObject areaPlayer, AreaPlayer script)
    {
        for (int i = 0; i < times; i++){
            GameObject playerCard = Instantiate(deck[0], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(areaPlayer.transform, false);
            deck.RemoveAt(0);
        }
    }

    public void shuffle()
    {
        var count = deck.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }

}

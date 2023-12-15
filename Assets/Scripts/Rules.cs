using System.Collections.Generic;
using UnityEngine;

public class Rules
{
    public static bool isDropZoneValidByRules(List<GameObject> cardList)
    {
        return basicRules(cardList);
    }

    private static bool basicRules(List<GameObject> cardList)
    {
        return isCardListGoingUp(cardList) || isCardListGoingDown(cardList) || isCardListTheSame(cardList);
    }

    private static bool isCardListGoingUp(List<GameObject> cardList)
    {
        bool isGoingUp = true;
        Card currentCard = null;
        Card nextCard = null;
        /* Regole base
         *  1. Se ci sono carte --> devono essere scale
         *  2. Stesso seme
         */

        //Check scala 1 2 3 4 ...
        for (int i = 1; i < cardList.Count; i++)
        {
            currentCard = cardList[i].GetComponent<Card>();
            nextCard = cardList[i + 1].GetComponent<Card>();

            if (currentCard.value < nextCard.value && currentCard.seed == nextCard.seed)
            {
                return false;
            }
        }

        return isGoingUp;
    }

    private static bool isCardListGoingDown(List<GameObject> cardList)
    {
        bool isGoingDown = true;
        Card currentCard = null;
        Card nextCard = null;
        /* Regole base
         *  1. Se ci sono carte --> devono essere scale
         *  2. Stesso seme
         */

        //Check scala 9 8 7 6 ...
        for (int i = 1; i < cardList.Count; i++)
        {
            currentCard = cardList[i].GetComponent<Card>();
            nextCard = cardList[i + 1].GetComponent<Card>();

            if (currentCard.value > nextCard.value && currentCard.seed == nextCard.seed)
            {
                return false;
            }
        }

        return isGoingDown;
    }

    private static bool isCardListTheSame(List<GameObject> cardList)
    {
        bool isTheSameSeed = true;
        Card currentCard = null;
        Card nextCard = null;
        /* Regole base
         *  1. Se ci sono carte --> devono essere tutte uguali
         *  2. Seme diverso
         */

        //Check carte uguali 3 = 3 = 3
        for (int i = 1; i < cardList.Count; i++)
        {
            currentCard = cardList[i].GetComponent<Card>();
            nextCard = cardList[i + 1].GetComponent<Card>();

            if (currentCard.value != nextCard.value && currentCard.seed != nextCard.seed)
            {
                return false;
            }
        }

        return isTheSameSeed;
    }

    public static bool isCardNextValueSameSeed(Card currentCard, Card toCheckCard, DropZone dropZone)
    {
        //eg. 6 is the next value of 5
        //Same seed required
        //Ladder or None zoneType required
        return currentCard.value == (toCheckCard.value + 1) && currentCard.seed == toCheckCard.seed && (dropZone.zoneType == ZoneTypeEnum.Ladder || dropZone.zoneType == ZoneTypeEnum.None);
    }
    public static bool isCardPreviousValueSameSeed(Card currentCard, Card toCheckCard, DropZone dropZone)
    {
        //eg. 6 is the previous value of 7
        //Same seed required
        //Ladder or None zoneType required
        return currentCard.value == (toCheckCard.value - 1) && currentCard.seed == toCheckCard.seed && (dropZone.zoneType == ZoneTypeEnum.Ladder || dropZone.zoneType == ZoneTypeEnum.None);
    }
    public static bool isCardSameValueDifferentSeed(Card currentCard, Card toCheckCard, DropZone dropZone)
    {
        //eg. 3 equals to 3
        //Different seed required
        //Poker or None zoneType required
        return currentCard.value == toCheckCard.value && currentCard.seed != toCheckCard.seed && (dropZone.zoneType == ZoneTypeEnum.Poker || dropZone.zoneType == ZoneTypeEnum.None);
    }
}

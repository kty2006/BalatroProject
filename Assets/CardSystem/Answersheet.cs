using System;
using System.Collections.Generic;
using UnityEngine;

public class Answersheet : MonoBehaviour
{
    public InGameData GameData;
    private List<Card> SortCards;
    private Dictionary<int, int> SameCards;
    private Queue<Func<bool>> markSheet = new();
    private int maxCardCount = 5;
    public void Start()
    {
        markSheet.Enqueue(StraighFlush);
        markSheet.Enqueue(FourCard);
        markSheet.Enqueue(FullHouse);
        markSheet.Enqueue(Flush);
        markSheet.Enqueue(Straight);
        markSheet.Enqueue(Triple);
        markSheet.Enqueue(TwoPair);
        markSheet.Enqueue(Pair);
    }

    public void MarkingTest()
    {
        SortCards = Sort(GameData.DeckData());

        SameCards = Same(SortCards);
        for (int i = 0; i < 8; i++)
        {
            if (markSheet.Dequeue().Invoke())
            {
                Debug.Log("끝");
                break;
            }
        }
    }

    public bool StraighFlush()
    {
        if (Straight() == true && Flush() == true)
        { Debug.Log("스트레이트플러시"); return true; }
        return false;
    }
    public bool FourCard()
    {
        if (SameCard(4))
        { Debug.Log("포카드"); return true; }
        return false;
    }
    public bool FullHouse()
    {
        if (SameCard(3) == true && SameCard(2) == true)
        { Debug.Log("풀하우스"); return true; }
        return false;
    }
    public bool Flush()
    {
        if (maxCardCount == SortCards.Count)
        {
            for (int i = 1; i <= SortCards.Count - 1; i++)
            {
                if (SortCards[0].Type() != SortCards[i].Type())
                {
                    return false;
                }
            }
            Debug.Log("플러시");
            return true;
        }
        return false;
    }
    public bool Straight()
    {
        if (maxCardCount == SortCards.Count)
        {
            for (int i = 0; i < SortCards.Count - 1; i++)
            {
                if ((SortCards[i].GroupNumber() + 1) != SortCards[i + 1].GroupNumber())
                {
                    return false;
                }
            }
            Debug.Log("스트레이트");
            return true;
        }
        return false;
    }
    public bool Triple()
    {
        if (SameCard(3))
        {
            Debug.Log("트리플");
            return true;
        }
        return false;
    }
    public bool TwoPair()
    {
        int checkCard = 15;
        int checkCount = 0;
        for (int i = 0; i < SortCards.Count - 1; i++)
        {
            if (SameCards.ContainsKey(SortCards[i].GroupNumber()) && SortCards[i].GroupNumber()
                != checkCard && SameCards[SortCards[i].GroupNumber()] == 2)
            {
                checkCard = SortCards[i].GroupNumber();
                checkCount++;
                if (checkCard == 2)
                { Debug.Log("투페어"); return true; }
            }
        }
        return false;
    }
    public bool Pair()
    {
        if (SameCard(2))
        {
            Debug.Log("페어");
            return true;
        }
        return false;
    }

    public bool SameCard(int count)
    {
        for (int i = 0; i < SortCards.Count - 1; i++)
        {
            if (SameCards.ContainsKey(SortCards[i].GroupNumber()) && SameCards[SortCards[i].GroupNumber()] == count)
            {
                return true;
            }
        }
        return false;
    }

    public List<Card> Sort(List<Card> deck)
    {
        for (int i = 0; i < deck.Count - 1; i++)
        {
            for (int j = 1; j <= deck.Count - 1; j++)
            {
                if (deck[i].GroupNumber() > deck[j].GroupNumber())
                {
                    (deck[i], deck[j]) = (deck[j], deck[i]);
                }
            }
        }
        return deck;
    }

    public Dictionary<int, int> Same(List<Card> deck)
    {
        Dictionary<int, int> sameCardCount = new();
        for (int i = 0; i < deck.Count - 1; i++)
        {
            Debug.Log(deck[i].GroupNumber()+ " : "+ deck[i+1].GroupNumber());
            if (deck[i].GroupNumber() == deck[i + 1].GroupNumber())
            {
                if (!sameCardCount.ContainsKey(deck[i].GroupNumber()))
                {
                    Debug.Log("등록");
                    sameCardCount.Add(deck[i].GroupNumber(), 2);
                }
                else
                {
                    sameCardCount[deck[i].GroupNumber()]++;
                }
            }
        }
        return sameCardCount;
    }

    //플러시
    //스트레이트
    //스트레이트플러시

    //투페어
    //풀하우스

    //SameCard
    //포카드
    //트리플
    //페어
    //하이카드
}

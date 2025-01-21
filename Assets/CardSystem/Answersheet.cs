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
    private Stack<Action> lastAction = new();
    //private Action lastAction;
    public void Start()
    {
        Set();
    }

    public void Set()
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
                int count = lastAction.Count;
                for (int j = 0; j < count; j++)
                {
                    lastAction.Pop()?.Invoke();
                }
                break;
            }
        }
    }
    public bool StraighFlush()
    {
        if (Straight() && Flush())
        {
            ReMoveLastAction(2);
            lastAction.Push(() => Debug.Log("��Ʈ����Ʈ�÷���"));
            lastAction.Push(() => GameData.ChipSum(100).ScoreSum(8));
            return true;
        }
        return false;
    }
    public bool FourCard()
    {
        if (SameCard(4))
        {
            lastAction.Push(() => Debug.Log("��ī��"));
            lastAction.Push(() => GameData.ChipSum(60).ScoreSum(7));
            return true;
        }
        lastAction.Clear();
        return false;
    }
    public bool FullHouse()
    {
        if (SameCard(3) == true && SameCard(2) == true)
        {
            lastAction.Push(() => Debug.Log("Ǯ�Ͽ콺"));
            lastAction.Push(() => GameData.ChipSum(40).ScoreSum(4));
            return true;
        }
        lastAction.Clear();
        return false;
    }
    public bool Flush()
    {
        int count = 1;
        if (maxCardCount <= SortCards.Count)
        {
            for (int i = 0; i < SortCards.Count; i++)
            {
                int index = i; // ���� ������ ĸó
                lastAction.Push(() => GameData.ChipSum(SortCards[index].SumNumber()));

                if (i > 0)
                {
                    count++;
                    if (SortCards[0].Type() != SortCards[i].Type())
                    {
                        return false;
                    }
                }
            }

            if (count == maxCardCount)
            {
                lastAction.Push(() => Debug.Log("�÷���"));
                lastAction.Push(() => GameData.ChipSum(35).ScoreSum(4));
                return true;
            }
        }
        lastAction.Clear();
        return false;
    }
    //
    public bool Straight()
    {
        if (maxCardCount <= SortCards.Count)
        {
            for (int i = 0; i < maxCardCount; i++)
            {
                int index = i;
                lastAction.Push(() => GameData.ChipSum(SortCards[index].SumNumber()));
                if (i < 4)
                {
                    if ((SortCards[i].GroupNumber() + 1) != SortCards[i + 1].GroupNumber())
                    {
                        return false;
                    }
                }
            }
            lastAction.Push(() => Debug.Log("��Ʈ����Ʈ"));
            lastAction.Push(() => GameData.ChipSum(30).ScoreSum(4));
            return true;
        }
        lastAction.Clear();
        return false;
    }
    public bool Triple()
    {
        if (SameCard(3))
        {
            lastAction.Push(() => Debug.Log("Ʈ����"));
            lastAction.Push(() => GameData.ChipSum(30).ScoreSum(3));
            return true;
        }
        lastAction.Clear();
        return false;
    }
    public bool TwoPair()
    {
        HashSet<int> checkCard = new();
        int checkCount = 0;
        for (int i = 0; i <= SortCards.Count - 1; i++)
        {
            if (SameCards.ContainsKey(SortCards[i].GroupNumber()) && !checkCard.Contains(SortCards[i].GroupNumber()) && SameCards[SortCards[i].GroupNumber()] == 2)
            {
                checkCard.Add(SortCards[i].GroupNumber());
                checkCount++;
                if (checkCard.Count == 2)
                {
                    lastAction.Push(() => GameData.ChipSum(SortCards[i].SumNumber() * 2));
                    lastAction.Push(() => Debug.Log("�����"));
                    lastAction.Push(() => GameData.ChipSum(20).ScoreSum(2));
                    return true;
                }
            }
        }
        lastAction.Clear();
        return false;
    }
    public bool Pair()
    {
        if (SameCard(2))
        {
            lastAction.Push(() => Debug.Log("���"));
            lastAction.Push(() => GameData.ChipSum(10).ScoreSum(2));
            return true;
        }
        lastAction.Clear();
        return false;
    }

    public bool SameCard(int count)
    {
        for (int i = 0; i < SortCards.Count - 1; i++)
        {
            if (SameCards.ContainsKey(SortCards[i].GroupNumber()) && SameCards[SortCards[i].GroupNumber()] == count)
            {
                lastAction.Push(() => GameData.ChipSum(SortCards[i].SumNumber() * count));
                return true;
            }
        }
        return false;
    }

    public List<Card> Sort(List<Card> deck)
    {
        for (int i = 0; i < deck.Count - 1; i++)
        {
            for (int j = i + 1; j <= deck.Count - 1; j++)
            {
                if (deck[i].GroupNumber() >= deck[j].GroupNumber())
                {
                    Card sub = deck[i];
                    deck[i] = deck[j];
                    deck[j] = sub;
                }
            }
        }
        return deck;
    }

    public Dictionary<int, int> Same(List<Card> deck)
    {
        Dictionary<int, int> sameCardCount = new();
        HashSet<int> checkCard = new();
        for (int i = 0; i < deck.Count - 1; i++)
        {
            for (int j = i + 1; j <= deck.Count - 1; j++)
            {
                if (!checkCard.Contains(deck[i].GroupNumber()) && deck[i].GroupNumber() == deck[j].GroupNumber())
                {
                    if (!sameCardCount.ContainsKey(deck[i].GroupNumber()))
                    {
                        sameCardCount.Add(deck[i].GroupNumber(), 2);
                    }
                    else
                    {
                        sameCardCount[deck[i].GroupNumber()]++;
                    }
                }
            }
            checkCard.Add(deck[i].GroupNumber());
        }
        return sameCardCount;
    }

    public void ReMoveLastAction(int count)
    {
        for (int i = 0; i < count; i++)
        {
            lastAction.Pop();
        }
    }

}

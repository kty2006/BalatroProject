using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardsData", menuName = "Scriptable Objects/CardsData")]
public class CardsData : ScriptableObject
{
    public Sprite[] CardBacks;
    public Sprite[] PlayingCards;
    public List<Card> cards = new();
    public List<Card> dack = new();
    public string CardsImageName;
    public string CardBackImageName;

    [ContextMenu("GetCards")]
    public void GetCardsData()
    {
        PlayingCards = Resources.LoadAll<Sprite>(CardsImageName);
    }

    [ContextMenu("GetCardBacks")]
    public void GetCardBacksData()
    {
        CardBacks = Resources.LoadAll<Sprite>(CardBackImageName);
    }

    public Card FindCard(Image obj)
    {
        foreach (var card in cards)
        {
            if (card.Image().Equals(obj))
            {
                return card;
            }
        }
        return null;
    }

    public void CardsAdd(Card card)
    {
        cards.Add(card);
    }

    public Card CardGet(int index)
    {
        return cards[index];
    }

    public void DackAdd(Card card)
    {
        if (dack.Count < 6)
        {
            dack.Add(card);
        }
        else
        {
            Debug.LogError($"[CardData] DackOverflow {nameof(dack)}");
        }
    }

    public void DackReMove(Card card)
    {
        dack.Remove(card);
    }

    public void SettingDack()
    {
        cards = new();
        dack = new();
    }
}

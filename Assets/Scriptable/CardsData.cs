using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardsData", menuName = "Scriptable Objects/CardsData")]
public class CardsData : ScriptableObject
{
    public Sprite[] CardBacks;
    public Sprite[] PlayingCards;
    public HashSet<Card> Cards =new();
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
}

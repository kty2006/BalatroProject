using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardsDataBase", menuName = "Scriptable Objects/CardsDataBase")]
public class CardsDataBase : ScriptableObject
{
    public Sprite[] CardBacks;
    public Sprite[] PlayingCards;
    
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
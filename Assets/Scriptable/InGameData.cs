using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{
    [SerializeField] private List<Card> randomCardList = new();
    [SerializeField] private List<Card> deckData = new();
    [SerializeField] private List<GameObject> deckUi = new();

    #region RandomCardList
    public Card FindCard(Image obj)
    {
        foreach (var card in randomCardList)
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
        randomCardList.Add(card);
    }

    public Card CardGet(int index)
    {
        return randomCardList[index];
    }
    #endregion

    #region deck
    public List<Card> DeckData()
    { return deckData; }
    public void DeckAdd(Card card, GameObject cardUI)
    {
        if (deckData.Count < 6)//하드코딩***
        {
            deckData.Add(card);
            deckUi.Add(cardUI);
        }
        else
        {
            Debug.LogError($"[CardData] DackOverflow {nameof(deckData)}");
        }
    }

    public InGameData DeckReMove(Card card)//매개변수를 여러개두고 작업하면 코드가 복잡해지고 함수를 사용할때 어떤 인자값인지 정확하지 않기때문에 빌더패턴처럼 반환값을 설정해 여러개함수로 쪼개 직관적으로 사용함
    {
        deckData.Remove(card);
        return this;
    }

    public InGameData DeckUiReMove(GameObject card)
    {
        deckUi.Remove(card);
        return this;
    }

    public int DeckAllReMove()//오브젝트풀링으로 고쳐야함
    {
        int count = deckUi.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(deckUi[i]);
        }
        deckData.Clear();
        deckUi.Clear();
        return count;
    }
    #endregion
    public void SettingDack()
    {
        randomCardList = new();
        deckData = new();
        deckUi = new();
    }
}

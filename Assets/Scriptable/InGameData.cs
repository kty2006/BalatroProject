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
        if (deckData.Count < 6)//�ϵ��ڵ�***
        {
            deckData.Add(card);
            deckUi.Add(cardUI);
        }
        else
        {
            Debug.LogError($"[CardData] DackOverflow {nameof(deckData)}");
        }
    }

    public InGameData DeckReMove(Card card)//�Ű������� �������ΰ� �۾��ϸ� �ڵ尡 ���������� �Լ��� ����Ҷ� � ���ڰ����� ��Ȯ���� �ʱ⶧���� ��������ó�� ��ȯ���� ������ �������Լ��� �ɰ� ���������� �����
    {
        deckData.Remove(card);
        return this;
    }

    public InGameData DeckUiReMove(GameObject card)
    {
        deckUi.Remove(card);
        return this;
    }

    public int DeckAllReMove()//������ƮǮ������ ���ľ���
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

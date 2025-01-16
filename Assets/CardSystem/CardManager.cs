using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //�� Ŭ������ MVP������ �Ⱦ����� ui������ �Է����� �� �ϱ� ������ �� ������ Presenter�� Model�� ��������ٺκ��� ���� CardUi�� Mvp������ ������� �ʾҴ�.
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;
    private int number;
    public void Start()
    {
        InGameData.SettingDack();
        CreateCards(5);
    }

    public void CardDrow()//������ƮǮ������ ���ľ���
    {
        CreateCards(InGameData.DeckAllReMove());
    }

    [ContextMenu("CardCreate")]
    public void CreateCards(int count)//ī�� ������ �������� ī�� ����Ŭ������ ���δ°� �������� ���� ���������ؼ� ���� �Լ��� ������� ������ //������ƮǮ������ ��ü�ؾ���
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, cards.transform);
            number = Random.Range(0, 51);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[number];
            InGameData.CardsAdd(new CardBuild().Image(cardObject.GetComponent<Image>()).Number(number).Type(number).Class(number).Build());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Card card;
        if (clickedObject.TryGetComponent(out Image image))
        {
            card = InGameData.FindCard(image);
            if (card.State() == CardState.Drow)
            {
                clickedObject.transform.localPosition += Vector3.up * 100;//�ϵ��ڵ�***
                InGameData.DeckAdd(card, clickedObject);
            }
            else
            {
                clickedObject.transform.localPosition += Vector3.down * 100;//�ϵ��ڵ�***
                InGameData.DeckReMove(card).DeckUiReMove(clickedObject);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one * 1.1f;//�ϵ��ڵ�***
    }
}

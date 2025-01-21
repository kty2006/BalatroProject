using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //�� Ŭ������ MVP������ �Ⱦ����� ui������ �Է����� �� �ϱ� ������ �� ������ Presenter�� Model�� ��������ٺκ��� ���� CardUi�� Mvp������ ������� �ʾҴ�.
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;

    //�и��Ұ�
    public Button DrowButton;
    public TextMeshProUGUI TotalChip;

    public void Start()
    {
        InGameData.SettingDack();
        CreateCards(CardsData.PlayingCards.Length);
        CardDrow(5);
        DrowButton.onClick.AddListener(() => CardDrow(InGameData.DeckAllReMove()));
    }

    public void Update()
    {
        TotalChip.text = $"{InGameData.TotalChip().Item1} : {InGameData.TotalChip().Item2}";
    }

    public void CardDrow(int count)//������ƮǮ������ ���ľ���
    {
        int num = 0;
        int currentCount = count;
        for (int i = 0; i < currentCount; i++)
        {
            num = Random.Range(0, 52);
            if (InGameData.CardDBContains(num))
            {
                GameObject cardObject = Instantiate(cardPrefab, cards.transform);
                cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[num]; //ĸ��ȭ �ؾ���***
                InGameData.CardsAdd(new CardBuild().Image(cardObject.GetComponent<Image>()).Number(num).Type(num).Class(num).Build());
                InGameData.CardDBReMove(num);
            }
            else
            {
                Mathf.Clamp(currentCount++, 1, 10);//
            }
        }
    }

    [ContextMenu("CardCreate")]
    public void CreateCards(int count)//ī�� ������ �������� ī�� ����Ŭ������ ���δ°� �������� ���� ���������ؼ� ���� �Լ��� ������� ������ //������ƮǮ������ ��ü�ؾ���
    {
        for (int i = 0; i < count; i++)
        {
            InGameData.CardDBAdd(i);
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
                //Debug.Log(card.GroupNumber());
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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //이 클래스에 MVP패턴을 안쓴이유 ui조작을 입력조작 만 하기 때문에 이 조작은 Presenter가 Model과 연결시켜줄부분이 없어 CardUi는 Mvp패턴을 사용하지 않았다.
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;

    //분리할것
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

    public void CardDrow(int count)//오브젝트풀링으로 고쳐야함
    {
        int num = 0;
        int currentCount = count;
        for (int i = 0; i < currentCount; i++)
        {
            num = Random.Range(0, 52);
            if (InGameData.CardDBContains(num))
            {
                GameObject cardObject = Instantiate(cardPrefab, cards.transform);
                cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[num]; //캡슐화 해야함***
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
    public void CreateCards(int count)//카드 종류가 많아지면 카드 생성클래스로 빼두는게 좋을수도 있음 생성관련해서 여러 함수가 생길수도 있으니 //오브젝트풀링으로 교체해야함
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
                clickedObject.transform.localPosition += Vector3.up * 100;//하드코딩***
                InGameData.DeckAdd(card, clickedObject);
                //Debug.Log(card.GroupNumber());
            }
            else
            {
                clickedObject.transform.localPosition += Vector3.down * 100;//하드코딩***
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
        eventData.pointerEnter.transform.localScale = Vector3.one * 1.1f;//하드코딩***
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //이 클래스에 MVP패턴을 안쓴이유 ui조작을 입력조작 만 하기 때문에 이 조작은 Presenter가 Model과 연결시켜줄부분이 없어 CardUi는 Mvp패턴을 사용하지 않았다.
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

    public void CardDrow()//오브젝트풀링으로 고쳐야함
    {
        CreateCards(InGameData.DeckAllReMove());
    }

    [ContextMenu("CardCreate")]
    public void CreateCards(int count)//카드 종류가 많아지면 카드 생성클래스로 빼두는게 좋을수도 있음 생성관련해서 여러 함수가 생길수도 있으니 //오브젝트풀링으로 교체해야함
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
                clickedObject.transform.localPosition += Vector3.up * 100;//하드코딩***
                InGameData.DeckAdd(card, clickedObject);
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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //이 클래스에 MVP패턴을 안쓴이유 ui조작을 입력조작 만 하기 때문에 이 조작은 Presenter가 Model과 연결시켜줄부분이 없어 CardUi는 Mvp패턴을 사용하지 않았다.
    [SerializeField] private CardCreate cardCreate;
    public CardsData CardsData;
    public void Start()
    {
        CardsData.SettingDack();
        cardCreate.CreateCards(5);//***의존성
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Card card;
        if (clickedObject.TryGetComponent(out Image image))
        {
            card = CardsData.FindCard(image);
            if (card.State() == CardState.Drow)
            {
                clickedObject.transform.localPosition += Vector3.up * 100;
                CardsData.DackAdd(card);
            }
            else
            {
                clickedObject.transform.localPosition += Vector3.down * 100;
                CardsData.DackReMove(card);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one * 1.1f;
    }
}

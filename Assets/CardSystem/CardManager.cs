using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //�� Ŭ������ MVP������ �Ⱦ����� ui������ �Է����� �� �ϱ� ������ �� ������ Presenter�� Model�� ��������ٺκ��� ���� CardUi�� Mvp������ ������� �ʾҴ�.
    [SerializeField] private CardCreate cardCreate;
    public CardsData CardsData;
    public void Start()
    {
        CardsData.SettingDack();
        cardCreate.CreateCards(5);//***������
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

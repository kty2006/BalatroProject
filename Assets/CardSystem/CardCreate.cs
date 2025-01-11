using UnityEngine;
using UnityEngine.UI;

public class CardCreate : MonoBehaviour
{
    public CardsData CardsData;
    public Canvas canvas;
    public GameObject CardPrefab;

    public void Start()
    {
    }
    [ContextMenu("CardCreate")]
    public void CreateCards(/*int count*/)
    {
        GameObject cardObject = Instantiate(CardPrefab, canvas.transform);
        cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[0];
        CardsData.Cards.Add(new CardBuild().Image(cardObject.GetComponent<Image>()).Number(1).Type(CardType.Heart).Build());
    }
}

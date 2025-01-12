using UnityEngine;
using UnityEngine.UI;

public class CardCreate : MonoBehaviour
{
    public CardsData CardsData;
    public GameObject cards;
    public GameObject CardPrefab;
    private int number;

    [ContextMenu("CardCreate")]
    public void CreateCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cardObject = Instantiate(CardPrefab, cards.transform);
            number = Random.Range(0, 51);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[number];
            CardsData.CardsAdd(new CardBuild().Image(cardObject.GetComponent<Image>()).Number(number).Type().Build());
            if (i != 0)
                CardsData.CardGet(i).CardSpacing(i);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Heart, Clover, Diamond, Spade }
public enum CardState { None, Drow }
public class Card : MonoBehaviour
{
    private Image image;//이미지
    private int number;//숫자
    private CardType type;//타입
    private CardState state = CardState.None;
    private int spacing = 150;
    //추가능력

    public Card(Image image, int number, CardType type)
    {
        this.image = image;
        this.number = number;
        this.type = type;
    }

    public void CardSpacing(float count)
    {
        image.rectTransform.localPosition = new Vector3(count * spacing, 0, 0);
    }

    public Image Image()
    {
        return image;
    }

    public CardType Type()
    {
        return type;
    }

    public int Number()
    {
        return number;
    }

    public CardState State()
    {
        return state = (state == CardState.None) ? CardState.Drow : CardState.None;
    }
}

public class CardBuild
{
    private Image image;//이미지
    private int number;//숫자
    private CardType type;//타입

    public CardBuild Image(Image image)
    {
        this.image = image;
        return this;
    }

    public CardBuild Number(int number)
    {
        this.number = number;
        return this;
    }

    public CardBuild Type()
    {
        switch (number)
        {
            case <= 12:
                type = CardType.Heart;
                break;
            case <= 25:
                type = CardType.Clover;
                break;
            case <= 38:
                type = CardType.Diamond;
                break;
            case <= 51:
                type = CardType.Spade;
                break;
        }
        return this;
    }

    public Card Build()
    {
        return new Card(image, number, type);
    }
}

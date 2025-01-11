using UnityEngine;
using UnityEngine.UI;

public enum CardType { Heart, Clover, Diamond, Spade }
public class Card 
{
    public Image Image;//이미지
    public int Number;//숫자
    public CardType Type;//타입
    //추가능력

    public Card(Image image, int number, CardType type)
    {
        Image = image;
        Number = number;
        Type = type;
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

    public CardBuild Type(CardType type)
    {
        this.type = type;
        return this;
    }

    public Card Build()
    {
        return new Card(image, number, type);
    }
}

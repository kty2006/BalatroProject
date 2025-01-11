using UnityEngine;
using UnityEngine.UI;

public enum CardType { Heart, Clover, Diamond, Spade }
public class Card 
{
    public Image Image;//�̹���
    public int Number;//����
    public CardType Type;//Ÿ��
    //�߰��ɷ�

    public Card(Image image, int number, CardType type)
    {
        Image = image;
        Number = number;
        Type = type;
    }

}

public class CardBuild
{
    private Image image;//�̹���
    private int number;//����
    private CardType type;//Ÿ��

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

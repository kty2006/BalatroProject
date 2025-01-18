using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Heart, Clover, Diamond, Spade }
public enum CardState { None, Drow }
public enum CardClass { Number, Image }
[System.Serializable]
public class Card 
{
    //특수카드같은경우 특수능력 실행 클래스를 만들고  종류를 enum으로 만들어 특수카드 능력 실행시 Card클래스에 상태만 특수능력 클래스 함수에 넘겨주어 타입에 따라 능력실행되게 
    private Image image;//이미지
    private int sumNumber;//공격 숫자
    private int groupNumber;//포커 체크 숫자
    private CardType type;//타입
    private CardClass cardClass;//카드 종류
    private CardState state = CardState.None;
    //추가능력

    public Card(Image image, int sumNumber, int groupNumber, CardType type, CardClass cardClass)
    {
        this.image = image;
        this.sumNumber = sumNumber;
        this.groupNumber = groupNumber;
        this.type = type;
        this.cardClass = cardClass;
    }

    public Image Image()
    {
        return image;
    }

    public CardType Type()
    {
        return type;
    }

    public int SumNumber()
    {
        return sumNumber;
    }

    public int GroupNumber()
    {
        return groupNumber;
    }

    public CardClass Class()
    {
        return cardClass;
    }

    public CardState State()
    {
        return state = (state == CardState.None) ? CardState.Drow : CardState.None;
    }
}

public class CardBuild
{
    private Image image;//이미지
    private int sumNumber;//공격 숫자
    private int groupNumber;//포커 체크 숫자
    private CardType type;//타입
    private CardClass cardClass;//종류

    public CardBuild Image(Image image)
    {
        this.image = image;
        return this;
    }

    public CardBuild Number(int number)//중복된곳 함수로 빼기
    {
        switch (number)
        {
            case <= 12:
                this.sumNumber = (number != 12) ? Mathf.Clamp(number + 2, 2, 10) : 11;
                groupNumber = number;
                break;
            case <= 25:
                this.sumNumber = (number != 25) ? Mathf.Clamp((number + 2) - 13, 2, 10) : 11;
                groupNumber = number - 13;
                break;
            case <= 38:
                this.sumNumber = (number != 38) ? Mathf.Clamp((number + 2) - 26, 2, 10) : 11;
                groupNumber = number - 26;
                break;
            case <= 51:
                this.sumNumber = (number != 51) ? Mathf.Clamp((number + 2) - 39, 2, 10) : 11;
                groupNumber = number - 39;
                break;
        }
        return this;
    }

    public CardBuild Type(int number)
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

    public CardBuild Class(int number)
    {
        switch (number)
        {
            case <= 12:
                SelectClass(number, 9, 11);
                break;
            case <= 25:
                SelectClass(number, 22, 24);
                break;
            case <= 38:
                SelectClass(number, 35, 37);
                break;
            case <= 51:
                SelectClass(number, 48, 50);
                break;
        }
        return this;
    }

    private void SelectClass(int number, int frontNum, int backNum)
    {
        cardClass = (frontNum <= number && number <= backNum) ? CardClass.Image : CardClass.Number;
    }

    public Card Build()
    {
        return new Card(image, sumNumber, groupNumber, type, cardClass);
    }
}

using UnityEngine;
using Cysharp.Threading.Tasks;

public class TurnUIPresenter : MonoBehaviour
{
    public TurnUIView playUI;

    public void Start()
    {//������ ���� ��ư�� ���������� �̺�Ʈ �ڵ鷯 �ൿ�� �ٲ��
        playUI.Attack.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerAttack>(new PlayerAttack()));
        playUI.Move.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerMove>(new PlayerMove()));
        playUI.Start.onClick.AddListener(() => Local.TurnSystem.TurnStart(true));
        playUI.Remove.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerTurnSystem>(new PlayerTurnSystem()));
    }
}

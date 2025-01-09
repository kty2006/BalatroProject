using UnityEngine;
using Cysharp.Threading.Tasks;

public class UIPresenter : MonoBehaviour
{
    public UIView playUI;

    public void Start()
    {//������ ���� ��ư�� ���������� �̺�Ʈ �ڵ鷯 �ൿ�� �ٲ��
        playUI.Attack.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerAttack>(new PlayerAttack()));
        playUI.Move.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerMove>(new PlayerMove()));
        playUI.Start.onClick.AddListener(() => Local.TurnSystem.Invoke().Forget());
        playUI.Remove.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerTurnSystem>(new PlayerTurnSystem()));
    }
}

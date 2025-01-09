using UnityEngine;
using Cysharp.Threading.Tasks;

public class UIPresenter : MonoBehaviour
{
    public UIView playUI;

    public void Start()
    {//옵저버 패턴 버튼이 눌릴때마다 이벤트 핸들러 행동이 바뀌게
        playUI.Attack.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerAttack>(new PlayerAttack()));
        playUI.Move.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerMove>(new PlayerMove()));
        playUI.Start.onClick.AddListener(() => Local.TurnSystem.Invoke().Forget());
        playUI.Remove.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerTurnSystem>(new PlayerTurnSystem()));
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public void Start()
    {
        Local.EventHandler.Invoke<PlayerTurnSystem>(new PlayerTurnSystem());
        Local.TurnSystem.Invoke().Forget();
    }

    public void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ComeHere : MonoBehaviour
{
    public MyEvent OnCame;
    [Inject] private Player player;

    public async void CallPlayer()
    {
        await player.GoHere(transform);
        OnCame.Invoke();
    }
}

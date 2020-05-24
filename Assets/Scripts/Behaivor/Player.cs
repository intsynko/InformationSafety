using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private MoveByJoystic moveByJoystic;
    [SerializeField] private MoveByPathFinder moveByPathFinder;
    [Inject] private MySceneController _mySceneController;
    [Inject] private MessageBox _messageBox;
    public Move CurrentMove {
        get {
            if (moveByPathFinder.enabled) return moveByPathFinder;
            else return moveByJoystic;
        }
    }

    private async void Start()
    {
        moveByPathFinder.BoxCollider2D = GetComponent<BoxCollider2D>();
        moveByPathFinder.enabled = false;
        _mySceneController.TeleportMeIfIMust(gameObject);
        await _messageBox.SaveAnim();
    }

    public async Task GoHere(Transform transform)
    {
        moveByPathFinder.enabled = true;
        moveByJoystic.enabled = false;
        await moveByPathFinder.MoveToEntity(transform);
        moveByJoystic.enabled = true;
        moveByPathFinder.enabled = false;
    }
}

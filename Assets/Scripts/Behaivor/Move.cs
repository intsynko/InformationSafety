using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Move : MonoBehaviour
{
    public float Speed = 2f;
    [Inject] private JoysticController _joysticController;
    [Inject] private MySceneController _mySceneController;
    [Inject] private MessageBox _messageBox;
    private Vector2 Direction { get { return _joysticController.currentJoystick.Direction; } }


    // ToDO: вынести отсюда на хер телепор, далжен быть специльный класс Player
    private void Start()
    {
        _messageBox.SaveAnim();
        _mySceneController.TeleportMeIfIMust(gameObject);
    }

    void Update()
    { 
        TurnAround(Direction);
        transform.Translate(Speed * Time.deltaTime * Direction);
    }

    private void TurnAround(Vector2 dir)
    {
        if (dir.x > 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (dir.x < 0) transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}

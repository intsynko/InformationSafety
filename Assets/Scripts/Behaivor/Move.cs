using Pathfinding;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Move : MonoBehaviour
{
    public float Speed = 4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private MyAIPath myAIPath;
    [SerializeField] private AIDestinationSetter destinationSetter;

    [Inject] private JoysticController _joysticController;
    [Inject] private MySceneController _mySceneController;
    [Inject] private MessageBox _messageBox;
    
    private BoxCollider2D boxCollider2D;
    private GameObject grafix;
    public Vector2 Direction { get {
            if (_moveByAIPath)
                return myAIPath.desiredVelocity;
            return _joysticController.currentJoystick.Direction;
        }
    }
    private Rigidbody2D rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private bool _moveByAIPath;


    public async Task MoveToEntity(Transform transform)
    {
        destinationSetter.target = transform;
        myAIPath.enabled = true;
        _moveByAIPath = true;
        myAIPath.isComplited = false;
        boxCollider2D.enabled = false;
        await Task.Yield();
        while (!myAIPath.isComplited) await Task.Yield();
        myAIPath.enabled = false;
        _moveByAIPath = false;
        boxCollider2D.enabled = true;
    }


    // ToDO: вынести отсюда на хер телепор, далжен быть специльный класс Player
    private void Start()
    {
        myAIPath.enabled = false;
        grafix = transform.GetChild(0).gameObject;
        boxCollider2D = GetComponent<BoxCollider2D>();
        _messageBox.SaveAnim();
        _mySceneController.TeleportMeIfIMust(gameObject);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        TurnAround(Direction);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, Direction * Speed, ref m_Velocity, m_MovementSmoothing);
    }

    private void TurnAround(Vector2 dir)
    {
        Vector2 localScale = grafix.transform.localScale;
        if (dir.x > 0) grafix.transform.localScale = new Vector2(Mathf.Abs(localScale.x), localScale.y);
        else if (dir.x < 0) grafix.transform.localScale = new Vector2(-Mathf.Abs(localScale.x), localScale.y);
    }
}

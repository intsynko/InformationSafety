using Pathfinding;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Move : MonoBehaviour
{
    public float Speed = 4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

    protected BoxCollider2D boxCollider2D;
    private GameObject grafix;
    public abstract Vector2 Direction { get; }
    private Rigidbody2D myRigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private void Start()
    {
        grafix = transform.GetChild(0).gameObject;
        boxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        TurnAround(Direction);
        myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, Direction * Speed, ref m_Velocity, m_MovementSmoothing);
    }

    private void TurnAround(Vector2 dir)
    {
        Vector2 localScale = grafix.transform.localScale;
        if (dir.x > 0) grafix.transform.localScale = new Vector2(Mathf.Abs(localScale.x), localScale.y);
        else if (dir.x < 0) grafix.transform.localScale = new Vector2(-Mathf.Abs(localScale.x), localScale.y);
    }
}

using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour
{

    public float damping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    
    public bool isOffsetCamera = true;
    public bool isFloatingCamera = true;

    private Transform player;
    private bool faceLeft;
    private int lastX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(faceLeft);
    }

    public void FindPlayer(bool playerFaceLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerFaceLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) faceLeft = false;
            else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            target = new Vector3(player.position.x, player.position.y, transform.position.z);

            if (isOffsetCamera)
            {
                if (faceLeft)
                {
                    target += new Vector3(-offset.x, offset.y, 0);
                }
                else
                {
                    target += new Vector3(-offset.x, offset.y, 0);
                }
            }
            Vector3 currentPosition;
            if (isFloatingCamera)
                currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            else
                currentPosition = new Vector3(target.x, target.y, -10);
            transform.position = currentPosition;
        }
    }
}
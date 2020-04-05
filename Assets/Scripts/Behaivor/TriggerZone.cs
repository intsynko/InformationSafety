using UnityEngine;

public delegate void DTrigger(GameObject target);


public class TriggerZone : MonoBehaviour
{
    public event DTrigger TriggerEnter;
    public event DTrigger TriggerExit;
    
    [SerializeField] public bool isTriggered { get; private set; }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide with Player");
            if (!isTriggered)
            {
                isTriggered = true;
                if (TriggerEnter != null)
                    TriggerEnter(this.gameObject);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("DeCollide with Player");
            if (isTriggered)
            {
                isTriggered = false;
                if (TriggerExit != null)
                    TriggerExit(this.gameObject);
            }
        }
    }
}

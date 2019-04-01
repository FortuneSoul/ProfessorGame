using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    [SerializeField] GameObject objectToInteract = default;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.GetComponent<Player>() != null || 
            other.transform.GetComponent<Clone>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IDeactivatable deactivatable = objectToInteract.GetComponent<IDeactivatable>();
                deactivatable.OnInteract();
            }
        }
    }
}

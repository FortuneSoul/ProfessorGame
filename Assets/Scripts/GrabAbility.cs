using System.Collections;
using UnityEngine;

public class GrabAbility : MonoBehaviour
{
    bool hasAnObject = false;
    Grabbable grabbedObject;
    IEnumerator ThrowRoutine;

    public Transform grabbedObjectPoint;

    private void Awake()
    {
        ThrowRoutine = AwaitForThrowRoutine();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasAnObject)
        {
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable != null && Input.GetKeyDown(KeyCode.E))
            {
                StopCoroutine(ThrowRoutine);
                grabbable.OnInteract(transform);
                grabbedObject = grabbable;
                hasAnObject = true;
                StartCoroutine(ThrowRoutine);
            }
        }
    }

    private IEnumerator AwaitForThrowRoutine()
    {
        yield return new WaitForSeconds(.25f);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                grabbedObject.OnInteract(transform);
                hasAnObject = false;
            }

            yield return null;
        }
    }
}

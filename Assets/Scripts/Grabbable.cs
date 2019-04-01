using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Grabbable : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector2 throwStrengthMinMax = new Vector2(100f, 150f);
    private Transform grabbedObjectPoint = default(Transform);
    private Transform parent;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private float offset;
    private bool grabbed;
    private float originalGravityScale;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        originalGravityScale = rigidbody.gravityScale;
    }

    private void Update()
    {
        if (grabbed)
        {
            transform.localPosition = grabbedObjectPoint.localPosition;
        }
    }

    private void Grab(Transform parent)
    {
        grabbed = true;
        MakeThisChild(parent);
        rigidbody.gravityScale = 0f;
        rigidbody.freezeRotation = true;
    }

    private void Throw(Transform interactor)
    {
        transform.parent = null;
        grabbed = false;
        rigidbody.freezeRotation = false;
        rigidbody.gravityScale = originalGravityScale;
        // Vector2 forceVelocity = (Vector2.up  - (Vector2)transform.localPosition) * Random.Range(throwStrengthMinMax.x, throwStrengthMinMax.y);
        // rigidbody.AddForce(forceVelocity);
    }

    private void MakeThisChild(Transform parent)
    {
        GrabAbility grabAbility = parent.gameObject.GetComponent<GrabAbility>();
        grabbedObjectPoint = grabAbility.grabbedObjectPoint;
        transform.parent = parent;
        float halfParentHeigth = parent.GetComponent<Collider2D>().bounds.extents.y;
        float myHeigth = collider.bounds.extents.y;
        offset = myHeigth + halfParentHeigth + .1f;
    }

    public void OnInteract(Transform interactor)
    {
        if (grabbed)
        {
            Throw(interactor);
        }
        else
        {
            Grab(interactor);
        }
    }
}

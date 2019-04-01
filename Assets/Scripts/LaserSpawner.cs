using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSpawner : MonoBehaviour, IDeactivatable
{
    private LineRenderer laserLine;
    #pragma warning disable 0649
    [SerializeField] private Transform laserStartingPoint;
    [SerializeField] private float laserWidth = .25f;
    [SerializeField] private float maxLaserDistance = 500f;
    #pragma warning restore 0649

    private bool isActive;

    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        isActive = true;
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
    }

    private void Start()
    {
        laserLine.startWidth = laserLine.endWidth = laserWidth;
        laserLine.useWorldSpace = true;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            laserLine.positionCount = 2;
            laserLine.SetPosition(0, laserStartingPoint.position);
            CastLaserBeam();
        }
    }

    private void CastLaserBeam()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserStartingPoint.position,
                                            transform.up, maxLaserDistance);
        if (hit.transform.GetComponent<LaserReflector>())
        {
            Vector2 dir = hit.point - (Vector2)laserStartingPoint.position;
            laserLine.SetPosition(1, hit.point);
            hit.transform.GetComponent<LaserReflector>().ReflectLaser(laserLine, dir, hit.normal, hit.point);
        }
        else if (hit.transform.GetComponent<ILivingEntity>() != null)
        {
            hit.transform.GetComponent<ILivingEntity>().Die();
        }
        else if (hit.transform)
        {
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, transform.position + transform.up * maxLaserDistance);
        }
    }

    public void OnActivate()
    {
        isActive = true;
        laserLine.enabled = true;
    }

    public void OnDeactivate()
    {
        isActive = false;
        laserLine.enabled = false;
    }

    public void OnInteract(Transform interactor = null)
    {
        if (isActive)
        {
            OnDeactivate();
        }
        else
        {
            OnActivate();
        }
    }
}

using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    public void ReflectLaser(LineRenderer laserLine, Vector2 inDirection, Vector2 normal, Vector2 hitPoint)
    {
        Vector2 reflectDir = Vector2.Reflect(inDirection, normal);
        RaycastHit2D hitInfo = Physics2D.Raycast(hitPoint, reflectDir);

        if (hitInfo.transform == null)
        {
            laserLine.positionCount++;
            laserLine.SetPosition(laserLine.positionCount - 1, reflectDir * 100f);
        }
        else if (hitInfo.transform.GetComponent<LaserReflector>())
        {
            laserLine.positionCount++;
            laserLine.SetPosition(laserLine.positionCount - 1, hitInfo.point);
            Vector2 dir = (hitInfo.point - hitPoint).normalized;
            hitInfo.transform.GetComponent<LaserReflector>().ReflectLaser
                (laserLine, dir, hitInfo.normal, hitInfo.point);
        }
        else if (hitInfo.transform.GetComponent<ILivingEntity>() != null)
        {
            hitInfo.transform.GetComponent<ILivingEntity>().Die();
            laserLine.positionCount++;
            laserLine.SetPosition(laserLine.positionCount - 1, hitInfo.point);
        }
        else if (hitInfo.transform)
        {
            laserLine.positionCount++;
            laserLine.SetPosition(laserLine.positionCount - 1, hitInfo.point);
        }
    }
}
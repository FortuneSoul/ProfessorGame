using System;
using UnityEngine;

public class ToxinDrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        ILivingEntity entity = other.transform.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            entity.Die();
            Die();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

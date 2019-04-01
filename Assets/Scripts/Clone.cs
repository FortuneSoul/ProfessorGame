using UnityEngine;

public class Clone : MonoBehaviour, ILivingEntity
{
    private void OnMouseDown()
    {
        Die();
    }

    public void Die()
    {
        Player.hasActiveClone = false;
        Destroy(gameObject);
    }
}
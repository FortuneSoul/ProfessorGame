using UnityEngine;

public class CloneSpawner : MonoBehaviour
{
    [SerializeField]
    Clone clonePrefab = null;

    private void OnMouseDown()
    {
        if (!Player.hasActiveClone)
        {
            Instantiate(clonePrefab, transform.position, Quaternion.identity);
            Player.hasActiveClone = true;
        }
        else
        {
            DestroyExistingCloneAndCreateNewOne();
        }
    }

    private void DestroyExistingCloneAndCreateNewOne()
    {
        Clone existingClone = FindObjectOfType<Clone>();
        Destroy(existingClone.gameObject);
        Instantiate(clonePrefab, transform.position, Quaternion.identity);
        Player.hasActiveClone = true;
    }
}
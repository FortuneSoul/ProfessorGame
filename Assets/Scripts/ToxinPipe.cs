using UnityEngine;
using System.Collections;

public class ToxinPipe : MonoBehaviour
{
    [SerializeField] private float spawnRateInSeconds = .25f;
    [SerializeField] private ToxinDrop toxinDropPrefab = default;
    [SerializeField] private Transform dropSpawnPoint = default;

    void Start()
    {
        StartCoroutine(SpawnToxinDropsRoutine());
    }

    IEnumerator SpawnToxinDropsRoutine()
    {
        while (true)
        {
            Instantiate(toxinDropPrefab, dropSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRateInSeconds);
        }
    }
}

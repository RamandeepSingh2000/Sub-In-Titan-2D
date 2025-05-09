using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Biter biterPrefab;
    [SerializeField] Vector2 size;
    [SerializeField] int numberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        for(int i=0;i<numberOfEnemies; i++) 
        {
            float randomX = Random.Range(transform.position.x - size.x/2, transform.position.x + size.x/2);
            float randomY = Random.Range(transform.position.y - size.y/2, transform.position.y + size.y/2);

            Instantiate(biterPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}

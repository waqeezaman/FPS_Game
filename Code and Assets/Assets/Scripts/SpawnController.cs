using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> SpawnPoints = new List<Vector3>();

    [SerializeField]
    private int MaximumEnemies;

    [SerializeField]
    private List<GameObject> EnemyTypes = new List<GameObject>();

    [SerializeField]
    private int SpawnInterval;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform trans  in GetComponentsInChildren<Transform>())
        {
            SpawnPoints.Add(trans.position);
        }
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        int EnemyTurn=0;
        while (true)
        {
            
            yield return  new WaitForSeconds(SpawnInterval);
            
            if (EnemyTypes[EnemyTurn].GetComponent<EnemyController>().WillSpawn() && GameObject.FindGameObjectsWithTag("Enemy").Length<MaximumEnemies)
            {
                Instantiate(EnemyTypes[EnemyTurn],SpawnPoints[Random.Range(0,SpawnPoints.Count)],new Quaternion(0,0,0,0));
            }


            EnemyTurn += 1;
            if (EnemyTurn >= EnemyTypes.Count)
            {
                EnemyTurn = 0;
            }
        }
    }

 
}

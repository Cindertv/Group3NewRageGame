using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public Transform[] spawnPoints;
    public int spawnTime;
    public GameObject enemy;
    private Coroutine Wave;
    public int amountofwaves = 2;
    public int amountofenemys = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Wave == null)
            {
                Wave = StartCoroutine(Waves());
            }
        } 
         
    }

    private void Enemyspawner ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    private IEnumerator Waves()
    {

        for (int i = 0; i < amountofwaves; i++)
        {

            for (int j = 0; j < amountofenemys; j++)
            {

                Enemyspawner();
                yield return new WaitForSecondsRealtime (1);
            }
            yield return new WaitForSecondsRealtime(5);
        }

        Wave = null;
    }
}

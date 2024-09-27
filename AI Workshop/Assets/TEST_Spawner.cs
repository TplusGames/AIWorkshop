using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int spawnAmount;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}

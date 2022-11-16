using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsSpawner : MonoBehaviour
{
    public int boidAmount = 100;
    public Boid boid;

    private void Start()
    {
        for (int i = 0; i < boidAmount; i++)
        {
            Instantiate(boid, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}

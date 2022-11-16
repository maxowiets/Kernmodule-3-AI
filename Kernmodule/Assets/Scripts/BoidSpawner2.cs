using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner2 : MonoBehaviour
{
    public int boidAmount = 100;
    public Boid2 boid;
    List<Boid2> boids = new List<Boid2>();

    private void Start()
    {
        for (int i = 0; i < boidAmount; i++)
        {
            boids.Add(Instantiate(boid, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360))));
        }
        foreach (Boid2 boid in boids)
        {
            boid.CreateBoidsList(boids);
        }
    }
}

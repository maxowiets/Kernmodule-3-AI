using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float perceptionRange = 5f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 90f;
    public float avoidCollisionRange = 2f;

    public float seperationValue = 1;
    public float alignmentValue = 1;
    public float cohesionValue = 1;

    public float boundsRange = 5;

    public LayerMask boidMask;
    List<Boid> nearbyBoids = new List<Boid>();

    private void Update()
    {
        CheckNearbyBoids();
        if (nearbyBoids.Count > 0)
        {
            Seperation();
            Alignment();
            Cohesion();
        }
        Move();   
    }

    void CheckNearbyBoids()
    {
        nearbyBoids.Clear();
        var newBoidsArray = Physics2D.OverlapCircleAll(transform.position, perceptionRange, boidMask);
        foreach (var newBoid in newBoidsArray)
        {
            nearbyBoids.Add(newBoid.GetComponent<Boid>());
        }
    }

    void Seperation()
    {
        foreach (Boid boid in nearbyBoids)
        {
            if ((boid.transform.position - transform.position).magnitude <= avoidCollisionRange)
            {
                Rotate(boid.transform.position, -seperationValue);
            }
        }
    }

    void Alignment()
    {
        Vector3 tempForward = Vector3.zero;
        foreach (Boid boid in nearbyBoids)
        {
            tempForward += boid.transform.up + boid.transform.position;
        }
        tempForward /= nearbyBoids.Count;
        Rotate(tempForward, alignmentValue);
    }

    void Cohesion()
    {
        int testInt = 0;
        Vector3 boidsPosition = Vector3.zero;
        foreach (Boid boid in nearbyBoids)
        {
            testInt++;
            boidsPosition += boid.transform.position;
        }
        boidsPosition /= nearbyBoids.Count;
        Rotate(boidsPosition, cohesionValue);
        //Debug.Log(testInt);
        //Debug.Log(nearbyBoids.Count);
    }

    void Move()
    {
        if (transform.position.x > boundsRange || transform.position.x < -boundsRange)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > boundsRange || transform.position.y < -boundsRange)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void Rotate(Vector3 boidPosition, float value)
    {
        if (Vector2.SignedAngle(transform.up, boidPosition - transform.position) > 0)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * value * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * value * Time.deltaTime));
        }
    }

    float GetAngle(Vector2 target)
    {
        return Mathf.Atan2(target.y, target.x) * (180 / Mathf.PI);
    }
}

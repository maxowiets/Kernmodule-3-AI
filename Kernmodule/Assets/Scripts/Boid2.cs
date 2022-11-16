using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid2 : MonoBehaviour
{
    public float perceptionRange = 5f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 90f;
    public float avoidCollisionRange = 2f;

    public float seperationValue = 1;
    public float alignmentValue = 1;
    public float cohesionValue = 1;

    public float boundsRange = 5;

    List<Boid2> boids = new List<Boid2>();

    private void Update()
    {
        Move();

        int closeByBoids = 0;
        Vector3 tempAlignmentVector = Vector3.zero;
        Vector3 tempCohesionVector = Vector3.zero;
        foreach (Boid2 boid in boids)
        {
            if (boid != this)
            {
                //Seperation
                if ((boid.transform.position - transform.position).magnitude <= avoidCollisionRange)
                {
                    Rotate(boid.transform.position, -seperationValue);
                }
                //----------

                if ((boid.transform.position - transform.position).magnitude <= perceptionRange)
                {
                    closeByBoids++;

                    //Alignment
                    tempAlignmentVector += boid.transform.up + boid.transform.position;

                    //Cohesion
                    tempCohesionVector += boid.transform.position;
                }
            }
        }
        if (closeByBoids != 0)
        {
            tempAlignmentVector /= closeByBoids;
            Rotate(tempAlignmentVector, alignmentValue);

            tempCohesionVector /= closeByBoids;
            Rotate(tempCohesionVector, cohesionValue);
        }
    }

    public void CreateBoidsList(List<Boid2> boids2)
    {
        boids = boids2;
    }

    
    void Move()
    {
        if (transform.position.x > boundsRange || transform.position.x < -boundsRange)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y * 0.95f, transform.position.z);
        }
        if (transform.position.y > boundsRange || transform.position.y < -boundsRange)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y * 0.95f, transform.position.z);
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

    // Basic implementation of object dropping
    public Transform[] plantObjects;
    List<Transform> plants = new List<Transform>();

    float ticker = 5.0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (ticker > 0)
        {
            ticker -= Time.deltaTime;
        }
        
        if (ticker < 0 && ticker > -10)
        {
            SpawnPlants();
            ticker = -1000;
        }
    }

    void SpawnPlants(int plantCount = 100, float proximityRadius = 10f)
    {
        for(int i = 0; i < plantCount; i++)
        {
            Transform plant = plantObjects[Random.Range(0, plantObjects.Length)];
            Vector3 position = new Vector3(0, 0, 0);
            // Check the new position isn't too close to the existing plants
            int j = 100;
            while (j > 0)
            {
                // try to place plant
                position = new Vector3(Random.Range(-200,200), 100, Random.Range(-200, 200));
                if(!ObjectIsNear(position, proximityRadius))
                {
                    j = -1;
                }
                else
                {
                    j -= 1;
                    Debug.Log("attempts left " + j.ToString());
                }
            }

            // Ensure the height hits the Mesh right
            int layerMask = 1 << 7; 
            RaycastHit hit;
           
            if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit Y = " + hit.point.y);
            }
            Debug.DrawRay(position, Vector3.down * hit.distance, Color.yellow, 10f);

            //position += new Vector3(0, -hit.distance, 0);

            Transform p = Instantiate(plant, position, Quaternion.identity);
            plants.Add(p);
        }
    }

    bool ObjectIsNear(Vector3 position, float proximityRadius)
    {
        try
        {
            foreach (Transform p in plants)
            {
                Collider[] hitColliders = Physics.OverlapSphere(position, proximityRadius);
                int i = 0;
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].transform.tag == "Plant")
                    {
                        return true;
                    }
                    i++;
                }
            }
        }
        catch
        {
            // No plants in list
        }

        return false;
    }

}
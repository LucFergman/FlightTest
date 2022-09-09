using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnCity : MonoBehaviour
{
    public GameObject terrain;
    public GameObject[] buildings;
    public int buildingsCount;
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("City");
        var terreinSize = 512;
        for (int x = 0; x < buildingsCount; x++)
        {
            Vector3 RandomPosition = new Vector3(Random.Range(0, terreinSize), Random.Range(0, 10), Random.Range(20, terreinSize));
            Quaternion randomRotate = Quaternion.Euler(Random.Range(-15, 15), Random.Range(0, 360), Random.Range(-15, 15));

            GameObject buildingSpawn = Instantiate(buildings[Random.Range(0, buildings.Length)], new Vector2(0, 0), Quaternion.identity);

            buildingSpawn.transform.SetParent(terrain.transform);
            buildingSpawn.transform.localPosition = RandomPosition;
            buildingSpawn.transform.localRotation = randomRotate;

            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

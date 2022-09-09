using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SocialPlatforms.Impl;
using System.Reflection;

public class Game : MonoBehaviour
{
    public Transform airCraft;
    public Transform cameraAnchor;
    public Vector3 startPosition;

    public float baseSpeed = 0;
    public float increseSpeed = 0.1f;

    public float maxSpeed = 50;
    public float minSpeed = 4;

    public float rotationX = 30;
    public float rotationY = 30;
    public float rotationZ = 30;

    public Text positionInfo;
    public GameObject spawnButtons;

    void Start()
    {
        Application.targetFrameRate = 60;
        airCraft = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("spawnButtons");
        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].GetComponent<Button>().onClick.AddListener(() => { ChooseStartPoint(); });

        }  
    }
    // Update is called once per frame
    void Update()
    {
        Flight();
        transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotationX * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationY * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationZ * Time.deltaTime);
        var posX = airCraft.transform.position.x;
        var posY = airCraft.transform.position.y;
        var posZ = airCraft.transform.position.z;

        var rTX = airCraft.transform.rotation.x;
        var rTY = airCraft.transform.rotation.y;
        var rTZ = airCraft.transform.rotation.z;

        cameraAnchor.transform.position = new Vector3(posX, posY, posZ);//якорь камеры
        cameraAnchor.transform.rotation = Quaternion.Euler(0, 0, 0);
        positionInfo.text = "Speed " + baseSpeed.ToString("F0") + "\n" + "Altitude " + posY.ToString("F0");
    }
    private void OnTriggerEnter(Collider other)
    {
        baseSpeed = 0;
        increseSpeed = 0.1f;
        rotationX = 0;
        rotationY = 0;
        rotationZ = 0;
        StartCoroutine("RestorePosition");

        
    }

    public void Flight()
    {
        //Ускорение
        if (Input.GetKey(KeyCode.R))
        {

            baseSpeed += increseSpeed * Time.deltaTime;
            increseSpeed += 0.1f;
            if (baseSpeed > maxSpeed || increseSpeed > maxSpeed)
            {
                baseSpeed = maxSpeed;
                increseSpeed = maxSpeed;
            }
        }
        //Замедление
        if (Input.GetKey(KeyCode.F))
        {
            baseSpeed -= increseSpeed * Time.deltaTime;
            increseSpeed -= 0.1f;
            if (baseSpeed < minSpeed || increseSpeed < minSpeed)
            {
                baseSpeed = minSpeed;
                increseSpeed = 0.1f;
            }
        }
        
        if (baseSpeed >= 5)
        {
            //up
            if (Input.GetKey(KeyCode.S))
            {
                rotationX = -60;
            }
            //down
            else if (Input.GetKey(KeyCode.W) && airCraft.transform.position.y > 0)
            {
                rotationX = 60;

            }
            else rotationX = 0;

            //поворот и наклон
            if (Input.GetKey(KeyCode.A))//Left
            {
                rotationZ = 60;
            }
            else if (Input.GetKey(KeyCode.D))//Right
            {
                rotationZ = -60;
            }
            else rotationZ = 0;
        }
    }
    public void ChooseStartPoint()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "Button":
                SetSpawnPoint(0);
                break;
            case "Button (1)":
                SetSpawnPoint(1);
                break;
            case "Button (2)":
                SetSpawnPoint(2);
                break;
            case "Button (3)":
                SetSpawnPoint(3);
                break;
        }
        DisableButtons();
        airCraft.transform.position = startPosition;
    }
    public void DisableButtons()
    {
        spawnButtons.SetActive(false);
    }
    public void SetSpawnPoint(int id)
    {
        Vector3[] spawn = new Vector3[3];
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("Respawn");
        var pointX = spawnPoint[id].transform.position.x;
        var pointZ = spawnPoint[id].transform.position.z;
        var pointY = spawnPoint[id].transform.position.y + 40;
        var rtX = 0;
        var rtY = spawnPoint[id].transform.rotation.y;
        var rtZ = 0;
        startPosition = new Vector3(pointX, pointY, pointZ);
        airCraft.transform.rotation = Quaternion.Euler(rtX, rtY, rtZ);
        Debug.Log(rtX + " " + rtY + " " + rtZ + spawnPoint[id].transform.name);
    }
    IEnumerator RestorePosition()
    {
        var data = 0f;
        baseSpeed = 0;
        increseSpeed = 0.1f;
        rotationX = 0;
        rotationY = 0;
        rotationZ = 0;
        yield return new WaitForSeconds(2.5f);
        airCraft.transform.position = new Vector3(0, 0, 0);
        spawnButtons.SetActive(true);
    }
} 

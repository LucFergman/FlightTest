using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms.Impl;
using System.Reflection;

public class Camera : MonoBehaviour
{
	public Transform airCraft;
	public Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	public float zoom = 1f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 5; // макс. увеличение
	public float zoomMin = 3; // мин. увеличение
	private float X, Y;

	void Start()
	{
		airCraft = GameObject.Find("Cube").transform;
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
		transform.position = airCraft.position + offset;
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limit, limit);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + airCraft.position;
	}
}

﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

#if UNITY_EDITOR
#pragma warning disable 0414
#endif

public class RegionCapture : MonoBehaviour
{
	public bool UseBackgroundPlane = true;
	public Texture VideoBackgroundTexure;
	public GameObject BackgroundPlane;
	private bool InitializeComplete;

	public UnityEvent OutOfBounds;
	public UnityEvent ReturnInBounds;

	private bool PlaneIsOutOfBounds;
	private bool OutOfBounds_State;
	public bool Check_OutOfBounds;

	public bool HideFromARCamera;
	public bool FlipX, FlipY;
	public Camera ARCamera;

	Mesh RegionMesh;
	Vector3[] vertices;
	Vector2[] uvs, uvs_tmp;
	float KX, KY;


	void Start()
	{
		KX = 1.0f;
		KY = 1.0f;
		StartCoroutine(Start_Initialize());
	}


	private IEnumerator Start_Initialize()
	{
		yield return new WaitForEndOfFrame();
		Initialize();
	}


	private void Initialize()
	{
		if (GetComponent<MeshFilter> ()) 
		{
			RegionMesh = GetComponent<MeshFilter> ().mesh;
			vertices = RegionMesh.vertices;
			uvs = new Vector2[vertices.Length];
			uvs_tmp = new Vector2[vertices.Length];
		}
		else Debug.Log ("MeshFilter could not be found");


		if (!ARCamera && GameObject.Find ("ARCamera")) ARCamera = GameObject.Find ("ARCamera").GetComponent<Camera>();
		if (!ARCamera && GameObject.FindWithTag("MainCamera")) ARCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		if (!ARCamera) Debug.Log ("ARCamera could not be found");
		else if (HideFromARCamera) ARCamera.cullingMask &= ~(1 << gameObject.layer);

		if (UseBackgroundPlane) 
		{
			if (!BackgroundPlane) BackgroundPlane = GameObject.Find ("BackgroundPlane");

			if (BackgroundPlane && BackgroundPlane.GetComponent<MeshFilter> () && BackgroundPlane.GetComponent<MeshRenderer> ()) 
			{
				VideoBackgroundTexure = BackgroundPlane.GetComponent<MeshRenderer> ().material.mainTexture;
			} else
				Debug.Log ("Search BackgroundPlane");
		}

		if (!ARCamera || !RegionMesh || (UseBackgroundPlane && !BackgroundPlane) || !VideoBackgroundTexure || VideoBackgroundTexure.width == 0) 
		{
			StartCoroutine (Start_Initialize ());
			Debug.Log ("Restart Initialize");
		} 
		else 
		{
			GetComponent<MeshRenderer>().material.mainTexture = VideoBackgroundTexure;
			InitializeComplete = true;

			if (UseBackgroundPlane && BackgroundPlane) 
			{
				StartCoroutine (Check_StateLoop ());
			}
			StartCoroutine(MeshUpdate ());
		}
	}


	private IEnumerator Check_StateLoop()			// 10 frames per second
	{
		if (InitializeComplete) 
		{
			if (UseBackgroundPlane && BackgroundPlane) 
			{
				FindBackgroundPlaneBounds (BackgroundPlane);
			}

			if (Check_OutOfBounds)
			{
				On_OutOfBounds ();
			}
				
			yield return new WaitForSeconds(0.1f);
			StartCoroutine(Check_StateLoop ());
		}
	}


	private IEnumerator MeshUpdate()				// 100 frames per second
	{
		if (InitializeComplete) 
		{
			bool CheckComplete = false;
			
			for (int i = 0; i < uvs.Length; i++) 
			{
				uvs [i] = ARCamera.WorldToViewportPoint (transform.TransformPoint (vertices [i]));

				uvs [i].x = (uvs [i].x - 0.5f) * KX + 0.5f;
				uvs [i].y = (uvs [i].y - 0.5f) * KY + 0.5f;

				if (FlipX)
					uvs [i].x = 1.0f - uvs [i].x;
				if (FlipY)
					uvs [i].y = 1.0f - uvs [i].y;

				#if !UNITY_EDITOR && !UNITY_STANDALONE

				if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
				{
					uvs_tmp [i].x = uvs [i].y;
					uvs_tmp [i].y = uvs [i].x;

					uvs [i].x = uvs_tmp [i].x;
					uvs [i].y = uvs_tmp [i].y;
				}

				#endif

				if (Check_OutOfBounds && !CheckComplete) 
				{
					if (uvs [i].x > 1.0f || uvs [i].y > 1.0f || uvs [i].x < 0.0f || uvs [i].y < 0.0f)
					{
						PlaneIsOutOfBounds = true;
						CheckComplete = true;
					} 

					else 
					{
						PlaneIsOutOfBounds = false;
					}
				}
			}
			RegionMesh.uv = uvs;

			yield return new WaitForSeconds(0.01f);
			StartCoroutine(MeshUpdate ());
		}
	}


	private void On_OutOfBounds ()
	{
		if (OutOfBounds_State != PlaneIsOutOfBounds) 
		{
			if (PlaneIsOutOfBounds)
			{
				if (this.enabled == false) return;
				if (OutOfBounds != null)
				{
					OutOfBounds.Invoke();
				}
			}

			else
			{
				if (this.enabled == false) return;
				if (ReturnInBounds != null)
				{
					ReturnInBounds.Invoke();
				}
			}
		}

		OutOfBounds_State = PlaneIsOutOfBounds;
	}


	private void FindBackgroundPlaneBounds(GameObject plane)
	{
		Vector3[] vertices_tmp = plane.GetComponent<MeshFilter>().mesh.vertices;
		Vector2[] uvs_tmp = new Vector2[vertices_tmp.Length];

		float max_x_tmp = 0;
		float max_y_tmp = 0;
		float min_x_tmp = 0;
		float min_y_tmp = 0;

		for (int i = 0; i < uvs_tmp.Length; i++)
		{
			uvs_tmp [i] = ARCamera.WorldToViewportPoint (plane.transform.TransformPoint (vertices_tmp [i]));

			if (uvs_tmp [i].x > max_x_tmp) max_x_tmp = uvs_tmp [i].x;
			if (uvs_tmp [i].y > max_y_tmp) max_y_tmp = uvs_tmp [i].y;
			if (uvs_tmp [i].x < min_x_tmp) min_x_tmp = uvs_tmp [i].x;
			if (uvs_tmp [i].y < min_y_tmp) min_y_tmp = uvs_tmp [i].y;
		}

		KX = (1.0f / (((max_x_tmp - 1.0f) * 2.0f) + 1.0f));
		KY = (1.0f / (((max_y_tmp - 1.0f) * 2.0f) + 1.0f));
	}
}
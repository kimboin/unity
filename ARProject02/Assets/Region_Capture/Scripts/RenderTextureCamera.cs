﻿using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
#pragma warning disable 0219
#endif

public class RenderTextureCamera : MonoBehaviour
{
	[Space(20)]
	public int TextureResolution = 512;
	[Space(10)]
    private string screensPath;
    private int TextureResolutionX;
	private int TextureResolutionY;
	[Space(20)]
	private Camera Render_Texture_Camera;
	private RenderTexture CameraOutputTexture;

    public RenderTexture GetRenderTexture()
    {
        return CameraOutputTexture;
    }

	void Start() 
	{
		Render_Texture_Camera = GetComponent<Camera>();
		StartCoroutine(StartRenderingToTexture());
	}

	IEnumerator StartRenderingToTexture() 
	{
		yield return new WaitForSeconds(0.5f);

		if (transform.parent && transform.parent.localScale.x >= transform.parent.localScale.z)
		{
			TextureResolutionX = TextureResolution;
			TextureResolutionY = (int)(TextureResolution * transform.parent.localScale.z / transform.parent.localScale.x);
		}

		if (transform.parent && transform.parent.localScale.x < transform.parent.localScale.z)
		{
			TextureResolutionX =  (int)(TextureResolution * transform.parent.localScale.x / transform.parent.localScale.z);
			TextureResolutionY = TextureResolution;
		}

		CameraOutputTexture = new RenderTexture(TextureResolutionX, TextureResolutionY, 0);
		CameraOutputTexture.Create();
		Render_Texture_Camera.targetTexture = CameraOutputTexture;

		gameObject.layer = transform.parent.gameObject.layer;
		Render_Texture_Camera.cullingMask = 1 << gameObject.layer;
	}


	public void RecalculateTextureSize() 
	{
		StartCoroutine(RecalculateRenderTexture());
	}


	private IEnumerator RecalculateRenderTexture() 
	{
		yield return new WaitForEndOfFrame();

		Render_Texture_Camera.targetTexture = null;
		CameraOutputTexture.Release();
		CameraOutputTexture = null;

		StartCoroutine(StartRenderingToTexture());
	}
	

    public void MakeScreen() 
	{
        if (screensPath == null) 
		{
		#if UNITY_ANDROID && !UNITY_EDITOR
			screensPath = "/sdcard/DCIM/RegionCapture";

		#elif UNITY_IPHONE && !UNITY_EDITOR
			screensPath = Application.persistentDataPath;

		#else
            screensPath = Application.dataPath + "/Screens";

		#endif
            System.IO.Directory.CreateDirectory(screensPath);
        }
        StartCoroutine(TakeScreen());
    }


    private IEnumerator TakeScreen() 
	{
        yield return new WaitForEndOfFrame();

        Texture2D FrameTexture = new Texture2D(CameraOutputTexture.width, CameraOutputTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = CameraOutputTexture;
        FrameTexture.ReadPixels(new Rect(0, 0, CameraOutputTexture.width, CameraOutputTexture.height), 0, 0);
        RenderTexture.active = null;

        FrameTexture.Apply();
        saveImgToGallery(FrameTexture.EncodeToPNG());
    }


    private void saveImgToGallery(byte[] img)
    {
    	string fileName = saveImg(img);
    }


    private string saveImg(byte[] imgPng)
    {
        string fileName = screensPath + "/screen_" + System.DateTime.Now.ToString("dd_MM_HH_mm_ss") + ".png";

        Debug.Log("write to " + fileName);

        System.IO.File.WriteAllBytes(fileName, imgPng);
        return fileName;
    }
}
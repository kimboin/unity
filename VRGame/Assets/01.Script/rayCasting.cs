using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rayCasting : MonoBehaviour
{
    public Image redImage;
    float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        raycast();
    }
    void raycast()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward * 1000);
        if (Physics.Raycast(transform.position, forward, out hit))
        {
            timeElapsed = Time.deltaTime + timeElapsed;
            redImage.fillAmount = timeElapsed / 3;

            Debug.Log(timeElapsed);

            if (timeElapsed >= 3)
            {
                if (hit.transform.tag == "ship01")
                {
                    PlayerPrefs.SetInt("ship", 1);
                }
                else if (hit.transform.tag == "ship02")
                {
                    PlayerPrefs.SetInt("ship", 2);
                }
                else if (hit.transform.tag == "ship03")
                {
                    PlayerPrefs.SetInt("ship", 3);
                }
                else if (hit.transform.tag == "ship04")
                {
                    PlayerPrefs.SetInt("ship", 4);
                }
                else if (hit.transform.tag == "ship05")
                {
                    PlayerPrefs.SetInt("ship", 5);
                }
                else {
                    Debug.Log("hit!!!");
                    hit.transform.GetComponent<Button>().onClick.Invoke();
                }
                
                timeElapsed = 3;
            }
            // 1초동안 채워지길 기다림.
            // redImage.fillAmount = Time.deltaTime + redImage.fillAmount;

            // 3초동안 채워지길 기다림.
            // redImage.fillAmount = Time.deltaTime/3 + redImage.fillAmount;

            // Debug.Log(redImage.fillAmount);
            

        }
        else {
            timeElapsed = timeElapsed - Time.deltaTime;
            redImage.fillAmount = timeElapsed / 3;
            if (timeElapsed < 0)
            {
               
                timeElapsed = 0;
            }
        }
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}

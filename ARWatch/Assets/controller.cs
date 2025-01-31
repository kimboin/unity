using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class controller : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(CrossPlatformInputManager.GetAxis("Horizontal")*10f,
            CrossPlatformInputManager.GetAxis("Vertical") *10f, 0);
        //transform.Translate(CrossPlatformInputManager.GetAxis("Horizontal"),
        //0, CrossPlatformInputManager.GetAxis("Vertical"));

    }
}

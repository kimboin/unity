using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceShipMove : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject cam;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }
    void Movement()
    {
        head.transform.Translate(cam.transform.forward * 3f * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCasting : MonoBehaviour
{
    public GameObject head;
    public GameObject pos01;
    public GameObject pos02;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayCast();
    }

    void rayCast() {
        RaycastHit hit;

        //원점에서 발사되는 방향 
        Vector3 dir = transform.TransformDirection(Vector3.forward * 1000);
        
        if (Physics.Raycast(transform.position, dir, out hit)) {
            if (hit.transform.tag == "ref") {
                Debug.Log("ref HIT!!!");
                movement("ref");
;            }
            if (hit.transform.tag == "tv")
            {
                Debug.Log("tv HIT!!!");
                movement("tv");
                ;
            }
        }

        Debug.DrawRay(transform.position, dir, Color.red);

    }

    void movement(string what) {
        Debug.Log(what);
        if (what == "ref")
        {
            head.transform.position =
                Vector3.MoveTowards(
                    head.transform.position,
                    pos01.transform.position,
                    Time.deltaTime
                );
        }
        if (what == "tv")
        {
            head.transform.position =
                Vector3.MoveTowards(
                    head.transform.position,
                    pos02.transform.position,
                    Time.deltaTime
                );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (anim.GetBool("isopen"))
            {
                anim.SetBool("isopen", false);

            }
            else {
                anim.SetBool("isopen", true);

            }
        }
        
    }
}
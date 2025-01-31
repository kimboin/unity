using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script02 : MonoBehaviour
{
    Animator anim;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        if (!Input.anyKey)
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isWalkLeft", false);
            anim.SetBool("isWalkBack", false);
            anim.SetBool("isWalkRight", false);
        }
        else if (Input.GetKey(KeyCode.W))
        { //Input.GetKey(KeyCode.UpArrow)
            anim.SetBool("isWalk", true);
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime );
            Debug.Log("W");
            anim.SetBool("isWalkLeft", false);
            anim.SetBool("isWalkBack", false);
            anim.SetBool("isWalkRight", false);
        } else if (Input.GetKey(KeyCode.A))
        { //Input.GetKey(KeyCode.LeftArrow)
            anim.SetBool("isWalkLeft", true);
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime );
            Debug.Log("A");
            anim.SetBool("isWalk", false);
            anim.SetBool("isWalkBack", false);
            anim.SetBool("isWalkRight", false);
        } else if (Input.GetKey(KeyCode.S)) //Input.GetKey(KeyCode.DownArrow)
        {
            anim.SetBool("isWalkBack", true);
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime );
            Debug.Log("S");
            anim.SetBool("isWalk", false);
            anim.SetBool("isWalkLeft", false);
            anim.SetBool("isWalkRight", false);

        }
        else if (Input.GetKey(KeyCode.D)) //Input.GetKey(KeyCode.RightArrow)
        {
            anim.SetBool("isWalkRight", true);
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime);
            Debug.Log("D");
            anim.SetBool("isWalk", false);
            anim.SetBool("isWalkLeft", false);
            anim.SetBool("isWalkBack", false);
        } 

    }
    void shoot() {
        if (Input.GetMouseButtonDown(0)) {
            bullet.SetActive(true);
        }
    }
}

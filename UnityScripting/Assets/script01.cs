using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script01 : MonoBehaviour
{
    string name01;
    public int number01;
    public float number02;

    public float answer;

    // Start is called before the first frame update
    void Start()
    {
        name01 = "boinKim";
        name01 = "KimBoin";
        Debug.Log(name01);
        Debug.Log("start!!!");
    }

    // Update is called once per frame
    void Update()
    {
        answer = number01 + number02;
        Debug.Log(answer);
    }
}

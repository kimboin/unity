using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script03 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(sum(0, 10, 20)); // 함수 호출 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int sum(int number, int num01, int num02) // 함수 선언
    {
        number = num01 + num02;
        return number;
    }
}

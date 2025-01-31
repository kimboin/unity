using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchManager02 : MonoBehaviour
{
    public GameObject[] watches;

    public void convertWatches(int num) {
        for (int i = 0; i < watches.Length; i++){
            if (i == num)
            {
                watches[i].SetActive(true);
            }
            else {
                watches[i].SetActive(false);

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

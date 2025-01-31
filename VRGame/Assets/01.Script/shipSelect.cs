using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipSelect : MonoBehaviour
{
    public GameObject[] ship;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ship") == 1) {
            ship[0].SetActive(true);
        }
         else if (PlayerPrefs.GetInt("ship") == 2)
        {
            ship[1].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ship") == 3)
        {
            ship[2].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ship") == 4)
        {
            ship[3].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ship") == 5)
        {
            ship[4].SetActive(true);
        }
        //if (PlayerPrefs.GetInt("ship") < 6)
        //{
        //    ship[PlayerPrefs.GetInt("ship") - 1].SetActive(true);
        //}
       

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

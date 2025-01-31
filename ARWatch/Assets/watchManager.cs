using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchManager : MonoBehaviour
{
    public GameObject[] watchList;

    public void convertWatch()
    {
        int activeIndex = 0;

        for (int i = 0; i < watchList.Length; i++) {
            GameObject obj = watchList[i];
 
            if (obj.activeSelf)
            {
                activeIndex = i;
                obj.SetActive(false);
            }
        }
        int nextIndex = (activeIndex + 1) % 4;
        watchList[nextIndex].SetActive(true);
    }

    //public void convertWatch01() {
    //    watch01.SetActive(true);
    //    watch02.SetActive(false);
    //}

    //public void convertWatch02()
    //{
    //    watch01.SetActive(false);
    //    watch02.SetActive(true);
    //}
}

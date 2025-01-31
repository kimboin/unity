using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public void playGameButton()
    {
        SceneManager.LoadScene("Game");
    }
}

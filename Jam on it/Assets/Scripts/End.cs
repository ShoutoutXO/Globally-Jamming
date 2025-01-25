using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
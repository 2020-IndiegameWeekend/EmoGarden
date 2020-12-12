using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OutGameMainUI : MonoBehaviour
{
    public void LoadInGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OutGameMainUI : MonoBehaviour
{
    public void LoadInGame()
    {
        SoundManager.instance.PlayEffectSound("Touch_Effect");
        SceneManager.LoadScene("InGame");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleSceneSelect : MonoBehaviour
{
    public void LoadBubbleDemo01()
    {
        SceneManager.LoadScene("bubble_demo01");
    }
    public void LoadBubbleDemo02()
    {
        SceneManager.LoadScene("bubble_demo02");
    }
    public void LoadBubbleSoapDemo01()
    {
        SceneManager.LoadScene("bubblesoap_demo01");
    }
	public void LoadBubbleMiscDemo()
    {
        SceneManager.LoadScene("bubblemiscdemo");
    }
	public void LoadBubbleExplosionDemo()
    {
        SceneManager.LoadScene("bubbleexplosiondemo");
    }
}
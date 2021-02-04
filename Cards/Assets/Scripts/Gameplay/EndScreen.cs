using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Animator BlackScreen;
    public Animator Window;
    public Text Description;
    public Image Icon;

    public void EndingStart()
    {
        BlackScreen.SetTrigger("Fade");
        Window.SetTrigger("Scroll");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToMenu()
    {

    }
}

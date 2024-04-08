using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public Animator introAnimator;

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}

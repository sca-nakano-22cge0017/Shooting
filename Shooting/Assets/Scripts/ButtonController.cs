using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڃ{�^���̐���
/// </summary>
public class ButtonController : MonoBehaviour
{
    public void OnClickStart() {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickExit() {
        Application.Quit();
    }

    public void OnClickTitle() {
        SceneManager.LoadScene("TitleScene");
    }
}

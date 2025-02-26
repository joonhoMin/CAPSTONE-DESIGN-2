using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManagerGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickQuizAccept()
    {
        LoadingSceneManager.LoadScene("QuizScene", GameMapManager.getMapIndex());
    }

    public void OnClickClose()
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        currentBtn.transform.root.gameObject.SetActive(false);
    }

    public void OnClickSetting()
    {
        monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.SettingCanvas);
    }

    public void OnClickControl()
    {
        monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.ControlCanvas);
    }

    public void OnClickQuit()
    {
        monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.QuitMessageCanvas);
    }

    public void OnClickQuitAccept()
    {
        SceneManager.LoadScene("MainScene");
    }
}

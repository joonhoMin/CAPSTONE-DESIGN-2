using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagerMain: MonoBehaviour
{
    public InputField inputField;

    public void OnClickPlay()
    {
        monitorActiveManagerMain.instance.SetCanvasActive(mCanvasName.SubjectInputCanvas);
    }

    public void OnClickSubmit()
    {
        LoadingSceneManager.LoadScene("GameScene", GameMapManager.getMapIndex());
        string topic = inputField.text;
        PlayerPrefs.SetString("Topic", topic);
    }

    public void OnClickRecord()
    {
        monitorActiveManagerMain.instance.SetCanvasActive(mCanvasName.RecordCanvas);
    }

    public void OnClickShop()
    {
        monitorActiveManagerMain.instance.SetCanvasActive(mCanvasName.ShopCanvas);
    }
    public void OnClickSetting()
    {
        monitorActiveManagerMain.instance.SetCanvasActive(mCanvasName.SettingCanvas);
    }

    public void OnClickClose()
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        currentBtn.transform.root.gameObject.SetActive(false);
    }
    public void OnClickBack()
    {
        monitorActiveManagerMain.instance.SetCanvasActive(mCanvasName.MainCanvas);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

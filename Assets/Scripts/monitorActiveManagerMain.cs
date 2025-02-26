using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class monitorActiveManagerMain : MonoBehaviour
{
    public static monitorActiveManagerMain instance;
    public GameObject mainCanvas;
    public GameObject recordCanvas;
    public GameObject settingCanvas;
    public GameObject shopCanvas;
    public GameObject subjectInputCanvas;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.SetActive(true);
        recordCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        subjectInputCanvas.SetActive(false);
    }

    public void SetCanvasActive(mCanvasName canvasName)
    {
        // MainCanvas
        if (canvasName == mCanvasName.MainCanvas)
        {
            mainCanvas.SetActive(true);
            recordCanvas.SetActive(false);
            settingCanvas.SetActive(false);
            shopCanvas.SetActive(false);
            subjectInputCanvas.SetActive(false);
        }
        // RecordCanvas
        else if (canvasName == mCanvasName.RecordCanvas)
        {
            mainCanvas.SetActive(true);
            recordCanvas.SetActive(true);
            settingCanvas.SetActive(false);
            shopCanvas.SetActive(false);
            subjectInputCanvas.SetActive(false);
        }
        // SettingCanvas
        else if (canvasName == mCanvasName.SettingCanvas)
        {
            mainCanvas.SetActive(true);
            recordCanvas.SetActive(false);
            settingCanvas.SetActive(true);
            shopCanvas.SetActive(false);
            subjectInputCanvas.SetActive(false);
        }
        // ShopCanvas
        else if (canvasName == mCanvasName.ShopCanvas)
        {
            mainCanvas.SetActive(false);
            recordCanvas.SetActive(false);
            settingCanvas.SetActive(false);
            shopCanvas.SetActive(true);
            subjectInputCanvas.SetActive(false);
        }
        // SubjectInputCanvas
        else if (canvasName == mCanvasName.SubjectInputCanvas)
        {
            mainCanvas.SetActive(true);
            recordCanvas.SetActive(false);
            settingCanvas.SetActive(false);
            shopCanvas.SetActive(false);
            subjectInputCanvas.SetActive(true);
        }
    }
}

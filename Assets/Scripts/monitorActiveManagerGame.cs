using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class monitorActiveManagerGame : MonoBehaviour
{
    public static monitorActiveManagerGame instance;
    public GameObject statusCanvas;
    public GameObject pauseCanvas;
    public GameObject settingCanvas;
    public GameObject controlCanvas;
    public GameObject inventoryCanvas;
    public GameObject inquiryCanvas;
    public GameObject informationCanvas;
    public GameObject quitMessageCanvas;
    public GameObject quizMessageCanvas;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        statusCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        controlCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
        inquiryCanvas.SetActive(false);
        informationCanvas.SetActive(false);
        quitMessageCanvas.SetActive(false);
        quizMessageCanvas.SetActive(false);
    }

    public void SetCanvasActive(mCanvasName canvasName)
    {
        if (canvasName == mCanvasName.PauseCanvas)
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }
        // SettingCanvas
        else if (canvasName == mCanvasName.SettingCanvas)
        {
            settingCanvas.SetActive(!settingCanvas.activeSelf);
        }
        else if (canvasName == mCanvasName.QuitMessageCanvas)
        {
            quitMessageCanvas.SetActive(true);
        }
        // InventoryCanvas
        else if (canvasName == mCanvasName.InventoryCanvas)
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
        // InquiryCanvas
        else if (canvasName == mCanvasName.InquiryCanvas)
        {
            inquiryCanvas.SetActive(!inquiryCanvas.activeSelf);
        }
        else if (canvasName == mCanvasName.ControlCanvas)
        {
            controlCanvas.SetActive(true);
        }
        else if (canvasName == mCanvasName.QuitMessageCanvas)
        {
            quitMessageCanvas.SetActive(true);
        }
        else if (canvasName == mCanvasName.QuizMessageCanvas)
        {
            quizMessageCanvas.SetActive(true);
        }

    }
}

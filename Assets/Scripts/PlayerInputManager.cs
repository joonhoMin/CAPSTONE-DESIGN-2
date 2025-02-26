using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public bool GameIsPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.PauseCanvas);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.InventoryCanvas);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            monitorActiveManagerGame.instance.SetCanvasActive(mCanvasName.InquiryCanvas);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}

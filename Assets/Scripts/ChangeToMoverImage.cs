using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeToMoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public Sprite mOverImage;
    public Sprite mOutImage;
    private Image buttonSprite;
    GameObject closeBtn;

    private void MakeUnclicked()
    {
        buttonSprite.sprite = mOutImage;
    }
    void Start()
    {
        buttonSprite = GetComponent<Image>();
        buttonSprite.sprite = mOutImage;
        closeBtn = this.gameObject;
        if (closeBtn.GetComponent<Button>() != null) 
        {
            closeBtn.GetComponent<Button>().onClick.AddListener(MakeUnclicked);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonSprite.sprite = mOverImage;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonSprite.sprite = mOutImage;
    }
}

using UnityEngine;
using System.Collections;

public class MiscUIController : MonoBehaviour {

    public GameObject boardControllerGameObject;
    private BoardController boardController;
    private RectTransform panelRectTransform;

    private void Awake()
    {
        if (!boardControllerGameObject)
        {
            Debug.LogError("No board controller GameObject found!");
        }
        else {
            boardController = boardControllerGameObject.GetComponent<BoardController>();
        }
        panelRectTransform =
            transform.FindChild("Canvas").FindChild("Panel_Misc").GetComponent<RectTransform>();
    }

    public void FinishButton_OnClick() {
        boardController.commitCurrentItem();
    }

    public void CancelButton_OnClick()
    {
        boardController.deleteUncommittedItem();
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemBuilderController : MonoBehaviour {

	public GameObject boardControllerGameObject;
	private BoardController boardController;
	private RectTransform panelRectTransform;
	private bool visible = true;


	private void Awake()
	{
		if (!boardControllerGameObject) {
			Debug.LogError ("No board controller GameObject found!");
		} else {
			boardController = boardControllerGameObject.GetComponent<BoardController>();
		}
		panelRectTransform = 
			transform.FindChild("Canvas").FindChild("Panel_Builder").GetComponent<RectTransform>();
	}

	public void toggleBuilder_OnClick()
	{
		visible = !visible;
		if (visible) {
			GameStates.Drawing = false;
			//mapController.enableMapControls();
			panelRectTransform.localPosition += new Vector3(0,106,0);
		} 
		else {
			GameStates.Drawing = true;
			//mapController.disableMapControls();
			panelRectTransform.localPosition -= new Vector3(0,106,0);
		}

	}

	public void item_OnClick(string item)
	{
        boardController.deleteUncommittedItem();
		switch (item) {
		case "grassPatch4x4":
			GameStates.CurrentItem = Enums.CraftableItems.GrassPatch4x4;
			boardController.drawItem(Vector3.zero, Enums.CraftableItems.GrassPatch4x4);
			break;
		case "basicWall":
			GameStates.CurrentItem = Enums.CraftableItems.BasicWall;
			boardController.drawItem(Vector3.zero, Enums.CraftableItems.BasicWall);
			break;
		default:
			break;
		}
        toggleBuilder_OnClick();

    }
	


}

using UnityEngine;
using System.Collections;

public class ToolsController : MonoBehaviour {

	//private MapController mapController;
	private Transform panelBuilder;
	private Transform panelMove;
	private Transform panelSelect;
	
	private void Awake(){
		//mapController = transform.GetComponent<MapController> ();
		panelBuilder = transform.FindChild("Canvas").FindChild("Panel_Builder");
	}

	public void selectTool_OnClick()
	{
		GameStates.CurrentTool = Enums.Tool.Select;
		panelBuilder.gameObject.SetActive (false);
	}

	public void moveTool_OnClick()
	{
		GameStates.CurrentTool = Enums.Tool.Move;
		panelBuilder.gameObject.SetActive (false);
	}

	public void buildTool_OnClick()
	{
		GameStates.CurrentTool = Enums.Tool.Build;
		panelBuilder.gameObject.SetActive (true);
	}
	
}

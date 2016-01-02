using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardController : MonoBehaviour {

	public struct IntVector2
	{
		public int x,y;

		public IntVector2(int X, int Y)
		{
			x = X;
			y = Y;
		}

        public override string ToString() {
            return "x: " + x + " y: " + y;
        }

    }

	public int boardSize;

	private int MinBoardSize = 8;
	private int MaxBoardSize = 56;
    private int[,] boardSpotMembers;
    private int boardConflicts = 0;

    private MessageController msgController;

    private Material boardMaterial;
	private GameObject boardPositionIdentifierInstance;
	private GameObject currentUncommitedItem;

	public GameObject boardPositionIdentifier;
	public GameObject basicWall;
	public GameObject grassPatch4x4;

	private void Awake(){

		if (!boardPositionIdentifier) {
			Debug.LogError("No boardPositionIdentifier found!");
		}
		if (!basicWall) {
			Debug.LogError("No basicWall found!");
		}
		if (!grassPatch4x4) {
			Debug.LogError("No grassPatch4x4 found!");
		}
		if (boardSize < MinBoardSize) {
			Debug.LogError ("Board size specified is smaller than minimum board size!");
			boardSize = MinBoardSize;
		}
		if (boardSize > MaxBoardSize) {
			Debug.LogError ("Board size specified is greater that maximum board size!");
			boardSize = MaxBoardSize;
		}
		if(boardSize % 2 != 0)
		{
			Debug.LogError ("Board size specified is non power of 2!");
			boardSize = MinBoardSize;
		}

        msgController = GameObject.Find("UI").GetComponent<MessageController>();

	}

	private void Start () 
	{
        
        boardMaterial = transform.GetComponent<Renderer>().material;
        var boardTiling = ((float)boardSize / MinBoardSize) * boardMaterial.mainTextureScale.x;
        boardMaterial.mainTextureScale = new Vector2(boardTiling, boardTiling);
		boardPositionIdentifierInstance = Instantiate (boardPositionIdentifier, Vector3.zero, Quaternion.identity) as GameObject;
        boardSpotMembers = new int[boardSize, boardSize];

        transform.localScale = new Vector3 (boardSize, 1, boardSize);
		transform.position = new Vector3 (0,-0.5f,0);
	}
	
	private void LateUpdate () {
		if (GameStates.CurrentTool == Enums.Tool.Build) {
			Vector2 pos;
			if(Helpers.getGridPosition(out pos))
			{
				drawSquareIndicator(new Vector3(pos.x + 0.5f,0,pos.y + 0.5f));
			}
		}

        if (Input.GetMouseButtonDown(0) && !currentUncommitedItem)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0F))
            {
                if (hit.collider.tag == "CraftableItem") {
                    hit.collider.GetComponent<ItemController>().uncommit();
                    currentUncommitedItem = hit.collider.gameObject;
                }
            }
        }

    }

	private void drawSquareIndicator(Vector3 position)
	{
		boardPositionIdentifierInstance.transform.position = position;
	}

	public void drawItem(Vector3 position, Enums.CraftableItems item)
	{
 

        //position.y += ItemLift;
        //Debug.Log(position);
        var itemGameObject = toGameObject (item);
		var itemController = itemGameObject.GetComponent<ItemController> ();
		currentUncommitedItem = Instantiate (itemGameObject, position, Quaternion.identity) as GameObject;
        incrementSlots(position, itemController.xLength, itemController.zLength);
        //printBoardSlotMembers();

    }

    public void deleteUncommittedItem() {
        if (!currentUncommitedItem)
            return;

        var curItem = currentUncommitedItem.GetComponent<ItemController>();
        decrementSlots(currentUncommitedItem.transform.position, curItem.xLength, curItem.zLength);
        Destroy(currentUncommitedItem);
        currentUncommitedItem = null;
    }

    public void commitCurrentItem() {

        if (!currentUncommitedItem)
            return;

        if (boardConflicts != 0) {
            msgController.throwMessage("Oops, you can't build there!");
            return;
        }

        var itemController = currentUncommitedItem.GetComponent<ItemController>();
        //itemController.disableTranslateControls();
        itemController.commit();
        //currentUncommitedItem.transform.Translate(new Vector3(0, -ItemLift, 0));
        currentUncommitedItem = null;
        
    }

    public void incrementSlots(Vector3 position, int xlength, int zlength)
    {
        var x = Mathf.FloorToInt(position.x);
        var z = Mathf.FloorToInt(position.z);

        var startIdx = posToBoardIdx(new Vector2(x - xlength / 2,
                                                 z - zlength / 2));
        var endIdx = posToBoardIdx(new Vector2(x + xlength / 2,
                                               z + zlength / 2));
        
        //if (xlength % 2 != 0)
        //	endIdx.x += 1;
        //if (zlength % 2 != 0)
        //	endIdx.y += 1;

        if (xlength == 1)
            endIdx.x += 1;
        if (zlength == 1)
            endIdx.y += 1;

        for (int i = startIdx.x; i < endIdx.x; i++)
        {
            for (int j = startIdx.y; j < endIdx.y; j++)
            {
                boardSpotMembers[i, j]++;
                if (boardSpotMembers[i, j] > 1){
                    var boardPos = boardIdxToPos(new IntVector2(i, j));
                    var boardConflictInidicator = GameObject.Instantiate(
                        Resources.Load("prefabs/boardIdentifier/boardSpotTakenIndicator"),
                        new Vector3(boardPos.x, 0, boardPos.y), Quaternion.identity) as GameObject;
                    boardConflictInidicator.name = "boardConflict_" + i + "_" + j;
                    boardConflicts++;
                }
            }
        }
    }

    public void incrementSlots(IntVector2 startIdx, IntVector2 endIdx)
    {
        //Debug.Log(startIdx + " " + endIdx);
        for (int i = startIdx.x; i < endIdx.x; i++)
        {
            for (int j = startIdx.y; j < endIdx.y; j++)
            {
                boardSpotMembers[i, j]++;
                if (boardSpotMembers[i, j] > 1)
                {
                    var boardPos = boardIdxToPos(new IntVector2(i, j));
                    var boardConflictInidicator = GameObject.Instantiate(
                        Resources.Load("prefabs/boardIdentifier/boardSpotTakenIndicator"),
                        new Vector3(boardPos.x, 0, boardPos.y), Quaternion.identity) as GameObject;
                    boardConflictInidicator.name = "boardConflict_" + i + "_" + j;
                    boardConflicts++;
                }
            }
        }
    }

    public void decrementSlots(Vector3 position, int xlength, int zlength)
    {
        var x = Mathf.FloorToInt(position.x);
        var z = Mathf.FloorToInt(position.z);

        var startIdx = posToBoardIdx(new Vector2(x - xlength / 2,
                                                 z - zlength / 2));
        var endIdx = posToBoardIdx(new Vector2(x + xlength / 2,
                                               z + zlength / 2));

        //if (xlength % 2 != 0)
        //	endIdx.x += 1;
        //if (zlength % 2 != 0)
        //	endIdx.y += 1;

        if (xlength == 1)
            endIdx.x += 1;
        if (zlength == 1)
            endIdx.y += 1;

        for (int i = startIdx.x; i < endIdx.x; i++)
        {
            for (int j = startIdx.y; j < endIdx.y; j++)
            {
                boardSpotMembers[i, j]--;
                if (boardSpotMembers[i, j] == 1)
                {
                    Destroy(GameObject.Find("boardConflict_" + i + "_" + j));
                    boardConflicts--;
                }
            }
        }
    }

    public void decrementSlots(IntVector2 startIdx, IntVector2 endIdx)
    {

        for (int i = startIdx.x; i < endIdx.x; i++)
        {
            for (int j = startIdx.y; j < endIdx.y; j++)
            {
                boardSpotMembers[i, j]--;
                if (boardSpotMembers[i, j] == 1)
                {
                    Destroy(GameObject.Find("boardConflict_" + i + "_" + j));
                    boardConflicts--;
                }
            }
        }
    }

    public void updateUncommittedSlots(Enums.ArrowDirection dir, Vector3 position, int xlength, int zlength) {

        var x = Mathf.FloorToInt(position.x);
        var z = Mathf.FloorToInt(position.z);

        var startIdx = posToBoardIdx(new Vector2(x - xlength / 2,
                                                 z - zlength / 2));
        var endIdx = posToBoardIdx(new Vector2(x + xlength / 2,
                                               z + zlength / 2));

        if (xlength == 1)
            endIdx.x += 1;
        if (zlength == 1)
            endIdx.y += 1;

        IntVector2 newStartIdx, oldStartIdx, newEndIdx, oldEndIdx;
        newStartIdx = oldStartIdx = startIdx;
        newEndIdx = oldEndIdx = endIdx;
        if (dir == Enums.ArrowDirection.Up) {
            newStartIdx.y = endIdx.y - 1;

            oldStartIdx.y = startIdx.y - 1;
            oldEndIdx.y = startIdx.y;
        }
        else if (dir == Enums.ArrowDirection.Down){
            newEndIdx.y = startIdx.y + 1;

            oldStartIdx.y = endIdx.y;
            oldEndIdx.y = endIdx.y + 1;
        }
        else if (dir == Enums.ArrowDirection.Right){
            newStartIdx.x = endIdx.x - 1;

            oldStartIdx.x = startIdx.x - 1;
            oldEndIdx.x = startIdx.x;
        }
        else{
            newEndIdx.x = startIdx.x + 1;

            oldStartIdx.x = endIdx.x;
            oldEndIdx.x = endIdx.x + 1;
        }
        incrementSlots(newStartIdx, newEndIdx);
        decrementSlots(oldStartIdx, oldEndIdx);
        //printBoardSlotMembers();
    }

    private IntVector2 posToBoardIdx(Vector2 position){
		var x = (int)position.x + this.boardSize / 2;
		var y = (int)position.y + this.boardSize / 2;
		return new IntVector2 (x,y);
	}

    private Vector2 boardIdxToPos(IntVector2 boardIdx)
    {
        var x = boardIdx.x - this.boardSize / 2;
        var y = boardIdx.y - this.boardSize / 2;
        return new Vector2(x, y);
    }

    private GameObject toGameObject(Enums.CraftableItems item)
	{
		switch (item) {
		case Enums.CraftableItems.BasicWall:
			return basicWall;
		case Enums.CraftableItems.GrassPatch4x4:
			return grassPatch4x4;
		default:
			return grassPatch4x4;
		}
	}

    private void printBoardSlotMembers()
    {
        string output = "";
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++){
                output += boardSpotMembers[i, j].ToString();
            }
            output += Environment.NewLine;
        }
        Debug.Log(output);
    }
}

using UnityEngine;
using System.Collections;

public class PositionArrowsController : MonoBehaviour {

	private BoardController boardController;
	private GameObject left, right, up, down, directionArrows;
	private int xLength, zLength;

	private void Awake(){

		boardController = GameObject.FindGameObjectWithTag ("Board").GetComponent<BoardController> ();

		var leftVector = new Vector3 (-1, 0, 0);
		var rightVector = new Vector3 (1, 0, 0);
		var upVector = new Vector3 (0, 0, 1);
		var downVector = new Vector3 (0, 0, -1);

		directionArrows = new GameObject();
		left = Instantiate (Resources.Load ("prefabs/boardIdentifier/UpArrow_Prefab"),
		                    leftVector,
		                    Quaternion.LookRotation(leftVector, Vector3.up)) as GameObject;
		right = Instantiate (Resources.Load ("prefabs/boardIdentifier/UpArrow_Prefab"),
		                    rightVector,
		                    Quaternion.LookRotation(rightVector, Vector3.up)) as GameObject;
		up = Instantiate (Resources.Load ("prefabs/boardIdentifier/UpArrow_Prefab"),
		                  	upVector,
		                  	Quaternion.LookRotation(upVector, Vector3.up)) as GameObject;
		down = Instantiate (Resources.Load ("prefabs/boardIdentifier/UpArrow_Prefab"),
		                    downVector,
		                    Quaternion.LookRotation(downVector, Vector3.up)) as GameObject;

		directionArrows.name = "directionArrows";
		left.name = "Left";
		right.name = "Right";
		up.name = "Up";
		down.name = "Down";

		directionArrows.transform.SetParent (this.transform);
		left.transform.SetParent (directionArrows.transform);
		right.transform.SetParent (directionArrows.transform);
		up.transform.SetParent (directionArrows.transform);
		down.transform.SetParent (directionArrows.transform);


	}
		
	private void Start(){
		fitToItem (Vector3.zero);
	}

	private void LateUpdate(){

		Enums.ArrowDirection direction;
		if (Input.GetMouseButtonDown(0) && getDirectionArrow (out direction)) {

		}
	}

	public void setSize(int xlength, int zlength){
		this.xLength = xlength;
		this.zLength = zlength;
	}

	private void move(Enums.ArrowDirection direction){
		if (direction == Enums.ArrowDirection.Left)
			this.transform.Translate (new Vector3(-1,0,0));
		else if(direction == Enums.ArrowDirection.Right)
			this.transform.Translate (new Vector3(1,0,0));
		else if(direction == Enums.ArrowDirection.Up)
			this.transform.Translate (new Vector3(0,0,1));
		else
			this.transform.Translate (new Vector3(0,0,-1));
	}

	private bool getDirectionArrow(out Enums.ArrowDirection direction) 
	{
		direction = Enums.ArrowDirection.Left;
		RaycastHit hitInfo;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
		var hit = Physics.Raycast (ray.origin, ray.direction, out hitInfo, 100f);
		if (hit && hitInfo.transform.tag == "ArrowDirection") 
		{	
			//Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(hitInfo.point, ray.origin), Color.cyan, 20f);

			var collider = hitInfo.collider.transform.parent.name;
			var topLevelParent = hitInfo.transform.parent.parent.parent;
            var oldTopLevelParentPos = topLevelParent.position;
            if (collider == "Left"){
				direction = Enums.ArrowDirection.Left;
				topLevelParent.Translate (new Vector3(-1,0,0));
			}
			else if(collider == "Right"){
				direction = Enums.ArrowDirection.Right;
				topLevelParent.Translate (new Vector3(1,0,0));
			}
			else if(collider == "Up"){
				direction = Enums.ArrowDirection.Up;
				topLevelParent.Translate (new Vector3(0,0,1));
			}
			else{
				direction = Enums.ArrowDirection.Down;
				topLevelParent.Translate (new Vector3(0,0,-1));
			}
            //boardController.updateBoardSlots(direction, oldTopLevelParentPos, topLevelParent.position, this.xLength, this.zLength);
            boardController.updateUncommittedSlots(direction, topLevelParent.position, this.xLength, this.zLength);
            return true;
		}
		return false;
	}

	private void fitToItem(Vector3 position)
	{
		var halfX = xLength / 2f;
		var halfZ = zLength / 2f;

		var offsetX = (xLength % 2 == 0) ? 0 : 0.5f;
		var offsetZ = (zLength % 2 == 0) ? 0 : 0.5f;

		left.transform.position = new Vector3(-(halfX + 0.5f), 0, 0);
		right.transform.position = new Vector3((halfX + 0.5f), 0, 0);
		down.transform.position = new Vector3(0,0, -(halfZ + 0.5f));
		up.transform.position = new Vector3(0,0, (halfZ + 0.5f));
		directionArrows.transform.position = this.transform.position;
	}
	



}

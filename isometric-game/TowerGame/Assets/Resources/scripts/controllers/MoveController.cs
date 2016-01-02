using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    public GameObject boardControllerGameObject;
    private BoardController boardController;

    private void Awake()
    {
        if (!boardControllerGameObject)
        {
            Debug.LogError("No board controller GameObject found!");
        }
        else {
            boardController = boardControllerGameObject.GetComponent<BoardController>();
        }
        
    }




}

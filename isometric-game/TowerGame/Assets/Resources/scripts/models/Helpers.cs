using UnityEngine;
using System.Collections;

public static class Helpers{

    /// <summary>
    /// Casts a ray from the main camera and returns true if the ray intersects the board,
    /// otherwise returns false.
    /// </summary>
    /// <param name="position">Stores the rounded grid position</param>
    /// <returns></returns>
    public static bool getGridPosition(out Vector2 position) 
	{
		position = Vector2.zero;
		RaycastHit hitInfo;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
		var hit = Physics.Raycast (ray.origin, ray.direction, out hitInfo, 100f);
		if (hit && hitInfo.transform.tag == "Board") 
		{	
			//Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(hitInfo.point, ray.origin), Color.cyan, 20f);
			position = new Vector2(Mathf.Floor(hitInfo.point.x),Mathf.Floor(hitInfo.point.z));
			return true;
		}
		return false;
	}

    /// <summary>
    /// Returns the direction of a Vector2.
    /// </summary>
    /// <param name="oldPosition"></param>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    public static Vector2 getDirection(Vector2 oldPosition, Vector2 newPosition)
    {
        return newPosition - oldPosition;
    }

    /// <summary>
    /// Returns the x and y screen space position. 
    /// </summary>
    /// <returns></returns>
    public static Vector2 getPositionScreenSpace()
    {

        var pos = Input.mousePosition;
        var pos2 = new Vector2(pos.x, pos.y);

        return pos2;

    }

    /// <summary>
    /// Returns the y screen space position. 
    /// </summary>
    /// <returns></returns>
    public static float getVerticalPositionScreenSpace()
    {
        return Input.mousePosition.y;
    }


}

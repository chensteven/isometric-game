using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controller for panning and zooming of the main camera.
/// </summary>
public class CameraController : MonoBehaviour
{

    private GameObject camSpaceParallelToBoard;
    private Vector2 oldPosition, currentPosition;

    private void Start()
    {
        camSpaceParallelToBoard = new GameObject();
        camSpaceParallelToBoard.transform.Rotate(0, Camera.main.transform.localRotation.eulerAngles.y, 0);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            oldPosition = Helpers.getPositionScreenSpace();
        }
        else if (Input.GetMouseButton(0))
        {
            currentPosition = Helpers.getPositionScreenSpace();
            var dir = Helpers.getDirection(oldPosition, currentPosition) * Globals.CameraPanSpeed;
            Camera.main.transform.Translate(dir.x, 0, dir.y, camSpaceParallelToBoard.transform);
            oldPosition = currentPosition;
        }
        else if (Input.GetMouseButton(1))
        {
            currentPosition.y = Helpers.getVerticalPositionScreenSpace();
            var dir = (currentPosition.y - oldPosition.y) * Globals.CameraZoomSpeed;
            cameraZoom(dir);
            oldPosition.y = currentPosition.y;
        }
    }

    private void cameraZoom(float direction)
    {
        var zoom = Camera.main.orthographicSize + direction;
        if (zoom > Globals.MaxCameraZoom || zoom < Globals.MinCameraZoom)
        {
            return;
        }

        Camera.main.orthographicSize += direction;
    }

}

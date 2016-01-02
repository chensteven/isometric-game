using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	public int xLength, zLength;
	private PositionArrowsController translateControls;
    private Animator animator;
    private float itemLift = 0.25f;

	private void Awake(){

        animator = transform.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load("animations/CraftableItemController")) as RuntimeAnimatorController;

		if (xLength * zLength < 1) {
			Debug.LogError ("Can't have non positive xLength or zLength!");
			xLength = 1;
			zLength = 1;
		}

        var collider = transform.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(xLength, 0.2f, zLength);
        collider.center = new Vector3(0, 0.1f, 0);

		fixOffset ();
		translateControls = this.gameObject.AddComponent<PositionArrowsController> ();
		translateControls.setSize (xLength, zLength);

    }

    private void Start() {
        uncommit();
    }

    public void commit() {
        animator.SetBool("committed", true);
        disableTranslateControls();
        transform.Translate(0, -itemLift, 0);
    }

    public void uncommit()
    {
        animator.SetBool("committed", false);
        enableTranslateControls();
        transform.Translate(0, itemLift, 0);
    }

    private void fixOffset()
	{
		var offsetX = (xLength % 2 == 0) ? 0 : 0.5f;
		var offsetZ = (zLength % 2 == 0) ? 0 : 0.5f;
		this.transform.position += new Vector3 (offsetX, 0, offsetZ);
	}

	public void disableTranslateControls()
	{
		transform.FindChild("directionArrows").gameObject.SetActive(false);
		translateControls.enabled = false;
	}

	public void enableTranslateControls()
	{
		transform.FindChild("directionArrows").gameObject.SetActive(true);
		translateControls.enabled = true;
	}

}

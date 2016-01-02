using UnityEngine;
using System.Collections;

public class tempScript : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = transform.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("committed", !animator.GetBool("committed"));
        }
	}
}

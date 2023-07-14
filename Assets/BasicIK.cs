using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicIK : MonoBehaviour {

	public GameObject Head;

	Vector2 Offset;
	Vector2 RelativeOffset;

	void SolveIK(){
		RelativeOffset = Head.transform.position - this.transform.position;

		if (RelativeOffset != Vector2.zero) {
			this.transform.rotation = Quaternion.LookRotation (RelativeOffset, Vector3.up);

			RelativeOffset = RelativeOffset.normalized * Offset.magnitude;
			this.transform.position = new Vector2(Head.transform.position.x, Head.transform.position.y) - RelativeOffset;
		}
	}

	// Use this for initialization
	void Start () {
		Offset = this.transform.position - Head.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		SolveIK ();
	}
}


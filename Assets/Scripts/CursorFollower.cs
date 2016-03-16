using UnityEngine;
using System.Collections;

public class CursorFollower : MonoBehaviour {

    private Transform thisTrans;

    void Awake()
    {
        thisTrans = transform;
    }



	// Update is called once per frame
	void Update () 
    {
        var newPos = Input.mousePosition;
        newPos.z = 10.0f;
        newPos = Camera.main.ScreenToWorldPoint(newPos);
        thisTrans.position = newPos;
	
	}
}

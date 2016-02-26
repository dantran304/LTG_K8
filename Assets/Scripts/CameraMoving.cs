using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        //tinh khoang cach chenh lech giua camera va player
        offset = gameObject.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        gameObject.transform.position = player.transform.position + offset;
	}
}

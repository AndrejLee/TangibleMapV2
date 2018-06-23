using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    int i = 0;
    int max = 200;
    Vector2 LongLat = new Vector2(106.6821995f, 10.7629221f);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        i++;
        if (i == max)
        {
            LongLat.y += 0.0001f;
            //GameObject.Find("Test").GetComponent<TestMap>().moveCenter(LongLat);
            i = 0;
        }
	}
}

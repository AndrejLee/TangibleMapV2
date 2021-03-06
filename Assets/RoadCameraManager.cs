﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCameraManager : MonoBehaviour
{

    public string URL;
    public static bool beginRayCasting = false;
    private GameObject socketController;
    SocketV2 socket;
    public int x = 12;
    public int y = 7;
    public float z = -20.14f;
    public Vector3 directionZ = new Vector3(0, 0, 100);
    private GameObject screenObject;
    private readonly string TAG = "road_camera";
    private static bool isRaycasting = false;
    private static bool isSent = false;
    private readonly object lock_ = new object();

    // Use this for initialization
    void Start()
    {
        socketController = GameObject.Find("SocketController");
        socket = socketController.GetComponent<SocketV2>();
        screenObject = GameObject.Find("Screen");
    }

    private void OnMouseDown()
    {
        SocketV2 socket = socketController.GetComponent<SocketV2>();
        socket.SendData(Constant.TOKEN_BEGIN_URL + URL + Constant.TOKEN_END);
    }

    // Update is called once per frame
    void Update()
    {
        if (!beginRayCasting)
        {
            return;
        }
        RaycastHit hit;

        Vector3 test = new Vector3(CalibrateObject.curPos.x + screenObject.transform.position.x, CalibrateObject.curPos.z + screenObject.transform.position.y, z);
        Debug.DrawRay(test, directionZ, Color.green);

        lock (lock_)
        {

            if (Physics.Raycast(test, Vector3.forward, out hit))
            {
                if (hit.collider.gameObject.tag == TAG && !isSent)
                {
                    isSent = true;
                    if (socket != null)
                    {
                        Debug.Log("pang pang:: " + isSent);
                        socket.SendData(Constant.TOKEN_BEGIN_URL + URL + Constant.TOKEN_END);
                    }
                }
                else if (hit.collider.gameObject.tag != TAG)
                {
                    isSent = false;
                }
            } else
            {
                isSent = false;
            }
        }
    }
}

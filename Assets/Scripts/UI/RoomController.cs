﻿using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private GameObject container = null;

    public void ShowWindow()
    {
        container.SetActive(true);
    }

    public void Exit()
    {
        container.SetActive(false);
    }
}

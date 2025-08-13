using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    public int doorID;
    private Tilemap tilemap;

    private static List<DoorController> doors = new List<DoorController>();

    private void Awake()
    {
        doors.Add(this);
    }

    private void OnDestroy()
    {
        doors.Remove(this);
    }

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public static void UnlockDoor(int id)
    {
        foreach (var door in doors)
        {
            if (door.doorID == id)
            {
                door.gameObject.SetActive(false);
            }
        }
    }
}

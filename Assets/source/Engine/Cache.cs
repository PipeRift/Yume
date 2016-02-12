using UnityEngine;
using System.Collections;
using Crab.Controllers;

public class Cache : MonoBehaviour {
    [Header("Character Prefabs")]
    public PlayerController playerPrefab;

    [Header("References")]
    public PlayerController player;
    public CameraController cameraController;


    //Singletone
    private static Cache instance;
    public static Cache Get
    {
        get {
            return instance? instance : instance = FindObjectOfType<Cache>();
        }
        private set { instance = value; }
    }
}

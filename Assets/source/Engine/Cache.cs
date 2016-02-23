using UnityEngine;
using System.Collections;
using Crab.Controllers;

public class Cache : MonoBehaviour {
    [Header("Character Prefabs")]
    public PlayerController playerPrefab;

    public PlayerController player {
        get {
            return _player? _player : _player = FindObjectOfType<PlayerController>();
        }
    }
    private PlayerController _player;

    public CameraController cameraController
    {
        get
        {
            return _cameraController ? _cameraController : _cameraController = FindObjectOfType<CameraController>();
        }
    }
    private CameraController _cameraController;

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

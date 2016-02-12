using UnityEngine;
using System.Collections;

public enum RotationState
{
    UP = 0,
    RIGHT = 90,
    DOWN = 180,
    LEFT = 270,
    NONE = 370
}

public class GameStats : MonoBehaviour
{
    // Static singleton property
    private static GameStats instance;
    public static GameStats Get {
        get { 
            return instance ? instance : instance = FindObjectOfType<GameStats>();
        } 
        private set { instance = value; }
    }
    
    public static void NextRotation()
    {
        int iState = (int)Get.rotationState + 90;
        if (iState >= 360) iState = (int)RotationState.UP;
        Get.rotationState = (RotationState)iState;
    }

    public static void PreviousRotation()
    {
        int iState = (int)Get.rotationState - 90;
        if (iState < 0) iState = (int)RotationState.LEFT;
        Get.rotationState = (RotationState)iState;
    }



    void Awake()
    {
        if (Get && Get != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public RotationState rotationState
    {
        get {
            return _rotationState;
        }
        set {
            _rotationState = value;
            UpdateRotation();
        }
    }

    [SerializeField]
    private RotationState _rotationState;

    
    void UpdateRotation()
    {
        //Cache.Get.cameraController.yRotation = (int)_rotationState;
    }

}
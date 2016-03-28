using UnityEngine;
using System.Collections.Generic;

public class cubepuzzle : MonoBehaviour {
    public EButton blueButton;
    public EButton greenButton;
    public EButton redButton;

    public Color color = Color.yellow;

    public  List<Transform> cubes = new List<Transform>();
    private List<Color>    colors = new List<Color>();

    void Start() {
        Setup();
    }

    void Setup() {
        for (int i = 0; i < cubes.Count; i++) {
            Color color = new Color(Random01(), Random01(), 0);
            Debug.Log(color);

            Renderer rend = cubes[i].GetComponent<Renderer>();
            rend.material.SetColor("_Color", color);
        }
    }

    int Random01() {
        Random rand = new Random();
        
        return (Random.Range(0.0f, 1f) >= 0.5f) ? 1 : 0;
    }
}

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    int Carrots = 0;
    public TextMeshProUGUI CarrotText;

    void Awake() {
        Instance = this;
    }

    public void IncScore(int ds) {
        Carrots += ds;
        CarrotText.text = "Carrots: " + Carrots;
    }

    void Start() {
        
    }

    void Update() {
        
    }

}

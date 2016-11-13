using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoubleText : MonoBehaviour {

    public string s;
    public Text t1, t2;

    void Start() {
        t1.text = s;
        t2.text = s;
    }

    void Update() {
    }

    public void SetText(string s) {
        this.s = s;
        Start();
    }
}

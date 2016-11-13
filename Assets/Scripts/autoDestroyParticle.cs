using UnityEngine;
using System.Collections;

public class autoDestroyParticle : MonoBehaviour {

    public ParticleSystem system;


    void Start() {
        Destroy(this.gameObject, system.duration);
    }
}

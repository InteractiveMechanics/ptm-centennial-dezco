using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class DestroyOnCollision : MonoBehaviour {

    void OnIsoTriggerEnter(IsoCollider other)
    {
        Debug.Log("object collided");
        Destroy(other.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCubes : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
    }

}

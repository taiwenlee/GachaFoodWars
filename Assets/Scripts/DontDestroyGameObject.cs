using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameObject : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}

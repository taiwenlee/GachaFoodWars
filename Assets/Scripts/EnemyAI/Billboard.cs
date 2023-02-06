using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// obtained from:
// https://gamedevbeginner.com/billboards-in-unity-and-how-to-make-your-own/#:~:text=A%20billboard%20in%20Unity%20is,the%20sprite%20with%20any%20perspective.
public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // point the sprite at the camera
        transform.LookAt(mainCamera.transform);
    }
}

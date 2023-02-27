using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDuplicateGameObject : MonoBehaviour
{
   void Awake(){
     int numInvManagers = FindObjectsOfType<Inventory>().Length;
     if (numInvManagers != 1)
     {
         Destroy(this.gameObject);
     }
     // if more then one object is in the scene destroy itself
     else
     {
         DontDestroyOnLoad(gameObject);
     }
 }
}

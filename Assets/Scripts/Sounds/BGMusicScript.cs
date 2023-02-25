using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicScript : MonoBehaviour
{
//  public AudioClip otherClip;
//  public AudioSource audioSource;
 private static BGMusicScript _instance;

     void Awake()
     {
         //if we don't have an [_instance] set yet
         if(!_instance)
             _instance = this;
         //otherwise, if we do, kill t$$anonymous$$s t$$anonymous$$ng
         else
             Destroy(this.gameObject);
 
 
         DontDestroyOnLoad(this.gameObject);
     }
}
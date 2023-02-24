using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public GameObject GameObj;

    [Header("Animation")]
    public new Animator animation;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //flips sprite if moving either left or right
        // if(!spriteRenderer.flipX && GameObj.GetComponent<Movement>().move.x > 0) {
        //      spriteRenderer.flipX = true;
        // }else if(spriteRenderer.flipX && GameObj.GetComponent<Movement>().move.x < 0){
        //     spriteRenderer.flipX = false;
        // }
        //animation.SetFloat("WalkSpeed", GameObj.GetComponent<Movement>().movement.magnitude );
        
        //animation.SetFloat("WalkSpeed",  1);
    }
}

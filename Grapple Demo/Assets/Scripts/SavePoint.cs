using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameManager game;
    public Animator anim;
    private bool saving = false;
    public bool saveAnimator;

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            if (Input.GetKeyDown(KeyCode.E) && saving == false)
            {
                saving = true;
            }
        }
    }

    private IEnumerator SaveProcess()
    {
        anim.SetBool("saveAnimator", saveAnimator);
    }
}

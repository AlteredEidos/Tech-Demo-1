using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameManager game;
    public Animator anim;
    private bool saving = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && saving == false)
            {
                saving = true;
                StartCoroutine(Save());
                Debug.Log("saving");
            }
        }
    }
    
    IEnumerator Save()
    {
        game.SavePlayer();
        anim.SetBool("saveAnimation", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("saveAnimation", false);
        saving = false;
    }
}

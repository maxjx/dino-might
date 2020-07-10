using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Logic : MonoBehaviour
{
    public GameObject player;
    public GameObject TeleportShrine;

    private Animator animator;
    private BoxCollider2D thisCollider;
    private Vector2 jumpAttackColliderOffset = new Vector2(1.14f, 0);
    private Vector2 jumpForwardColliderOffset = new Vector2(0.924f, 0);


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    public void DoNextAction()
    {
        float distanceFromPlayer = (transform.position - player.transform.position).magnitude;
        animator.SetFloat("distanceFromPlayer", distanceFromPlayer);
    }

    public void ToggleCollider()
    {
        thisCollider.enabled = !(thisCollider.enabled);
    }

    // for jumping animation states
    public void OffsetCollider(int index)
    {
        switch (index)
        {
            case 1:
                thisCollider.offset = jumpAttackColliderOffset;
                break;
            case 2:
                thisCollider.offset = jumpForwardColliderOffset;
                break;
            default:
                break;
        }
    }

    public void ActivateShrine()
    {
        TeleportShrine.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss2Logic : MonoBehaviour
{
    public GameObject player;
    [Space]
    public UnityEvent deathEvents;      // invoked upon death animation

    private Animator animator;
    private BoxCollider2D thisCollider;
    private Vector2 jumpAttackColliderOffset = new Vector2(1.14f, 0);
    private Vector2 jumpForwardColliderOffset = new Vector2(0.924f, 0);


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        thisCollider = GetComponent<BoxCollider2D>();
        if (deathEvents == null)
        {
            deathEvents = new UnityEvent();
        }
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

    public void InvokeDeathEvents()
    {
        deathEvents.Invoke();
    }

    public void SpareHim()
    {
        Global.masterSpared = true;
    }
}

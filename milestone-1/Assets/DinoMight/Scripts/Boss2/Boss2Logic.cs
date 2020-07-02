﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Actions
{
    JumpAttack,     //0
    Attack,         //1
    JumpForward,    //2
    Summon,         //3
    Walk            //4
}

public class Boss2Logic : MonoBehaviour
{
    public GameObject player;

    private Animator animator;
    private BoxCollider2D bcollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bcollider = GetComponent<BoxCollider2D>();
    }

    public void FinishTalking()
    {
        animator.SetBool("talking", false);
    }

    public void DoNextAction()
    {
        animator.SetInteger("nextMove", CalculateBestMove());
    }

    private int CalculateBestMove()
    {
        return 0;
    }

    public void ToggleCollider()
    {
        bcollider.enabled = !(bcollider.enabled);
    }
}

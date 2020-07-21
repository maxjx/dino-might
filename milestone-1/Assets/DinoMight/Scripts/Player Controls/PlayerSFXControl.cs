using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXControl : MonoBehaviour
{
    public AudioClip kick;
    public AudioClip fireball;
    public AudioClip dash;
    public AudioClip jump;
    public AudioClip damage;
    public AudioClip cure;

    public void playKick()
    {
        AudioManager.Instance.PlayEffect(kick);
    }

    public void playFireball()
    {
        AudioManager.Instance.PlayEffect(fireball);
    }

    public void playDash()
    {
        AudioManager.Instance.PlayEffect(dash);
    }

    public void playJump()
    {
        AudioManager.Instance.PlayEffect(jump);
    }

    public void playDamage()
    {
        AudioManager.Instance.PlayEffect(damage);
    }

    public void playCure()
    {
        AudioManager.Instance.PlayEffect(cure);
    }
}

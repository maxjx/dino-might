using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    public GameObject kingNPC;
    public GameObject kingSprite;       // no dialogue
    public GameObject masterNPC;
    public Transform wizard;
    public GameObject cloud;
    public GameObject lightning;
    public GameObject fist;
    public GameObject sword;
    public Monologger monologger;
    public GameObject fightingWizard;
    public GameObject bosshealthbar;

    private bool wizAttacked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckSparedBosses()
    {
        // masterNPC.SetActive(true);
        // kingSprite.SetActive(true);

        bool kingSpared = Global.kingSpared;
        bool masterSpared = Global.masterSpared;
        if (kingSpared && !masterSpared)
        {
            kingNPC.SetActive(true);
        }
        else if (kingSpared && masterSpared)
        {
            kingSprite.SetActive(true);
            masterNPC.SetActive(true);
        }
        else if (!kingSpared && masterSpared)
        {
            masterNPC.SetActive(true);
        }
        else
        {
            fightingWizard.SetActive(true);
            bosshealthbar.SetActive(true);
            wizard.gameObject.SetActive(false);
        }
    }

    public void EndSparedBosses()
    {
        masterNPC.SetActive(false);
        kingNPC.SetActive(false);
        kingNPC.SetActive(false);
    }

    public void KingAlsoAttack()
    {
        if (kingSprite.activeInHierarchy)
            kingSprite.GetComponent<Animator>().SetTrigger("attack");
    }

    public void AttackWizard()
    {
        if (!wizAttacked)
        {
            wizAttacked = true;
            StartCoroutine(CoAttackWiz());
        }
    }

    private IEnumerator CoAttackWiz()
    {
        if (masterNPC.activeInHierarchy)
        {
            Instantiate(cloud, wizard.position + new Vector3(0, 3.5f, 0), wizard.rotation);
            yield return new WaitForSeconds(1f);
            Instantiate(lightning, wizard.position + new Vector3(0, 1.2f, 0), wizard.rotation);

        }
        if (kingSprite.activeInHierarchy || kingNPC.activeInHierarchy)
        {
            Instantiate(fist, wizard.position + new Vector3(0, 4f, 0), wizard.rotation);
        }

        yield return new WaitForSeconds(1f);

        wizard.GetComponent<Animator>().SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        Instantiate(sword, masterNPC.transform.position - new Vector3(1f, 0, 0), sword.transform.rotation);

        yield return new WaitForSeconds(0.5f);

        if (masterNPC.activeInHierarchy)
        {
            masterNPC.GetComponent<Animator>().SetBool("dead", true);
        }
        if (kingSprite.activeInHierarchy)
        {
            kingSprite.GetComponent<Animator>().SetBool("dead", true);
        }
        if (kingNPC.activeInHierarchy)
        {
            kingNPC.GetComponent<Animator>().SetBool("dead", true);
        }

        monologger.ManualTrigger();

        yield return new WaitForSeconds(1f);

        fightingWizard.SetActive(true);
        bosshealthbar.SetActive(true);
        wizard.gameObject.SetActive(false);
    }

    public void Shake()
    {
        // screen shake
        if (CinemachineShake.Instance != null)
        {
            CinemachineShake.Instance.ShakeCamera(2f, 0.2f);
        }
    }
}

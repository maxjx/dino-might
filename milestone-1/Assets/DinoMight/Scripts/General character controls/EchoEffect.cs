using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public ParticleSystem ps;
    
    public void StartEcho()
    {
        StartCoroutine(CoEcho());
    }

    private IEnumerator CoEcho()
    {
        if (ObjectPooler.Instance != null)
                ObjectPooler.Instance.SpawnFromPool("Ring", transform.position, transform.rotation);
        ps.transform.rotation = transform.rotation;
        ps.Play();
        yield return new WaitForSeconds(0.07f);
        for (int i = 0; i < 3; i++)
        {
            if (ObjectPooler.Instance != null)
                ObjectPooler.Instance.SpawnFromPool("Echo", transform.position, transform.rotation);
            yield return new WaitForSeconds(0.08f);
        }
    }
}

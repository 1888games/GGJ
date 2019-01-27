using System.Collections;
using UnityEngine;

public class Tambourine : AbstractTarget
{
    public override void React()
    {
        StartCoroutine(playtambourine());
        ToddlerController.Instance.OnMadeNoise();
    }

    IEnumerator playtambourine()
    {
        for (int i = 0; i < 10; i++)
        {
            Fabric.EventManager.Instance.PostEvent("Toy_Tambourine", Fabric.EventAction.PlaySound, null, gameObject);
            yield return new WaitForSeconds(1f);
        }
    }
}

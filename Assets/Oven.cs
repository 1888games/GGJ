using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;

    public override void React()
    {
        if (ToddlerController.CurrentTool is IBurnable)
        {
            Burn();
        }
        else
        {
            // TODO: Do we allow cook only?  What happens to tool afterwards?
            //Cook();
        }
    }

    private void Burn()
    {
        print("Oven success reaction.");

        ToddlerController.Instance.DestroyTool();

        // TODO: Need correct sound for burning in oven

        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();
        Fabric.EventManager.Instance.PostEvent("Broken_Blender", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();
    }

    private void Cook()
    {
        // TODO: animation or sound?
    }
}

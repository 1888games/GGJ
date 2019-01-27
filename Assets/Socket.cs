using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
   
    public override void React()
    {
        if (IsReactableTool())
        {
            Shock();
        }
        else
        {
            // do nothing
        }
    }

    private void Shock()
    {
        print("Tambourine success reaction.");

        // TODO: animation or particle effects?

        Fabric.EventManager.Instance.PostEvent("Powerplug_Shock", Fabric.EventAction.PlaySound, null, gameObject);
        Instantiate(_noisePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }
}

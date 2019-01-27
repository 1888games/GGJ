using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    [SerializeField] ParticleSystem _ps;
   
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
        Fabric.EventManager.Instance.PostEvent("Powerplug_Shock", Fabric.EventAction.PlaySound, null, gameObject);
        ToddlerController.Instance.OnMadeNoise();
        ExperienceController.Instance.UpdateExperienceAndAnguish(name, ToddlerController.CurrentTool.name);
        ToddlerController.Instance.DestroyTool();
        _ps.Play();
//        Instantiate(_noisePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }
}

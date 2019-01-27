using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WashingMachine : AbstractTarget
{
    
    [SerializeField] GameObject _noisePrefab;

	public Transform door;
    
     void OnEnable()
    {
        ToddlerController.OnChaseModeFinished += ResetState;
    }
    
    public override void React()
    {
        if (IsReactableTool())
        {
            Wash();
        }
        else
        {
            // do nothing
        }
    }
    
    void ResetState()
    {
        Animation animation = GetComponent<Animation>();
        animation.Stop();
       // transform.rotation = Quaternion.identity;
        
        door.DORotate (new Vector3 (0f, 75f, 90f), 0.25f);
    }

    private void Wash()
    {
        print("Washing machine success reaction.");

        ToddlerController.Instance.DestroyTool();

        // TODO: Need correct sound for washing machine 

        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();
        Fabric.EventManager.Instance.PostEvent("Washing_Machine_Sequence", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();

		door.DORotate (new Vector3 (0f, 0f, 90f), 0.25f);
        
    }
}

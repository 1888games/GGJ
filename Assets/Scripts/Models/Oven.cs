using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oven : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    [SerializeField] ParticleSystem _firePS;

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

//        SceneManager.LoadScene()

        // TODO: Need correct sound for burning in oven

        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();

        Fabric.EventManager.Instance.PostEvent("Oven_Sequence", Fabric.EventAction.PlaySound, null, gameObject);
        ExperienceController.Instance.UpdateExperienceAndAnguish(name,ToddlerController.CurrentTool.name );
        
        _firePS.Play();
        ToddlerController.Instance.OnMadeNoise();
        ToddlerController.Instance.DestroyTool();

    }

    private void Cook()
    {
        // TODO: animation or sound?
    }
}

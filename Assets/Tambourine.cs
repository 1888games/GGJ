
using UnityEngine;

public class Tambourine : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    public override void React()
    {
        Fabric.EventManager.Instance.PostEvent("Toy_Tambourine", Fabric.EventAction.PlaySound, null, gameObject);        
        Instantiate(_noisePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) ,Quaternion.identity);
    }
}

using UnityEngine;
using System.Collections;

public class PronamaChanController : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject = null;
    public GameObject TargetObject { get { return _targetObject; } }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == TargetObject)
        {
            GameObject.Destroy(TargetObject);
        }
    }
}

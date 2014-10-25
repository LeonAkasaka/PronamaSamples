using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Locomotor))]
public class LocomotionController : MonoBehaviour
{
    private Locomotor _locomotor;
    public Locomotor Locomotor { get { return _locomotor; } }

    protected virtual void Start()
    {
        _locomotor = GetComponent<Locomotor>();
	}
}

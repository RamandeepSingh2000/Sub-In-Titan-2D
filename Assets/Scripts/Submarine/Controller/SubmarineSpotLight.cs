using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Submarine
{
    public class SubmarineSpotLight : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        void Update()
        {
            transform.position = _target.position;
            transform.rotation = _target.rotation;
        }
    }
}
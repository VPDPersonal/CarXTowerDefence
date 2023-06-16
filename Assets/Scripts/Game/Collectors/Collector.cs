using UnityEngine;

namespace Game.Collectors
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Collector<T> : MonoBehaviour
        where T : Object

    {
        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<T>())
                other.gameObject.SetActive(false);
        }
    }
}

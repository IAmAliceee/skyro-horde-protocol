using AliceTools.Variables;
using UnityEngine;

namespace AliceTools.Events.Tools
{
    public class OnCollisionEnterEvent : MonoBehaviour
    {
        [SerializeField] private Dimensions dimensions;
        [SerializeField] private GameEvent OnCollision;
        [SerializeField] private StringVariable HasTag;

        public enum Dimensions
        {
            Dim2D,
            Dim3D
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (dimensions == Dimensions.Dim2D)
                OnCollisionMethod(col.collider.tag);
        }

        private void OnCollisionEnter(Collision col) {
            if (dimensions == Dimensions.Dim3D)
                OnCollisionMethod(col.collider.tag);
        }

        private void OnCollisionMethod(string tag) {
            if(HasTag == null || tag == HasTag)
                OnCollision.Invoke();
        }
    }
}
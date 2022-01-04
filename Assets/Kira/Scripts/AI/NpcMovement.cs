using UnityEngine;
using UnityEngine.AI;

namespace Kira
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector3[] _waypoints;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}
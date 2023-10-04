namespace Chillplay.Tools
{
    using UnityEngine.AI;

    public static class NavMeshAgentExtension
    {
        //? http://answers.unity3d.com/answers/746157/view.html
        public static bool HasArrived(this NavMeshAgent agent)
        {
            bool one = !agent.pathPending;
            bool two = agent.remainingDistance <= agent.stoppingDistance;
            bool three = !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
            bool hasArrived = one && two && three;
            return hasArrived;
        }
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/NPC")]
public class NPCParameters : ScriptableObject
{
    [System.Serializable]
    public struct DetectionParameters
    {
        public float viewDistance;
        public float fieldOfView;
        public float hearDistance;
    }

    [System.Serializable]
    public struct MovementParameters
    {
        public float patrolSpeed;
        public float chaseSpeed;
    }

    [System.Serializable]
    public struct TimerParameters
    {
        public Vector2 idleTimer;
        public Vector2 patrolTimer;
        public Vector2 searchTimer;
        public Vector2 searchIdleTimer;
    }

    [System.Serializable]
    public struct BehaviourParameters
    {
        public float health;
        public float attackCooldown;
        public bool isFriendly;
    }

    [Header("Detection Parameters")]
    public DetectionParameters detection;

    [Header("Movement Parameters")]
    public MovementParameters movement;

    [Header("Timer Parameters")]
    public TimerParameters timeRanges;

    [Header("Behaviour Parameters")]
    public BehaviourParameters behaviour;
}

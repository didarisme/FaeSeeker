using UnityEngine;
using UnityEngine.AI;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private string currentState;
    [Space]
    public NPCParameters parameters;
    public Transform viewPoint;

    [Header("Patrol route")]
    public EnemyPath path;

    private int waypointIndex;
    private float distance;
    private Vector3 lastKnowPos, lastPatrolPoint;

    private PlayerMove playerMove;
    private StateMachine stateMachine;

    private NavMeshAgent agent;
    private Transform player;
    private Animator characterAnimator;
    public bool isAnimatorExist;

    public NavMeshAgent Agent { get => agent; }
    public Transform Player { get => player; }
    public Animator CharacterAnimator { get => characterAnimator; }

    public int WaypointIndex { get => waypointIndex; set => waypointIndex = value; }
    public float PlayerDistance { get => distance; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    public Vector3 LastPatrolPoint { get => lastPatrolPoint; set => lastPatrolPoint = value; }    

    private void Start()
    {
        if (parameters == null)
        {
            Debug.LogWarning("Parameters variable is not configured, npc not going to work!");
            return;
        }

        stateMachine = GetComponent<StateMachine>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMove>();

        agent = GetComponent<NavMeshAgent>();

        if (TryGetComponent<Animator>(out Animator animator))
        {
            characterAnimator = animator;
            isAnimatorExist = true;
        }
        else
        {
            isAnimatorExist = false;
            Debug.LogWarning("Animtor not found");
        }

        stateMachine.Initialise();
    }

    private void Update()
    {
        if (parameters == null) return;

        DistanceCheck();
        currentState = stateMachine.activeState.ToString();
    }

    private void DistanceCheck()
    {
        distance = Vector3.Distance(transform.position, player.position);
    }

    public bool CanSeePlayer()
    {
        if (player != null && !parameters.behaviour.isFriendly)
        {
            if (distance < parameters.detection.viewDistance)
            {
                Vector3 targetDirection = (player.position + Vector3.up) - viewPoint.position;
                float angleToPlayer = Vector3.Angle(targetDirection, viewPoint.forward);

                if (angleToPlayer >= -parameters.detection.fieldOfView && angleToPlayer <= parameters.detection.fieldOfView)
                {
                    Ray ray = new Ray(viewPoint.position, targetDirection);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo, parameters.detection.viewDistance))
                    {
                        if (hitInfo.transform.gameObject == player.gameObject)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * parameters.detection.viewDistance);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public bool CanHearPlayer()
    {
        if (playerMove.CurrentState != PlayerMove.MovementState.crouchning && Vector3.Magnitude(playerMove.CurrentInput) > 0 && !parameters.behaviour.isFriendly)
        {
            if (distance < parameters.detection.hearDistance)
            {
                return true;
            }
        }

        return false;
    }

    public Vector3 RandomDestination()
    {
        Vector3 previousDestionation = agent.destination;
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        randomPosition.y = Mathf.Clamp(randomPosition.y, -3f, 3f);

        Vector3 newDestination = transform.position + randomPosition;

        while (previousDestionation == newDestination)
        {
            randomPosition = Random.insideUnitSphere * 10f;
            newDestination = transform.position + randomPosition;
        }

        //agent.SetDestination(newDestination); need one frame to calculate!
        return newDestination;
    }
}
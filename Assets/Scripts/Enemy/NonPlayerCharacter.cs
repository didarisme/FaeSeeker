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
    public int waypointIndex;
    public Vector3 lastPatrolPoint;

    private PlayerMove playerMove;
    private StateMachine stateMachine;

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 lastKnowPos;
    private Animator enemyAnimator;

    public NavMeshAgent Agent { get => agent; }
    public Transform Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    public Animator EnemyAnimator { get => enemyAnimator; }

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
        enemyAnimator = GetComponentInChildren<Animator>();
        stateMachine.Initialise();
    }

    private void Update()
    {
        if (parameters == null) return;

        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) < parameters.detection.viewDistance)
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
        if (playerMove.CurrentState != PlayerMove.MovementState.crouchning && Vector3.Magnitude(playerMove.CurrentInput) > 0)
        {
            float distance = Vector3.Distance(transform.position, player.position);

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
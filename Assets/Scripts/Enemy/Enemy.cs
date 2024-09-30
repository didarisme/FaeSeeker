using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 lastKnowPos;
    private Animator enemyAnimator;
    private PlayerMove playerMove;

    public NavMeshAgent Agent { get => agent; }
    public Transform Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    public Animator EnemyAnimator { get => enemyAnimator; }

    [SerializeField] private string currentState;

    [Header("Parameters")]
    public float viewDistance = 10f;
    public float hearDiatance = 20f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float fieldOfView = 85f;
    public Transform viewPoint;

    [Header("Patrol route")]
    public EnemyPath path;
    public int waypointIndex;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponentInChildren<Animator>();
        playerMove = player.GetComponent<PlayerMove>();
        stateMachine.Initialise();
    }

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) < viewDistance)
            {
                Vector3 targetDirection = (player.position + Vector3.up) - viewPoint.position;
                float angleToPlayer = Vector3.Angle(targetDirection, viewPoint.forward);

                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(viewPoint.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo, viewDistance))
                    {
                        if (hitInfo.transform.gameObject == player.gameObject)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * viewDistance);
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

            if (distance < hearDiatance)
            {
                return true;
            }
        }

        return false;
    }
}

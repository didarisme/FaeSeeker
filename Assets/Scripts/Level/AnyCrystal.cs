using System.Collections;
using UnityEngine;

public class AnyCrystal : Interactable
{
    [Header("Gameplay")]
    [SerializeField] private string hoverName;
    [SerializeField] private CrystalType crystalType;

    [Header("Parameters")]
    [SerializeField][Tooltip("if true OnCollect, else OnInteract")] private bool isCollectable;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float collectSpeed = 1f;

    private Collider manaCollider;
    private Outline outline;

    private float defaultYPos;
    private float timer;

    private enum CrystalType
    {
        health,
        mana
    }

    private void Start()
    {
        manaCollider = GetComponent<Collider>();
        defaultYPos = transform.position.y;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update()
    {
        Levitation();
    }

    public override void OnFocus()
    {
        HoverText.OnHoverText?.Invoke(hoverName);
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        if (manaCollider.enabled && !isCollectable)
        {
            manaCollider.enabled = false;
            StartCoroutine(Collect());
        }
    }

    public override void OnCollect()
    {
        if (manaCollider.enabled && isCollectable)
        {
            manaCollider.enabled = false;
            StartCoroutine(Collect());
        }
    }

    public override void OnLoseFocus()
    {
        HoverText.OnHoverText?.Invoke(null);
        outline.enabled = false;
    }

    private void Levitation()
    {
        timer += Time.deltaTime;

        transform.Rotate(0, 10f * speed * Time.deltaTime, 0);

        if (manaCollider != null) return;

        transform.position = new Vector3(transform.position.x, defaultYPos + Mathf.PingPong(timer * amplitude, amplitude), transform.position.z);
    }

    private IEnumerator Collect()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        
        while (Vector3.Distance(player.position + Vector3.up, transform.position) > 0.1f)
        {
            collectSpeed += collectSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position + Vector3.up, collectSpeed * Time.deltaTime);
            yield return null;
        }

        switch (crystalType)
        {
            case CrystalType.health:
                FindObjectOfType<PlayerStats>().OnHealth(1);
                break;
            case CrystalType.mana:
                FindObjectOfType<PlayerStats>().OnMana(1);
                break;
            default:
                Debug.Log("idi nahuy");
                break;
        }

        Destroy(gameObject);
    }
}
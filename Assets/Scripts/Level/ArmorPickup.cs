using System.Collections;
using UnityEngine;

public class ArmorPickup : Interactable
{
    [Header("Gameplay")]
    [SerializeField] private string hoverName;
    [SerializeField] private GameObject armor;

    [Header("Parameters")]
    [SerializeField][Tooltip("if true OnCollect, else OnInteract")] private bool isCollectable;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float collectSpeed = 1f;

    private Collider armorCollider;
    private Outline outline;
    private DaxForms daxForms;

    private float defaultYPos;
    private float timer;

    private void Start()
    {
        daxForms = FindObjectOfType<DaxForms>();
        armorCollider = GetComponent<Collider>();
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
        if (armorCollider.enabled && !isCollectable)
        {
            armorCollider.enabled = false;
            StartCoroutine(Collect());
        }
    }

    public override void OnCollect()
    {
        if (armorCollider.enabled && isCollectable)
        {
            armorCollider.enabled = false;
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

        if (armorCollider != null) return;

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

        if (armor != null)
        {
            armor.SetActive(true);
        }

        daxForms.ChangeForm(DaxForms.FormType.Physical);//future thoughts

        Destroy(gameObject);
    }
}
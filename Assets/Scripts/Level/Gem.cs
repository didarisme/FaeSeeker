using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gem : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float collectSpeed = 1f;
 
    private float timer = 0;
    [SerializeField] private float defaultYPos;

    // Update is called once per frame
    void Update()
    {
        Levitation();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            StartCoroutine(Collect(other.transform, stats));
        }
    }
    private void Levitation()
    {
        timer += Time.deltaTime;

        transform.Rotate(0, 10f * speed * Time.deltaTime, 0);

        transform.position = new Vector3(transform.position.x, defaultYPos + Mathf.PingPong(timer * amplitude, amplitude), transform.position.z);
    }

    private IEnumerator Collect(Transform player, PlayerStats stats)
    {
        while (Vector3.Distance(player.position + Vector3.up, transform.position) > 1f)
        {
            collectSpeed += collectSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position + Vector3.up, collectSpeed * Time.deltaTime);
            yield return null;
        }
        ChangeStat(stats);
        Destroy(gameObject);
    }

    public abstract void ChangeStat(PlayerStats stats);
}

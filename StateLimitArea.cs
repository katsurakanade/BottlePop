using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLimitArea : MonoBehaviour
{

    [Header("設定")]
    [Tooltip("制限するタイプ")]
    public Status.StatusMode type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus ps))
            {
                if (!ps.HasStatus(type))
                {
                    ps.StateChange();
                }

                ps.AddStatus(type);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus ps))
            {
                ps.Limited = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus ps))
            {
                ps.Limited = false;
            }
        }
    }

   
}

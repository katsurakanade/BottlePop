using System.Collections;
using UnityEngine;
using TMPro;

public class LevelTopDiaglog : MonoBehaviour
{

    private Animator animator;
    private MeshRenderer TextRender;

    private SpriteRenderer ame;
    private static readonly int Play = Animator.StringToHash("Play");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        TextRender = transform.GetChild(0).GetComponent<MeshRenderer>();
        ame = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.instance.barController.clear)
        {
            animator.SetBool(Play, true);
            StartCoroutine(DisplayText());
        }

        else if (collision.CompareTag("Player") && GameManager.instance.barController.clear)
        {
            GameManager.instance.playerController.enabled = false;
            GameManager.instance.playerStatus.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CloseDisplay());
        }
    }

    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(0.5f);
        
        TextRender.enabled = true;
        transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text =
            $"あと{(GameManager.instance.barController.clearCount - GameManager.instance.barController.collectedCount)}こ";
        transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().sortingOrder = 1;
        ame.enabled = true;
    }


    IEnumerator CloseDisplay()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(Play, false);
        TextRender.enabled = false;
        ame.enabled = false;
    }
}

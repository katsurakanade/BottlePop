using UnityEngine;
using UnityEngine.EventSystems;

public class SelectAnimation : MonoBehaviour
{
    public GameObject firstselect;
    // Start is called before the first frame update
    private void Start()
    {
        Animation();
    }
    
    private void Animation()
    {
        var eventSystem = FindObjectOfType<EventSystem>();
        
        if (SceneController.Instance != null)
        {
            int targetStage = SceneController.Instance.lastClearedScene + 1;

            if (targetStage >= SceneController.FirstStageIndex && targetStage <= SceneController.LastStageIndex)
            {
                foreach (Transform child in transform)
                {
                    var stageIcon = child.GetComponent<StageSelect>();

                    if (!stageIcon) continue;

                    if (stageIcon.target == targetStage)
                    {
                        eventSystem.SetSelectedGameObject(stageIcon.gameObject);
                        break;
                    }
                }
            }
            else
            {
                eventSystem.SetSelectedGameObject(firstselect);
            }
        }
        else
        {
            eventSystem.SetSelectedGameObject(firstselect);
        }
    }
}

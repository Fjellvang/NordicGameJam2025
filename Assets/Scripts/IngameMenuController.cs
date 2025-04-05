using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenuController : MonoBehaviour
{
    public GameObject GameOverMenu;

    private void Start()
    {
        SleepManager.Instance.OnSleep += ShowGameOverMenu;
    }

    [ContextMenu("ShowGameOverMenu")]
    public void ShowGameOverMenu()
    {
        GameOverMenu.SetActive(true);
        GameOverMenu.transform.localScale = Vector3.zero;
        GameOverMenu.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

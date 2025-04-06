using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenuController : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject TryAgainMenu;

    private bool isShowingMenu;

    private void Start()
    {
        SleepManager.Instance.OnSleep.AddListener(() => ShowGameOverMenu(GameOverMenu));
    }

    [ContextMenu("ShowGameOverMenu")]
    public void ShowGameOverMenu(GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce);
    }

    public void Update()
    {
        var pressed = Input.GetKeyDown(KeyCode.Escape);
        if(pressed && !isShowingMenu)
        {
            ShowGameOverMenu(TryAgainMenu);
            isShowingMenu = true;
        } else if(pressed && isShowingMenu)
        {
            TryAgainMenu.SetActive(false);
            isShowingMenu = false;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

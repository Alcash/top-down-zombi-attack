using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIController : MonoBehaviour
{
    protected BackgroundPanel backgroundPanel = null;

    [SerializeField]
    protected GameObject window = null;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        backgroundPanel = GetComponentInChildren<BackgroundPanel>();
        if (backgroundPanel)
        {
            backgroundPanel.OnTap += Close;
        }
    }

    public void Close(bool isMoment)
    {
        if(isMoment)
        {
            Close();
        }
        else
        {
            StartCoroutine(CloseAnimation());
        }
    }

    protected virtual void Close()
    {
        if (backgroundPanel != null)
            backgroundPanel.gameObject.SetActive(false);
        window.SetActive(false);
    }

    protected virtual IEnumerator CloseAnimation()
    {
        yield return null;
        if (backgroundPanel != null)
            backgroundPanel.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
    }

    public void Open(bool isMoment)
    {
        if (isMoment)
        {
            Open();
        }
        else
        {
            StartCoroutine(OpenAnimation());
        }
    }

    protected virtual void Open()
    {
        if(backgroundPanel != null)
            backgroundPanel.gameObject.SetActive(true);
        window.gameObject.SetActive(true);
    }

    protected virtual IEnumerator OpenAnimation()
    {
        yield return null;
        if (backgroundPanel != null)
            backgroundPanel.gameObject.SetActive(true);
        window.gameObject.SetActive(true);
    }
}

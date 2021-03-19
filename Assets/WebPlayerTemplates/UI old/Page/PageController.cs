using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public static PageController instance;

    public bool debug;
    public PageID entryPage;
    public Page[] pages;

    private Hashtable m_Pages;
    private List<Page> m_OnList;
    private List<Page> m_OffList;

    #region Unity Functions
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            m_Pages = new Hashtable();
            m_OnList = new List<Page>();
            m_OffList = new List<Page>();
            RegisterAllPages();

            if (entryPage != PageID.None)
            {
                TurnPageOn(entryPage);
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Turn on page with type '_type'
    /// </summary>
    public void TurnPageOn(PageID _id)
    {
        if (_id == PageID.None) return;
        if (!PageExists(_id))
        {
            LogWarning("You are trying to turn a page on [" + _id + "] that has not been registered.");
            return;
        }

        Page _page = GetPage(_id);
        _page.gameObject.SetActive(true);
        _page.Activate(true);
    }

    /// <summary>
    /// Turn off page with type '_off'
    /// Optionally turn page with type '_on' on
    /// Optionally wait for page to exit before turning on optional page
    /// </summary>
    public void TurnPageOff(PageID _off, PageID _on = PageID.None, bool _waitForExit = false)
    {
        if (_off == PageID.None) return;
        if (!PageExists(_off))
        {
            LogWarning("You are trying to turn a page off [" + _on + "] that has not been registered.");
            return;
        }

        Page _offPage = GetPage(_off);
        if (_offPage.gameObject.activeSelf)
        {
            _offPage.Activate(false);
        }

        TurnPageOn(_on);
    }

    public bool PageIsOn(PageID _type)
    {
        if (!PageExists(_type))
        {
            LogWarning("You are trying to detect if a page is on [" + _type + "], but it has not been registered.");
            return false;
        }

        return GetPage(_type).isOn;
    }
    #endregion

    #region Private Functions

    private void RegisterAllPages()
    {
        foreach (Page _page in pages)
        {
            RegisterPage(_page);
        }
    }

    private void RegisterPage(Page _page)
    {
        if (PageExists(_page.id))
        {
            LogWarning("You are trying to register a page [" + _page.id + "] that has already been registered: <color=#f00>" + _page.gameObject.name + "</color>.");
            return;
        }

        m_Pages.Add(_page.id, _page);
        Log("Registered new page [" + _page.id + "].");
    }

    private Page GetPage(PageID _id)
    {
        if (!PageExists(_id))
        {
            LogWarning("You are trying to get a page [" + _id + "] that has not been registered.");
            return null;
        }

        return (Page)m_Pages[_id];
    }

    private bool PageExists(PageID _id)
    {
        return m_Pages.ContainsKey(_id);
    }

    private void Log(string _msg)
    {
        if (!debug) return;
        Debug.Log("[Page Controller]: " + _msg);
    }

    private void LogWarning(string _msg)
    {
        if (!debug) return;
        Debug.LogWarning("[Page Controller]: " + _msg);
    }
    #endregion
}

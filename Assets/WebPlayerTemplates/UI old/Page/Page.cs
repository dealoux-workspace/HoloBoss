using System.Collections;
using UnityEngine;

public class Page : MonoBehaviour
{
    public static readonly string FLAG_ON = "On";
    public static readonly string FLAG_OFF = "Off";
    public static readonly string FLAG_NONE = "None";

    public PageID id;
    public bool useAnimation;
    public string targetState { get; private set; }

    /*
     * Animaton Requirements...
     *  - This class uses certain controls to determine page state
     *  - Pages have three core states:
     *      1. Resting
     *      2. Turning On
     *      3. Turning Off
     *  - The animator must have a control boolean called 'on'. Otherwise the animator will not work.
     */
    private bool m_IsOn;

    public bool isOn
    {
        get
        {
            return m_IsOn;
        }
        private set
        {
            m_IsOn = value;
        }
    }

    #region Public Functions
    /// <summary>
    /// Call this to turn the page on or off by setting the control '_on'
    /// </summary>
    public void Activate(bool _on)
    {
        
        if (useAnimation)
        {
            // Animate here
        }
        else
        {
            if (!_on)
            {
                isOn = false;
                gameObject.SetActive(false);
            }
            else
            {
                isOn = true;
            }
        } 
    }
    #endregion

    #region Private Functions
    #endregion
}

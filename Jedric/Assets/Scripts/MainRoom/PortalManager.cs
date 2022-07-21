using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public static PortalManager instance { get; private set; }

    // progress level 0 -> first portal open only
    // progress level 1 -> first and second portal open
    // progress level 2 -> first, second third portal
    // progress level 3 -> first, second, third, fourth portal open
    [SerializeField] Portal[] portals;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Portal Manager in scene");

        }
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            if (i <= GameManager.instance.progressLevel)
            {
                portals[i].gameObject.SetActive(true);
            } else
            {
                portals[i].gameObject.SetActive(false);
            }
        }
    }

}

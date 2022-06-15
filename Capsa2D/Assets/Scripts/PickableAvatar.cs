using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableAvatar : Avatar
{
    private ToggleGroup toggleGroup;
    private Toggle toggle;
    private AvatarInfo info;

    public void Picked(bool value)
    {
        PlayerAtr.instance.AvatarId = info.avatarId;
    }

    public void Init(AvatarInfo info, ToggleGroup toggleGroup)
    {
        Debug.Log("Init");
        toggle = GetComponent<Toggle>();
        Init(info);
        this.info = info;
        this.toggleGroup = toggleGroup;
        toggle.group = toggleGroup;
    }
}

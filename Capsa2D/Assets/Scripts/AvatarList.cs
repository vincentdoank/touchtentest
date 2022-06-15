using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarList : MonoBehaviour
{
    public DefaultAvatar avatarData;
    public List<PickableAvatar> avatarList;
    public Transform avatarParent;
    public GameObject avatarPrefab;
    public ToggleGroup toggleGroup;

    private void Awake()
    {
        if (avatarData != null)
        {
            foreach (AvatarInfo info in avatarData.avatarList)
            {
                SpawnAvatarItem(info);
            }
        }
    }

    private void SpawnAvatarItem(AvatarInfo info)
    {
        GameObject go = Instantiate(avatarPrefab, avatarParent, false);
        go.transform.localScale = Vector3.one;
        PickableAvatar avatar = go.GetComponent<PickableAvatar>();
        Debug.Log("info : " + info);
        Debug.Log("togglegroup : " + toggleGroup);
        Debug.Log("avatar : " + avatar);
        avatar.Init(info, toggleGroup);
        avatarList.Add(avatar);
    }
}

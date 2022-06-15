using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvatarList", menuName = "ScriptableObjects/DefaultAvatar", order = 1)]
public class DefaultAvatar : ScriptableObject
{
    public List<AvatarInfo> avatarList;
}

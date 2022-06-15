using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AvatarInfo
{
    public string avatarId;
    public string faceImageName;
    public string hairImageName;
    public string dressImageName;
}

public class Avatar : MonoBehaviour
{
    public Image faceImage;
    public Image hairImage;
    public Image dressIamge;

    public Sprite happyFaceSprite;
    public Sprite sadFaceSprite;

    private AvatarInfo info;

    public void Init(AvatarInfo info)
    {
        this.info = info;
        faceImage.sprite = Resources.Load<Sprite>("Textures/Faces/" + info.faceImageName);
        hairImage.sprite = Resources.Load<Sprite>("Textures/Hairs/" + info.hairImageName);
        dressIamge.sprite = Resources.Load<Sprite>("Textures/Kits/" + info.dressImageName);
    }

    public void Init(string avatarId)
    {
        List<AvatarInfo> avatarInfoList = CapsaGameManager.instance.avatar.avatarList;
        AvatarInfo info = avatarInfoList.Where(x => x.avatarId == avatarId).FirstOrDefault();
        if (info != null)
        {
            this.info = info;
            faceImage.sprite = Resources.Load<Sprite>("Textures/Faces/" + info.faceImageName);
            hairImage.sprite = Resources.Load<Sprite>("Textures/Hairs/" + info.hairImageName);
            dressIamge.sprite = Resources.Load<Sprite>("Textures/Kits/" + info.dressImageName);
        }
    }

    public void OnWinState()
    {
        faceImage.sprite = happyFaceSprite;
    }

    public void OnLoseState()
    {
        faceImage.sprite = sadFaceSprite;
    }

    public void Reset()
    {
        faceImage.sprite = Resources.Load<Sprite>("Textures/Faces/" + info.faceImageName);
    }
}

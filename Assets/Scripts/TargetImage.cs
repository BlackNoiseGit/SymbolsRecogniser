using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetImage : MonoBehaviour {

    [SerializeField]
    private Sprite image;

    [SerializeField]
    private string symbolName;



    public Sprite TargetSymbolImage
    {
        get { return image; }
    }


    public string TargetSymbolName
    {
        get { return symbolName; }
    }

}

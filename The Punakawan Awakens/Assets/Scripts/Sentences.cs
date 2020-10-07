using UnityEngine;

[System.Serializable]
public class Sentences
{
    public enum Direction { LEFT, RIGHT };
    public enum WhereToPlace { LEFT, RIGHT };

    public string name;
    public Sprite characterSprite;
    public Direction dir;
    public WhereToPlace whereToPlace;
    public Sprite item;
    public bool isMiddleSpriteActive;


    [TextArea()]
    public string sentence;
}

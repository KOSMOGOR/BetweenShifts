using UnityEngine;

[CreateAssetMenu(fileName = "DialogueCharacter", menuName = "Scriptable Objects/DialogueCharacter")]
public class DialogueCharacter : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
}

using UnityEngine;

    [CreateAssetMenu(fileName="dialogue",menuName="dialogue",order =1)]
public class Conversation : ScriptableObject
{
    [TextArea(2, 40)] public string conversationDialogue;
    [SerializeField] Sprite charSprite;
    public Conversation[] options;
    Conversation head = null;       
}

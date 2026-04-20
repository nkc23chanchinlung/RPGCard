using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D sourceTexture;
    [SerializeField] private Material spriteMaterial;
    [SerializeField] private Transform parent;

    public void CreateSpriteObject()
    {
        if (sourceTexture == null)
        {
            Debug.LogError("Texture2D ‚ª“ü‚Á‚Ä‚È‚¢‚æ");
            return;
        }

        Sprite sprite = Sprite.Create(
            sourceTexture,
            new Rect(0, 0, sourceTexture.width, sourceTexture.height),
            new Vector2(0.5f, 0.5f),
            100f
        );

        GameObject go = new GameObject("GeneratedSprite");
        if (parent != null)
        {
            go.transform.SetParent(parent, false);
        }

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        if (spriteMaterial != null)
        {
            sr.material = spriteMaterial;
        }
    }
}
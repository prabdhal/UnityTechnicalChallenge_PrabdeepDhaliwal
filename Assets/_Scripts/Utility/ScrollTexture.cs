using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float scrollSpeedX;
    [SerializeField]
    private float scrollSpeedY;

    private MeshRenderer meshRenderer;
    #endregion

    #region Init & Update
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * scrollSpeedX, Time.realtimeSinceStartup * scrollSpeedY);
    }
    #endregion
}

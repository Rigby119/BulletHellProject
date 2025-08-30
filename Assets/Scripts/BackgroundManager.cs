using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] backgrounds;
    private float backgroundWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform bg in backgrounds){
            bg.Translate(Vector3.up * speed * Time.deltaTime);

            if (bg.position.x <= -backgroundWidth)
            {
                float rightMostX = GetRightmostBackgroundX();
                bg.position = new Vector3(rightMostX + backgroundWidth, 0, 92);
            }
        }
    }

    float GetRightmostBackgroundX()
    {
        float maxX = float.MinValue;
        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x > maxX)
                maxX = bg.position.x;
        }
        return maxX;
    }
}

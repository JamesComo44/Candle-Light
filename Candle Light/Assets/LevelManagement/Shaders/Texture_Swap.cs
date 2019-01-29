using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture_Swap : MonoBehaviour
{
    public Texture f_MainTexture;
    public Texture s_MainTexture;
    public Texture t_MainTexture;
    public Texture r_MainTexture;
    public Texture f_Normal;
    Renderer m_Renderer;
    int changeTex;
    float scrollSpeed = 0.5f;
    // Use this for initialization
    void Start()
    {
        changeTex = 1;
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();

        m_Renderer.material.SetTexture("_MainTex", f_MainTexture);
        m_Renderer.material.SetTexture("_BumpMap", f_Normal);

    }
    void SwapTex()
    {
        if (Input.GetKeyDown("r"))
        {
            changeTex += 1;
            if (changeTex > 4)
            {
                changeTex = 1;
            }
        }
        switch (changeTex)
        {
            case 1:
                {
                    m_Renderer.material.SetTexture("_MainTex", f_MainTexture);
                    float offset = Time.time * scrollSpeed;
                    m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, -offset));
                    m_Renderer.material.SetTextureOffset("_BumpMap", new Vector2(offset, -offset));
                    break;
                }
            case 2:
                {
                    m_Renderer.material.SetTexture("_MainTex", s_MainTexture);
                    m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
                    break;
                }
            case 3:
                {
                    m_Renderer.material.SetTexture("_MainTex", t_MainTexture);
                    m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
                    break;
                }
            case 4:
                {
                    m_Renderer.material.SetTexture("_MainTex", r_MainTexture);
                    m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
                    break;
                }
        }
    }
    void Update()
    {
        SwapTex();
    }
}

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class RenderToTexture : MonoBehaviour {

    public  RenderTexture[] mrtTex = new RenderTexture[2];
    private RenderBuffer[] mrtRB = new RenderBuffer[2];

    public Material LightingMat;

    // Use this for initialization
    void Start () {
        mrtTex[0] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        mrtTex[0].name = "RT0";
        mrtTex[1] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        mrtTex[1].name = "RT1";
        //mrtTex[2] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        mrtRB[0] = mrtTex[0].colorBuffer;
        mrtRB[1] = mrtTex[1].colorBuffer;

        GetComponent<Camera>().SetTargetBuffers(mrtRB, mrtTex[0].depthBuffer);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPreRender()
    {
        //RenderTexture oldRT = RenderTexture.active;
        //Graphics.SetRenderTarget(mrtRB, mrtTex[0].depthBuffer);
        //GetComponent<Camera>().SetTargetBuffers(mrtRB, mrtTex[0].depthBuffer);
        GL.Clear(true, true, Color.black);
       // GetComponent<Camera>().SetTargetBuffers(mrtRB, mrtTex[0].depthBuffer);
        //RenderTexture.active = oldRT;
    }
    void OnPostRender()
    {
        //Display screen1 = Display.displays[0];
        //Graphics.SetRenderTarget(screen1.colorBuffer, screen1.depthBuffer);

        LightingMat.SetTexture("_Tex0", mrtTex[0]);
        LightingMat.SetTexture("_Tex1", mrtTex[1]);

        RenderTexture oldRT = RenderTexture.active;

        Display screen1 = Display.displays[0];
        Graphics.SetRenderTarget(screen1.colorBuffer, screen1.depthBuffer);

        GL.Clear(false, true, Color.clear);

        GL.PushMatrix();
        GL.LoadOrtho();

        LightingMat.SetPass(0);     //Pass 0 outputs 2 render textures.

        //Render the full screen quad manually.
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 0.1f);
        GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 0.0f, 0.1f);
        GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 0.1f);
        GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(0.0f, 1.0f, 0.1f);
        GL.End();

        GL.PopMatrix();

        RenderTexture.active = oldRT;
    }

    /*void OnRenderImage(RenderTexture source, RenderTexture destination)
     {

        //Graphics.Blit(source, destination);
        // Display screen1 = Display.displays[0];
        //Graphics.SetRenderTarget(screen1.colorBuffer, screen1.depthBuffer);
        LightingMat.SetTexture("_Tex0", mrtTex[0]);

        RenderTexture oldRT = RenderTexture.active;

        Display screen1 = Display.displays[0];
        Graphics.SetRenderTarget(screen1.colorBuffer, screen1.depthBuffer);

        GL.Clear(false, true, Color.clear);

        GL.PushMatrix();
        GL.LoadOrtho();

        LightingMat.SetPass(0);     //Pass 0 outputs 2 render textures.

        //Render the full screen quad manually.
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 0.1f);
        GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 0.0f, 0.1f);
        GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 0.1f);
        GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(0.0f, 1.0f, 0.1f);
        GL.End();

        GL.PopMatrix();

        RenderTexture.active = oldRT;

        Graphics.Blit(source, destination);
    }*/
}

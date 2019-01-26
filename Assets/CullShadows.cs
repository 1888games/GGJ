using UnityEngine;
 using System.Collections;
 
 public class CullShadows : MonoBehaviour {
 
     public Shader unlitShader;
 
     void Start() {
       //  unlitShader = Shader.Find("Unlit/Texture");
       //  GetComponent<Camera>().SetReplacementShader(unlitShader,"");
     }
     
     private float previousShadowDistance;
      
    void OnPreRender () {
        previousShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }
    void OnPostRender(){
        QualitySettings.shadowDistance = previousShadowDistance;
    }
 }
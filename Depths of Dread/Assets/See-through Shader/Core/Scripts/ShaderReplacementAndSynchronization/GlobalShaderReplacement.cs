using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

namespace ShaderCrew.SeeThroughShader
{
    [AddComponentMenu(Strings.COMPONENTMENU_GLOBAL_SHADER_REPLACEMENT)]
    public class GlobalShaderReplacement : ReplacementAndSyncBaseAbstract
    {
        public Dictionary<Material, Shader> cachedOriginalShaders = new Dictionary<Material, Shader>();
        public Shader replacementShader;
        private string seeThroughShaderName;

        public Material referenceMaterial;

        public bool replacementByReplacementShader;
        public LayerMask layerMasksWithReplacement = ~0;

        public Camera replacementCamera;
                

        Transform[] transformsWithSTS;
        void Update()
        {
            if (this.isActiveAndEnabled)
            {
                updateGlobalShaderVariables();
                updateShaderKeywords();
            }
        }

        protected override void Awake()
        {
            if (this.isActiveAndEnabled)
            {
                base.Awake();


                seeThroughShaderName = GeneralUtils.getUnityVersionAndRenderPipelineCorrectedShaderString().versionAndRPCorrectedShader;

                // always replacementShader  because option for shader selection got removed
                if (replacementShader == null)
                {
                    replacementShader = Shader.Find(seeThroughShaderName);
                }
                else
                {
                    seeThroughShaderName = replacementShader.name;
                }


                applyReplacementShader();
                updateGlobalShaderVariables();
                updateShaderKeywords();
            }
        }

        
        private void OnValidate()
        {
            if (this.isActiveAndEnabled)
            {
                
                if (replacementByReplacementShader)
                {
                    if (replacementCamera == null && GetComponent<Camera>() != null)
                    {
                        replacementCamera = GetComponent<Camera>();
                    }
                }

            }
        }
        

        private void applyReplacementShader()
        {
            
            Shader.SetGlobalFloat(SeeThroughShaderConstants.PROPERTY_IS_REPLACEMENT_SHADER, 1);

            if (replacementByReplacementShader && replacementShader != null)
            {
                if (replacementCamera != null)
                {
                    replacementCamera.SetReplacementShader(replacementShader, "RenderType");
                }
            }
            else
            {
                applyReplacementShaderToGameObjectsMaterials(listAllGameObjectsFromLayerMask(layerMasksWithReplacement));
            }
                        
        }

        private List<GameObject> listAllGameObjectsFromLayerMask(LayerMask layerMask)
        {
            GameObject[] gameObjectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            List<GameObject> gameObjectList = new List<GameObject>();
            for (int i = 0; i < gameObjectArray.Length; i++)
            {
                if (doesLayerMaskContainLayer(layerMask, gameObjectArray[i].layer))
                {
                    gameObjectList.Add(gameObjectArray[i]);
                }
            }

            //Debug.Log("gameObjectList.Count: " + gameObjectList.Count);
            if (gameObjectList.Count == 0)
            {
                return null;
            }
            return gameObjectList;
        }

        private bool doesLayerMaskContainLayer(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        private void applyReplacementShaderToGameObjectsMaterials(List<GameObject> gameObjects)
        {
            if (gameObjects != null && gameObjects.Count > 0)
            {
                Dictionary<string, Material> materialTracker = new Dictionary<string, Material>();
                List<Transform> tmpTransformsWithSTS = new List<Transform>();
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject != null && gameObject.GetComponent<Renderer>() != null)
                    {

                        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
                        for (int i = 0; i < renderers.Length; i++)
                        {
                            if (renderers[i] != null && renderers[i].materials.Length > 0)
                            {
                                Material[] updatedMaterials = renderers[i].materials;
                                for (int j = 0; j < renderers[i].materials.Length; j++)
                                {
                                    if (renderers[i].materials[j] != null)
                                    {
                                        if (!UnityToSTSShaderMapping.ContainsValue(renderers[i].materials[j].shader))
                                        {
                                            //Debug.Log(renderers[i].materials[j].shader.name);
                                            GeneralUtils.AddSTSInstancePrefix(renderers[i].materials[j]);
                                            string name = renderers[i].materials[j].name.Replace(" (Instance)", "");

                                            if (!name.Contains(" - Global Replaced by " + referenceMaterial.name))
                                            {
                                                name += " - Global Replaced by " + referenceMaterial.name;
                                            }
                                            renderers[i].materials[j].name = name;

                                            if (!materialTracker.ContainsKey(renderers[i].materials[j].name))
                                            {
                                                Material mat = new Material(renderers[i].materials[j]);
                                                mat.shader = UnityToSTSShaderMapping.TryGetValue(mat.shader.name, out Shader value) ? value : UnityToSTSShaderMapping[SeeThroughShaderConstants.STS_SHADER_DEFAULT_KEY];
                                                mat.SetFloat(SeeThroughShaderConstants.PROPERTY_IS_REPLACEMENT_SHADER, 1);
                                                mat.EnableKeyword(SeeThroughShaderConstants.KEYWORD_REPLACEMENT);

                                                materialTracker.Add(renderers[i].materials[j].name, mat);
                                            }
                                            updatedMaterials[j] = materialTracker[renderers[i].materials[j].name];

                                            if (!tmpTransformsWithSTS.Contains(gameObject.transform))
                                            {
                                                tmpTransformsWithSTS.Add(gameObject.transform);
                                            }
                                        }
                                    }
                                }
                                renderers[i].materials = updatedMaterials;
                            }
                        }
                    }

                }
                if (tmpTransformsWithSTS.Count > 0)
                {
                    transformsWithSTS = tmpTransformsWithSTS.ToArray();
                }
            }
        }
        

        private void updateGlobalShaderVariables()
        {
            if (referenceMaterial != null)
            {
                Texture disTex = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX);
                Shader.SetGlobalTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX_GLOBAL, disTex);

                Texture dissolveMask = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK);
                Shader.SetGlobalTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK_GLOBAL, dissolveMask);

                Texture obstructionCurve = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE);
                Shader.SetGlobalTexture(SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE_GLOBAL, obstructionCurve);

                Shader.SetGlobalColor(SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR_GLOBAL, referenceMaterial.GetColor(SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR));
                foreach (string propertyName in GeneralUtils.STS_SYNC_PROPERTIES_LIST)
                {
                    if (referenceMaterial.HasProperty(propertyName))
                    {
                        float temp = referenceMaterial.GetFloat(propertyName);
                        Shader.SetGlobalFloat(propertyName + SeeThroughShaderConstants.PROPERTY_GLOBAL, temp);
                    }
                }
            }
        }



        private void updateShaderKeywords()
        {
            if (referenceMaterial != null)
            {
                if (transformsWithSTS != null && transformsWithSTS.Length > 0 && UnityToSTSShaderMapping != null)
                {

                    GeneralUtils.updateSeeThroughShaderMaterialPropertiesAndKeywords(transformsWithSTS, UnityToSTSShaderMapping.Values.ToList(), referenceMaterial);

                }
            }
        }

        
    }
}
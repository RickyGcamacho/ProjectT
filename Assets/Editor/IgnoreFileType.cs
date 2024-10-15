using UnityEditor;
using UnityEngine;

public class IgnoreFileType : AssetPostprocessor
{
    static string[] ignoreExtensions = new string[] { ".ini", ".ini.meta" };

    // OnPreprocessAsset no debe ser estático, ya que 'assetPath' es un miembro de instancia.
    void OnPreprocessAsset()
    {
        string extension = System.IO.Path.GetExtension(assetPath).ToLower();
        if (System.Array.Exists(ignoreExtensions, ext => ext == extension))
        {
            Debug.Log("Ignoring file: " + assetPath);
            // Esto evitará que Unity cargue archivos con la extensión ignorada
            AssetDatabase.DeleteAsset(assetPath);
        }
    }
}


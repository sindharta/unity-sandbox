using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpriteAtlasCallbackManager : MonoBehaviour {

    private void OnEnable() {
        SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        
    }
    
    private void OnDisable() {
        SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
    }

//----------------------------------------------------------------------------------------------------------------------    
    private void OnAtlasRequested(string atlasName, System.Action<SpriteAtlas> callback) {
        string address = $"Assets/AddressableContent/Atlases/{atlasName}.spriteatlasv2";
        AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>(address);
        handle.Completed += op => {
            if (op.Status == AsyncOperationStatus.Succeeded && op.Result != null) {
                callback(op.Result);
            } else {
                Debug.LogError($"Failed to load SpriteAtlas '{atlasName}' from Addressables at '{address}'.");
                callback(null);
            }
        };
    }
}

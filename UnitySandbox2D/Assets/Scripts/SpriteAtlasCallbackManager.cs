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
    private void OnAtlasRequested(string atlasName, System.Action<SpriteAtlas> callback)
    {
        Debug.Log($"Atlas requested: {atlasName}");
        // Load SpriteAtlas from Addressables
        var handle = Addressables.LoadAssetAsync<SpriteAtlas>("Assets/AddressableContent/Atlases/UnityChanExtraActionsAtlas.spriteatlasv2");
        handle.Completed += (AsyncOperationHandle<SpriteAtlas> op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded && op.Result != null)
            {
                callback(op.Result);
            }
            else
            {
                Debug.LogError($"Failed to load SpriteAtlas '{atlasName}' from Addressables.");
                callback(null);
            }
        };
    }


}

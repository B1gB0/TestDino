using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AssetReference[] _sceneReferences;
    [SerializeField] private AssetReference _currentSceneRef;
    
    private AsyncOperationHandle<SceneInstance> _currentSceneHandle;
    private List<string> _sceneAddresses = new ();
    
    private void Start()
    {
        InitializeSceneAddresses();
    }
    
    private void InitializeSceneAddresses()
    {
        _sceneAddresses.Clear();
        foreach (var sceneRef in _sceneReferences)
        {
            _sceneAddresses.Add(sceneRef.AssetGUID);
        }
    }
    
    public async void LoadLevelScene(int index)
    {
        if (index >= 0 && index < _sceneAddresses.Count)
        {
            await LoadSceneByAddress(_sceneAddresses[index]);
        }
        else
        {
            Debug.LogError($"Scene index {index} is out of range!");
        }
    }
    
    public async void LoadNextScene()
    {
        int currentIndex = GetCurrentSceneIndex();
        if (currentIndex >= 0 && currentIndex < _sceneAddresses.Count - 1)
        {
            await LoadSceneByAddress(_sceneAddresses[currentIndex + 1]);
        }
    }
    
    public async void LoadPreviousScene()
    {
        int currentIndex = GetCurrentSceneIndex();
        if (currentIndex > 0)
        {
            await LoadSceneByAddress(_sceneAddresses[currentIndex - 1]);
        }
    }
    
    public async void ReloadScene()
    {
        if (_currentSceneRef != null && _currentSceneRef.RuntimeKeyIsValid())
        {
            await LoadSceneByAddress(_currentSceneRef.AssetGUID);
        }
    }
    
    private async Awaitable LoadSceneByAddress(string sceneAddress)
    {
        if (_currentSceneHandle.IsValid())
        {
            Addressables.Release(_currentSceneHandle);
        }
        
        _currentSceneHandle = Addressables.LoadSceneAsync(sceneAddress, activateOnLoad: true);
        
        await _currentSceneHandle.Task;
        
        UpdateCurrentSceneReference(sceneAddress);
    }
    
    private int GetCurrentSceneIndex()
    {
        if (_currentSceneRef != null && _currentSceneRef.RuntimeKeyIsValid())
        {
            string currentGuid = _currentSceneRef.AssetGUID;
            return _sceneAddresses.IndexOf(currentGuid);
        }
        return -1;
    }
    
    private void UpdateCurrentSceneReference(string sceneAddress)
    {
        foreach (var sceneRef in _sceneReferences)
        {
            if (sceneRef.AssetGUID == sceneAddress)
            {
                _currentSceneRef = sceneRef;
                return;
            }
        }
        Debug.LogWarning($"Scene address {sceneAddress} not found in references!");
    }
}
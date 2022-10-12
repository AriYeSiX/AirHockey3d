using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

public class FireBaseService : MonoBehaviour
{
    private const string FIREBASE_URL_KEY = "GetUrl";

    private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

    public event Action OnFireBaseInit;

    private void Start() =>
        FetchRemoteConfig();
    

    private void FetchRemoteConfig()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            _dependencyStatus = task.Result;

            if (_dependencyStatus == DependencyStatus.Available)
            {
                InitializeFireBase();
            }
            else
            {
                Debug.LogError("Could not resolve all FireBase dependencies " + _dependencyStatus);
            }
        });
    }

    private void InitializeFireBase()
    {
        Dictionary<string, object> defaults =
            new Dictionary<string, object>
            {
                {FIREBASE_URL_KEY, "DefaultUrl"}
            };


        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
        
        FetchDataAsync();
    }

    private Task FetchDataAsync() {
        Task fetchTask = 
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    
    private void FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled) {
            Debug.Log("Fetch canceled.");
        } else if (fetchTask.IsFaulted) {
            Debug.Log("Fetch encountered an error.");
        } else if (fetchTask.IsCompleted) {
            Debug.Log("Fetch completed successfully!");
        }

        var info = FirebaseRemoteConfig.DefaultInstance.Info;
        
        switch (info.LastFetchStatus) {
            case LastFetchStatus.Success:
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                break;
            case LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason) {
                    case FetchFailureReason.Error:
                        Debug.LogError("Fetch failed for unknown reason");
                        break;
                    case FetchFailureReason.Throttled:
                        Debug.LogError("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case LastFetchStatus.Pending:
                Debug.LogError("Latest Fetch call still pending.");
                break;
        }
        
        OnFireBaseInit?.Invoke();
    }
}
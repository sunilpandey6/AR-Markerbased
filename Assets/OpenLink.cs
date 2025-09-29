using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OpenLink : MonoBehaviour
{
    private static bool hasOpenedURL = false;

    [Tooltip("The URL to open when the marker is detected")]
    public string urlToOpen = "https://www.lunduniversity.lu.se/lubas/i-uoh-lu-TAVAR";

    private ARTrackedImageManager arTrackedImageManager;

    void Awake()
    {
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        if (arTrackedImageManager != null)
        {
            arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    void OnDisable()
    {
        if (arTrackedImageManager != null)
        {
            arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var addedImage in args.added)
        {
            if (!hasOpenedURL && addedImage.trackingState == TrackingState.Tracking)
            {
                hasOpenedURL = true;
                Application.OpenURL(urlToOpen);
            }
        }

        // Optional: Handle updated images
        foreach (var updatedImage in args.updated)
        {
            if (!hasOpenedURL && updatedImage.trackingState == TrackingState.Tracking)
            {
                hasOpenedURL = true;
                Application.OpenURL(urlToOpen);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OpenLink : MonoBehaviour
{
    private static bool hasOpenedURL = false;  // Static flag to make sure URL is opened only once

    [Tooltip("The URL to open when the marker is detected")]
    public string urlToOpen = "https://www.lunduniversity.lu.se/lubas/i-uoh-lu-TAVAR";

    private ARTrackedImageManager arTrackedImageManager;

    void Awake()
    {
        // Get the ARTrackedImageManager component attached to the XR Origin (or AR Session Origin)
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Iterate over all added or updated images
        foreach (var addedImage in args.added)
        {
            // Check if the image is being tracked (not null and in the "Tracking" state)
            if (!hasOpenedURL && addedImage.trackingState == TrackingState.Tracking)
            {
                hasOpenedURL = true; // Mark that the URL has been opened
                Application.OpenURL(urlToOpen);  // Open the URL
            }
        }

    }
}

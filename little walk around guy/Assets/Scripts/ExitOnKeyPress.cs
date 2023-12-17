using UnityEngine;
using UnityEngine.Video;

public class ExitOnKeyPress : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private float timer = 0f;
    private bool canExit = false;
    public float exitDelay = 30f; // Time in seconds after which the user can press a key to exit

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found on the GameObject.");
        }
    }

    void Update()
    {
        // Start the timer once the video starts playing
        if (videoPlayer.isPlaying)
        {
            timer += Time.deltaTime;
            if (timer >= exitDelay)
            {
                canExit = true;
            }
        }

        // Check if the user can exit and if any key is pressed
        if (canExit && Input.anyKeyDown)
        {
            // Exit the game
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

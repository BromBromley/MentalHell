using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscordManager : MonoBehaviour
{
    public static DiscordManager Instance { get; private set; }

    private Discord.Discord discord;

    // Mapping of Scene-IDs or Namens for Activitys
    private Dictionary<string, Discord.Activity> sceneActivities;

    void Start()
    {
        // Singleton-Check
        if (Instance == null)
        {
            // Set Singleton-Instance of Object
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize Discord
            discord = new Discord.Discord(1270427024535847083, (ulong)Discord.CreateFlags.NoRequireDiscord);

            // Definition of Actitvitys
            sceneActivities = new Dictionary<string, Discord.Activity>
            {
                { "MainMenu", new Discord.Activity
                    {
                        Details = "Chilling in Main Menu",
                        Assets =
                        {
                            LargeImage = "mental_hell_icon",
                        },
                        Timestamps =
                        {
                            Start = GetCurrentUnixTimestamp()
                        }
                    }
                },
                { "Outside", new Discord.Activity
                    {
                        Details = "Lost Outside",
                        Assets =
                        {
                            LargeImage = "mental_hell_icon",
                        },
                        Timestamps =
                        {
                            Start = GetCurrentUnixTimestamp()
                        }
                    }
                },
                { "Main", new Discord.Activity
                    {
                        Details = "Trapped in the asylum",
                        Assets =
                        {
                            LargeImage = "mental_hell_icon",
                        },
                        Timestamps =
                        {
                            Start = GetCurrentUnixTimestamp()
                        }
                    }
                }
            };

            // Register Sceneswitch-Listener
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Change Activity after Start
            ChangeActivity(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        if (discord != null)
        {
            discord.Dispose();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeActivity(scene.name);
    }

    private void ChangeActivity(string sceneName)
    {
        if (sceneActivities.TryGetValue(sceneName, out var activity))
        {
            var activityManager = discord.GetActivityManager();
            activity.Timestamps.Start = GetCurrentUnixTimestamp();
            activityManager.UpdateActivity(activity, (res) =>
            {
                Debug.Log("Activity updated for scene: " + sceneName);
            });
        }
        else
        {
            Debug.LogWarning("No activity defined for scene: " + sceneName);
        }
    }

    private long GetCurrentUnixTimestamp()
    {
        return new System.DateTimeOffset(System.DateTime.UtcNow).ToUnixTimeSeconds();
    }

    void Update()
    {
        if (discord != null)
        {
            discord.RunCallbacks();
        }
    }
}

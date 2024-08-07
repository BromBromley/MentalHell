using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscordManager : MonoBehaviour
{
    // Statische Instanz der Klasse
    public static DiscordManager Instance { get; private set; }

    private Discord.Discord discord;

    // Mapping von Szenen-IDs oder Namen zu Aktivitätsdetails
    private Dictionary<string, Discord.Activity> sceneActivities;

    // Start ist aufgerufen, bevor das erste Frame aktualisiert wird
    void Start()
    {
        // Singleton-Check
        if (Instance == null)
        {
            // Setze die Singleton-Instanz auf dieses Objekt
            Instance = this;

            // Stelle sicher, dass dieses Objekt beim Szenenwechsel nicht zerstoert wird
            DontDestroyOnLoad(gameObject);

            // Initialisiere Discord
            discord = new Discord.Discord(1270427024535847083, (ulong)Discord.CreateFlags.NoRequireDiscord);

            // Definiere Aktivitäten für verschiedene Szenen
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

            // Registriere den Szenenwechsel-Listener
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Ändere die Aktivität, wenn das Skript startet
            ChangeActivity(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Wenn bereits eine Instanz existiert, dann loesche dieses Objekt
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
    }

    private long GetCurrentUnixTimestamp()
    {
        return new System.DateTimeOffset(System.DateTime.UtcNow).ToUnixTimeSeconds();
    }

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        if (discord != null)
        {
            discord.RunCallbacks();
        }
    }
}

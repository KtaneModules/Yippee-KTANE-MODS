    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using KModkit;
    using Rnd = UnityEngine.Random;
    using UnityEngine.UI;

    public class Yippe : MonoBehaviour {
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public Material[] materials; //normal, birthday, normal solve, birthday solve
    public GameObject surface;
    public AudioClip solveSound; 
    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;
    private int materialIndex = -1;
    void Awake () 
    {
        ModuleId = ModuleIdCounter++;
        
    }

    void Start()
    {
        surface.GetComponent<KMSelectable>().OnInteract += delegate () { Debug.Log("A"); StartCoroutine(Solve()); Debug.Log("B"); return false; };
        DateTime currentDate = DateTime.Now;
        if (currentDate.Month == 1 && currentDate.Day == 23)
        {
            Logging("IT'S MY BIRTHDAAAAAAY!!!!!!!!!!!!!!");
            materialIndex = 2;
        }
        else
            materialIndex = 0;
        surface.GetComponent<Renderer>().material = materials[materialIndex];
    }
    IEnumerator Solve()
    {
        surface.GetComponent<Renderer>().material = materials[materialIndex + 1];
        Logging("YIPEE");
        Debug.Log("1");
        Audio.PlaySoundAtTransform(solveSound.name, transform);
        yield return new WaitForSeconds(solveSound.length);
        Debug.Log("2");
        GetComponent<KMBombModule>().HandlePass();
        ModuleSolved = true;
    }
    private void Logging(string s)
    {
        Debug.Log($"[Yipee #{ModuleId}] {s}");
    }
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand (string Command) {
        surface.GetComponent<KMSelectable>().OnInteract();
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve () {
        yield return null;
    }
    }

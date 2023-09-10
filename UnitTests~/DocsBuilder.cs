﻿using System;
using System.Diagnostics;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;

public static class DocsBuilder
{
    public static void BuildDocs()
    {
        ProjectGeneration projectGeneration = new ProjectGeneration();
        AssetDatabase.Refresh();
        projectGeneration.GenerateAndWriteSolutionAndProjects();
        
        RunProcess("dotnet tool update -g docfx");
        RunProcess("ls -l");
    }

    private static void RunProcess(string command)
    {
        System.Console.Error.WriteLine("=== Running command: " + command + " ===");
        var process = Process.Start(command);
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception("Process failed: " + command + " - with error code " + process.ExitCode);
        }
    }
}

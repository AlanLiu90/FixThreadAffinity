using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public static class ThreadHelper
{
#if !UNITY_EDITOR && UNITY_ANDROID
    [DllImport("ThreadAffinity")]
    private static extern int NativeSetThreadAffinity(int tid, int mask);
#endif

    public static void SetAffinity(string name, int mask)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        string pattern = "Name:\t" + name;

        string[] directories = Directory.GetDirectories("/proc/self/task/");
        foreach (string dir in directories)
        {
            int tid;
            if (!int.TryParse(Path.GetFileName(dir), out tid))
                continue;

            string statusFile = Path.Combine(dir, "status");

            using (var sr = new StreamReader(statusFile))
            {
                string line = sr.ReadLine();

                if (line == pattern)
                {
                    int result = NativeSetThreadAffinity(tid, mask);
                    if (result != 0)
                        Debug.LogErrorFormat("Failed to set thread affinity, name={0}, mask={1}", name, mask);
                }
            }
        }
#endif
    }
}

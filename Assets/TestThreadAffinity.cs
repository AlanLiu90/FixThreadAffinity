using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestThreadAffinity : MonoBehaviour 
{
    public Text DeviceModelText;
    public Text OSText;
    public Text ProcessorText;
    public Button TestButton;

    private void Start()
    {
        DeviceModelText.text = "DeviceModel: " + SystemInfo.deviceModel;
        OSText.text = "OS: " + SystemInfo.operatingSystem;
        ProcessorText.text = "ProcessorType: " + GetProcessorType();

        TestButton.interactable = NeedFix();
    }

    public void Test()
    {
        ThreadHelper.SetAffinity("Worker Thread", -1);
        ThreadHelper.SetAffinity("BackgroundWorke", -1);
    }

    private static bool NeedFix()
    {
        return Application.platform == RuntimePlatform.Android &&
            SystemInfo.deviceModel.Contains("HUAWEI") &&
            SystemInfo.operatingSystem.Contains("/102.0.0.") &&
            GetProcessorType().Contains("/kirin810/");
    }

    private static string GetProcessorType()
    {
        string processorType = string.Empty;

#if !UNITY_EDITOR && UNITY_ANDROID
        try
        {
            using (var sr = new StreamReader("/proc/cpuinfo"))
            {
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("Hardware") && line.Contains(":"))
                    {
                        processorType = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        break;
                    }
                }
            }

            string hardware;
            string board;
            using (var buildClass = new AndroidJavaClass("android.os.Build"))
            {
                hardware = buildClass.GetStatic<string>("HARDWARE");
                board = buildClass.GetStatic<string>("BOARD");
            }

            processorType = string.Format("{0}/{1}/{2}", processorType, hardware, board);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if (string.IsNullOrEmpty(processorType))
                processorType = SystemInfo.processorType;
        }
#endif

        return processorType;
    }
}

using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The class responsible for setting up an overlay window with transparency.
/// </summary>
public class OverlayWindow : MonoBehaviour
{


    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    // The handle to the topmost window.
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    const uint LWA_COLORKEY = 0x00000001;

    private bool interactable = true;

    // The active window
    IntPtr hwnd;


    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        //MessageBox(IntPtr.Zero, "Hello World!", "Hello Dialog", 0);

#if !UNITY_EDITOR
        SetOverlay();
#endif

    }


    /// <summary>
    /// Sets up the overlay window with transparency.
    /// </summary>
    private void SetOverlay()
    {
        Debug.Log("Setting overlay");

        hwnd = GetActiveWindow();

        MARGINS margins = new() { cxLeftWidth = -1 };

        DwmExtendFrameIntoClientArea(hwnd, ref margins);

        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);

        if (interactable)
        {
            EnableClickthrough();
        } else
        {
            DisableClickthrough();
        }

        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

    }

    private void Update() 
    {
        //SetClickthrough(Physics2D.Raycast(GetMouseWorldPosition(), Vector2.zero, 0f, LayerMask.GetMask("UI")));
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }

    private void EnableClickthrough()
    {
        SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY);
    }

    private void DisableClickthrough()
    {
        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
    }

    private void SetClickthrough(bool clickthrough)
    {
        if (clickthrough)
        {
            Debug.Log("Clickthrough");
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

    public void SwitchInteractable()
    {
        interactable = !interactable;
        if (interactable)
        {
            EnableClickthrough();
        }
        else
        {
            DisableClickthrough();
        }
    }
}

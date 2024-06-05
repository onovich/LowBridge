using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LowBridge {

    public static class LowBridgeAPI {

        public static void MouseMove(int x, int y) {
#if UNITY_STANDALONE_WIN
            LowAPI_Win32_Mouse.mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE), (uint)x, (uint)y, 0, UIntPtr.Zero);
#endif
        }

        public static string GetClipboardText() {
#if UNITY_STANDALONE_WIN
            if (!LowAPI_Win32_Clipboard.OpenClipboard(IntPtr.Zero)) return null;
            IntPtr handle = LowAPI_Win32_Clipboard.GetClipboardData(13); // CF_UNICODETEXT
            if (handle == IntPtr.Zero) return null;
            IntPtr pointer = LowAPI_Win32_Global.GlobalLock(handle);
            if (pointer == IntPtr.Zero) return null;
            string text = Marshal.PtrToStringUni(pointer);
            LowAPI_Win32_Global.GlobalUnlock(handle);
            LowAPI_Win32_Clipboard.CloseClipboard();
            return text;
#elif UNITY_STANDALONE_OSX
            return null;
#endif
        }

        public static bool SetClipboardText(string text) {
#if UNITY_STANDALONE_WIN
            if (!LowAPI_Win32_Clipboard.OpenClipboard(IntPtr.Zero)) return false;
            LowAPI_Win32_Clipboard.EmptyClipboard();
            IntPtr hGlobal = Marshal.StringToHGlobalUni(text);
            bool success = LowAPI_Win32_Clipboard.SetClipboardData(13, hGlobal); // CF_UNICODETEXT
            if (!success) {
                Marshal.FreeHGlobal(hGlobal);
                LowAPI_Win32_Clipboard.CloseClipboard();
                return false;
            }
            LowAPI_Win32_Clipboard.CloseClipboard();
            return true;
#elif UNITY_STANDALONE_OSX
            return false;
#endif
        }

        public static bool ToggleCapsLock() {
#if UNITY_STANDALONE_WIN
            const int VK_CAPSLOCK = 0x14;
            bool capsLock = LowAPI_Win32_Keyboard.GetKeyState(VK_CAPSLOCK) == 1;
            if (capsLock) {
                LowAPI_Win32_Keyboard.keybd_event((byte)VK_CAPSLOCK, 0x45, 0, 0);
                LowAPI_Win32_Keyboard.keybd_event((byte)VK_CAPSLOCK, 0x45, 2, 0);
            } else {
                LowAPI_Win32_Keyboard.keybd_event((byte)VK_CAPSLOCK, 0x45, 0, 0);
                LowAPI_Win32_Keyboard.keybd_event((byte)VK_CAPSLOCK, 0x45, 2, 0);
            }
            return !capsLock;
#elif UNITY_STANDALONE_OSX
            return false;
#endif
        }

        public static string GetUsername() {
#if UNITY_STANDALONE_WIN
            return Environment.UserName;
#elif UNITY_STANDALONE_OSX
            return null;
#endif
        }

        public static string GetForegroundWindowTitle() {
#if UNITY_STANDALONE_WIN
            IntPtr hWnd = LowAPI_Win32_Window.GetForegroundWindow();
            StringBuilder sb = new StringBuilder(256);
            if (LowAPI_Win32_Window.GetWindowText(hWnd, sb, sb.Capacity) > 0) {
                return sb.ToString();
            }
            return null;
#elif UNITY_STANDALONE_OSX
            return null;
#endif
        }

    }

}
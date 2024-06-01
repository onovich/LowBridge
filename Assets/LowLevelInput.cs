using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Win32LowAPI {

    public static class LowLevelInput {

        [Flags]
        enum MouseEventFlags {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EmptyClipboard();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UIntPtr GlobalSize(IntPtr hMem);
#endif

        public static void MouseMove(int x, int y) {
#if UNITY_STANDALONE_WIN
            mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE), (uint)x, (uint)y, 0, UIntPtr.Zero);
#endif
        }

#if UNITY_STANDALONE_WIN
        public static string GetClipboardText() {
            if (!OpenClipboard(IntPtr.Zero)) return null;
            IntPtr handle = GetClipboardData(13); // CF_UNICODETEXT
            if (handle == IntPtr.Zero) return null;
            IntPtr pointer = GlobalLock(handle);
            if (pointer == IntPtr.Zero) return null;
            string text = Marshal.PtrToStringUni(pointer);
            GlobalUnlock(handle);
            CloseClipboard();
            return text;
        }

        public static bool SetClipboardText(string text) {
            if (!OpenClipboard(IntPtr.Zero)) return false;
            EmptyClipboard();
            IntPtr hGlobal = Marshal.StringToHGlobalUni(text);
            bool success = SetClipboardData(13, hGlobal); // CF_UNICODETEXT
            if (!success) {
                Marshal.FreeHGlobal(hGlobal);
                CloseClipboard();
                return false;
            }
            CloseClipboard();
            return true;
        }

        public static bool ToggleCapsLock() {
            const int VK_CAPSLOCK = 0x14;
            bool capsLock = GetKeyState(VK_CAPSLOCK) == 1;
            if (capsLock) {
                keybd_event((byte)VK_CAPSLOCK, 0x45, 0, 0);
                keybd_event((byte)VK_CAPSLOCK, 0x45, 2, 0);
            } else {
                keybd_event((byte)VK_CAPSLOCK, 0x45, 0, 0);
                keybd_event((byte)VK_CAPSLOCK, 0x45, 2, 0);
            }
            return !capsLock;
        }

        public static string GetUsername() {
            return Environment.UserName;
        }

        public static string GetForegroundWindowTitle() {
            IntPtr hWnd = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(256);
            if (GetWindowText(hWnd, sb, sb.Capacity) > 0) {
                return sb.ToString();
            }
            return null;
        }
#endif

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
    }
}
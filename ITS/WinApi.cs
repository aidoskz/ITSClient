using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WinApi
{

    class User32
    {
        public const Int32 WM_COMMAND = 273;
        public const Int32 MF_ENABLED = 0;
        public const Int32 MF_GRAYED = 1;
        public const Int32 LVM_FIRST = 4096;
        public const Int32 LVM_DELETEITEM = (LVM_FIRST + 8);
        public const Int32 LVM_SORTITEMS = (LVM_FIRST + 48);
        [DllImport("user32", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32", EntryPoint = "FindWindowExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 FindWindowEx(Int32 hWnd1, Int32 hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32", EntryPoint = "EnableWindow", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool EnableWindow(Int32 hwnd, Int32 fEnable);
        [DllImport("user32", EntryPoint = "GetMenu", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 GetMenu(Int32 hwnd);
        [DllImport("user32", EntryPoint = "GetSubMenu", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 GetSubMenu(Int32 hMenu, Int32 nPos);
        [DllImport("user32", EntryPoint = "GetMenuState", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 GetMenuState(Int32 hMenu, Int32 wID, Int32 wFlags);
        [DllImport("user32", EntryPoint = "GetMenuItemID", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 GetMenuItemID(Int32 hMenu, Int32 nPos);
        [DllImport("user32", EntryPoint = "EnableMenuItem", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 EnableMenuItem(Int32 hMenu, Int32 wIDEnableItem, Int32 wEnable);
        /*[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);*/

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, String lParam);
        //Also can add 'ref' or 'out' ahead 'String lParam'
        // -- Do not use 'out String', use '[Out] StringBuilder' instead and initialize the string builder
        // with proper length first. Dunno why but that is the only thing that worked for me.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 GetDesktopWindow();
        [DllImport("user32", EntryPoint = "LockWindowUpdate", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern Int32 LockWindowUpdate(Int32 hwndLock);
    }
}
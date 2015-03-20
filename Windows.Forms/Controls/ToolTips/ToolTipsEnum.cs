using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Forms.Controls.WinAPI;

namespace Windows.Forms.Controls.ToolTips
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INITCOMMONCONTROLSEX
    {
        internal INITCOMMONCONTROLSEX(int flags)
        {
            this.dwSize = Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX));
            this.dwICC = flags;
        }

        internal int dwSize;
        internal int dwICC;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMCUSTOMDRAW
    {
        internal NMHDR hdr;
        internal uint dwDrawStage;
        internal IntPtr hdc;
        internal RECT rc;
        internal IntPtr dwItemSpec;
        internal uint uItemState;
        internal IntPtr lItemlParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        internal NMHDR(int flag)
        {
            this.hwndFrom = IntPtr.Zero;
            this.idFrom = 0;
            this.code = 0;
        }

        internal IntPtr hwndFrom;
        internal int idFrom;
        internal int code;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTCUSTOMDRAW
    {
        internal NMCUSTOMDRAW nmcd;
        internal uint uDrawFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTDISPINFO
    {
        internal NMTTDISPINFO(int flags)
        {
            this.hdr = new NMHDR(0);
            this.lpszText = IntPtr.Zero;
            this.szText = IntPtr.Zero;
            this.hinst = IntPtr.Zero;
            this.uFlags = 0;
            this.lParam = IntPtr.Zero;
        }

        internal NMHDR hdr;
        internal IntPtr lpszText;
        internal IntPtr szText;
        internal IntPtr hinst;
        internal int uFlags;
        internal IntPtr lParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public RECT rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TRACKMOUSEEVENT
    {
        internal uint cbSize;
        internal TRACKMOUSEEVENT_FLAGS dwFlags;
        internal IntPtr hwndTrack;
        internal uint dwHoverTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TOOLINFO
    {
        internal TOOLINFO(int flags)
        {
            this.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            this.uFlags = flags;
            this.hwnd = IntPtr.Zero;
            this.uId = IntPtr.Zero;
            this.rect = new RECT(0, 0, 0, 0);
            this.hinst = IntPtr.Zero;
            this.lpszText = IntPtr.Zero;
            this.lParam = IntPtr.Zero;
        }

        public int cbSize;
        public int uFlags;
        public IntPtr hwnd;
        public IntPtr uId;
        public RECT rect;
        public IntPtr hinst;
        public IntPtr lpszText;
        public IntPtr lParam;
    }

    public enum TRACKMOUSEEVENT_FLAGS : uint
    {
        TME_HOVER = 1,
        TME_LEAVE = 2,
        TME_QUERY = 0x40000000,
        TME_CANCEL = 0x80000000
    }
}

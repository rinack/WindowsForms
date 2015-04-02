#region Copyright (c) Jeff Anderson
/*
Copyright (c) 2009 Jeff Anderson of Braid Art Labs (www.groboto.com)

This code is released under the Code Project
Open License 1.02. See cpol.html for
license details. NOTE: cpol.html MUST be included
with any source code distribution of this code.                                                              
*/
#endregion

#region Version 1.1.3

// Version 1.0 Initial CodeProject Release
// Version 1.1 Initial Commercial Release
//  1. Added AlphaFormTransformer.Fade() for fade in/out functionality
//     To use, override OnShown() and OnClosing() and add call to Fade().
//     If fading in, main form Opacity property should be set to 0
//     in advance AND for fading in you must pass 0 as the
//     argument to TransformForm() to set the initial layered form opacity.
//     See the examples "TestFormGrobotoTV" or "ControlsOnTopOfSkin" projects
//     as they both use Fade().
//  2. Reworked OnPaint override in AlphaFormTransformer (see method summary
//     & PowerToys issue)
//  3. Added CanDrag property. If true user can click and drag on
//     background of AFT to drag the composite form (see property summary
//     for more details)
//  4. Added example project "ControlsOnTopOfSkin" showing how to create
//     a form where the controls appear on top of the background image
//     (uses the new CanDrag property). The basic idea is to set the 
//     AFT's background image to one where the alpha channel cuts out
//     the control region, and then set the main form's background to the
//     same image without the cutout region.
//  5. Fixed issues with main form scaling set to DPI and running under
//     different DPI's. Added scaling of main form background image (which
//     becomes the AFT's background at runtime).
//     We also adjust the region fillBorder for the AFM markers if DPI scaling
//     is needed. This is necessary as image rescaling adds or removes to the 
//     semitransparent border width of the alpha channel.
//  6. Reorganized solution layout with all sample projects in "Test Projects" 
//     folder.
// Version 1.1.1 - CodeProject License Release
//  1. Changed primary namespace and refactored. 
// Version 1.1.2 (beta)
//  1. Fixed SetWindowLong PInvoke signature to be 64 bit compatible, but
//     we have not been able to test this component on X64. 
//  2. Impulse class made public as it's useful for dampened motion if the user
//     wants to animate (either the location or opacity) an alpha transformed window.
//     For example see the SlidingPanes project. 
// Version 1.1.3 (beta)
//  1. Implementing a Visible change event on the main form and keeping the layered
//     window Visible propery in sync was a bad idea. In this component we need to manipulate
//     Visible separately for the fade routine so we removed it. 
//  2. Added SetAlphaAndParentFormVisible and SetAlphaAndParentFormOpacity utility routines.
//  3. Re-worked the SlidingPanes example project to better illustrate animating the drawer and bug fix.
#endregion

using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Windows.Forms.Controls.WinAPI;

namespace Windows.Forms.Controls.AlphaForm
{
    public partial class AlphaForm : Form
    {
        public AlphaForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
        }

        private object theobj;

        #region Overrides
        /// <summary>
        /// This window can be the active window when you click
        /// to drag it, and it can receive a close event from
        /// the system (e.g., user clicks Alt+F4), therefore we 
        /// instruct the owner to close and cancel the close for
        /// this window.
        /// </summary>
        /// 
        protected void tstlock()
        {
            lock (theobj)
            {
                theobj = null;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Owner != null)
            {
                e.Cancel = true;
                base.OnClosing(e);
                Owner.Close();
            }
            else
                base.OnClosing(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            InitializeStyles();
            base.OnHandleCreated(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }
        #endregion

        #region Methods
        public void SetBits(Bitmap bitmap, byte opacity)
        {
            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The bitmap must be 32 bits per pixel with an alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);
            Win32.Point topLoc = new Win32.Point(Left, Top);
            Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
            Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
            Win32.Point srcLoc = new Win32.Point(0, 0);
            blendFunc.BlendOp = Win32.AC_SRC_OVER;
            blendFunc.SourceConstantAlpha = opacity;
            blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
            blendFunc.BlendFlags = 0;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);
                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }

        private void InitializeStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        #endregion
    }

    /// <summary>
    /// Defines a line segment. Used by AlphaFormTransformer.UpdateRectListFromAlpha()
    /// </summary>
    internal struct LineSeg
    {
        public int x1;
        public int x2;
        public int y;
        public int dy;

        public LineSeg(int x1, int x2, int y, int dy)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y = y;
            this.dy = dy;
        }
    }

    /// <summary>
    /// A simple pulse function with exponential dampening
    /// input [0..1], output [0..1]
    /// Function is steep at beginning and flat at end for Dampening > 2
    /// and lesser values not really useful
    /// The greater the dampening the steeper the initial ramp
    /// If you want it flat at the beginning then call 1.0 - Evaluate(1.0-t)
    /// </summary>
    public class Impulse
    {

        #region Class Variables
        double m_pScale;
        double m_pNorm = 1.0;
        #endregion

        #region Contructors
        public Impulse(double s)
        {
            m_pNorm = 1.0;
            m_pScale = s;
        }
        #endregion

        #region Properties
        public double Dampening
        {
            get
            {
                return m_pScale;
            }
            set
            {
                m_pScale = value;
            }
        }
        #endregion

        #region Methods
        void UpdateScale()
        {
            m_pNorm = 1.0 / EvalInternal(1.0);
        }

        double EvalInternal(double t)
        {
            double val;

            t = t * m_pScale;
            if (t < 1.0)
            {
                val = t - (1 - Math.Exp(-t));
            }
            else
            {
                double start = Math.Exp(-1.0);
                t -= 1.0;
                double expx = 1 - Math.Exp(-t);
                val = start + (expx * (1.0 - start));
            }

            return val * m_pNorm;
        }

        public void Reset()
        {
            m_pNorm = 1.0;
        }

        public double Evaluate(double t)
        {
            if (t >= 1.0)
                return 1.0;
            if (t <= 0.0)
                return 0.0;
            if (m_pNorm == 1.0)
                UpdateScale();
            return EvalInternal(t);
        }
        #endregion
    }
}

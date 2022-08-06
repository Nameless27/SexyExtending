using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexyExtending.Win32
{
    public enum WindowCompositionAttribute
    {
        Undefined = 0,
        NCRenderingEnabled = 1,
        NCRenderingPolicy = 2,
        TransitionsForceDisabled = 3,
        AllowNCPaint = 4,
        CaptionButtonBounds = 5,
        NonClientRTLLayout = 6,
        ForceIconicRepresentation = 7,
        ExtendedFrameBounds = 8,
        HasIconicBitmap = 9,
        ThemeAttributes = 10,
        NCRenderingExiled = 11,
        NCAdornmentInfo = 12,
        ExcludedFromLivePreview = 13,
        VideoOverlayActive = 14,
        ForceActiveWindowAppearance = 15,
        DisallowPeek = 16,
        Cloak = 17,
        Cloaked = 18,
        AccentPolicy = 19,
        FreezeRepresentation = 20,
        EverUncloaked = 21,
        VisualOwner = 22,
        Last = 23
    }
}

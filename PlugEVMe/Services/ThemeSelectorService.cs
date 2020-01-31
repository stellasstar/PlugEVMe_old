using System;
using System.Collections.Generic;
using System.Text;

namespace PlugEVMe.Dependencies
{
    public interface ThemeSelectorService
    {
        void OnThemeChanged(ThemeManager.Themes theme);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSProjectManager.Properties;

namespace VSProjectManager.Model
{
    static class SettingsManager
    {
        public static SettingMG GetSettings() => new SettingMG(  //думаю нужно постараться переделать
                Settings.Default.directoryPath,
                Settings.Default.scan,
                Settings.Default.sort,
                Settings.Default.topMost
                );

        public static void SetSettings(SettingMG setting)
        {
            Settings.Default.directoryPath = setting.DirectoryPath;
            Settings.Default.scan = setting.Scan;
            Settings.Default.sort = setting.Sort;
            Settings.Default.topMost = setting.TopMost;
            Settings.Default.Save();
        }

        public static bool IsExist()
        {
            if (Settings.Default.directoryPath == String.Empty) return false;
            return true;
        }
    }
}

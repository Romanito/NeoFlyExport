using System.Collections.Specialized;
using System.Configuration;

namespace NeoFlyExport
{
    public class UserSettings : ApplicationSettingsBase
    {
        // External viewers
        // List of command lines to open the exported GPX file with an external program
        [UserScopedSetting()]
        public StringCollection ExternalViewers
        {
            get
            {
                if (this["ExternalViewers"] == null)
                    this["ExternalViewers"] = new StringCollection();
                return (StringCollection)this["ExternalViewers"];
            }
            set => this["ExternalViewers"] = value;
        }

        // Default external viewer (first one in the list)
        public string DefaultExternalViewer => ExternalViewers.Count > 0 ? ExternalViewers[0] : null;
    }
}

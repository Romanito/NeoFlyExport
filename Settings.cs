using System.Configuration;

namespace NeoFlyExport.Properties
{
    internal partial class Settings
    {
        public Settings()
        {
            if (ExternalViewers == null)
                ExternalViewers = new System.Collections.Generic.List<string>();
        }

        [UserScopedSetting()]
        public System.Collections.Generic.List<string> ExternalViewers
        {
            get => (System.Collections.Generic.List<string>)this["ExternalViewers"];
            set => this["ExternalViewers"] = value;
        }

        // Default external viewer (first one in the list)
        public string DefaultExternalViewer => ExternalViewers.Count > 0 ? ExternalViewers[0] : null;
    }
}

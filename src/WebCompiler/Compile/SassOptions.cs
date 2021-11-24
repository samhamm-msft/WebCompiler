using System;
using Newtonsoft.Json;

namespace WebCompiler
{
    /// <summary>
    /// Give all options for the Sass compiler
    /// </summary>
    public class SassOptions : BaseOptions<SassOptions>
    {
        private readonly char[] separators = new char[] { ';', ',' };

        /// <summary> Creates a new instance of the class.</summary>
        public SassOptions()
        { }

        /// <summary>
        /// Loads the settings based on the config
        /// </summary>
        protected override void LoadSettings(Config config)
        {
            base.LoadSettings(config);

            string autoPrefix = GetValue(config, "autoPrefix");
            if (autoPrefix != null)
                AutoPrefix = autoPrefix;

            string loadPaths = GetValue(config, "loadPaths");
            if (loadPaths != null)
                LoadPaths = loadPaths.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            string style = GetValue(config, "style");
            if (style != null && Enum.TryParse(style.ToString(), true, out SassStyle styleValue))
                Style = styleValue;

            if (int.TryParse(GetValue(config, "precision"), out int precision))
                Precision = precision;

            if (bool.TryParse(GetValue(config, "quietDeps"), out bool quietDeps))
                QuietDeps = quietDeps;
        }

        /// <summary>
        /// The file name should match the compiler name
        /// </summary>
        protected override string CompilerFileName
        {
            get { return "sass"; }
        }

        /// <summary>
        /// Autoprefixer will use the data based on current browser popularity and
        /// property support to apply prefixes for you.
        /// </summary>
        [JsonProperty("autoPrefix")]
        public string AutoPrefix { get; set; } = string.Empty;

        /// <summary>
        /// Paths to look for imported files
        /// </summary>
        [JsonProperty("loadPaths")]
        public string[] LoadPaths { get; set; } = new string[0];

        /// <summary>
        /// Type of output style
        /// </summary>
        [JsonProperty("style")]
        public SassStyle Style { get; set; } = SassStyle.Expanded;

        /// <summary>
        /// Precision
        /// </summary>
        [JsonProperty("precision")]
        public int Precision { get; set; } = 10;

        /// <summary>
        /// Quiet
        /// </summary>
        [JsonProperty("quiet")]
        public bool Quiet { get; set; } = true;

        /// <summary>
        /// QuietDeps
        /// </summary>
        [JsonProperty("quietDeps")]
        public bool QuietDeps { get; set; } = true;
    }

    public enum SassStyle
    {
        Expanded,
        Compressed
    }
}

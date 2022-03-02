using System;
using Newtonsoft.Json;

namespace WebCompiler
{
    /// <summary>
    /// Give all options for the Sass compiler
    /// </summary>
    public class SassOptions : BaseOptions<SassOptions>
    {
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
                LoadPaths = loadPaths;

            string style = GetValue(config, "style");
            if (style != null && Enum.TryParse(style, true, out SassStyle _))
                Style = style;

            if (bool.TryParse(GetValue(config, "sourceMap"), out bool sourceMap))
                SourceMap = sourceMap;

            string sourceMapUrls = GetValue(config, "sourceMapUrls");
            if (sourceMapUrls != null && Enum.TryParse(sourceMapUrls, true, out SassSourceMapUrls _))
                SourceMapUrls = sourceMapUrls;

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
        public string LoadPaths { get; set; } = string.Empty;

        /// <summary>
        /// Type of output style
        /// </summary>
        [JsonProperty("style")]
        public string Style { get; set; } = SassStyle.Expanded.ToString().ToLowerInvariant();

        /// <summary>
        /// SourceMapUrls
        /// </summary>
        [JsonProperty("sourceMapUrls")]
        public string SourceMapUrls { get; set; } = SassSourceMapUrls.Relative.ToString().ToLowerInvariant();

        /// <summary>
        /// This option allows you to re-write URL's in imported files so that the URL is always
        /// relative to the base imported file.
        /// </summary>
        [JsonProperty("relativeUrls")]
        public bool RelativeUrls { get; set; } = true;

        /// <summary>
        /// Quiet
        /// </summary>
        [JsonIgnore]
        public bool Quiet { get; set; } = true;

        /// <summary>
        /// QuietDeps
        /// </summary>
        [JsonIgnore]
        public bool QuietDeps { get; set; } = true;
    }

    public enum SassStyle
    {
        Expanded,
        Compressed
    }

    public enum SassSourceMapUrls
    {
        Relative,
        Absolute
    }
}

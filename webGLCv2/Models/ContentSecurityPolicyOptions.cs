namespace webGLCv2.Models;

public sealed class ContentSecurityPolicyOptions
{
    public const string SectionName = "ContentSecurityPolicy";

    public bool EnableContentSecurityPolicy { get; set; } = true;
    public List<string> TrustedOrigins { get; set; } = new();
    public List<string> DefaultSrc { get; set; } = new() { "'self'" };
    public List<string> BaseUri { get; set; } = new() { "'self'" };
    public List<string> ObjectSrc { get; set; } = new() { "'none'" };
    public List<string> FrameAncestors { get; set; } = new() { "'self'" };
    public List<string> FormAction { get; set; } = new() { "'self'" };
    public List<string> ImgSrc { get; set; } = new() { "'self'", "data:", "blob:", "https:" };
    public List<string> FontSrc { get; set; } = new() { "'self'", "data:" };
    public List<string> StyleSrc { get; set; } = new() { "'self'", "'unsafe-inline'" };
    public List<string> ScriptSrc { get; set; } = new() { "'self'" };
    public List<string> ConnectSrc { get; set; } = new() { "'self'" };
    public List<string> FrameSrc { get; set; } = new() { "'self'" };
    public List<string> WorkerSrc { get; set; } = new() { "'self'", "blob:" };
    public bool UpgradeInsecureRequests { get; set; } = true;
}

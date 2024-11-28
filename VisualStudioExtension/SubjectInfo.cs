using System.IO;
using System.Reflection;

namespace VisualStudioExtension
{
    public class SubjectInfo
    {
        public string ProjectNamespace { get; set; }
        public string FileNamespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }

    public class SpecInfo
    {
        public FileInfo Project { get; set; }
        public FileInfo Folder { get; set; }
        public FileInfo Class { get; set; }
    }
}
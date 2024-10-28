using System;
namespace EsriMapDemo.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SampleAttribute : System.Attribute
    {
        private readonly string _name;
        private readonly string _category;
        private readonly string _description;
        private readonly string _instructions;
        private readonly string[] _tags;

        public SampleAttribute(string name, string category, string description, string instructions, params string[] tags)
        {
            _name = name;
            _category = category;
            _description = description;
            _instructions = instructions;
            _tags = tags;
        }

        public string Name
        { get { return _name; } }

        public string Category
        { get { return _category; } }

        public string Description
        { get { return _description; } }

        public string Instructions
        { get { return _instructions; } }

        public System.Collections.Generic.IReadOnlyList<string> Tags
        { get { return _tags; } }
    }

    public abstract class AdditionalFilesAttribute : Attribute
    {
        private readonly string[] _files;

        protected AdditionalFilesAttribute(params string[] files)
        {
            _files = files;
        }

        public IReadOnlyList<string> Files
        { get { return _files; } }
    }

    /// <summary>
    /// Attribute for annotating a sample with XAML layout files it uses.
    /// This should not be used for the primary layout on WPF, UWP, WinUI, MAUI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class XamlFilesAttribute : AdditionalFilesAttribute
    {
        public XamlFilesAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with android layout files it uses.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AndroidLayoutAttribute : AdditionalFilesAttribute
    {
        public AndroidLayoutAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with additional class files.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassFileAttribute : AdditionalFilesAttribute
    {
        public ClassFileAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with additional embedded resources files.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EmbeddedResourceAttribute : AdditionalFilesAttribute
    {
        public EmbeddedResourceAttribute(params string[] files) : base(files)
        {
        }
    }
}


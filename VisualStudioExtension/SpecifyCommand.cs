using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace VisualStudioExtension
{
    internal sealed class SpecifyCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("1c3bd5f8-ad7b-4dd7-9815-3d06aec0ed05");

        private readonly AsyncPackage _package;

        private SpecifyCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static SpecifyCommand Instance
        {
            get;
            private set;
        }

        private IAsyncServiceProvider ServiceProvider => _package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new SpecifyCommand(package, commandService);
        }

        private async void Execute(object sender, EventArgs e)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                var textView = await GetCurrentViewAsync();
                var project = await GetProjectAsync();

                var subjectInfo = await GetSubjectInfoAsync(textView, project);
                var specInfo = await GetSpecInfoAsync(subjectInfo, project);
                ShowInfoMessageBox("Generate test",
$@"Project namespace: {subjectInfo.ProjectNamespace}
Namespace: {subjectInfo.FileNamespace}
Class: {subjectInfo.ClassName}
Method: {subjectInfo.MethodName}");
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
        }

        private async Task<SpecInfo> GetSpecInfoAsync(SubjectInfo testInfo, EnvDTE.Project project)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var projectPath = project.FullName;
            return null;
        }

        private async Task<SubjectInfo> GetSubjectInfoAsync(IWpfTextView textView, EnvDTE.Project project)
        {
            if (textView == null)
                throw new InvalidOperationException("No active text view");

            var doc = textView.TextSnapshot.GetText();
            var methodAtCaret = GetMethod(textView, GetRoot(doc))
                ?? throw new InvalidOperationException("No method at caret position");
            if (!methodAtCaret.Modifiers.Any(SyntaxKind.PublicKeyword))
                throw new InvalidOperationException("Can only generate test from public method");
            string namespaceName = GetNamespace(doc)
                ?? throw new InvalidOperationException("File namespace could not be determined");
            var projectNamespace = await GetNamespaceAsync(project);
            string className = GetClass(methodAtCaret)?.Identifier.Text
                ?? GetRecord(methodAtCaret)?.Identifier.Text
                ?? throw new InvalidOperationException("Class name could not be determined");

            return new SubjectInfo
            {
                ProjectNamespace = projectNamespace,
                FileNamespace = namespaceName,
                ClassName = className,
                MethodName = methodAtCaret.Identifier.Text
            };
        }

        private SyntaxNode GetRoot(string doc)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(doc, new CSharpParseOptions(LanguageVersion.Default));
            return syntaxTree.GetRoot();
        }

        private MethodDeclarationSyntax GetMethod(IWpfTextView textView, SyntaxNode root)
        {
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            var caretLine = textView.Caret.Position.BufferPosition.GetContainingLine().LineNumber;
            return methodDeclarations.LastOrDefault(m => m.GetLocation().GetLineSpan().StartLinePosition.Line <= caretLine);
        }

        private ClassDeclarationSyntax GetClass(SyntaxNode descendant)
            => descendant is null
            ? null
            : descendant as ClassDeclarationSyntax ?? GetClass(descendant.Parent);

        private RecordDeclarationSyntax GetRecord(SyntaxNode descendant)
            => descendant is null ? null : descendant as RecordDeclarationSyntax ?? GetRecord(descendant.Parent);

        private string GetNamespace(string doc)
        {
            var namespaceLine = doc
                .Split(new string[] { Environment.NewLine, ";", "{" }, StringSplitOptions.None)
                .Select(l => l.ToString().Trim())
                .FirstOrDefault(str => str.StartsWith("namespace"));
            if (namespaceLine == null)
                return null;
            return namespaceLine.Substring("namespace".Length).Trim();
        }

        private void ShowErrorMessageBox(string message)
            => ShowMessageBox("Error", message, OLEMSGICON.OLEMSGICON_WARNING);

        private void ShowInfoMessageBox(string title, string message)
            => ShowMessageBox(title, message, OLEMSGICON.OLEMSGICON_INFO);

        private void ShowMessageBox(string title, string message, OLEMSGICON icon)
        {
            VsShellUtilities.ShowMessageBox(
                _package,
                message,
                title,
                icon,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private async Task<IWpfTextView> GetCurrentViewAsync()
        {
            var userData = await GetUserDataAsync();
            if (userData == null)
                return null;

            Guid guidViewHost = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;
            userData.GetData(ref guidViewHost, out object holder);
            var viewHost = (IWpfTextViewHost)holder;
            return viewHost.TextView;
        }

        private async Task<IVsUserData> GetUserDataAsync()
        {
            var textManager = await ServiceProvider.GetServiceAsync(typeof(SVsTextManager)) as IVsTextManager;
            textManager.GetActiveView(1, null, out IVsTextView textView);
            return textView as IVsUserData;
        }

        private async Task<EnvDTE.Project> GetProjectAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var service = await ServiceProvider.GetServiceAsync(typeof(SDTE));
            var dte = (EnvDTE.DTE)service;
            var activeDocument = dte.ActiveDocument;
            var projectItem = activeDocument.ProjectItem;
            return projectItem.ContainingProject;
            //return project.Properties.Item("DefaultNamespace").Value.ToString();
        }

        private async Task<string> GetNamespaceAsync(EnvDTE.Project project)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            return project.Properties.Item("DefaultNamespace").Value.ToString();
        }
    }
}
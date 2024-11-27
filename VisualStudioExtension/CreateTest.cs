using Microsoft.CodeAnalysis;
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
    internal sealed class CreateTest
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("1c3bd5f8-ad7b-4dd7-9815-3d06aec0ed05");

        private readonly AsyncPackage _package;

        private CreateTest(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.DoCreateTest, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static CreateTest Instance
        {
            get;
            private set;
        }

        private IAsyncServiceProvider ServiceProvider => _package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CreateTest's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CreateTest(package, commandService);
        }

        private async void DoCreateTest(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var textView = await GetCurrentViewAsync();
            if (textView == null)
            {
                ShowMessageBox("Error", "No active text view", OLEMSGICON.OLEMSGICON_WARNING);
                return;
            }

            // Get the method and class names
            var syntaxTree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(textView.TextSnapshot.GetText());
            var root = syntaxTree.GetRoot();
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            // Get the method name at the caret position
            var caretLine = textView.Caret.Position.BufferPosition.GetContainingLine().LineNumber;
            var methodAtCaret = methodDeclarations.LastOrDefault(m => m.GetLocation().GetLineSpan().StartLinePosition.Line <= caretLine);
            string methodNameAtCaret = methodAtCaret?.Identifier.Text ?? "No method at caret position";
            string className = GetClass(methodAtCaret)?.Identifier.Text ?? "No class for method at caret position";

            // Show the method and class names in a message box
            ShowMessageBox("Code Elements", $"Class: {className}\nMethod at Caret: {methodNameAtCaret}", OLEMSGICON.OLEMSGICON_INFO);
        }

        private ClassDeclarationSyntax GetClass(SyntaxNode node) 
            => node is null ? null : node as ClassDeclarationSyntax ?? GetClass(node.Parent);

        private void ShowMessageBox(string title, string message, OLEMSGICON icon)
        {
            VsShellUtilities.ShowMessageBox(
                this._package,
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
    }
}
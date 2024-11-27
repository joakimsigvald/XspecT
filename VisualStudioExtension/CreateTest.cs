using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace VisualStudioExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CreateTest
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1c3bd5f8-ad7b-4dd7-9815-3d06aec0ed05");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTest"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CreateTest(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.DoCreateTest, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CreateTest Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
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

            // Get the current text view
            var textManager = await ServiceProvider.GetServiceAsync(typeof(SVsTextManager)) as IVsTextManager;
            textManager.GetActiveView(1, null, out IVsTextView textView);

            if (textView == null)
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "No active text view",
                    "Error",
                    OLEMSGICON.OLEMSGICON_WARNING,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            // Get the WPF text view
            var userData = textView as IVsUserData;
            if (userData == null)
            {
                return;
            }

            Guid guidViewHost = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;
            userData.GetData(ref guidViewHost, out object holder);
            var viewHost = (IWpfTextViewHost)holder;
            var wpfTextView = viewHost.TextView;

            // Get the selected text or the word under the caret
            string selectedText = wpfTextView.Selection.SelectedSpans[0].GetText();
            if (string.IsNullOrEmpty(selectedText))
            {
                var caretPosition = wpfTextView.Caret.Position.BufferPosition;
                var currentSnapshot = caretPosition.Snapshot;
                var extent = currentSnapshot.GetLineFromPosition(caretPosition.Position);
                selectedText = extent.GetText();
            }

            // Get the method and class names
            var syntaxTree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(wpfTextView.TextSnapshot.GetText());
            var root = syntaxTree.GetRoot();
            var methodDeclarations = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax>();
            var classDeclarations = root.DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>();

            string methodNames = string.Join(", ", methodDeclarations.Select(m => m.Identifier.Text));
            string classNames = string.Join(", ", classDeclarations.Select(c => c.Identifier.Text));

            // Show the method and class names in a message box
            VsShellUtilities.ShowMessageBox(
                this.package,
                $"Methods: {methodNames}\nClasses: {classNames}",
                "Code Elements",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}

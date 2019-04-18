using System.Windows.Forms;

namespace gloEMR.Help
{
	/// <summary>
	/// Help processor interface.
	/// </summary>
    public interface IHelpProcessor
    {
		/// <summary>
		/// Processes the help for a <see cref="Control" />.
		/// </summary>
		/// <param name="control">The control.</param>
        void ProcessControlHelp(Control control);
    }
}

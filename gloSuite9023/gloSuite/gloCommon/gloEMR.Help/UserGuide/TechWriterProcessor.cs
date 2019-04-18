namespace gloEMR.Help
{
	/// <summary>
	/// Builder help processor.
	/// </summary>
    public class TechWriterProcessor : IHelpProcessor
    {
        #region IHelpProcessor Members
		/// <summary>
		/// Processes the control help.
		/// </summary>
		/// <param name="control">The control.</param>
        public void ProcessControlHelp(System.Windows.Forms.Control control)
        {
            TechWriterEntry myTechWriterEntry = new TechWriterEntry(control);
            myTechWriterEntry.ShowDialog(myTechWriterEntry.Parent);
            myTechWriterEntry.Dispose();
            myTechWriterEntry = null;
        }
        #endregion
    }
}
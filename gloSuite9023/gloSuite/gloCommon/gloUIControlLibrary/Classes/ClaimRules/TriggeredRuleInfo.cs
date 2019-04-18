using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections;
using System.Xml.Linq;


namespace gloUIControlLibrary.Classes.ClaimRules
{
    public enum RuleType
    {
        //Warning = 1,
        //Information = 2,
        //Error = 3,
        
        Error = 1,
        Warning = 2,
        Information = 3,
        FailedRuleEvaluation = 4,
        None = 0
    }

    public class TriggeredRuleInfo
    {
        private Int64 ruleId = 0;
        public Int64 RuleId
        { get { return ruleId; } }

        private string ruleName = "";
        public string RuleName
        { get { return ruleName; } }

        private string ruleMessage = "";
        public string RuleMessage
        { get { return ruleMessage; } }

        private RuleType ruleTypeInfo = RuleType.None;
        public RuleType RuleTypeInfo
        { get { return ruleTypeInfo; } }

        private string ruleCategory = "";
        public string RuleCategory
        { get { return ruleCategory; } }

        private string ruleSource = "";
        public string RuleSource
        { get { return ruleSource; } }

        private string ruleSourceImgPath = "";
        public string RuleSourceImgPath
        { get { return ruleSourceImgPath; } }

        public TriggeredRuleInfo(Int64 ruleid, string rulename, string rulemessage, RuleType ruletypeinfo, string rulecategory="",string rulesource="local")
        {
            this.ruleId = ruleid;
            this.ruleName = rulename;
            this.ruleMessage = rulemessage;
            this.ruleTypeInfo = ruletypeinfo;
            this.ruleCategory = rulecategory;
            this.ruleSource = rulesource;
            if (rulesource.ToLower() == "global")
            {
                this.ruleSourceImgPath = @"/gloUIControlLibrary;component/WPFForms/Images/header3.png";
            }
            else
            {
                this.ruleSourceImgPath = @""; ;
            }
            
        }
    }


    public class TriggeredRuleInfoObservable : IDisposable
    {
        private ObservableCollection<TriggeredRuleInfo> _TriggeredRuleInfoList;
        public ObservableCollection<TriggeredRuleInfo> TriggeredRuleInfoList
        {
            get { return _TriggeredRuleInfoList; }
            set { _TriggeredRuleInfoList = value; }
        }

        public TriggeredRuleInfoObservable()
        {
            this._TriggeredRuleInfoList = new ObservableCollection<TriggeredRuleInfo>();
        }

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (this._TriggeredRuleInfoList != null)
                    {
                        this._TriggeredRuleInfoList.Clear();
                        this._TriggeredRuleInfoList = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }


    public class RuleInfoVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Error))
            { returned = "ICO/RuleError.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.FailedRuleEvaluation))
            { returned = "ICO/RuleInvalid.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Information))
            { returned = "ICO/RuleInformation.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Warning))
            { returned = "ICO/RuleWarning.ico"; }
           
            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class RuleTextVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Error))
            { returned = "Error"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.FailedRuleEvaluation))
            { returned = "Failed Rule Evaluation"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Information))
            { returned = "Information"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(RuleType.Warning))
            { returned = "Warning"; }

            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}

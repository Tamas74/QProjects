using System;
using System.Collections.Generic;
using System.Text;

namespace gloEDocumentV3
{
    namespace Enumeration
    {
        #region Enumarations"

        //Document Extension
        public enum enumDocumentExtension
        {
            None = 0, PDF = 1, TIFF = 2
        }

        //Document SourceBin
        public enum enum_DocumentSourceBin
        { None = 0, Category = 1, Scanner = 2, External = 3, Migration = 4, Fax = 5, OMROCRICR = 6, MigrationV1toV3 = 7, MigrationV2toV3 = 8,Import = 9 }

        //Document Column Type
        public enum enum_DocumentColumnType
        { None = 0, Category = 1, CategoryInformation = 2, MonthName = 3, MonthInformation = 4, Document = 5, DocumentInformation = 6, Pages = 7, PagesInformation = 8,SubCategory=9 }

        public enum enum_DocumentEventType
        { None = 0, ImportDocument = 1, ScanDocument = 2, ImportInExisting = 3, ScanInExisting = 4, SendToNewDocument = 5, SendToExistingDcument = 6, SendToExistingWithDcumentName = 7, DeleteDocument = 8, DeletePages = 9, PrintDocument = 10, PrintPages = 11, FaxDocument = 12, FaxPages = 13, AcknowledgeDocument = 14, AcknowledgePages = 15, ViewAcknowledgeDocument = 16, ViewAcknowledgePages = 17, RenameDocument = 18, RenamePages = 19, SendToPatient = 20, AddNote = 21, DeleteNote = 22, AddUserTag = 23, DeleteUserTag = 24, SelectAll = 25, UnselectAll = 26, InsertSigneture = 27, ViewLargeIcon = 28, ViewSmallIcon = 29, ViewList = 30, ViewTile = 31, UnAcknowledgeDocument = 32, UnAcknowledgePages = 33, SendToDMS = 34 }

        public enum enum_DocumentView
        { FullView = 1, YearView = 2, MonthView = 3 }

        public enum enum_DocumentAcknowledgeReviewed
        { NotAcknowledge = 1, Acknowledge = 2, Reviwed = 3, AcknowledgeReviwed = 4 }

        public enum enum_ScannerSettingType
        { BlackWhiteScan150 = 1, BlackWhiteScan200 = 2, BlackWhiteScan240 = 3, BlackWhiteScan300 = 4, ColorScan150 = 5, ColorScan200 = 6, ColorScan240 = 7, ColorScan300 = 8 }

        public enum enum_NTAOType
        {
            None = 0, Notes = 1, Tag = 2, Acknowledge = 3, Other = 4
        }

        public enum enum_OpenExternalSource
        {
            None = 0, AdvanceDirective = 1, DashBoard = 2, ViewPatientSummary = 3, LabOrder = 4, ViewTask = 5, RxMeds = 6, Immunization = 7, IntuitMessage = 8,SecureMessage = 9,Amedments=10, RCM = 11,DirectMessage =12,PHI =13
        }

        public enum enum_OpenEDocumentAs
        {
            None = 0, ScanDocument = 1, ViewDocument = 2, ViewDocumentForExternalModule = 3,
            //20091006 ViewAllDocuments  Added to Just View all Documents of Patient (Not Import, Scan or Delete)
            ViewAllDocuments = 4
        }

        public enum enum_ZoomState { FitToHeight = 0, BestFit = 1, FitToWidth = 2, ZoomPercent = 3 }

        #endregion
    }
}

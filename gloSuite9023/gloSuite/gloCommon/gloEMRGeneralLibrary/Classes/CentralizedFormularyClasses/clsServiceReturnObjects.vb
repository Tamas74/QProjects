Imports System.Runtime.Serialization

Namespace gloGeneral

    Public Enum URLOption
        GetFormulary
        RxHGetNLFormularyStatus
        RxHGetCopayInfo
        GetFormularyStatus
        RxHGetCoverageResourceLinkInfo
        RxHGetTherapeuticAlternatives_SL
        RxHGetTherapeuticAlternatives
        GetPayerAlternative
        RxHGetCoverageInfo
        RxHGetCopayInfo_SL
        SearchPartI
        RxHGetTherapeuticAlternatives_Optimized

        RxHGetTherapeuticAlternatives_Inner
        RxHGetTherapeuticAlternatives_Outer
        RxHGetTherapeuticAlternatives_DIB_Formulary
        CheckURLConnectivity
        GetAbbrevatedCopayInfoWithFormularyStatus
    End Enum

    <DataContract()>
 Public Class SearchPartI
        Public Property P As String '//FormularyStatusID3
        Public Property Q As String '//ProductID
        Public Property R As String '//FormularyStatus
    End Class

    <DataContract()>
    Public Class RxHGetNLFormularyStatus

        <DataMember()>
        Public Property L As String

        <DataMember()>
        Public Property Ll As String

        <DataMember()>
        Public Property N As String

        <DataMember()>
        Public Property Nn As String

        <DataMember()>
        Public Property Li As String
    End Class

    <DataContract()>
    Public Class Argument_RxHGetTherapeuticAlternatives_Optimized

        <DataMember()>
        Public Property PBMSenderID As String

        <DataMember()>
        Public Property FormularyID As String

        <DataMember()>
        Public Property DDID As String

    End Class

    <DataContract()>
    Public Class RxHGetTherapeuticAlternatives_DIB_Formulary

        <DataMember()>
        Public Property A As Int32        '                             //dddid 
        <DataMember()>
        Public Property B As String

        <DataMember()>
        Public Property C As Int32         '                          //meddevind 
        '   
        <DataMember()>
        Public Property D As String        '                                //GPI 
        <DataMember()>
        Public Property E As String        '                       //NameTypeCode 
        <DataMember()>
        Public Property F As String        '                        //genericcode 

        '
        <DataMember()>
        Public Property G As String        '                          //RxOtcCode 
        <DataMember()>
        Public Property H As String        '                    //FormularyStatus 
        <DataMember()>
        Public Property I As String        '                  //FormularyStatusID 
        '          
        <DataMember()>
        Public Property J As String        '                         //RecordType 
        <DataMember()>
        Public Property K As String        '                          //VersionNo 
        <DataMember()>
        Public Property L As String        '                           //SenderID 
        '          
        <DataMember()>
        Public Property M As String        '         //sSenderParticipantPassword 
        <DataMember()>
        Public Property N As String        '                         //ReceiverID 
        <DataMember()>
        Public Property O As String        '                         //SourceName 
        '          
        <DataMember()>
        Public Property P As String        '          //TransmissionControlNumber 
        <DataMember()>
        Public Property Q As String        '               //TransmissionDateTime 
        <DataMember()>
        Public Property R As String        '               //TransmissionFileType 
        '                      As String
        <DataMember()>
        Public Property S As String        '                 //TransmissionAction 
        <DataMember()>
        Public Property T As String        '                        //ExtractDate 
        <DataMember()>
        Public Property U As String        '                           //FileType 

        '                      As String
        <DataMember()>
        Public Property V As String        '                        //FormularyID 
        <DataMember()>
        Public Property W As String        '                      //FormularyName 
        <DataMember()>
        Public Property X As String        '    //NonListedRxBrandFormularyStatus 
        '                      As String
        <DataMember()>
        Public Property Y As String        '  //NonListedRxGenericFormularyStatus 
        <DataMember()>
        Public Property Z As String        '   //NonListedOTCBrandFormularyStatus 
        <DataMember()>
        Public Property Aa As String        '  //NonListedOTCGenericFormularyStatus
        '                      As String
        <DataMember()>
        Public Property Bb As String        '    //NonListedSuppliesFormularyStatus 
        <DataMember()>
        Public Property Cc As String        '                  //RealativeCostLimit 
        <DataMember()>
        Public Property Dd As String        '                          //ListAction 
        '                      As String
        <DataMember()>
        Public Property Ee As String        '                   //ListEffectiveDate 
    End Class

    <DataContract()>
    Public Class RxHGetAbbvedCopayInfo_Opt_DS

        <DataMember()>
        Public Property r As String '//RecordType
        <DataMember()>
        Public Property c As String '//CopayID
        <DataMember()>
        Public Property o As String '//CopayListType

        <DataMember()>
        Public Property p As String '//ProductID
        <DataMember()>
        Public Property f As String '//FlatCoPayAmount
        <DataMember()>
        Public Property l As String '//ListEffectiveDate

        <DataMember()>
        Public Property e As String '//PercentCoPayRate
        <DataMember()>
        Public Property t As String '//PharmacyType
        <DataMember()>
        Public Property i As String '//FirstCoPayTerm

        <DataMember()>
        Public Property m As String '//MinimumCoPay
        <DataMember()>
        Public Property a As String '//MaximumCoPay
        <DataMember()>
        Public Property d As String '//DaysSupplyPerCoPay

        <DataMember()>
        Public Property g As String '//CoPayTier
        <DataMember()>
        Public Property n As String '//MaximumCoPayTier
    End Class

    <DataContract()>
    Public Class RxHGetAbbvedCopayInfo_Opt_SL

        <DataMember()>
        Public Property r As String      'RecordType   
        <DataMember()>
        Public Property c As String         'CopayID   
        <DataMember()>
        Public Property o As String   'CopayListType   

        <DataMember()>
        Public Property f As String 'FlatCoPayAmount   
        <DataMember()>
        Public Property l As String 'PercentCoPayRate  

        <DataMember()>
        Public Property e As String 'formularyStatus   
        <DataMember()>
        Public Property t As String    'PharmacyType   
        <DataMember()>
        Public Property i As String  'FirstCoPayTerm   

        <DataMember()>
        Public Property m As String    'MinimumCoPay   
        <DataMember()>
        Public Property a As String    'MaximumCoPay   
        <DataMember()>
        Public Property d As String 'DaysSupplyPerCoPay

        <DataMember()>
        Public Property g As String       'CoPayTier   
        <DataMember()>
        Public Property n As String 'MaximumCoPayTier  

    End Class

    <DataContract()>
    Public Class RxHGetPayerAlternatives

        <DataMember()>
        Public r As Int64 'RecordType          
        <DataMember()>
        Public Property s As String 'SourceProductID     
        <DataMember()>
        Public Property a As String 'AlternativeProductID
        <DataMember()>
        Public Property p As String 'PreferenceLevel     
        <DataMember()>
        Public Property f As String 'FormularyStatus     


        Public Property b As String                'DrugName      
        Public Property c As String          'NameSourceCode
        Public Property d As String            'NameTypeCode  
        Public Property e As String             'GenericCode   
        Public Property g As String               'RxotcCode     
        Public Property h As Int16                'MedDevInd     
        Public Property i As String                     'GPI           

    End Class


    <DataContract()>
    Public Class RxHGetCoverageInfo

        <DataMember()>
        Public Property c As String                                'CoverageID
        <DataMember()>
        Public Property p As String                                 'ProductID                       
        <DataMember()>
        Public Property n As String                        'NumberofDrugsToTry              

        <DataMember()>
        Public Property Ss As String                                 'StepOrder                       
        <DataMember()>
        Public Property Mm As String          'MaximumAmountTimePeriodStartDate
        <DataMember()>
        Public Property M As String            'MaximumAmountTimePeriodEndDate  

        <DataMember()>
        Public Property x As String              'MaximumAmountTimePeriodUnits    
        <DataMember()>
        Public Property g As String                                    'Gender                          
        <DataMember()>
        Public Property S As String                              'MessageShort                    

        <DataMember()>
        Public Property Ll As String                               'MessageLong                     
        <DataMember()>
        Public Property L As String                                'MinimumAge                      
        <DataMember()>
        Public Property Aa As String                       'MinimumAgeQualifier             

        <DataMember()>
        Public Property A As String                                'MaximumAge                      
        <DataMember()>
        Public Property Tt As String                       'MaximumAgeQualifier             
        <DataMember()>
        Public Property T As String                                'RecordType                      

        <DataMember()>
        Public Property Uu As String                          'CoverageListType                
        <DataMember()>
        Public Property U As String                             'StepProductID                   
        <DataMember()>
        Public Property Ii As String                             'MaximumAmount                   

        <DataMember()>
        Public Property I As String                   'sMaximumAmountQualifier         
        <DataMember()>
        Public Property z As String                   'MaximumAmountTimePeriod         
    End Class
    <DataContract()>
    Public Class RxHGetCoverageResourceLinkInfo

        <DataMember()>
        Public Property Rr As String 'RecordType
        <DataMember()>
        Public Property R As String 'CoverageID
        <DataMember()>
        Public Property Pp As String 'ProductID
        <DataMember()>
        Public Property P As String 'ResourceLinkType
        <DataMember()>
        Public Property u As String 'URL


    End Class

    <DataContract()>
    Public Class RxHGetCopayInfo

        <DataMember()>
        Public Property A As String 'RecordType         
        <DataMember()>
        Public Property B As String 'CopayID            
        <DataMember()>
        Public Property C As String 'CopayListType   

        <DataMember()>
        Public Property D As String 'ProductID          
        <DataMember()>
        Public Property E As String 'FlatCoPayAmount    
        <DataMember()>
        Public Property F As String 'ListEffectiveDate  

        <DataMember()>
        Public Property G As String 'PercentCoPayRate   
        <DataMember()>
        Public Property H As String 'PharmacyType       
        <DataMember()>
        Public Property I As String 'FirstCoPayTerm     

        <DataMember()>
        Public Property J As String 'MinimumCoPay       
        <DataMember()>
        Public Property K As String 'MaximumCoPay       
        <DataMember()>
        Public Property L As String 'DaysSupplyPerCoPay 

        <DataMember()>
        Public Property M As String 'CoPayTier          
        <DataMember()>
        Public Property N As String 'MaximumCoPayTier   


    End Class
    <DataContract()>
    Public Class RxHGetCoverageInfo_SL

        <DataMember()>
        Public Property Rr As String               'RecordType       
        <DataMember()>
        Public Property R As String                  'CopayID        
        <DataMember()>
        Public Property Cc As String            'CopayListType     
        <DataMember()>
        Public Property C As String          'FlatCoPayAmount   

        <DataMember()>
        Public Property Pp As String         'PercentCoPayRate  

        <DataMember()>
        Public Property P As String             'PharmacyType      

        <DataMember()>
        Public Property Ff As String           'FirstCoPayTerm    
        <DataMember()>
        Public Property F As String             'MinimumCoPay      
        <DataMember()>
        Public Property Gg As String             'MaximumCoPay      

        <DataMember()>
        Public Property G As String       'DaysSupplyPerCoPay
        <DataMember()>
        Public Property Tt As String                'CoPayTier         
        <DataMember()>
        Public Property T As String         'MaximumCoPayTier  
    End Class
End Namespace

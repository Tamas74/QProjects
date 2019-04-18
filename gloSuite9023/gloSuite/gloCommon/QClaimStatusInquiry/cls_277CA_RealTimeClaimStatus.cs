using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriArqEDIRealTimeClaimStatus
{
    public class cls_277CA_RealTimeClaimStatus
    {
       private string _RequestString = "";
       public string RequestString
       {
           get { return _RequestString; }
           set { _RequestString = value; }
       }

       private string _ResponseString = "";
       public string ResponseString
       {
           get { return _ResponseString; }
           set { _ResponseString = value; }
       }

       private string _ResponseError = "";
       public string ResponseError
       {
           get { return _ResponseError; }
           set { _ResponseError = value; }
       }

       private string _StatusCategoryCode = "";
       public string StatusCategoryCode
       {
           get { return _StatusCategoryCode; }
           set { _StatusCategoryCode = value; }
       }

       private string _StatusCategoryCodeDesc = "";
       public string StatusCategoryCodeDesc
       {
           get { return _StatusCategoryCodeDesc; }
           set { _StatusCategoryCodeDesc = value; }
       }

       private string _StatusCode = "";
       public string StatusCode
       {
           get { return _StatusCode; }
           set { _StatusCode = value; }
       }

       private string _StatusCodeDesc = "";
       public string StatusCodeDesc
       {
           get { return _StatusCodeDesc; }
           set { _StatusCodeDesc = value; }
       }

       private string _StatusEffectiveDate = null;
       public string StatusEffectiveDate
       {
           get { return _StatusEffectiveDate; }
           set { _StatusEffectiveDate = value; }
       }

       private string _StatusMessge = "";
       public string StatusMessge
       {
           get { return _StatusMessge; }
           set { _StatusMessge = value; }
       }

       private string _PayerId = "";
       public string PayerId
       {
           get { return _PayerId; }
           set { _PayerId = value; }
       }

       private string _PayerName = "";
       public string PayerName
       {
           get { return _PayerName; }
           set { _PayerName = value; }
       }


       private string _RequestFilePath = "";
       public string RequestFilePath
       {
           get { return _RequestFilePath; }
           set { _RequestFilePath = value; }
       }

       private string _ResponseFilePath = "";
       public string ResponseFilePath
       {
           get { return _ResponseFilePath; }
           set { _ResponseFilePath = value; }
       }

       private long _RequestFileId = 0;
       public long RequestFileId
       {
           get { return _RequestFileId; }
           set { _RequestFileId = value; }
       }


       private long _ResponseFileId = 0;
       public long ResponseFileId
       {
           get { return _ResponseFileId; }
           set { _ResponseFileId = value; }
       }


       private long _RequestId = 0;
       public long RequestId
       {
           get { return _RequestId; }
           set { _RequestId = value; }
       }

       private long _ResponseId = 0;
       public long ResponseId
       {
           get { return _ResponseId; }
           set { _ResponseId = value; }
       }

    }


   
}

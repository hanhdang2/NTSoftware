using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace NTSoftware.Service.Interface
{
   public  interface IFillForm
    {
       void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object textReplace);
        void CreateWordDocument(object filename, object savaAs, object image);
    }
}

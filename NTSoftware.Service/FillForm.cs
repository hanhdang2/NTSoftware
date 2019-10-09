using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;
using NTSoftware.Service.Interface;

namespace NTSoftware.Service
{
    public class FillFrom : IFillForm
    {
        public void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object textReplace)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object matchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref matchAllForms, ref forward,
                ref wrap, ref format, ref textReplace,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }
        public void CreateWordDocument(object filename, object savaAs, object image)
        {
            object missing = Missing.Value;
            string tempPath = null;
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document aDoc = null;

            if (File.Exists((string)filename))
            {
                DateTime today = DateTime.Now;
                object readOnly = false;
                wordApp.Visible = false;
                aDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                   ref missing, ref missing, ref missing,
                   ref missing, ref missing, ref missing,
                   ref missing, ref missing, ref missing,
                   ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                this.FindAndReplace(wordApp, @"Doc1.docx", "asdd");
            }
            else
            {
                return;
            }
            aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
            aDoc.Close(ref missing, ref missing, ref missing);
            File.Delete(tempPath);

        }


    }
}


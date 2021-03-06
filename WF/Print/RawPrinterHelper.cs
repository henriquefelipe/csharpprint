﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

namespace WF.Net
{
    class PrinterService
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);


        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);


        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        [DllImport("winspool.drv", EntryPoint = "ReadPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadPrinter(IntPtr hPrinter, out IntPtr pBytes, Int32 dwCount, out Int32 dwNoBytesRead);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, string docname, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            di.pDocName = docname;
            di.pDataType = "RAW";
            // Open the printer.0x00000008 //IntPtr.Zero
            try
            {
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    string errorMessage = new Win32Exception(Marshal.GetLastWin32Error()).Message;
                    //logger.Error(string.Format(errorMessage));
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static bool SendFileToPrinter(string szPrinterName, string docname, string szFileName)
        {
            // Open the file.
            using (FileStream fs = new FileStream(szFileName, FileMode.Open))
            {
                // Create a BinaryReader on the file.
                BinaryReader br = new BinaryReader(fs);
                // Dim an array of bytes big enough to hold the file's contents.
                Byte[] bytes = new Byte[fs.Length];
                bool bSuccess = false;
                // Your unmanaged pointer.
                IntPtr pUnmanagedBytes = new IntPtr(0);
                int nLength;
                nLength = Convert.ToInt32(fs.Length);
                // Read the contents of the file into the array.
                bytes = br.ReadBytes(nLength);
                // Allocate some unmanaged memory for those bytes.
                pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                // Send the unmanaged bytes to the printer.
                bSuccess = SendBytesToPrinter(szPrinterName, docname, pUnmanagedBytes, nLength);
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                return bSuccess;
            }
        }
        /*public static bool SendStringToPrinter(string szPrinterName, string docname, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.

            //byte[] toBytes = Encoding.ASCII.GetBytes(somestring);

            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, docname, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }*/
        public static bool SendStringToPrinter(string szPrinterName, string docname, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.

            //System.Text.Encoding.ASCII.GetBytes(tmp)

            //char[] bytes = szString.ToCharArray();

            //byte[] bytes = Encoding.ASCII.GetBytes(szString);

            //byte[] toBytes = Encoding.ASCII.GetBytes(somestring);

            //pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            //SendBytesToPrinter(szPrinterName, docname, pBytes, dwCount);

            var e = Encoding.GetEncoding("iso-8859-1");
            byte[] bytes = e.GetBytes(szString);

            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == (byte)127)
                    bytes[i] = 0;
            }
            /*byte b = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i];
                if (b == 0)
                    sb.Append("#00");
                if (b == 10)
                    sb.AppendLine();
                else if (b < 32)
                    sb.Append("#" + b.ToString());
                else
                    sb.Append((char)b);
            }
            using (StreamWriter outfile = new StreamWriter(@"D:\Meus Documentos\LogPrinter.txt"))
            {
                outfile.Write(sb.ToString());
            }*/
            // Allocate some unmanaged memory for those bytes.
            pBytes = Marshal.AllocCoTaskMem(dwCount);
            try
            {
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pBytes, dwCount);
                // Send the unmanaged bytes to the printer.
                SendBytesToPrinter(szPrinterName, docname, pBytes, dwCount);
            }
            finally
            {
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pBytes);
            }
            //byte[] toBytes = Encoding.ASCII.GetBytes(somestring);

            //pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            //SendBytesToPrinter(szPrinterName, docname, pBytes, dwCount);
            //Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
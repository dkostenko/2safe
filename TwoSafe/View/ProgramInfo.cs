﻿using System;
using System.Reflection;

namespace TwoSafe.View
{
    static internal class ProgramInfo
    {
        static internal string AssemblyGuid
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
            }
        }
        static internal string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }
    }
}

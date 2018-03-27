using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using EnvDTE;
using EnvDTE80;
using VSLangProj;

namespace XJT.Com.EntitySql.DatabaseSource
{
    class EnvDTEHelper
    {
        public static void AddFilesToProject(string projectName, List<string> files)
        {
            try
            {
                DTE dte = GetIntegrityServiceInstance();
                if (dte != null)
                {
                    foreach (EnvDTE.Project item in dte.Solution.Projects)
                    {
                        if (item.Name.Contains(projectName))
                        {
                            foreach (string file in files)
                                item.ProjectItems.AddFromFile(file);
                            item.Save();
                        }
                    }
                }
            }
            finally
            {
            }
        }

        public static void AddProjectReferences(string projectName, Dictionary<string, string> references)
        {
            try
            {
                DTE dte = GetIntegrityServiceInstance();
                if (dte != null)
                {
                    foreach (EnvDTE.Project item in dte.Solution.Projects)
                    {
                        if (item.Name.Contains(projectName))
                        {
                            VSProject vsProject = (VSProject)item.Object;
                            foreach (KeyValuePair<string, string> reference in references)
                            {
                                if (vsProject.References.Find(reference.Key) == null)
                                {
                                    vsProject.References.Add(reference.Value);
                                }
                            }
                            item.Save();
                        }
                    }
                }
            }
            finally
            {
            }
        }

        public static DTE GetIntegrityServiceInstance()
        {
            List<string> projectNames = new List<string>();
            IEnumerable<DTE> dtes = GetAllInstances();
            if (dtes.Count() == 1)
                return dtes.First();
            foreach (DTE dte in GetAllInstances())
            {
                projectNames.Clear();
                foreach (Project project in dte.Solution.Projects)
                {
                    projectNames.Add(project.Name);
                }
                if ((projectNames.Contains("Service")) && (projectNames.Contains("NUnit")))
                    return dte;
            }
            return null;
        }

        private static DTE2 GetActiveInstance()
        {
            return (EnvDTE80.DTE2)Marshal.GetActiveObject("VisualStudio.DTE.11.0");
        }

        private static IEnumerable<DTE> GetAllInstances()
        {
            IRunningObjectTable rot;
            IEnumMoniker enumMoniker;
            int retVal = GetRunningObjectTable(0, out rot);

            if (retVal == 0)
            {
                rot.EnumRunning(out enumMoniker);

                IntPtr fetched = IntPtr.Zero;
                IMoniker[] moniker = new IMoniker[1];
                object punkObject = null;
                while (enumMoniker.Next(1, moniker, fetched) == 0)
                {
                    IBindCtx bindCtx;
                    CreateBindCtx(0, out bindCtx);
                    string displayName;
                    moniker[0].GetDisplayName(bindCtx, null, out displayName);
                    // Console.WriteLine("Display Name: {0}", displayName);  
                    bool isVisualStudio = displayName.StartsWith("!VisualStudio");
                    if (isVisualStudio)
                    {
                        //var dte = rot.GetObject(moniker) as DTE;  
                        //yield return dte;  
                        rot.GetObject(moniker[0], out punkObject);
                        var dte = (DTE)(punkObject);
                        yield return dte;
                    }
                }
            }
        }

        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NewsApp.Tests.Common
{
    public static class TestExtension
    {
        public static void Dump(this object sender)
        {
            sender.Dump(null, null);
        }

        public static void Dump(this object sender, object prefix)
        {
            sender.Dump(prefix, null);
        }

        public static void Dump(this object sender, object prefix, object suffix)
        {
            var str = sender as string;
            var list = sender as IList;
            var is2 = sender as ICollection;
            var enumerable = sender as IEnumerable;
            var message = "";
            var str3 = (prefix == null)
                ? ""
                : prefix.ToString();
            var str4 = (suffix == null)
                ? ""
                : suffix.ToString();
            if (!string.IsNullOrEmpty(str3))
            {
                message = str3 + " ";
            }
            if (str != null)
            {
                message = message + str;
            }
            else
            {
                object obj3;
                if (list != null)
                {
                    obj3 = message;
                    message = string.Concat(new[] {obj3, list.GetType()
                        .Name,
                        " ", list.Count});
                    foreach (var obj2 in list)
                    {
                        obj2.Dump(" ");
                    }
                }
                else if (is2 != null)
                {
                    obj3 = message;
                    message = string.Concat(new[] {obj3, is2.GetType()
                        .Name,
                        " ", is2.Count});
                    foreach (var obj2 in is2)
                    {
                        obj2.Dump(" ");
                    }
                }
                else if (enumerable != null)
                {
                    message = message + enumerable.GetType()
                        .Name + " Enumerable can't get length";
                    foreach (var obj2 in enumerable)
                    {
                        obj2.Dump(" ");
                    }
                }
                else
                {
                    message = message + sender;
                }
            }
            if (!string.IsNullOrEmpty(str4))
            {
                message = message + " " + str4;
            }
            Debug.WriteLine(message);
        }

        public static IEnumerable<Type> GetModelTypes(this IEnumerable<Assembly> sender, string defaultNamespace, string[] defaultExclude)
        {
            return sender.SelectMany(m => m.GetModelTypes(defaultNamespace, defaultExclude));
            //return (from m in sender
            //        select m.GetModelTypes(defaultNamespace, defaultExclude).ToList());
        }

        public static IEnumerable<Type> GetModelTypes(this Assembly sender, string defaultNamespace, string[] defaultExclude)
        {
            defaultExclude = defaultExclude ?? new[] {""};
            string[] clrExclue = {"<", "`", "_", "Map", "Repository"};
            return (from m in sender.GetTypes()
                where (((clrExclue.All(c => !m.FullName.Contains(c)) && m.FullName.StartsWith(defaultNamespace)) && (clrExclue.All(c => !c.EndsWith(m.FullName)) && !m.IsSubclassOf(typeof (Exception)))) && ((!m.IsInterface && !m.IsEnum) && (m.IsClass && !m.IsAbstract))) && !defaultExclude.Contains(m.Name)
                select m);
        }
    }
}
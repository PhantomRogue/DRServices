using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeniePlugin.Interfaces;
using System.Net;
using System.IO;

namespace BeastiaryQuery
{
    public class BeastiaryQuery : GeniePlugin.Interfaces.IPlugin
    {
        private GeniePlugin.Interfaces.IHost m_Host;
        private string sHashValue = string.Empty;

        private void GetJsonFromEPedia()
        {
            string[] separators = { ",", ".", "!", "?", ";", ":", " " };
            string value = "\bq Light_Edge Medium_Edge Evasion Defending Box Gems backstab skin undead construct";

            string sBaseURL = @"https://elanthipedia.play.net/mediawiki/index.php?title=Special:Ask&limit=20&q={0}p=format%3Djson&po=%3FMinSkillCap+is%3DMin+Skill%0A%3FMaxSkillCap+is%3DMax+Skill%0A&eq=yes";

            string sQuery = string.Empty;

            string sBaseQuery = "[[Creature {0}::true]]";

            string sSkillBase = "[[MinSkillCap is::<{0}]][[MaxSkillCap is::>{1}]]";

            // Average of Offenses and Defenses.  Will add 100 to this Value to determine the "Max Cap, so it returns a range not too out of scope
            int iDefOffAvg = 0;
            int iDefOffCnt = 0;

            string[] slValues = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in slValues)
            {
                string sLower = s.ToLower();
                switch (sLower)
                {
                    case "light_crossbow":
                    case "heavy_crossbow":
                    case "short_bow":
                    case "long_bow":
                    case "composite_bow":
                    case "light_thrown":
                    case "heavy_thrown":
                    case "halberd":
                    case "brawling":
                    case "light_edged":
                    case "heavy_edged":
                    case "twohanded_edged":
                    case "twohanded_blunt":
                    case "light_blunt":
                    case "medium_blunt":
                    case "heavy_blunt":
                    case "medium_edged":
                    case "quarter_staff":
                    case "short_stff":
                    case "pikes":
                    case "offhand_weapon":
                        iDefOffAvg += int.Parse(this.m_Host.get_Variable(s + ".Ranks"));
                        iDefOffCnt++;
                        break;

                    case "Gem":
                        sQuery += string.Format(sBaseQuery, "has gems");
                        break;
                    
                    case "box":
                    case "boxes":
                        sQuery += string.Format(sBaseQuery, "has boxes");
                        break;

                    case "backstab":
                        sQuery += string.Format(sBaseQuery, "is BackStabbable");
                        break;

                    case "construct":
                        sQuery += string.Format(sBaseQuery, "has gems");
                        break;

                    case "skin":
                    case "skinnable":
                        sQuery += string.Format(sBaseQuery, "is skinnable");
                        break;
                }
            }

            // Average it and add it to the sSkillBase
            iDefOffAvg = iDefOffAvg / iDefOffCnt;
            sSkillBase = string.Format(sSkillBase, iDefOffAvg.ToString());

            sBaseURL = string.Format(sBaseURL, sSkillBase + sQuery);

            HttpWebRequest wrEPedia = (HttpWebRequest)WebRequest.Create(sBaseURL);
            wrEPedia.Method = WebRequestMethods.Http.Get;
            wrEPedia.Accept = "application/json";

            var response = (HttpWebResponse)wrEPedia.GetResponse();
            var text = string.Empty;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            string sVars = "{" + ((string)text).Remove(0, ((string)text).IndexOf("results"));
        }

        public class JsonHelper
        {
            private const string INDENT_STRING = "    ";
            public static string FormatJson(string str)
            {
                var indent = 0;
                var quoted = false;
                var sb = new StringBuilder();
                for (var i = 0; i < str.Length; i++)
                {
                    var ch = str[i];
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            if (!quoted)
                            {
                                sb.AppendLine();
                                Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                            }
                            break;
                        case '}':
                        case ']':
                            if (!quoted)
                            {
                                sb.AppendLine();
                                Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                            }
                            sb.Append(ch);
                            break;
                        case '"':
                            sb.Append(ch);
                            bool escaped = false;
                            var index = i;
                            while (index > 0 && str[--index] == '\\')
                                escaped = !escaped;
                            if (!escaped)
                                quoted = !quoted;
                            break;
                        case ',':
                            sb.Append(ch);
                            if (!quoted)
                            {
                                sb.AppendLine();
                                Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                            }
                            break;
                        case ':':
                            sb.Append(ch);
                            if (!quoted)
                                sb.Append(" ");
                            break;
                        default:
                            sb.Append(ch);
                            break;
                    }
                }
                return sb.ToString();
            }
        }

        
        #region IPlugin Members

        string IPlugin.Author
        {
            get { return "R.Wahl."; }
        }

        string IPlugin.Description
        {
            get { return "Plugin to Actively Query Beastiary."; }
        }

        private bool m_Enabled = true;
        bool IPlugin.Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        void IPlugin.Initialize(IHost Host)
        {
            //GetJsonFromEPedia();
            string sNames = this.m_Host.get_Variable("charactername") + "_" + Environment.MachineName;
            
            int x = GetHashCode();

        }

        string IPlugin.Name
        {
            get { return "Queryville!"; }
        }

        void IPlugin.ParentClosing()
        {
            
        }

        string IPlugin.ParseInput(string Text)
        {
            return Text;
        }

        string IPlugin.ParseText(string Text, string Window)
        {
            return Text;
        }

        void IPlugin.ParseXML(string XML)
        {
            
        }

        void IPlugin.Show()
        {
            
        }

        void IPlugin.VariableChanged(string Variable)
        {
                        
        }

        string IPlugin.Version
        {
            get { return "1"; }
        }

        #endregion
    }

    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }

}

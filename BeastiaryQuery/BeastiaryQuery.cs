using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeniePlugin.Interfaces;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BeastiaryQuery
{
 
   
       /// <summary>
        /// The creature class.
        /// Contains information on a creature
        /// </summary>
    public class Creature
    {
        public String FullName { get; set; }
        public int MinSkills { get; set; }
        public int MaxSkills { get; set; }
        public String FullURL { get; set; }

        /// <summary>
        /// Initializes a new instance of the Creature class
        /// </summary>
        /// <param name="name">name of the creature</param>
        /// <param name="url">full url of the creature</param>
        /// <param name="minskills">minimum skills to play</param>
        /// <param name="maxskills">maximum that the creature trains</param>
        public Creature(String name, String fullurl, int minskills, int maxskills)
        {
            FullName = name;
            FullURL = fullurl;
            MinSkills = minskills;
            MaxSkills = maxskills;
        }
        public Creature()
        {

        }
        public override string ToString()
        {
            string sReturn = string.Empty;
            sReturn += FullName.PadRight(40) + "\t" ;
            sReturn += (MinSkills + "-" + MaxSkills).PadRight(20) + "\t";
            //sReturn += FullURL.PadLeft(10);
            //return " \"" + String.Format("{0,-30}{1,-150} {2}-{3}", FullName, FullURL, MinSkills, MaxSkills) + "\"";
            return "\"" + sReturn + "\"";
        }
    }


    public class BeastiaryQuery : GeniePlugin.Interfaces.IPlugin
    {
        private GeniePlugin.Interfaces.IHost m_Host;
        private string sHashValue = string.Empty;

        private void GetJsonFromEPedia(string sValue, bool bCustomAverage)
        {
            string[] separators = { ",", ".", "!", "?", ";", ":", " " };
            string value = sValue;
            
            string sBaseURL = @"https://elanthipedia.play.net/mediawiki/index.php?title=Special:Ask&q={0}&p=format%3Djson&po=%3FMinSkillCap+is%3DMin+Skill%0A%3FMaxSkillCap+is%3DMax+Skill%0A&eq=yes";

            string sQuery = string.Empty;

            string sBaseQuery = "[[Creature {0}::true]]";

            string sSkillBase = "[[MinSkillCap is::<{0}]][[MaxSkillCap is::>{1}]][[MaxSkillCap is::!0]]";

            // Average of Offenses and Defenses.  Will add 100 to this Value to determine the "Max Cap, so it returns a range not too out of scope
            int iDefOffAvg = 0;
            int iDefOffCnt = 0;

            string[] slValues = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in slValues)
            {
                string sLower = s.ToLower();
                switch (sLower)
                {
                    case "shield_usage":
                    case "light_armor":
                    case "chain_armor":
                    case "brigadine":
                    case "plate_armor":
                    case "defending":
                    case "evasion":
                    case "stealth":
                    case "targetted_magic":
                    case "primary_magic":
                    case "debilitation":
                    case "bows":
                    case "brawling":
                    case "crossbows":
                    case "expertise":
                    case "heavy_thrown":
                    case "large_blunt":
                    case "large_edged":
                    case "light_thrown":
                    case "offhand_weapon":
                    case "polearms":
                    case "slings":
                    case "small_blunt":
                    case "small_edged":
                    case "staves":
                    case "twohanded_blunt":
                    case "twohanded_edged":
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

            // If we have a Custom Average, time to Ignore your skillset
            if (bCustomAverage)
            {
                // Average it and add it to the sSkillBase
                iDefOffAvg = int.Parse(sValue);
                sSkillBase = string.Format(sSkillBase, iDefOffAvg.ToString(), (iDefOffAvg * 1.5).ToString());

                sBaseURL = string.Format(sBaseURL, sSkillBase + sQuery);               
            }
            else
            {
                // Average it and add it to the sSkillBase
                iDefOffAvg = iDefOffAvg / iDefOffCnt;
                sSkillBase = string.Format(sSkillBase, iDefOffAvg.ToString(), (iDefOffAvg * 1.5).ToString());

                sBaseURL = string.Format(sBaseURL, sSkillBase + sQuery);

            }

            
            HttpWebRequest wrEPedia = (HttpWebRequest)WebRequest.Create(sBaseURL);
            wrEPedia.Method = WebRequestMethods.Http.Get;
            wrEPedia.Accept = "application/json";

            var response = (HttpWebResponse)wrEPedia.GetResponse();
            var text = string.Empty;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            dynamic jsonTest = JsonConvert.DeserializeObject<dynamic>(text);
            Dictionary<String, dynamic> creatures = JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonTest.results.ToString().Replace("[", "").Replace("]", ""));

            List<Creature> creatureList = new List<Creature>();
            foreach (dynamic creatureJson in creatures.Values)
            {
                String name = creatureJson.fulltext;
                String url = creatureJson.fullurl;
                int min = creatureJson.printouts["Min Skill"];
                int max = creatureJson.printouts["Max Skill"];
                creatureList.Add(new Creature(name, url, min, max));
            }

            // Time to send the Output to the Screen
            // Overriding the ToString output of the Creature Class

            foreach (Creature c in creatureList)
            {
                this.m_Host.SendText(@"#echo >CreatureList " + c.ToString());
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
            this.m_Host = Host;

            this.m_Host.SendText("test");
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
            // Check if we have a bq Command
            if(Text.StartsWith("/bq "))
            {
                GetJsonFromEPedia(Text.Remove(0, 4), false);
            }
            if (Text.StartsWith("/bqt "))
            {
                this.m_Host.SendText("#window add CreatureList");
                this.m_Host.SendText("#window show CreatureList");
                this.m_Host.SendText("#echo >CreatureList @suspend@");

                // Testing the BQ
                GetJsonFromEPedia(Text.Remove(0, 5), true);
            }

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

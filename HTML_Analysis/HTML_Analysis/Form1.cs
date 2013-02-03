using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Tags;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
using System.Text.RegularExpressions;

namespace HTML_Analysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
         
       

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://m.pm2d5.com/pm/benxi.html";
            System.Net.WebClient aWebClient = new System.Net.WebClient();
            aWebClient.Encoding = System.Text.Encoding.Default;
            string html = aWebClient.DownloadString(url);
            string strText = html;

            string[] strUlGroup = GetUlGroup(strText);

            GetUlToday(strUlGroup[0]);
            GetUlToday(strUlGroup[1]);
             
        }

        private void GetUlToday(string strUl)
        {
            string RegexString = @"<li class=\""clear\"">(?<Title>[^<].*?)</li>";
            string[] strLi = GetRegValue(strUl, RegexString, "Title", false);

            RegexString = @"<div(?<Title>[^<].*?)</div>";
            foreach (var t in strLi)
            {
                //string[] strDvi = ((t.Replace(" ","")).Replace(@"><", ">囧<")).Split('囧');
                string[] strDiv = GetRegValue(t, RegexString, "", false);

                RegexString = @"<div.*?>(?<Title>[^<].*?)(</span>)?</div>";
                foreach (var v in strDiv)
                {
                    string[] strC = GetRegValue(t, RegexString, "Title", false);
                }
            }
        }

        private string[] GetUlGroup(string strContent)
        {
            string RegexString = "<ul class=\"list2\">(?<Title>[^<].*?)</ul>";
            return GetRegValue(strContent, RegexString, "Title", false);
        }

        /// <summary>
        /// 正则表达式取值
        /// </summary>
        /// <param name="HtmlCode">源码</param>
        /// <param name="RegexString">正则表达式</param>
        /// <param name="GroupKey">正则表达式分组关键字</param>
        /// <param name="RightToLeft">是否从右到左</param>
        /// <returns></returns>
        public string[] GetRegValue(string HtmlCode, string RegexString, string GroupKey, bool RightToLeft)
        {
            MatchCollection m;
            Regex r;
            if (RightToLeft == true)
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft);
            }
            else
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            m = r.Matches(HtmlCode);
            string[] MatchValue = new string[m.Count];
            for (int i = 0; i < m.Count; i++)
            {
                MatchValue[i] = GroupKey == "" ? m[i].Value : m[i].Groups[GroupKey].Value; ;
            }
            return MatchValue;
        }



    }
}

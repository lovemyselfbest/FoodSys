using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Common
{
    public static class StringUtility
    {
        public static string ConvertToString<T>(IEnumerable<T> source)
        {
            if (source == null || source.Count() <= 0)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append(item).Append(',');
            }
            var result = sb.Remove(sb.Length - 1, 1).ToString();
            return result;
        }

        public static string ArrayJoin(string[] array, string joinStr)
        {
            string str = "";
            for (int i = 0; i < array.Count(); i++)
            {
                if (string.IsNullOrEmpty(array[i]))
                    continue;
                str += array[i] + joinStr;
            }
            return str == "" ? "" : str.Substring(0, str.Length - joinStr.Length);
        }

        /// <summary>
        /// 根据ID获得文件保存路径以及文件名(不带扩展名)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetSavePath(int id, out string fileName)
        {
            string src = string.Format("{0:X8}", id);
            fileName = src.Substring(6, 2);
            return string.Format("/{0}/{1}/{2}/", src.Substring(0, 2), src.Substring(2, 2), src.Substring(4, 2));
        }

        public static string CutString(string source, int length)
        {
            if (source == null || source.Length <= length) return source;
            return source.Substring(0, length) + "...";
        }

        public static string ClearWhiteSpace(string source)
        {
            Regex r = new Regex(@"\s+");
            return r.Replace(source, @" ");
        }

        public static string AddURLParameter(string url, string parameterName, string parameterValue)
        {
            if (url.IndexOf("?") == -1)
                url = url + "?";
            else
                url = url + "&";
            return url + parameterName + "=" + parameterValue;
        }

        public static string StripHTML(string source, int remainLength)
        {
            string temp = StripHTML(source);
            return temp.Length > remainLength ? temp.Substring(0, remainLength) : temp;
        }

        public static string StripHTML(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }

        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        /// <summary>
        /// 判断guid是否为空
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmptyGuid(Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static string EncodeBase64(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        public static string DecodeBase64(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串, 有异常返回 NULL </returns>
        public static string Encrypt(string encryptString)
        {
            return Security.Encrypt(encryptString);
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串</returns>
        /// <param name="keys"></param>
        public static string Decrypt(string decryptString, string keys)
        {
            return Security.Decrypt(decryptString, keys);
        }

        /// <summary>
        /// 将传进来的金额数字字符转化为大写
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ConvertToCnNum(string num)
        {
            Regex regNum = new Regex(@"^([1-9]\d+|[1-9])(\.\d\d?)*$");
            Match matchNum = regNum.Match(num);
            if (matchNum.Success)
            {
                string[] AA = new string[10] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
                string[] BB = new string[8] { "", "拾", "佰", "仟", "萬", "億", "", "" };
                string[] a = Regex.Replace(num, "(^0*)", "").Split('.');
                int k = 0; string re = "";
                for (int i = a[0].Length - 1; i >= 0; i--)
                {
                    char[] ca = a[0].ToCharArray();
                    string aTemp = "";
                    try
                    {
                        aTemp = Convert.ToString(ca[i + 2]);
                    }
                    catch { }
                    string bTemp = "";
                    try
                    {
                        bTemp = Convert.ToString(ca[i + 1]);
                    }
                    catch { }
                    if (aTemp != "0" && aTemp != "" && bTemp == "0")
                    {
                        re = AA[0] + re;
                    }
                    switch (k)
                    {
                        case 0: re = BB[7] + re; break;
                        case 4:
                            Regex regTemp = new Regex(@"0{4}\d{" + (a[0].Length - i - 1) + "}$");
                            Match matchTemp = regTemp.Match(a[0]);
                            if (!matchTemp.Success)
                            {
                                re = BB[4] + re;
                            }
                            break;
                        case 8: re = BB[5] + re; BB[7] = BB[5]; k = 0; break;

                    }
                    char[] caTemp = a[0].ToCharArray();
                    string cTemp = Convert.ToString(caTemp[i]);
                    if (cTemp != "0")
                    {
                        re = AA[Convert.ToInt32(cTemp)] + BB[k % 4] + re;
                    }
                    k++;
                }
                re = re.Replace(@"零萬", "萬零");
                re = re.Replace(@"零億", "億零");
                re = re + "圆";
                if (a.Length > 1)
                {
                    re += BB[6];
                    for (int i = 0; i < a[1].Length; i++)
                    {
                        char[] caTemp = a[1].ToCharArray();
                        string dTemp = Convert.ToString(caTemp[i]);
                        if (i == 0)
                        {
                            re += AA[Convert.ToInt32(dTemp)] + "角";
                        }
                        if (i == 1)
                        {
                            re += AA[Convert.ToInt32(dTemp)] + "分";
                        }
                    }
                }
                else if (re != "") re += "整";
                return re;
            }
            else
            {
                //不是数字
            }
            return "error";
        }
    }
}

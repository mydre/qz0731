using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
namespace WinAppDemo.tools
{
    class SelectUrl
    {

        public static ArrayList separate_url(string s)
        {
            ArrayList slots = new ArrayList();
            Regex re = new Regex(@"https?://[#,&,;,\w,.,/,?,=]+");//使用正则表达式过滤出http或https开头的url链接
            MatchCollection mc;//存放满足条件的字符串
            int i = s.Length;
            int j = i;
            i -= 4;
            while (i >= 0)//对每个以http开头的字符串进行分隔，以便于正则表达式从每一个以http开头的字符串中筛选出符合的url
            {
                if (s[i] != 'h') { i--; continue; }
                if (s.Substring(i, 4) == "http")
                {
                    try
                    {
                        //Console.WriteLine(s.Substring(i,j-i));
                        mc = re.Matches(s.Substring(i, j - i));//i表示截取开始处的下标，j-i表示截取的字符串的长度
                        string sv = mc[0].ToString();//sv是满足正则表达式的url
                        Slot slot = new Slot(i, sv.Length);//新建
                        slots.Add(slot);//保存
                                        //Console.WriteLine(sv);
                        j = i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    
                }
                i--;
            }
            return slots;
        }

        public static void print_msg_or_url(string content, WinAppDemo.tools.RichTextBoxEx richTextBoxEx1)
        {
            ArrayList alist = SelectUrl.separate_url(content);
            int c = alist.Count;//c代表slot的个数
            int cursor = 0;
            //string content = m.Content;
            if (c > 0)
            {
                for (int i = c - 1; i >= 0; i--)//倒着处理，因为separate_url函数计算每个url第一个字符的下标和该url长度时也是倒着来的(这样方便)
                {
                    Slot s = alist[i] as Slot;
                    int index, len;
                    index = s.Index;
                    len = s.Length;

                    int text_index = 0;
                    int text_len = 0;

                    if (cursor != index)//说明cursor指向的是文本的下标
                    {
                        //richTextBoxEx1.AppendText(content.Substring(cursor, index - cursor));//向RichTextBox中插入文本
                        text_index = cursor;
                        text_len = index - cursor;
                        cursor += (index - cursor);//让cursor到达url的第一个字符的下标
                    }
                    //richTextBoxEx1.InsertLink(content.Substring(index, len));//向RichTextBox中插入该url，并将其设置为链接
                    string url = content.Substring(index,len);

                    richTextBoxEx1.InsertLink(content.Substring(text_index,text_len) + "点击访问",url);
                    if(i > 0) richTextBoxEx1.AppendText("\n");

                    cursor += len;//让cursor指向下一个符合语义的字符串
                }
                richTextBoxEx1.AppendText("\n");
            }
            else//说明一个url链接都不存在，那么这个仅仅是包含文本的消息
            {
                richTextBoxEx1.AppendText($"{content}\n");
            }
        }

        public static void print_file(string mPath, WinAppDemo.tools.RichTextBoxEx richTextBoxEx1)
        {
            string path = mPath;
            string real_path = "";
            int len = path.Length;
            int i = 0;
            while (i < len)//凡是路径中出现一个反斜杠的地方，都要变为两个反斜杠(确保在richtextbox中中文不乱码的同时能够给链接添加隐藏的text)
            {
                if (path[i] == '\\') real_path += '\\';
                real_path += path[i];
                i++;
            }
            int index = path.LastIndexOf('\\');
            string file_name = path.Substring(index + 1);

            int dot_index = file_name.IndexOf('.');
            if(dot_index != -1)
            {
                string postFix = file_name.Substring(dot_index + 1);
                string preFix = file_name.Substring(0, dot_index);
                file_name = string.Format("{0}:{1}",postFix,preFix);
            }

            richTextBoxEx1.InsertLink(file_name, real_path);//插入link，第一个参数是文件的名字，第二个参数是文件的路径(第二个参数代表的就是隐藏的text)
            richTextBoxEx1.AppendText("\n");
        }
    }

    class Slot
    {
        int index;
        int length;
        public Slot(int index, int length)
        {
            this.index = index;
            this.length = length;
        }
        public int Index
        {
            get { return index; }
        }
        public int Length
        {
            get { return length; }
        }

    }
}

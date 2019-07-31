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
        public static void print_file(string title,string mPath, WinAppDemo.tools.RichTextBoxEx richTextBoxEx1)
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
            if (dot_index != -1)
            {
                string postFix = file_name.Substring(dot_index + 1);
                string preFix = file_name.Substring(0, dot_index);
                file_name = string.Format("{0}:{1}", postFix, preFix);
            }

            richTextBoxEx1.InsertLink(title, real_path);//插入link，第一个参数是文件的名字，第二个参数是文件的路径(第二个参数代表的就是隐藏的text)
            richTextBoxEx1.AppendText("\n");
        }
        public static void print_MsgOrUrl(string content, int type, WinAppDemo.tools.RichTextBoxEx richTextBoxEx1)
        {
            int startindex, midindex, endindex;
            string word = "";
            string title = "";
            string url = "";
            switch (type)
            {
                case 285212721:    //公众号链接
                    startindex = content.IndexOf("<item.title>");
                    endindex = content.IndexOf("<item.url>");
                    if (startindex >= 0 && endindex > startindex)
                        title = content.Substring(startindex + 12, endindex - startindex - 12);

                    startindex = content.IndexOf("<item1.title>");
                    if (startindex > endindex)
                        url = content.Substring(endindex + 10, startindex - endindex - 10);
                    else
                        url = content.Substring(endindex + 10, content.Length - endindex - 10);

                    richTextBoxEx1.InsertLink(title, url);
                    richTextBoxEx1.AppendText("\n");

                    for (int j = 1; j < 100; j++)
                    {
                        endindex = content.IndexOf("<item" + Convert.ToString(j) + ".url>");

                        if (endindex > startindex)
                            title = content.Substring(startindex + 13, endindex - startindex - 13);
                        else
                            break;
                        startindex = content.IndexOf("<item" + Convert.ToInt16(j + 1) + ".title>");
                        if (startindex > endindex)
                            url = content.Substring(endindex + 11, startindex - endindex - 11);
                        else
                            url = content.Substring(endindex + 11, content.Length - endindex - 11);
                        richTextBoxEx1.InsertLink(title, url);
                        richTextBoxEx1.AppendText("\n");
                    }
                    break;
                case 3:    //朋友圈网页链接
                    startindex = content.IndexOf("【文字】");
                    midindex = content.IndexOf("【标题】");
                    if (startindex >= 0 && midindex > startindex)
                        word = content.Substring(startindex + 4, midindex - startindex - 4);
                    if (word.Length > 0)
                        richTextBoxEx1.AppendText(word+"\n"); 
                    endindex = content.IndexOf("【链接URL】");
                    if (endindex > midindex)
                    {
                        title = content.Substring(midindex + 4, endindex - midindex - 4);
                        url = content.Substring(endindex + 7, content.Length - endindex - 7);
                    }

                    richTextBoxEx1.InsertLink(title, url);
                    richTextBoxEx1.AppendText("\n");    
                    
                    break;
                case 4:    //朋友圈音乐链接
                    startindex = content.IndexOf("【文字】");
                    midindex = content.IndexOf("【音乐平台】");
                    if (startindex >= 0 && midindex > startindex)
                        word = content.Substring(startindex + 4, midindex - startindex - 4);
                    if (word.Length > 0)
                        richTextBoxEx1.AppendText(word + "\n");
                    endindex = content.IndexOf("【链接URL】");
                    if (endindex > midindex)
                    {
                        title = content.Substring(midindex + 6, endindex - midindex - 6);
                        url = content.Substring(endindex + 7, content.Length - endindex - 7);
                        startindex = url.IndexOf("http");
                        url = url.Substring(startindex, url.Length - startindex);
                    }
                    richTextBoxEx1.InsertLink(title, url);
                    richTextBoxEx1.AppendText("\n");
                    break;

                case 15:    //朋友圈视频链接
                    startindex = content.IndexOf("【文字】");
                    endindex = content.IndexOf("【视频URL】");
                    if (startindex >= 0 && endindex > startindex)
                    {
                        title = content.Substring(startindex + 4, endindex - startindex - 4);
                        url = content.Substring(endindex + 7, content.Length - endindex - 7);
                    }

                    richTextBoxEx1.InsertLink(title, url);
                    richTextBoxEx1.AppendText("\n");
                    
                    break;
                case 2:    //朋友圈视频链接
                    startindex = content.IndexOf("【文字】");
                    
                    if (startindex >= 0)
                    {
                        title = content.Substring(startindex + 4, content.Length - startindex - 4);
                        richTextBoxEx1.AppendText(title+"\n");
                    }
                    break;
                case 1:    //朋友圈文字 or 图片
                    startindex = content.IndexOf("【文字】");
                    if (startindex == -1)    //没有文字，只有图片
                    {
                        endindex = content.IndexOf("【图片文件】");

                        if (endindex >= 0)
                        {
                            for (int j = 0; j < 20; j++)
                            {
                                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                                if (startindex > 0)
                                {
                                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                                    if (string.IsNullOrEmpty(url.Replace(" ", ""))) { }
                                    else
                                        print_file(url, url, richTextBoxEx1);
                                    richTextBoxEx1.AppendText("\n");
                                    endindex = startindex;
                                }
                                else
                                {
                                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                                    if (string.IsNullOrEmpty(url.Replace(" ", ""))) { }
                                    else
                                        print_file(url, url, richTextBoxEx1);
                                    richTextBoxEx1.AppendText("\n");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        endindex = content.IndexOf("【图片文件】");

                        if (endindex >= 0)
                        {
                            title = content.Substring(startindex + 4, endindex - startindex - 4);
                            richTextBoxEx1.AppendText(title + "\n");
                            for (int j = 0; j < 20; j++)
                            {
                                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                                if (startindex > 0)
                                {
                                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                                    if (string.IsNullOrEmpty(url.Replace(" ", ""))) { }
                                    else
                                        print_file(url, url, richTextBoxEx1);
                                    richTextBoxEx1.AppendText("\n");
                                    endindex = startindex;
                                }
                                else
                                {
                                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                                    if (string.IsNullOrEmpty(url.Replace(" ", ""))) { }
                                    else
                                        print_file(url, url, richTextBoxEx1);
                                    
                                    richTextBoxEx1.AppendText("\n");
                                    break;
                                }
                            }
                        }
                    }
                    
                    break;
                default:
                    richTextBoxEx1.AppendText(content + "\n");
                    break;
            }


        }
        public static void print_MsgOrUrl(string content, int type, string path, WinAppDemo.tools.RichTextBoxEx richTextBoxEx1)
        {
            int startindex, endindex;
            string title = "";
            string url = "";
            switch (type)
            {
                case 3:                    
                    print_file("图片:" + content, path, richTextBoxEx1);                    
                    break;
                case 49:
                    startindex = content.IndexOf("【标题】：");
                    endindex = content.IndexOf("【url】：");
                    if (startindex >= 0 && endindex > startindex)
                    {
                        title = content.Substring(startindex + 5, endindex - startindex - 5);
                        url = content.Substring(endindex + 6, content.Length - endindex - 6);
                        richTextBoxEx1.InsertLink(title, url);
                        richTextBoxEx1.AppendText("\n");
                    }

                    else
                    {
                        title = content.Substring(startindex + 5, content.Length - startindex - 5);
                        if (path == "")
                            richTextBoxEx1.AppendText( title + "\n");
                        else
                        {
                            if (string.IsNullOrEmpty(path.Replace(" ", ""))) { }
                            else
                                print_file(title, path, richTextBoxEx1);
                        }
                    }
                    break;
                case 34:
                    startindex = content.IndexOf("；");                    
                    title = content.Substring(0, startindex);
                    print_file(title, path, richTextBoxEx1);
                    break;
                case 43:
                    print_file("视频:"+content, path, richTextBoxEx1);
                    break;
                case 48:  //发送定位如何处理？？？       样例：经度：113.807121；纬度:34.794086；位置:创意岛大厦(郑州市金水区)
                    richTextBoxEx1.AppendText(content + "\n");
                    break;
                default:
                    richTextBoxEx1.AppendText(content + "\n");
                    break;
            }
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

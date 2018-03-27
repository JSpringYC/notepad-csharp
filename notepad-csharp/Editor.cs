using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace notepad_csharp
{
    class Editor
    {
        /// <summary>
        /// 当前正在编辑的文件
        /// </summary>
        FileInfo ContentFile { get; set; }
        /// <summary>
        /// 显示文本的组件
        /// </summary>
        TextBox ContentText;
        /// <summary>
        /// 当前正在编辑的文件的内容
        /// </summary>
        string ContentFileText;

        public Editor(TextBox textBox)
        {
            this.ContentText = textBox;
        }

        public bool IsTextChanged()
        {
            var Content = ContentText.Text;

            if(String.IsNullOrEmpty(Content) && String.IsNullOrEmpty(ContentFileText))
            {
                return false;
            }

            return !Content.Equals(ContentFileText);
        }

        public bool Save()
        {
            if(ContentFile == null)
            {
                return false;
            }
            if (IsTextChanged())
            {
                var Content = ContentText.Text;
                File.WriteAllText(ContentFile.FullName, Content);
                ContentFileText = File.ReadAllText(ContentFile.FullName);
                return Content.Equals(ContentFileText);
            }
            return true;
        }

        public bool Save(string path)
        {
            this.ContentFile = new FileInfo(path);

            return Save();
        }

        public bool Open(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            this.ContentFile = new FileInfo(path);
            this.ContentFileText = File.ReadAllText(path);
            this.ContentText.Text = ContentFileText;
            return true;
        }

        public void Clear()
        {
            this.ContentFile = null;
            this.ContentFileText = null;
            this.ContentText.Text = "";
        }

        public string GetName()
        {
            if(ContentFile == null)
            {
                return "无标题";
            }
            else
            {
                return ContentFile.Name;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXTRecognizer.Properties;

namespace TXTRecognizer.Utils
{
    internal class Translate
    {
        public static string Translation(string line)
        {
            switch(Settings.Default.lang)
            {
                case "rus":return rus[line];
                case "us": return eng[line];
                default : return eng[line];
            }
        }

        public static Dictionary<string, string> eng = new Dictionary<string, string>
        {
            ["KazRec"] = "қазақ",
            ["EngRec"] = "English",
            ["RusRec"] = "Русский",
            ["RuLang"] = "Русский",
            ["UsLang"] = "English",
            ["no_img_select"] = "No Image Select",
            ["OpenTextFile"] = "Open...",
            ["RecognizeText"] = "Recognize text",
            ["ExportMain"] = "Export",
            ["ExpTxt"] = "Text file",
            ["ExpPdf"] = "PDF file",
            ["ExpWord"] = "Word file",
            ["LangMenu"] = "Languages",
            ["LangProgramMenu"] = "Program language",
            ["LangRecognizeMenu"] = "Recognition",
            ["DragEnter"] = "Drag picture here",
            ["lblDrop"] = "Drag picture here",
            ["DownloadMoreLang"] = "Download more language...",
            ["about"] = "About...",
        };

        public static Dictionary<string, string> rus = new Dictionary<string, string>
        {
            ["KazRec"] = "қазақ",
            ["EngRec"] = "English",
            ["RusRec"] = "Русский",
            ["RuLang"] = "Русский",
            ["UsLang"] = "English",
            ["no_img_select"] = "Не выбрано изображение",
            ["OpenTextFile"] = "Открыть...",
            ["RecognizeText"] = "Распознать текст",
            ["ExportMain"] = "Экспорт",
            ["ExpTxt"] = "Текстовый файл",
            ["ExpPdf"] = "PDF",
            ["ExpWord"] = "Word",
            ["LangMenu"] = "Языки",
            ["LangProgramMenu"] = "Язык программы",
            ["LangRecognizeMenu"] = "Распознавание",
            ["DragEnter"] = "Перетащите картинку сюда",
            ["lblDrop"] = "Перетащите картинку сюда",
            ["DownloadMoreLang"] = "Скачать больше языков...",
            ["about"] = "О Программе...",
        };
    }
}

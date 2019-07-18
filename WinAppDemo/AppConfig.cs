using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDemo
{
    class AppConfig
    {

        private static AppConfig appConfig = new AppConfig();
        //保存设置的工作路径
        public string working_directory;
        //为添加证据而设置的辅助标志位
        public int caseId_selected_row;
        public int caseId_selected_working;
        //用于记录是否正在进行为案件添加证据的工作
        public bool already_working;
        //正在编辑的案件的案件Id
        public int caseId_Edited;
        //对证据进行编辑的证据的id
        public int proofId_selected;
        //public int selected_UcAjgl_Aj_rowIndex;

        private AppConfig()
        {
            this.working_directory = "";
            this.caseId_selected_row = -1;
            this.caseId_selected_working = -1;
            this.already_working = false;
            this.caseId_Edited = -1;
            this.proofId_selected = -1;
        }

        public static AppConfig getAppConfig()
        {
            if (appConfig == null) appConfig = new AppConfig();
            return appConfig;
        }

        public static void AppConfigAddAdvinceClear()
        {
            appConfig.already_working = false;
            appConfig.caseId_selected_working = -1;
        }
    }
}

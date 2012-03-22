using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CRM.Library.Common
{
    public class Constants
    {
        public const string PORTAL_PERMISSON = "/Portal,/Portal/LocationChart,/Portal/HelpCenter,/Portal/Error";
        public const string EXPORT_DETAIL_HEADER_LEFT = "font-size:9pt;font-family: Arial;font-style: bold;font-weight: 600;background:#cccccc;border-width: .5pt;border-style: solid;border-color: #000000;";
        public const string EXPORT_DETAIL_HEADER_MIDDLE = "font-size:9pt;font-family: Arial;font-style: bold;font-weight: 600;background:#cccccc;border-width: .5pt;border-style: solid solid solid none;border-color: #000000;";
        public const string EXPORT_DETAIL_DATA_LEFT = "font-size:9pt;font-family: Arial;border-width: .5pt;border-style: none solid solid solid;border-color: #000000;";
        public const string EXPORT_DETAIL_DATA_MIDDLE = "font-size:9pt;font-family: Arial;padding-left:5px;border-width: .5pt;border-style: none solid solid none;border-color: #000000;";
        // Mail server
        public const string SMTP_MAIL_SERVER = "smtp3.logigear.com";
        public static readonly string CRM_MAIL_FROM_ADDRESS = ConfigurationManager.AppSettings["from_email"];
        public static readonly string CRM_MAIL_FROM_NAME = ConfigurationManager.AppSettings["from_email_name"];
        public const string PREFIX_EMAIL_LOGIGEAR = "@logigear.com";
          
        //Cookies Name
        public const string COOKIE_PORTAL = "CookiePortal";
        public const string COOKIE_CRM = "CookieCRM";

        public const int ACTIVE_FALSE = 1;
        public const int ACTIVE_TRUE = 0;
        public const string TRUE = "true";
        public const string FALSE = "false";        
        public const bool IS_ACTIVE = true;
        public const bool IS_NOT_ACTIVE = false;
        public const string NODATA = "- N/D -";


        public const string CANDIDATELIST_SORT_ORDER = "candidatelist_sort_order";
        public const string CANDIDATELIST_SORT_NAME = "candidatelist_sort_name";
        // Number of item in auto complete
        public const int AUTO_COMPLETE_ITEMS = 10;
        public const string DATETIME_FORMAT_VIEW = "dd-MMM-yyyy";
        public const string DATETIME_FORMAT_VIEW_FULL = "dd-MMM-yyyy hh:mm tt";
        public const string DATETIME_FORMAT_CONTRACT = "yyyy.MM.dd";
        public const string DATETIME_FORMAT = "dd/MM/yyyy";
        public const string DATETIME_FORMAT_DB = "MM/dd/yyyy";
        public const string UNIQUE_CONTRACT = ".yyyy_MM_dd";
        public const string UNIQUE_TIME = "HH_mm_ss_ffff";
        public const string DATETIME_FORMAT_FULL_LOG = "dd-MMM-yyyy hh:mm";
        public const string DATETIME_FORMAT_JR = "dd/MM/yyyy hh:mm:ss tt";
        public const string DATETIME_FORMAT_FULL = "dd-MMM-yyyy hh:mm:ss tt";
        public const string DATETIME_FORMAT_SR = "dd-MMM-yyyy hh:mm tt";

        public static readonly int REQUESTOR_ID = int.Parse(ConfigurationManager.AppSettings["REQUESTOR_ID"]);
        public static readonly int MANAGER_ID = int.Parse(ConfigurationManager.AppSettings["MANAGER_ID"]);
        public static readonly int HR_ID = int.Parse(ConfigurationManager.AppSettings["HR_ID"]);

        // Job Request
        public const string JOBREQUEST_SPECIAL_ID = "9400";

        public const string CURRENCY_TEXT_VND = "VND";
        public const string CURRENCY_DELIMITER = ",";
        public const string JOB_REQUEST_PREFIX = "JRG-";
        public const string JOB_REQUEST_ITEM_PREFIX = "JR-";
        public const string JR_REQUEST_ID_HOLDER = "[#REQUEST_ID]";
        public const string JR_REQUEST_ITEM_ID_HOLDER = "[#REQUEST_ITEM_ID]";
        public const string JR_REQUEST_ITEM_JOB_TITLE_HOLDER = "[#ITEM_JOB_TITLE]";
        
        public const string JR_REQUEST_DATE_HOLDER = "[#REQUEST_DATE]";
        public const string JR_REQUESTOR_HOLDER = "[#REQUESTOR]";
        public const string JR_ASSIGN_TO_HOLDER = "[#ASSIGN_TO]";
        public const string JR_ASSIGN_TO_NAME_HOLDER = "[#ASSIGN_TO_NAME]";
        public const string JR_DEPARMENT_HOLDER = "[#DEPARMENT]";
        public const string JR_SUBDEPARMENT_HOLDER = "[#SUBDEPARMENT]";
        public const string JR_QUANTITY_HOLDER = "[#QUANTITY]";
        public const string JR_POSITION_HOLDER = "[#POSITION]";
        public const string JR_HISTORY_HOLDER = "[#HISTORY]";
        public const string JR_COMMENTS_HOLDER = "[#COMMENTS]";
        public const string JR_EXPECTED_START_DATE_HOLDER = "[#EXPECTED_START_DATE]";
        public const string JR_SALARY_SUGGESTION_HOLDER = "[#SALARY_SUGGESTION]";
        public const string JR_PROBATION_MONTH_HOLDER = "[#PROBATION_MONTH]";
        public const string JR_JUSTIFICATION_HOLDER = "[#JUSTIFICATION]";        
        public const string JR_STATUS_HOLDER = "[#STATUS]";
        public const string JR_ITEM_STATUS_HOLDER = "[#ITEM_STATUS]";
        
        public const string JR_RESOLUTION_HOLDER = "[#RESOLUTION]";
        public const string JR_LINK_HOLDER = "[#LINK]";
        public const string JR_CANDIDATE = "[#CANDIDATE]";
        public const string JR_EMP_ID = "[#EMP_ID]";
        public const string JR_GENDER = "[#GENDER]";
        public const string JR_ACTUAL_START_DATE = "[#ACTUAL_START_DATE]";
        public const string JR_PROBATION_SALARY = "[#PROBATION_SALARY]";
        public const string JR_PROBATION_SALARY_NOTE = "[#PROBATION_SALARY_NOTE]";
        public const string JR_CONTRACTED_SALARY = "[#CONTRACTED_SALARY]";
        public const string JR_CONTRACTED_SALARY_NOTE = "[#CONTRACTED_SALARY_NOTE]";
        public const string JR_EMPTY_HOLDER = "- N/D -";
        public const string JR_REQUEST_TYPE_HOLDER = "[#REQUEST_TYPE]";
        public static readonly string LOGIGEAR_EMAIL_DOMAIN = ConfigurationManager.AppSettings["logigear_email_domain"];
        public static readonly string DATE_LOCK_PTO = ConfigurationManager.AppSettings["DATE_LOCK_PTO"];
        public static readonly string DATE_LOCK_PTO_HR_VERIFIED = ConfigurationManager.AppSettings["DATE_LOCK_PTO_HR_VERIFIED"];
        public const string PRIVATE_DATA = "******";
        public const int JR_REQUEST_TYPE_NEW = 1;
        public const int JR_REQUEST_TYPE_REPLACE = 2;

        // tan.tran add new 2011.08.11
        public static readonly string PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT = ConfigurationManager.AppSettings["PTO_MANAGER_LIST_NOT_DISPLAY_IN_REPORT"];

        public static List<ListItem> JR_REQUEST_TYPE
        {
            get
            {
                List<ListItem> request = new List<ListItem>();
                request.Add(new ListItem("New", "1"));
                request.Add(new ListItem("Replace", "2"));
                return request;
            }
        }

        //WorkFlow Admin
        public const string JR_ADMIN_NAME = "jr_admin_name";
        public const string JR_ADMIN_WORKFLOW = "jr_admin_workflow";
        public const string JR_ADMIN_GROUP_NAME = "jr_admin_group_name";
        public const string JR_ADMIN_COLUMN = "jr_admin_column";
        public const string JR_ADMIN_ORDER = "jr_admin_order";
        public const string JR_ADMIN_PAGE_INDEX = "jr_admin_index";
        public const string JR_ADMIN_ROW_COUNT = "jr_admin_row_count";
        public const string FIRST_ITEM_WORKFLOW = "-Select Workflow-";
        public const string FIRST_ITEM_GROUP_NAME = "-Select Group Name-";

        public const string SEPARATE_FILE_EXT_SIGN = ",";
        public const char SEPARATE_FILE_EXT_CHAR = ',';
        public const string MANY_POSITION_CHECK = "Many";
        public const string SEPARATE_INVOLVE_SIGN = ",";
        public const char SEPARATE_INVOLVE_CHAR = ',';
        public const string SEPARATE_USER_ADMIN_ID_STRING = "@";
        public const char SEPARATE_USER_ADMIN_ID = '@';
        public const string SEPARATE_USER_ADMIN_USERNAME = ";";
        public const char SEPARATE_CC_LIST = ';';
        public const string MIN_DATE = "1/1/1900";

        public const string JR_EXPORT_EXCEL_NAME = "JobRequestList.xls";
        public const string JR_TILE_EXPORT_EXCEL = "Job Request List";

        public static readonly int STATUS_CLOSE = int.Parse(ConfigurationManager.AppSettings["STATUS_CLOSE"]);
        public static readonly int STATUS_OPEN = int.Parse(ConfigurationManager.AppSettings["STATUS_OPEN"]); 
        public static readonly int RESOLUTION_NEW_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_NEW_ID"]);
        public static readonly int RESOLUTION_CANCEL_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_CANCEL_ID"]);
        public static readonly int RESOLUTION_REJECT_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_REJECT_ID"]);
        public static readonly int RESOLUTION_COMPLETED_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_COMPLETED_ID"]);
        public static readonly int RESOLUTION_TO_BE_APPROVED_BY_HR_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_TO_BE_APPROVED_BY_HR_ID"]);
        public static readonly int RESOLUTION_TO_BE_APPROVED_BY_MANAGER_ID = int.Parse(ConfigurationManager.AppSettings["RESOLUTION_TO_BE_APPROVED_BY_MANAGER_ID"]);
        

        public static readonly int JR_ITEM_STATUS_OPEN = int.Parse(ConfigurationManager.AppSettings["JR_ITEM_STATUS_OPEN"]);
        public static readonly int JR_ITEM_STATUS_CANCEL = int.Parse(ConfigurationManager.AppSettings["JR_ITEM_STATUS_CANCEL"]);
        public static readonly int JR_ITEM_STATUS_SUCCESS = int.Parse(ConfigurationManager.AppSettings["JR_ITEM_STATUS_SUCCESS"]);
                
        //Action Job Request
        public const string JR_ACTION_ADDNEW = "Added New";
        public const string JR_ACTION_REJECT = "Rejected";
        public const string JR_ACTION_APPROVE = "Approved";
        public const string JR_ACTION_COMPLETE = "Closed";
        public const string JR_ACTION_CANCEL = "Cancelled";
        public const string JR_ACTION_SUBMIT = "Submitted";
        public const string JR_ACTION_ASSIGNTO = "Forwarded to";
       // public const string JR_ACTION_HOLD = "Hold";

        public const string JOB_REQUEST_DEFAULT_VALUE = "JR_JobRequest";
        public const string JOB_REQUEST_KEYWORD = "JR_Keyword";
        public const string JOB_REQUEST_DEPARTMENT      = "JR_Department";
        public const string JOB_REQUEST_SUB_DEPARTMENT = "JR_SubDepartment";
        public const string JOB_REQUEST_POSITION_ID = "JR_PositionID";
        public const string JOB_REQUEST_REQUESTOR_ID = "JR_RequestorID";
        public const string JOB_REQUEST_STATUS_ID = "JR_StatusID";
        public const string JOB_REQUEST_REQUEST_TYPE = "JR_RequestType";
        public const string JOB_REQUEST_REQUESTOR_FIRST_ITEM    = "-Select Requestor-";
        public const string JOB_REQUEST_STATUS_FIRST_ITEM       = "-Select Status-";
        public const string JOB_REQUEST_REQUEST_FIRST_ITEM = "-Select Request Type-";
        public const string JOB_REQUEST_TITLE_SELECT = "-Select Title-";

        public const string JOB_REQUEST_COLUMN = "SortColumn";
        public const string JOB_REQUEST_ORDER = "SortOrder";
        public const string JOB_REQUEST_PAGE_INDEX = "PageIndex";
        public const string JOB_REQUEST_ROW_COUNT = "RowCount";
        public const string JOB_REQUEST_ROLE = "JobRequestRole";
        
        // Gender
        public const bool MALE = true;       // male
        public const bool FEMALE = false;     // Female

        //LaborUnion
        public const bool LABOR_UNION_FALSE = false;       // yes
        public const bool LABOR_UNION_TRUE = true;     // no
        //married
        public const bool SINGLE = true;       // yes
        public const bool MARRIED = false;     // no
        // TextBox Search
        public const string JOB_REQUEST = "Request or Sub Request ID";
        public const string USERNAME_OR_USERID = "Username or ID";
        public const string FULLNAME_OR_USERID = "Name or ID";
        public const string USERNAME = "Username";
        public const string GROUPNAME = "Group Name";
        public const string QUESTIONNAME = "Question Content or ID";
        public const string TOPICNAME = "Topic Name";
        public const int ADDED_KEYWORD_LENGTH = 10;  

        //Employee List Resigned Filter
        public const string EMPLOYEE_LIST_RESIGNED_NAME = "employee_list_resigned_name";
        public const string EMPLOYEE_LIST_RESIGNED_DEPARTMENT = "employee_list_resigned_department";
        public const string EMPLOYEE_LIST_RESIGNED_SUB_DEPARTMENT = "employee_list_resigned_sub_department";
        public const string EMPLOYEE_LIST_RESIGNED_JOB_TITLE = "employee_list_resigned_job_title";
        //public const string EMPLOYEE_LIST_RESIGNED_JOB_TITLE_LEVEL = "employee_list_resigned_job_title_level";
        public const string EMPLOYEE_LIST_RESIGNED_COLUMN = "employee_list_resigned_column";
        public const string EMPLOYEE_LIST_RESIGNED_ORDER = "employee_list_resigned_order";
        public const string EMPLOYEE_LIST_RESIGNED_PAGE_INDEX = "employee_list_page_resigned_index";
        public const string EMPLOYEE_LIST_RESIGNED_ROW_COUNT = "employee_list_resigned_row_count";

        //Employee List Active Filter
        public const string EMPLOYEE_LIST_NAME = "employee_list_name";
        public const string EMPLOYEE_LIST_DEPARTMENT = "employee_list_department";
        public const string EMPLOYEE_LIST_FLOOR = "employee_list_floor";
        public const string EMPLOYEE_LIST_PROJECT = "employee_list_project";
        public const string EMPLOYEE_LIST_SUB_DEPARTMENT = "employee_list_sub_department";
        public const string EMPLOYEE_LIST_JOB_TITLE = "employee_list_job_title";
        public const string EMPLOYEE_LIST_JOB_TITLE_LEVEL = "employee_list_job_title_level";
        public const string EMPLOYEE_LIST_COLUMN = "employee_list_column";
        public const string EMPLOYEE_LIST_ORDER = "employee_list_order";
        public const string EMPLOYEE_LIST_PAGE_INDEX = "employee_list_page_index";
        public const string EMPLOYEE_LIST_ROW_COUNT = "employee_list_row_count";        
      
        //Employee
        public const string FIRST_ITEM = "-Choose-";

        public const string FIRST_ITEM_FLOOR = "-Select Floor-";
        public const string FIRST_ITEM_PROJECT = "-Project-";
        public const string FIRST_ITEM_DEPARTMENT = "-Select Department-";
        public const string FIRST_ITEM_SUB_DEPARTMENT = "-Select Sub Department-";
        public const string FIRST_ITEM_JOBTITLE = "-Select Job Title-";
        public const string FIRST_ITEM_JOBTITLELEVEL = "-Select Job Title Level-";
        public const string FIRST_ITEM_CLASS = "-Select Class-";
        public const string FIRST_ITEM_STATUS = "-Select Status-";
        public const string FIRST_ITEM_RESULT = "-Select Result-";
        public const string FIRST_ITEM_UNIVERSITY = "-Select University-";
        public const string EMPLOYEE_EXPORT_EXCEL_NAME = "EmployeeList.xls";
        public const string EMPLOYEE_TILE_EXPORT_EXCEL = "Employee List";
        public const string EMPLOYEE_RESIGN_EXPORT_EXCEL_NAME = "ResignedEmployeeList.xls";
        public const string EMPLOYEE_RESIGN_TILE_EXPORT_EXCEL = "Resigned Employee List";

        public const string FIRST_ITEM_BRANCH = "-Select Branch-";
        public const string FIRST_ITEM_OFFICE = "-Select Office-";
        // Calendar icon
        public const string CALENDAR_ICON_PATH = "/Content/Images/ExtraIcons/calendar.gif";

        // Employee upload root
        public const string CV_PATH = "/FileUpload/CVFiles/";
        public const string IMAGE_PATH = "/FileUpload/Images/";
        public const string CONTRACT_PATH = "/FileUpload/ContractRenewal/";         
        public static readonly int CV_MAX_SIZE = int.Parse(ConfigurationManager.AppSettings["CV_MAX_SIZE"]);
        public static readonly string CV_EXT_ALLOW = ConfigurationManager.AppSettings["CV_EXT_ALLOW"];
        public static readonly string CONTRACT_EXT_ALLOW = ConfigurationManager.AppSettings["CONTRACT_EXT_ALLOW"];
        public static readonly string CONTRACT_EXT_NOT_ALLOW = ConfigurationManager.AppSettings["CONTRACT_EXT_NOT_ALLOW"];
        public static readonly int IMAGE_MAX_SIZE = int.Parse(ConfigurationManager.AppSettings["IMAGE_MAX_SIZE"]);
        public static readonly string IMAGE_EXT_ALLOW = ConfigurationManager.AppSettings["IMAGE_EXT_ALLOW"];
		public const string ADDRESS = "Street";
        public const string AREA = "Ward";
        public const string DISTRICT = "Dist.";
        public const string CITYPROVINCE = "City/Prov";

        public const string VN_ADDRESS = "Đường";
        public const string VN_AREA = "Khu vực";
        public const string VN_DISTRICT = "Quận/huyện";
        public const string VN_CITYPROVINCE = "Tỉnh/Tp";

        //Employee Status
        public const string EMPLOYEE_DEFAULT_VALUE = "Employee";        
        public const int EMPLOYEE_ACTIVE = 1;
        public const int EMPLOYEE_NOT_ACTIVE = 0;
        public const int LEAVE_OR_ABSENCE = 3;
        public const int RESIGNED = 4;
        public const int CONTRACTTED = 1;
        public const int PROBATION = 2;
        public const string CV_FILE = "CV_file";
        public const string CONTRACT_FILE = "Contract";

        public const string ROW_ACTIVE_COLOR = "#00b050";

        //Training Certification Master
        public const int TRAINING_CERTIFICATION_MASTER_ACTIVE = 1;
        public const int TRAINING_CERTIFICATION_MASTER_NOT_ACTIVE = 0;
        public const string TRAINING_CERTIFICATION_MASTER_PAGE_TITLE = "Training Certification List";
        public const string TRAINING_CERTIFICATION_MASTER_SEARCH_NAME = "Certification Name";
        public const string TRAINING_CERTIFICATION_MASTER_CERTIFICATION_NAME = "training certification master certification name";
        public const string TRAINING_CERTIFICATION_MASTER_LIST_NAME = "training certification master list name";
        public const string TRAINING_CERTIFICATION_MASTER_LIST_COLUMN = "ttraining_certification_master_list_column";
        public const string TRAINING_CERTIFICATION_MASTER_LIST_ORDER = "training_certification_master_list_order";
        public const string TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX = "training_certification_master_list_page_index";
        public const string TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT = "training_certification_master_list_row_count";
        public const string TRAINING_CERTIFICATION_EXPORT_EXCEL_NAME = "TrainingCertification.xls";
        public const string TRAINING_CERTIFICATION_TITLE_EXPORT_EXCEL = "TRAINING CERTIFICATION MASTER LIST";


        //Training Employee Certification
        public const int TRAINING_EMPLOYEE_CERTIFICATION_ACTIVE = 1;
        public const int TRAINING_EMPLOYEE_CERTIFICATION_NOT_ACTIVE = 0;
        public const string TRAINING_EMPLOYEE_CERTIFICATION_PAGE_TITLE = "Training Employee Certification List";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_NAME = "training employee certification name";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_NAME = "training employee certification list name";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN = "training_employee_certification_list_column";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER = "training_employee_certification_list_order";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX = "training_employee_certification_list_page_index";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT = "training_employee_certification_list_row_count";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_EXCEL_NAME = "TrainingEmployeeCertification.xls";
        public const string TRAINING_EMPLOYEE_CERTIFICATIONTITLE_EXPORT_EXCEL = "TRAINING EMPLOYEE CERTIFICATION LIST";

        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_PAGE_TITLE = "Training Nonemployee Certification";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_SEARCH_NAME = "Nonemployee Name or Phone or Year or Class";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST_NAME = "Training certification nonemployee name";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST_COLUMN = "training_nonemployee_certification_list_column";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST_ORDER = "training_nonemployee_certification_list_order";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX = "training_nonemployee_certification_page_index";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST_ROW_COUNT = "training_nonemployee_certification_list_row_count";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_EXPORT_EXCEL_NAME = "TrainingNonEmployeeCertification.xls";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_TITLE_EXPORT_EXCEL = "TRANING NONEMPLOYEE CERTIFICATION LIST";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_TYPE = "- Select Certification Type -";
        //Hang
        public const string TRAINING_CENTER_CLASS_ENGLISSKILL_LABLE = "--Select English Skill--";
        public const string TRAINING_CENTER_CLASS_VERBAL_LABLE = "--Select Verbal Skill--";

        //hang
        public const string FIRST_ITEM_ENGLISHSKILL = "-Select English Skill-";
        public const string FIRST_ITEM_VERBALLSKILL = "-Select Verbal Skill-";
        public const string TRAINING_CENTER_UPDATE_ATTENDEE_PRIORITY = "Priority";
        //HANG
        public const string TRAINING_CENTER_PROFESSIONAL_ENGLISHSKILL = "TRAINING_CENTER_PROFESSIONAL_ENGLISHSKILL";
        public const string TRAINING_CENTER_PROFESSIONAL_VERBALSKILL = "TRAINING_CENTER_PROFESSIONAL_VERBALSKILL";


        public const string TC_TC_PROFESSIONAL_ENGLISHSKILL = "TC_TC_PROFESSIONAL_ENGLISHSKILL";
        public const string TC_TC_PROFESSIONAL_VERBALSKILL = "TC_TC_PROFESSIONAL_VERBALSKILL";
        public static readonly int TRAINING_CENTER_CLASS_RESULT_HIGH_PRIORITY =
           ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_HIGH_PRIORITY"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_NORMAL_PRIORITY =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_NORMAL_PRIORITY"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_LOW_PRIORITY =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_LOW_PRIORITY"]);

        //Training Employee Certification List DDL
        //public const string TRAINING_EMPLOYEE_CERTIFICATION_FIRST_ITEM_JOBTITLE = "--JobTitle--";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_FIRST_ITEM_MANAGER = "-Manager-";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_FIRST_ITEM_CERTIFICATION = "-Certification-";

        //Training Employee Certification text search
        public const string TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME = "ID or Name";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_JOBTITLE = "training employee certification list jobtitle";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER = "training employee certification list manager";
        public const string TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION = "training employee certification list certification";

        //Contract Renewal
        public const int PROBATION_CV = 1;
        public const int EXTENDED_PROBATION = 2;
        public const int FIRST_YEAR_CONTRACT = 3;
        public const int SECOND_YEAR_CONTRACT = 4;
        public const int PERMANENT_CONTRACT = 5;
        public const string DATE_MIN = "01/01/1900";

        //Question: added by DuyTai.Nguyen
        public static readonly int QUESTION_LENGTH_TO_TRUNCATE = int.Parse(ConfigurationManager.AppSettings["QUESTION_LENGTH_TO_TRUNCATE"]);
        public static readonly int QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE = int.Parse(ConfigurationManager.AppSettings["QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE"]);
        public static readonly int MAX_NUMBER_OF_ANSWERS = int.Parse(ConfigurationManager.AppSettings["MAX_NUMBER_OF_ANSWERS"]);
        public static readonly int MIN_NUMBER_OF_ANSWERS = int.Parse(ConfigurationManager.AppSettings["MIN_NUMBER_OF_ANSWERS"]);
        public static readonly int MIN_NUMBER_OF_QUESTIONS_IN_TOPIC = int.Parse(ConfigurationManager.AppSettings["MIN_NUMBER_OF_QUESTIONS_IN_TOPIC"]);
        public static readonly int MIN_NUMBER_OF_QUESTIONS_IN_PARAGRAPH = int.Parse(ConfigurationManager.AppSettings["MIN_NUMBER_OF_QUESTIONS_IN_PARAGRAPH"]);
        public static readonly int REPEAT_TIMES_MIN = 1;
        public static readonly int REPEAT_TIMES_MAX = 10;
        public static readonly int REPEAT_TIMES_DEFAULT = 3;
        public const string PREFIX_QUESTION_ID = "q";
        public const string PREFIX_TOPIC_ID = "t";
        public const string PREFIX_PARAGRAPH_ID = "p";
        public const string UNIQUEID_STRING_FORMAT = "MMddyyyyhhmmss";
        public const string SOUND_FOLDER = "/FileUpload/LOT/SoundFile/";
        public const int DETAILS_POPUP_WIDTH = 800;
        public const int FILE_NAME_MAX_LENGTH = 100;
        public const int TOPIC_NAME_MAX_LENGTH = 50;
        public const int ANSWER_CONTENT_MAX_LENGTH = 255;
        public const string SOUND_FILE_NOT_EXIST_MESSAGE = "The sound file does not exist !";
        public const string NO_FILE_ERROR = "No file";
        public const string DIV_MESSAGE_FORMAT = "<div id=\"systemmessage\" class=\"{0}\" style=\"display:{1}\">{2}</div>";
        public const int TEXTBOX_KEYWORD_MAX_LENGTH = 100;
        public const string CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE = "inline-block";

        //Annual Holiday: added by Tai Nguyen
        public const string ANNUAL_HOLIDAY_NAME = "Holiday Name";
        public const string ANNUAL_HOLIDAY_LIST_TITLE_EXPORT_EXCEL = "Annual Holiday List";
        public const string ANNUAL_HOLIDAY_LIST_FILE_NAME_PREFIX = "AnnualHolidayList";
        public const int ANNUAL_HOLIDAY_EXCEL_FILE_CELL_PADDING = 2;
        public const int ANNUAL_HOLIDAY_EXCEL_FILE_CELL_SPACING = 1;
        public const int ANNUAL_HOLIDAY_NAME_MAX_LENGTH = 255;
        public const int PTO_BALANCE_MAX_LENGTH = 3;
        public const string FIRST_ITEM_YEAR = "-Select a year-";  

        //PTOAdmin
        public static readonly int PTO_DEFAULT_FILTER_STATUS = int.Parse(ConfigurationManager.AppSettings["PTO_DEFAULT_FILTER_STATUS"]);
        public const string PTO_ADMIN_EMPLOYEE_NAME = "Employee Name";
        public const string PTO_ADMIN_TITLE_EXPORT_EXCEL = "PTO list";
        public const string PTO_ADMIN_EXPORT_EXCEL_FILE_NAME = "PTO_List_";
        //User Logs
        public const string PTO_USER_LOG_NAME = "pto_user_log_name";
        public const string PTO_USER_LOG_DATE = "pto_user_log_date";
        public const string PTO_USER_LOG_COLUMN = "pto_user_log_column";
        public const string PTO_USER_LOG_ORDER = "pto_user_log_order";
        public const string PTO_USER_LOG_PAGE_INDEX = "pto_user_log_page_index";
        public const string PTO_USER_LOG_ROW_COUNT = "pto_user_log_row_count";

        public enum ModelType : int
        {
            Question = 1,
            ListeningTopic = 2,
            ComprehensionParagraph = 3
        }
        // Constant Nationality
        public static List<ListItem> Nationality
        {
            get{
                List<ListItem> nations = new List<ListItem>();
                nations.Add(new ListItem("Viet Nam", "Viet Nam"));
                nations.Add(new ListItem("USA", "USA"));
                nations.Add(new ListItem("Philippines", "Philippines"));
                return nations;
            }                        
        }
        public static List<ListItem> VnNationality
        {
            get
            {
                List<ListItem> nations = new List<ListItem>();
                nations.Add(new ListItem("Việt Nam", "Việt Nam"));
                nations.Add(new ListItem("Mỹ", "Mỹ"));
                return nations;
            }
        }
        public static List<string> Venue
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Deming Corner");
                list.Add("Eagle Eye");
                list.Add("Hackers");
                list.Add("Neumann Corner");
                list.Add("The Matrix");
                list.Add("Transformers");
                list.Add("Wall - E");
                list.Add("Turning room");
                list.Add("Room 7.4, floor 7, Danabook bld, 76-78 Bach Dang, Hai Chau dist., Da Nang");
                return list;
            }
        }

        // Constant MarriedStatus
        public static List<ListItem> MarriedStatus
        {
            get
            {
                List<ListItem> marriedStatus = new List<ListItem>();
                marriedStatus.Add(new ListItem("Single", "True"));
                marriedStatus.Add(new ListItem("Married", "False"));
                return marriedStatus;
            }
        }

        // Constant Gender
        public static List<ListItem> Gender
        {
            get
            {
                List<ListItem> gender = new List<ListItem>();
                gender.Add(new ListItem("Male", "True"));
                gender.Add(new ListItem("Female", "False"));
                return gender;
            }
        }   

        // Contract of employee
        public static readonly int NOTIFICATION_DAYS = int.Parse(ConfigurationManager.AppSettings["CONTRACT_RENEWAL_NOTIFICATION_DAYS"]);
        //public const int NOTIFICATION_DAYS = 30; // 7 days before to alarm

        //User Log
        public const string INSERT = "Insert";
        public const string UPDATE = "Update";
        public const string DELETE = "Delete";
        public const string EMPLOYEE = "Employee";
        public const string CONTRACT = "Contract";
        public const string KEY_ATTACH_FILE = "AttachFile";
        public const string KEY_PRIVATE_SUGGESTION_SALARY = "SalarySuggestion";
        public const string KEY_PRIVATE_PROBATION_SALARY = "ProbationSalary";
        public const string KEY_PRIVATE_CONTRACTED_SALARY = "ContractedSalary";

        //History
        public const string DEPARTMENT = "Department";
        public const string SUBDEPARTMENT = "SubDepartment";
        public const string JOBTITLE = "JobTitle";

        //Jobtitle List Filter
        public const string JOB_TITLE_NAME = "job_title_name";
        public const string JOB_TITLE_DEPARTMENT = "job_title_department";
        public const string JOB_TITLE_COLUMN = "job_title_column";
        public const string JOB_TITLE_ORDER = "job_title_order";
        public const string JOB_TITLE_PAGE_INDEX = "job_title_page_index";
        public const string JOB_TITLE_ROW_COUNT = "job_title_row_count";


        //Jobtitle Level List Filter
        public const string JOB_TITLE_LEVEL_NAME = "job_title_level_name";
        public const string JOB_TITLE_LEVEL_SELECTION = "job_title_level_selection";
        public const string JOB_TITLE_LEVEL_COLUMN = "job_title_level_column";
        public const string JOB_TITLE_LEVEL_ORDER = "job_title_level_order";
        public const string JOB_TITLE_LEVEL_PAGE_INDEX = "job_title_level_page_index";
        public const string JOB_TITLE_LEVEL_ROW_COUNT = "job_title_level_row_count";

        //Group
        public const string GROUP_NAME = "group_name";
        public const string GROUP_COLUMN = "group_column";
        public const string GROUP_ORDER = "group_order";
        public const string GROUP_PAGE_INDEX = "group_page_index";
        public const string GROUP_ROW_COUNT = "group_row_count";

        //Account
        public const string ACCOUNT_NAME = "account_name";
        public const string ACCOUNT_GROUP_ID = "account_group";
        public const string ACCOUNT_COLUMN = "account_column";
        public const string ACCOUNT_ORDER = "account_order";
        public const string ACCOUNT_PAGE_INDEX = "account_page_index";
        public const string ACCOUNT_ROW_COUNT = "account_row_count";

        //User Logs
        public const string USER_LOG_NAME = "user_log_name";
        public const string USER_LOG_DATE = "user_log_date";
        public const string USER_LOG_COLUMN = "user_log_column";
        public const string USER_LOG_ORDER = "user_log_order";
        public const string USER_LOG_PAGE_INDEX = "user_log_page_index";
        public const string USER_LOG_ROW_COUNT = "user_log_row_count";

        //Annual Hodiday
        public const string ANNUAL_HOLIDAY_TEXT = "ANNUAL_HOLIDAY_TEXT";
        public const string ANNUAL_HOLIDAY_YEAR = "ANNUAL_HOLIDAY_YEAR";
        public const string ANNUAL_HOLIDAY_COLUMN = "ANNUAL_HOLIDAY_COLUMN";
        public const string ANNUAL_HOLIDAY_ORDER = "ANNUAL_HOLIDAY_ORDER";
        public const string ANNUAL_HOLIDAY_PAGE_INDEX = "ANNUAL_HOLIDAY_PAGE_INDEX";
        public const string ANNUAL_HOLIDAY_ROW_COUNT = "ANNUAL_HOLIDAY_ROW_COUNT";


        //PTO Admin
        public const string PTO_ADMIN_TEXT = "PTO_ADMIN_TEXT";
        public const string PTO_ADMIN_STATUS = "PTO_ADMIN_STATUS";
        public const string PTO_ADMIN_TYPE_PARENT = "PTO_ADMIN_TYPE_PARENT";
        public const string PTO_ADMIN_TYPE = "PTO_ADMIN_TYPE";
        public const string PTO_ADMIN_MONTH = "PTO_ADMIN_MONTH";
        public const string PTO_ADMIN_COLUMN = "PTO_ADMIN_COLUMN";
        public const string PTO_ADMIN_ORDER = "PTO_ADMIN_ORDER";
        public const string PTO_ADMIN_PAGE_INDEX = "PTO_ADMIN_PAGE_INDEX";
        public const string PTO_ADMIN_ROW_COUNT = "PTO_ADMIN_ROW_COUNT";

        //PTO Report
        public const string PTO_REPORT_TEXT = "PTO_REPORT_TEXT";
        public const string PTO_REPORT_DEPARTMENT = "pto_report_text";
        public const string PTO_REPORT_PROJECT = "pto_report_project";
        public const string PTO_REPORT_MONTH = "PTO_REPORT_MONTH";
        public const string PTO_REPORT_COLUMN = "PTO_REPORT_COLUMN";
        public const string PTO_REPORT_ORDER = "PTO_REPORT_ORDER";
        public const string PTO_REPORT_PAGE_INDEX = "PTO_REPORT_PAGE_INDEX";
        public const string PTO_REPORT_ROW_COUNT = "PTO_REPORT_ROW_COUNT";

        //PTO Confirm
        public const string PTO_CONFIRM_TEXT = "PTO_CONFIRM_TEXT";
        public const string PTO_CONFIRM_MANAGER = "PTO_CONFIRM_MANAGER";
        public const string PTO_CONFIRM_TYPE = "PTO_CONFIRM_TYPE";
        public const string PTO_CONFIRM_COLUMN = "PTO_CONFIRM_COLUMN";
        public const string PTO_CONFIRM_ORDER = "PTO_CONFIRM_ORDER";
        public const string PTO_CONFIRM_PAGE_INDEX = "PTO_CONFIRM_PAGE_INDEX";
        public const string PTO_CONFIRM_ROW_COUNT = "PTO_CONFIRM_ROW_COUNT";


        //PR_List        
        public const string PER_REVIEW_TEXT = "PER_REVIEW_TEXT";
        public const string PER_REVIEW_LOGIN_ROLE = "PER_REVIEW_LOGIN_ROLE";
        public const string PER_REVIEW_DEPARTMENT_LIST = "PER_REVIEW_DEPARTMENT_LIST";
        public const string PER_REVIEW_STATUS_LIST = "PER_REVIEW_STATUS_LIST";
        public const string PER_REVIEW_COLUMN = "PER_REVIEW_COLUMN";
        public const string PER_REVIEW_ORDER = "PER_REVIEW_ORDER";
        public const string PER_REVIEW_PAGE_INDEX = "PER_REVIEW_PAGE_INDEX";
        public const string PER_REVIEW_ROW_COUNT = "PER_REVIEW_ROW_COUNT";


        //Exam
        public const string EXAM_TEXT = "EXAM_TEXT";
        public const string EXAM_QUESTION = "EXAM_QUESTION";
        public const string EXAM_DATE_FROM = "EXAM_DATE_FROM";
        public const string EXAM_DATE_TO = "EXAM_DATE_TO";
        public const string EXAM_COLUMN = "EXAM_COLUMN";
        public const string EXAM_ORDER = "EXAM_ORDER";
        public const string EXAM_PAGE_INDEX = "EXAM_PAGE_INDEX";
        public const string EXAM_ROW_COUNT = "EXAM_ROW_COUNT";
        public const string EXAM_NONE_VERBAL_SECTION = "EXAM_NONE_VERBAL_SECTION";

        //Question
        public const string QUESTION_TEXT = "QUESTION_TEXT";
        public const string QUESTION_SECTION = "QUESTION_SECTION";
        public const string QUESTION_COLUMN = "QUESTION_COLUMN";
        public const string QUESTION_ORDER = "QUESTION_ORDER";
        public const string QUESTION_PAGE_INDEX = "QUESTION_PAGE_INDEX";
        public const string QUESTION_ROW_COUNT = "QUESTION_ROW_COUNT";
        public const string FIRST_ITEM_SECTION = "-Select a section-";

        //Export
        public const string ROW_HEADER = "font-family: Arial;font-size: 15px;font-weight: bold;color: #006699;border-color: #FFFFFF";
        public const string ROW_SUB_HEADER = "font-family: Arial;font-size: 15px;color: #000000;";
        public const string ROW_TITLE = "height:20px;font-family: Arial;font-size: 13px;font-weight: bold;color: #000000;background-color: #C0C0C0;border:.5pt solid black;vertical-align: middle;text-align: center;";
        public const string ROW_CSS = "height:25px;font-family: Arial;font-size: 10px;font-weight: normal;color: #000000;border:.5pt solid black;vertical-align: middle;";

        //Candidate update by Tuan.Minh.Nguyen
        public const string CANDIDATE_DEFAULT_VALUE = "Candidate";
        public const string CANDIDATE_NAME = "Candidate name or email";
        public const string CANDIDATE_SOURCE = "-Source-";
        public const string PHOTO_PATH_ROOT_CANDIDATE = "/FileUpload/Candidate/Images/";
        public const string CV_PATH_ROOT_CANDIDATE = "/FileUpload/Candidate/CVFiles/";
        public const string EXCEL_EXPORT_PATH_CANDIDATE = "~/Views/Candidate/CandidateList.xls";
        public const string CANDIDATE_EXPORT_EXCEL_NAME = "CandidateList.xls";
        public const string CANDIDATE_TILE_EXPORT_EXCEL = "CANDIDATE LIST";

        // Constant CandidateStatus
        public static List<ListItem> CandidateStatusList
        {
            get
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("Available",((int)CandidateStatus.Available).ToString()));
                list.Add(new ListItem("Unavailable", ((int)CandidateStatus.Unavailable).ToString()));
                
                return list;
            }
        }

        //Candidate Filter 
        public const string CANDIDATE_LIST_NAME = "candidate_list_name";
        public const string CANDIDATE_LIST_SOURCE = "candidate_list_source";
        public const string CANDIDATE_LIST_JOB_TITLE = "candidate_list_job_title";
        public const string CANDIDATE_LIST_FROM_DATE = "candidate_list_from_date";
        public const string CANDIDATE_LIST_TO_DATE = "candidate_list_to_date";
        public const string CANDIDATE_LIST_COLUMN = "candidate_list_column";
        public const string CANDIDATE_LIST_ORDER = "candidate_list_order";
        public const string CANDIDATE_LIST_PAGE_INDEX = "candidate_list_page_index";
        public const string CANDIDATE_LIST_ROW_COUNT = "candidate_list_row_count";
        public const string CANDIDATE_LIST_STATUS = "candidate_list_status";
        public const string CANDIDATE_LIST_UNIVERSITY = "candidate_list_university";
        public const string CANDIDATE_EXAM_LIST = "candidate_exam_list";
        public const string CANDIDATE_BACK_TO_EXAM_URL = "BackToExamURL";
        public const string CANDIDATE_LIST_INTER = "ListInter";

        public static List<ListItem> GetCandidateStatus
        {
            get
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("Available",((int)CandidateStatus.Available).ToString()));
                list.Add(new ListItem("Unavailable", ((int)CandidateStatus.Unavailable).ToString()));
                list.Add(new ListItem("Failed", ((int)CandidateStatus.Failed).ToString()));
                list.Add(new ListItem("Interviewing", ((int)CandidateStatus.Interviewing).ToString()));
                list.Add(new ListItem("Passed", ((int)CandidateStatus.Passed).ToString()));
                list.Add(new ListItem("Waiting", ((int)CandidateStatus.Waiting).ToString()));
                
                return list;
            }
        }
         
        //Common update by Tuan.Minh.Nguyen.
        public const string UPLOAD_PHOTO_SUCCESS = "Upload Photo successfully.";
        public const string UPLOAD_TEMP_PATH = "/FileUpload/TempUpload/";

        //upload mp3 file
        public const string UPLOAD_SOUND_FILE_SUCCESS = "Mp3 file is uploaded successfully.";
        public static readonly string SOUND_FILE_EXT_ALLOW = ConfigurationManager.AppSettings["SOUND_FILE_EXT_ALLOW"];
        public static readonly int SOUND_FILE_MAX_SIZE = int.Parse(ConfigurationManager.AppSettings["SOUND_FILE_MAX_SIZE"]);


        //STT
        public const string STT_DEFAULT_VALUE = "STT";
        public const int STT_RESULT_PASS = 1;
        public const int STT_RESULT_FAIL = 2;
        public const int STT_STATUS_OJT = 1;
        public const int STT_STATUS_IN_CLASS = 2;
        public const int STT_STATUS_ON_LEAVE = 3;
        public const int STT_STATUS_NEED_TO_PROMOTED = 4;
        public const int STT_STATUS_PROMOTED = 5;
        public const int STT_STATUS_REJECTED = 6;
        public const string PERFORMANCE_REVIEW_PATH = "~/FileUpload/PerformanceReview/";
        public const string STT_RESULT_PATH = "~/FileUpload/STT/Result/";
        public const string STT_PHOTO_PATH_ROOT = "/FileUpload/STT/Images/";
        public const string STT_CV_PATH_ROOT = "~/FileUpload/STT/CVFiles/";
        public const string STT_EXCEL_EXPORT_PATH = "~/Views/STT/STTList.xls";
        public const string STT_EXPORT_EXCEL_NAME = "STTList.xls";
        public const string STT_TILE_EXPORT_EXCEL = "STT List";

        //STT Filter
        public const string STT_LIST_NAME = "stt_list_name";
        public const string STT_LIST_CLASS = "stt_list_class";
        public const string STT_LIST_STATUS = "stt_list_status";
        public const string STT_LIST_RESULT = "stt_list_result";
        public const string STT_LIST_STARTDATE_FROM = "stt_list_startdate_from";
        public const string STT_LIST_STARTDATE_TO = "stt_list_startdate_to";
        public const string STT_LIST_FROMDATE_FROM = "stt_list_fromdate_from";
        public const string STT_LIST_FROMDATE_TO = "stt_list_fromdate_to";
        public const string STT_LIST_COLUMN = "stt_list_column";
        public const string STT_LIST_ORDER = "stt_list_order";
        public const string STT_LIST_PAGE_INDEX = "stt_list_page_index";
        public const string STT_LIST_ROW_COUNT = "stt_list_row_count";

        // Asset Category
        public const string ASSET_CATEGORY_LIST = "asset_category_list";
        public const string ASSET_CATEGORY_PAGE_TITLE = "Asset Category list";
        public const string ASSET_CATEGORY_SEARCH_TEXT = "asset_category_search_text";
        public const string ASSET_CATEGORY_NAME_OR_DESCRIPTION = "Name or Description";
        public const string ASSET_CATEGORY_NON_PARENT = "Un-Category";

        // Asset property        
        public const string ASSET_PROPERTY_SEARCH_TEXT = "asset_property_search_text";
        public const string ASSET_PROPERTY_PAGE_TITLE = "Asset Property List";
        public const string ASSET_PROPERTY_CATEGORY_LIST = "CategoryId";
        public const string ASSET_PROPERTY_DEFAULT_SEARCH_TEXT = "Property Name or Master Data";
        public const string ASSET_PROPERTY_DEFAULT_CATEGORY = "- Category -";
        public const string GRID_COLUMN = "grid_column";
        public const string GRID_ORDER = "grid_order";
        public const string GRID_PAGE_INDEX = "grid_page_index";
        public const string GRID_ROW_COUNT = "grid_row_count";

        public const string ASSET_PROPERTY_MODULE_LABEL = "--Module--";
        public const string ASSET_PROPERTY_PERMISSION_LABEL = "--Permission--";
        public const string ASSET_PROPERTY_MODULE_ID_PREFIX = "apModule_";
        public const string ASSET_PROPERTY_PERMISSION_ID_PREFIX = "apPermission_";
        public const string ASSET_PROPERTY_MODULE_NAME = "apModule";
        public const string ASSET_PROPERTY_PERMISSION_NAME = "apPermission";

        //file name view
        public static int FILE_NAME_VIEW = 20;
        public static char FILE_CHAR_PREFIX = ':';
        public static string FILE_STRING_PREFIX = ":";

        // import form excel file to DB
        public const string IMPORT_EMS = "/FileUpload/Import/";
        public const string IMPORT_READER_DATA = "/FileUpload/Import/";

        #region son.kim
        /// <summary>
        /// Get Hour List
        /// </summary>
        /// <returns>List</returns>
        public static List<ListItem> HourList
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < 24; i++)
                {
                    items.Add(new ListItem
                    {
                        Text = (i.ToString().Length == 1? "0" + i.ToString(): i.ToString()),
                        Value = i.ToString()
                    });
                }

                return items;
            }
        }

        /// <summary>
        /// Get Minute List
        /// </summary>
        /// <returns>List</returns>
        public static List<ListItem> MinuteList
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < 50; i+=15)
                {
                    items.Add(new ListItem
                    {
                        Text = (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString()),
                        Value = i.ToString()
                    });
                }

                return items;
            }
        }

        /// <summary>
        /// max of interview
        /// </summary>
        public const int MAX_INTERVIEW = 3;
        public const string T_INTERVIEW_STATUS = "-Interview status-";
        public const string T_INTERVIEWED_BY = "-Interviewed by-";
        public const string INTERVIEW_EXPORT_EXCEL_NAME = "InterviewList.xls";
        public const string INTERVIEW_TILE_EXPORT_EXCEL = "Interview List";
        public const string INTERVIEW_HISTORY_EXPORT_EXCEL_NAME = "Interview History";
        public const string INTERVIEW_HISTORY_TILE_EXPORT_EXCEL = "History Interview List";
        public const string DATETIME_FORMAT_TIME = "HH:mm dd-MMM-yyyy";

        //INTERVIEW Filter
        public const string INTERVIEW_LIST_NAME = "interview_list_name";
        public const string INTERVIEW_LIST_INTERVIEWER = "interview_list_interviewer";
        public const string INTERVIEW_LIST_STATUS = "interview_list_status";
        public const string INTERVIEW_LIST_RESULT = "interview_list_result";
        public const string INTERVIEW_LIST_DATE_FROM = "interview_list_date_from";
        public const string INTERVIEW_LIST_DATE_TO = "interview_list_date_to";
        public const string INTERVIEW_LIST_COLUMN = "interview_list_column";
        public const string INTERVIEW_LIST_ORDER = "interview_list_order";
        public const string INTERVIEW_LIST_PAGE_INDEX = "interview_list_page_index";
        public const string INTERVIEW_LIST_ROW_COUNT = "interview_list_row_count";

        public const string INTERVIEW_LIST_HISTORY_NAME = "interview_list_history_name";
        public const string INTERVIEW_LIST_HISTORY_POSITION = "interview_list_history_position";
        public const string INTERVIEW_LIST_HISTORY_SOURCE = "interview_list_history_source";
        public const string INTERVIEW_LIST_HISTORY_RESULT = "interview_list_history_result";
        public const string INTERVIEW_LIST_HISTORY_DATE_FROM = "interview_list_history_date_from";
        public const string INTERVIEW_LIST_HISTORY_DATE_TO = "interview_list_history_date_to";
        public const string INTERVIEW_LIST_HISTORY_COLUMN = "interview_list_history_column";
        public const string INTERVIEW_LIST_HISTORY_ORDER = "interview_list_history_order";
        public const string INTERVIEW_LIST_HISTORY_PAGE_INDEX = "interview_list_history_page_index";
        public const string INTERVIEW_LIST_HISTORY_ROW_COUNT = "interview_list_history_row_count";
        public const string INTERVIEW_LIST_ACTION = "interview_action";
        public const string INTERVIEW_LIST_INDEX = "index";
        public const string INTERVIEW_LIST_HISTORY = "history";

        public const string INTERVIEW_ROUND_1 = "1st Round-General interview";
        public const string INTERVIEW_ROUND_2 = "2nd Round-Technicall Professional interview";
        public const string INTERVIEW_ROUND_3 = "3rd Round-Final interview";
        public const string INTERVIEW_RESULT_TEMPLATE_1 = "INT-1";
        // Manipulation
        public enum ManipulateEnum
        {
            Insert,
            Update
        }

        public enum InterviewResult
        { 
            Null = 0,
            Passes = 1,
            Fails =2,
            WaitingList = 3,
            Recruit = 4,
            Absent = 5
        }

        #endregion
        //==>Efrom Constants
        public const string INTERVIEW_FORM_CODE = "INT";

        /* Person Type Enum for EForm */
        public enum PersonType : int
        {
            Candidate = 1,
            STT = 2,
            Employee = 3
        }
        /* Interview Email*/
        public static string CONTENT_ID = "$id$";
        public static string CONTENT_HOST = "$host$";
        public static string CONTENT_DATE = "$date$";
        public static string CONTENT_TIME = "$time$";
        public static string CONTENT_LOCATION = "$location$";
        public static string CONTENT_CANDIDATE = "$candidate$";
        public static string CONTENT_POSITION = "$positon$";
        public static string CONTENT_INTERVIEW = "$interview$";
        public static string HTML_TEMPLATE_PATH_INTERVIEW_MEETING = "~/Content/HtmlTemplate/Interview_MeetingRq.htm";
        public static string HTML_TEMPLATE_PATH_CANDIDATE_MAIL = "~/Content/HtmlTemplate/Interview_Candidate_email.html";
        public static string HTML_TEMPLATE_PATH_FAIL_CANDIDATE_MAIL = "~/Content/HtmlTemplate/Fail_Candidate_email.html";
        public static string HTML_TEMPLATE_PATH_PASS_CANDIDATE_MAIL = "~/Content/HtmlTemplate/Pass_Candidate_email.html";
        public static string HTML_TEMPLATE_PATH_PASSED_CANDIDATE_MAIL = "~/Content/HtmlTemplate/Passed_Candidate_email.html";        
        

        //Home - User Statistic Filter : tan.tran 2010.12.01
        public const string SELECT_USER_ADMIN = "- Select User Admin -";

        public const string HOME_STATISTIC_USER_ADMIN = "home_statistic_user_admin";
        public const string HOME_STATISTIC_FROM_DATE = "home_statistic_from_date";
        public const string HOME_STATISTIC_TO_DATE = "home_statistic_to_date";
        public const string HOME_STATISTIC_COLUMN = "home_statistic_column";
        public const string HOME_STATISTIC_ORDER = "home_statistic_order";
        public const string HOME_STATISTIC_ROW_COUNT = "home_statistic_row_count";
        public const string HOME_STATISTIC_PAGE_INDEX = "home_statistic_page_index";


        public const int WORK_FLOW_JOB_REQUEST = 1;
        public const int WORK_FLOW_PURCHASE_REQUEST = 2;
        public const int WORK_FLOW_PERFORMANCE_REVIEW = 3;
        public const int WORK_FLOW_PURCHASE_REQUEST_US = 4;
        //public const int WORK_FLOW_SERVICE_REQUEST = 4;

        public const string SORT_ASC = "asc";
        public const string SORT_DESC = "desc";

        public const string CTL_LINK_CANDIDATE_DETAIL = "/Exam/CandidateTestDetail/?id={0}&urlback=/Exam/CandidateTestList/{1}";
        public const string CTL_UPDATE_WRITING_MARK = "Update writing mark";
        public const string CTL_UPDATE_PROGRAMING_SKILL_REMARK = "Update Programming Mark";
        public const string CTL_UPDATE_TC_BUG_SKILL_MARK = "Update TC coverage and Bug report Test set Mark";
        public const string CTL_UPDATE_VERBAL_MARK = "Update Verbal Mark";
        public const string CTL_EMAIL_TITLE_DIALOG = "Send email to candidate";
        public const string CTL_EXCEL_BUTTON_TITLE = "Export result to excel";
        public const string CTL_EMAIL_SUBJECT = "LogiGear Vietnam - Thank You Letter";
        public static string HTML_EMAIL_TEMPLATE_PATH = "~/Content/HtmlTemplate/ThankEmailTemplate.htm";
        public const int CTL_EMAIL_DIALOG_WIDTH = 900;
        public const string CTL_SENDER_EMAIL_ADDRESS = "hr@logigear.com";
        public const string CTL_SENDER_NAME = "HR - Logigear VietNam";
        public const string SAMPLE_AUTO_COMPLETE = "<span style='color:#888'>Example: <b><i>huy.ly</i></b></span>";
        public const string SAMPLE_PTO_AUTO_COMPLETE = "<span style='color:#888;font-size:10px'>You can search by <b><i>'Anh Nguyen', 'Nguyen Anh', 'anh.nguyen' or '1001'</i></b></span>";

        public const int ROUND_NUMBER = 2;

        /*Full name format*/
        public enum FullNameFormat : int
        { 
            FirstMiddleLast = 1, //FirstName + MiddleName + LastName
            FirstLastMiddle = 2, //FirstName + LastName + MiddleName
            LastMiddleFirst = 3, //LastName + MiddleName + FirstName
            LastFirstMiddle = 4, //LastName + FirstName + MiddleName
            MiddleFirstLast = 5, //MiddleName + FirstName + LastName
            MiddleLastFirst = 6  //MiddleName + LastName + FirstName
        }
        /* Purchase Request */
        /// <summary>
        /// Get Minute List
        /// </summary>
        /// <returns>List</returns>
        public static List<ListItem> SaleTax
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("No Sale Tax", "0"));
                items.Add(new ListItem("TAX US (9.25%)", "1"));
                items.Add(new ListItem("TAX VN (10%)", "2"));
                return items;
            }
        }

        public static readonly float TAX_US = float.Parse(ConfigurationManager.AppSettings["TAX_US"]);
        public static readonly float TAX_VN = float.Parse(ConfigurationManager.AppSettings["TAX_VN"]);

        #region Service Request
        public const string SR_LIST_SETTING_INDEX = "SR_LIST_SETTING_INDEX";
        public const string SR_LIST_SETTING_TEXT = "SR_LIST_SETTING_TEXT";
        public const string SR_LIST_SETTING_BRANCH = "SR_LIST_SETTING_BRANCH";
        public const string SR_LIST_SETTING_OFFICE = "SR_LIST_SETTING_OFFICE";
        public const string SR_LIST_SETTING_PROJECT = "SR_LIST_SETTING_PROJECT";
        public const string SR_LIST_SETTING_COLUMN = "SR_LIST_SETTING_COLUMN";
        public const string SR_LIST_SETTING_ORDER = "SR_LIST_SETTING_ORDER";
        public const string SR_LIST_SETTING_PAGE_INDEX = "SR_LIST_SETTING_PAGE_INDEX";
        public const string SR_LIST_SETTING_ROW = "SR_LIST_SETTING_ROW";

        public const int SR_SEND_MAIL_COMMENT = 2;
        public const int SR_SEND_MAIL_DEFAULT = 1; 
        #endregion

        #region Purchase Request

        public const string FIRST_ITEM_SALE_TAX = "-Sale Tax-";
        public const string BILLABLETOCLIENT = "Yes";
        public const int TYPE_MONEY_USD = 1;
        public const int TYPE_MONEY_VND = 2;
        public const int TYPE_PAYMENT_TRANFER = 2;
        public const int TYPE_PAYMENT_CASH = 1;
        public const string TYPE_PAYMENT_TRANFER_STRING = "Transfer";
        public const string TYPE_PAYMENT_CASH_STRING = "Cash";
        public const string TYPE_MONEY_VND_STRING = "VND";
        public const string TYPE_MONEY_USD_STRING = "USD";

        public const string FIRST_ITEM_CHOOSE = "- Choose -";


        public static int URGENT_VALUE = 1;
        public static List<ListItem> PURCHASE_REQUEST_PRIORITY
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Urgent", "1"));
                return items;
            } 
        }


        //Define Status
        public static readonly int PR_RESOLUTION_NEW_ID = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_NEW_ID"]);
        // tan.tran add new 2011.06.28
        public static readonly int PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID"]);

        public static readonly int PR_RESOLUTION_COMPLETE_ID = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_COMPLETE_ID"]);
        public static readonly int PR_RESOLUTION_NOT_COMPLETE = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_NOT_COMPLETE"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING"]);
        public static readonly int PR_RESOLUTION_CANCEL = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_CANCEL"]);
        public static readonly int PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING"]);
        public static readonly int PR_RESOLUTION_REJECT = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_REJECT"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVED_BY_MANAGER = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVED_BY_MANAGER"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVED_BY_VP_OF_SALES = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVED_BY_VP_OF_SALES"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVED_BY_PRESIDENT_CEO = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVED_BY_PRESIDENT_CEO"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVAL_APPROVE = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVAL_APPROVE"]);
        public static readonly int PR_RESOLUTION_TO_BE_APPROVAL_REJECT = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_APPROVAL_REJECT"]);
        public static readonly int PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING"]);
        public static readonly int PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR"]);
        public static readonly int PR_RESOLUTION_TO_BE_PROCESSED = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_PROCESSED"]);        
        public static readonly int PR_RESOLUTION_SEND_BILL_TO_CUSTOMER = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_SEND_BILL_TO_CUSTOMER"]);
        public static readonly int PR_RESOLUTION_RECEIVE_PAY_MENT = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_RECEIVE_PAY_MENT"]);
        public static readonly int PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING_IMMEDIATELY = int.Parse(ConfigurationManager.AppSettings["PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING_IMMEDIATELY"]);
        //Define Role
        public static readonly int PR_REQUESTOR_ID = int.Parse(ConfigurationManager.AppSettings["PR_REQUESTOR_ID"]);
        public static readonly int PR_PURCHASING_ID = int.Parse(ConfigurationManager.AppSettings["PR_PURCHASING_ID"]);
        public static readonly int PR_SR_MANAGER_ID = int.Parse(ConfigurationManager.AppSettings["PR_SR_MANAGER_ID"]);
        public static readonly int PR_MANAGER_ID = int.Parse(ConfigurationManager.AppSettings["PR_MANAGER_ID"]);
        public static readonly int PR_VP_OF_SALES_ID = int.Parse(ConfigurationManager.AppSettings["PR_VP_OF_SALES_ID"]);
        public static readonly int PR_PRESIDENT_CEO_ID = int.Parse(ConfigurationManager.AppSettings["PR_PRESIDENT_CEO_ID"]);
        public static readonly int PR_US_ACCOUNTING = int.Parse(ConfigurationManager.AppSettings["PR_US_ACCOUNTING"]);


        public static readonly int PR_REQUESTOR_ID_US = int.Parse(ConfigurationManager.AppSettings["PR_REQUESTOR_ID_US"]);
        public static readonly int PR_PURCHASING_ID_US = int.Parse(ConfigurationManager.AppSettings["PR_PURCHASING_ID_US"]);
        public static readonly int PR_DEPARTMENT_HEAD = int.Parse(ConfigurationManager.AppSettings["PR_DEPARTMENT_HEAD"]);
        public static readonly int PR_CORORATE_CONTROLLER = int.Parse(ConfigurationManager.AppSettings["PR_CORORATE_CONTROLLER"]);
        public static readonly int PR_CEO = int.Parse(ConfigurationManager.AppSettings["PR_CEO"]);

        public const string PR_REQUEST_PREFIX = "PR-";
        public const string PURCHASE_REQUEST = "Input keyword...";
        public const string PURCHASE_REQUEST_DEFAULT_VALUE = "PR_Request";
        public const string PURCHASE_REQUEST_KEYWORD = "PR_Keyword";
        public const string PURCHASE_REQUEST_DEPARTMENT = "PR_Department";
        public const string PURCHASE_REQUEST_SUB_DEPARTMENT = "PR_SubDepartment";
        public const string PURCHASE_REQUEST_REQUESTOR_ID = "PR_RequestorID";
        public const string PURCHASE_REQUEST_ASSIGN_ID = "PR_AssignID";
        public const string PURCHASE_REQUEST_STATUS_ID = "PR_StatusID";
        public const int PURCHASE_REQUEST_JUSTIFICATION_MAX_LENGTH = 25;
        public const string PURCHASE_REQUEST_REQUESTOR_FIRST_ITEM = "-Select Requestor-";
        public const string PURCHASE_REQUEST_ASSIGN_FIRST_ITEM = "-Forward To-";
        public const string PURCHASE_REQUEST_STATUS_FIRST_ITEM = "-Select Status-";
        public const string PURCHASE_REQUEST_BILLABLE_FIRST_ITEM = "-Select Billable-"; //chau.ly add 13/02/2012
        public const string PURCHASE_REQUEST_APPROVAL_MAN = "-Select User-";
        public const string PURCHASE_REQUEST_APPROVAL_ROLE = "-Select Role-";
        public const string PURCHASE_REQUEST_RESOLUTION_LABEL = "-Select Resolution-";
        public const string PURCHASE_REQUEST_PRIORITY_LABEL = "-Select Priority-";
        public const string PURCHASE_REQUEST_RESOLUTION_ID = "PR_ResolutionID";
        public const string PURCHASE_REQUEST_BILLABLE = "Billable"; //chau.ly add 13/02/2012
        public const string PURCHASE_REQUEST_SUBMIT_FROM_DATE_KEY = "Requested From";
        public const string PURCHASE_REQUEST_SUBMIT_TO_DATE_KEY = "Requested To";
        public const string PURCHASE_REQUEST_REPORT_FROM_DATE = "report_from_date";
        public const string PURCHASE_REQUEST_REPORT_TO_DATE = "report_to_date";

        public const string PURCHASE_REQUEST_DEFAULT_VALUE_US = "PR_Request_Us";
        public const string PURCHASE_REQUEST_KEYWORD_US = "PR_Keyword_Us";
        public const string PURCHASE_REQUEST_DEPARTMENT_US = "PR_Department_Us";
        public const string PURCHASE_REQUEST_SUB_DEPARTMENT_US = "PR_SubDepartment_Us";
        public const string PURCHASE_REQUEST_REQUESTOR_ID_US = "PR_RequestorID_Us";
        public const string PURCHASE_REQUEST_ASSIGN_ID_US = "PR_AssignID_Us";
        public const string PURCHASE_REQUEST_STATUS_ID_US = "PR_StatusID_Us";
        public const int PURCHASE_REQUEST_JUSTIFICATION_MAX_LENGTH_US = 25;
        public const string PURCHASE_REQUEST_RESOLUTION_ID_US = "PR_ResolutionID_Us";
        public const string PURCHASE_REQUEST_REPORT_FROM_DATE_US = "report_from_date_Us";
        public const string PURCHASE_REQUEST_REPORT_TO_DATE_US = "report_to_date_Us";

        public const string EXCHANGE_RATE_KEYWORD = "EXCHANGE_RATE";

        public const string PURCHASE_REQUEST_COLUMN = "PR_SortColumn";
        public const string PURCHASE_REQUEST_ORDER = "PR_SortOrder";
        public const string PURCHASE_REQUEST_PAGE_INDEX = "PR_PageIndex";
        public const string PURCHASE_REQUEST_ROW_COUNT = "PR_RowCount";

        public const string PURCHASE_REQUEST_COLUMN_US = "PR_SortColumn_Us";
        public const string PURCHASE_REQUEST_ORDER_US = "PR_SortOrder_Us";
        public const string PURCHASE_REQUEST_PAGE_INDEX_US = "PR_PageIndex_Us";
        public const string PURCHASE_REQUEST_ROW_COUNT_US = "PR_RowCount_Us";

        public const string PR_EXPORT_EXCEL_NAME = "PurchaseRequestList.xls";
        public const string PR_TILE_EXPORT_EXCEL = "Purchase Request List";

        public const string PR_REPORT_EXPORT_EXCEL_NAME = "PurchaseRequestReportList.xls";
        public const string PR_REPORT_TILE_EXPORT_EXCEL = "Purchase Request Report List";

        public const string PR_ASSIGN_TO_HOLDER = "[#ASSIGN_TO]";
        public const string PR_REQUEST_ID_HOLDER = "[#REQUEST_ID]";
        public const string PR_REQUEST_DATE_HOLDER = "[#REQUEST_DATE]";
        public const string PR_EXPECTED_DATE_HOLDER = "[#EXPECTED_DATE]";
        public const string PR_REQUESTOR_HOLDER = "[#REQUESTOR]";
        public const string PR_DEPARMENT_HOLDER = "[#DEPARTMENT]";
        public const string PR_SUBDEPARMENT_HOLDER = "[#SUB_DEPARTMENT]";
        public const string PR_JUSTIFICATION_HOLDER = "[#JUSTIFICATION]";
        public const string PR_FORWARD_TO_NAME_HOLDER = "[#FORWARD_TO_NAME]";
        public const string PR_FORWARD_TO_HOLDER = "[#FORWARD_TO]";
        public const string PR_STATUS_HOLDER = "[#STATUS]";
        public const string PR_RESOLUTION_HOLDER = "[#RESOLUTION]";
        public const string PR_LINK_HOLDER = "[#LINK]";
        public const string PR_BILLABLE_TO_CLIENT_HOLDER = "[#BILLABLE_TO_CLIENT]";
        public const string PR_HISTORY_HOLDER = "[#HISTORY]";
        public const string PR_COMMENTS_HOLDER = "[#COMMENTS]";
        public const string PR_TEMPLATE_MAIL_PATH = "~/Views/PurchaseRequest/PRTemplateMail.htm";
        public const string PR_TEMPLATE_MAIL_PATH_US = "~/Views/PurchaseRequestUS/PRTemplateMail.htm";
        public const string PR_TEMPLATE_PRINT_PATH = "~/Views/PurchaseRequest/PRTemplatePrint.htm";

        //Action Purchase Request
        public const string PR_ACTION_ADDNEW = "Created New";
        public const string PR_ACTION_REJECT = "Rejected";
        public const string PR_ACTION_CLOSE = "Closed";
        public const string PR_ACTION_FORWARDTO = "Forwarded";

        //US Purchase Request Resolution
        public static readonly int PRUS_RESOLUTION_NEW = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_NEW"]);
        public static readonly int PRUS_RESOLUTION_WAITING_FOR_DEPT_HEAD_APPROVAL = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_WAITING_FOR_DEPT_HEAD_APPROVAL"]);
        public static readonly int PRUS_RESOLUTION_APPROVED = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_APPROVED"]);
        public static readonly int PRUS_RESOLUTION_REJECTED_TO_REQUESTOR = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_REJECTED_TO_REQUESTOR"]);
        public static readonly int PRUS_RESOLUTION_COMPLETED = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_COMPLETED"]);
        public static readonly int PRUS_RESOLUTION_CANCELLED = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_CANCELLED"]);
        public static readonly int PRUS_RESOLUTION_NOT_COMPLETE = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_NOT_COMPLETE"]);
        public static readonly int PRUS_RESOLUTION_WAITING_FOR_CORP_CTRLER_APPROVAL = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_WAITING_FOR_CORP_CTRLER_APPROVAL"]);
        public static readonly int PRUS_RESOLUTION_WAITING_FOR_CEO_APPROVAL = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_WAITING_FOR_CEO_APPROVAL"]);
        public static readonly int PRUS_RESOLUTION_TO_BE_PROCESSED = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_TO_BE_PROCESSED"]);
        public static readonly int PRUS_RESOLUTION_REJECTED_TO_DEPT_HEAD = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_REJECTED_TO_DEPT_HEAD"]);
        public static readonly int PRUS_RESOLUTION_REJECTED_TO_CORP_CTRLER = int.Parse(ConfigurationManager.AppSettings["PRUS_RESOLUTION_REJECTED_TO_CORP_CTRLER"]);
        public static readonly int PR_MAX_APPROVAL_US = int.Parse(ConfigurationManager.AppSettings["PR_MAX_APPROVAL_US"]);
        #endregion

        #region LOT

        public const char SEPARATE_IDS_CHAR = ',';            

        //Added by Huydeny 23-Dec-2010        
        //only FrontEnd
        public const string LOT_COOKIE_CANDIDATE_EXAM_ID = ".LOTCEID";
        public const string LOT_KEY_ERROR_DETAILS = ".LOTERROR";
        public const string LOT_COOKIE_TIME = ".LOTTIME";                
        public const string LOT_FORMAT_LEFT_TIME = "00:00";
        public const string LOT_CURRENT_SECTION = "CurrentSection";
        public const string LOT_SECTION_DATA = "SectionData";
        public const string LOT_SECTION_DESC = "SectionDescription";
        public const string LOT_EXAM_CANDIDATE_LIST_EXPORT_EXCEL = "ExamCandidateList";
        public const string LOT_EXAM_CANDIDATE_LIST_TITLE_EXPORT_EXCEL = "Exam Candidate List";
        public const string LOT_EXAM_CANDIDATE_PIN_EXPORT_EXCEL = "ExamCandidatePIN";
        public const string LOT_SEPARATOR_FOR_SAVE_DATA = "[=====LOTSEPARATOR=====]";
        //hard code section id, remember re-config when change DB
        public static readonly int LOT_LISTENING_QUESTION = int.Parse(ConfigurationManager.AppSettings["LOT_LISTENING_QUESTION"]);
        public static readonly int LOT_COMPREHENSION_QUESTION_ID = int.Parse(ConfigurationManager.AppSettings["LOT_COMPREHENSION_QUESTION_ID"]);
        public static readonly string LOT_TAG_NAME_ALLOWED = ConfigurationManager.AppSettings["LOT_TAG_NAME_ALLOWED"];
        public const int LOT_MULTIPLE_CHOICE_QUESTION = 2;
        public const int LOT_SENTENCE_CORRECTION_QUESTION = 5;
        public const int LOT_COMPREHENSION_SKILL_ID = 6;//int.Parse(ConfigurationManager.AppSettings["LOT_COMPREHENSION_SKILL_ID"]);
        public const int LOT_WRITING_SKILL_ID = 13;//int.Parse(ConfigurationManager.AppSettings["LOT_WRITING_SKILL_ID"]);
        public const int LOT_LISTENING_TOPIC_ID = 14;//int.Parse(ConfigurationManager.AppSettings["LOT_LISTENING_TOPIC_ID"]);
        public const int LOT_PROGRAMMING_SKILL_ID = 20;//int.Parse(ConfigurationManager.AppSettings["LOT_PROGRAMMING_SKILL_ID"]);
        public const int LOT_TECHNICAL_SKILL_ID = 22;
        public const int LOT_TC_COVERAGE_AND_BUG_REPORT_TEST_ID = 32;
        public const int LOT_CRITICAL_THINKING_ID = 16;
        public const int LOT_VERBAL_SKILL_ID = 21;

        public const int LOT_VERBAL_SECTION_TYPE_ID = 2;

        //public const int LOT_IT_TEST_ID = 29;
        //public const int LOT_ADVANCED_TEST_ID = 30;
        //public const int LOT_LOGICAL_TEST_ID = 31;
        //public const int LOT_TESTING_TEST_ID = 32;
        //public const int LOT_SQL_TEST_ID = 33;

        //public const int LOT_VERBAL_LEVEL_ID = 38;
        //public const int LOT_VERBAL_TOEIC_ID = 43;

        public const string NOT_AVAILABLE = "n/a";

        public const int LOT_VERBAL_MAX_MARK = 200;
        public const int LOT_MAX_RANDOM_TIMES = 5;
        public const string LOT_VERBAL_MARK_TYPE_LABEL = "--Type--";
        public const string LOT_VERBAL_LEVEL_LABEL = "--Level--";
        public const int LOT_VERBAL_MARK_TYPE_LEVEL = 0;
        public const int LOT_MARK_TYPE_TOEIC_SKILL = 1;
        public const int LOT_MARK_TYPE_TOEIC_VERBAL = 2;
        // Chau.ly add
        public const int LOT_SECTION_ENGLISH_TYPE = 1;
        public const int LOT_SECTION_VERBAL_TYPE = 2;
        public const int LOT_SECTION_VERBAL_TOEIC_ID = 43;
        public const int LOT_SECTION_VERBAL_LEVEL_ID = 38;
        public const int LOT_SECTION_TECHNICAL_TYPE = 3;
        // Linh.Quang.Le added
        public const int LOT_DEFAULT_MAX_MARK = 100;
        public const string LOT_CREATED_BY_SYSTEM = "System";
        public const string LOT_UPDATED_BY_SYSTEM = "System";
        public const string LOT_MARKED_BY_SYSTEM = "System";
        public const string LOT_SYSTEM_COMMENT = "Created by System";

        public const string LOT_VERBAL_MARK_STRING = "Level";

        public const string FIRST_ITEM_EXAM_QUESTION = "-Select Exam Question-";
        public const string EXAM_TITLE = "Exam Title or Candidate Name";
        
        public const string EXAM_ID = "ExamId";
        public const string EXAM_NAME = "ExamName";
        public const string EXAM_DATE = "ExamDate";
        public const string EXAM_LIST_QUESTION = "ExamList";

        public const int WRITTING_MARK_NULL = -1;
        public const string LOT_SENT_MAIL = "Sent";

        public const int LOT_CANDIDATE_EXAM_ID = 1;
        public const string LOT_CANDIDATE_EXAM_NAME = "Candidate";
        public const int LOT_EMPLOYEE_EXAM_ID = 2;
        public const string LOT_EMPLOYEE_EXAM_NAME = "Employee";

        public const string JOB_TITLE = "Job Title";
        public const string JOB_TITLE_lEVEL = "Job Title Level";
        public const int JOB_TITLE_LEVEL_STT_ID = 66;
        public const string FIRST_LEVEL = "-Choose Level-";
        public const string LOT_PROGRAMMING_TYPE_LABEL = "--Select Type--";
        /// <summary>
        /// List of Random section
        /// </summary>
        public static List<int> LOT_RandomList
        {
            get
            {
                List<int> items = new List<int>();
                items.Add(LOT_MULTIPLE_CHOICE_QUESTION);
                items.Add(LOT_SENTENCE_CORRECTION_QUESTION);
                items.Add(LOT_COMPREHENSION_SKILL_ID);               
                return items;
            }
        }

        #region Job Title Level

        public static List<ListItem> JobTitleLevel
        {
            get
            {
                List<ListItem> level = new List<ListItem>();
                for (int i = 1; i <= 7; i++)
                {
                    level.Add(new ListItem(i.ToString(),i.ToString()));
                }
                return level;
            }
        }

        public const string JTL_JOB_TITLE_LEVEL_LIST = "Job Title Level";
        public const string JTL_JOB_TITLE_LEVEL_FILE_NAME = "JobTitleLevel.xls";

        #endregion

        public const string LOT_SECTIONID_STARTWITH = "Section_";

        #endregion

        #region Performance Review

        public const string PRW_EMPLOYEE_LIST = "Performance Review Employee";
        public const string PRW_EMPLOYEE_FILE_NAME = "PerformanceReviewEmployee.xls";
        public static readonly int PRW_ROLE_PL = int.Parse(ConfigurationManager.AppSettings["PRW_PL/SUPERVISOR_ID"]);
        public static readonly int PRW_ROLE_MANAGER_ID = int.Parse(ConfigurationManager.AppSettings["PRW_MANAGER_ID"]);
        public static readonly int PRW_ROLE_HR_ID = int.Parse(ConfigurationManager.AppSettings["PRW_HR_ID"]);
        public static readonly int PRW_ROLE_EMPLOYEE_ID = int.Parse(ConfigurationManager.AppSettings["PRW_EMPLOYEE_ID"]);
        public static readonly int PRW_ROLE_EM_ID = int.Parse(ConfigurationManager.AppSettings["PRW_EM/DIRECTOR_ID"]);
        public const int ROLE_EMPLOYEE = 13;
        public const string PR_EMPLOYEE_ACTION_AGREED = "Agreed";
        public const string PR_EMPLOYEE_ACTION_DIDNT_AGREED = "Did't Agree";
        public static readonly int PRW_RESOLUTION_COMPLETE_ID = int.Parse(ConfigurationManager.AppSettings["PRW_RESOLUTION_COMPLETE_ID"]);        
        public static readonly int PRW_RESOLUTION_CANCEL = int.Parse(ConfigurationManager.AppSettings["PRW_RESOLUTION_CANCEL"]);
        public static readonly int PRW_RESOLUTION_TO_BE_APPROVED = int.Parse(ConfigurationManager.AppSettings["PRW_RESOLUTION_TO_BE_APPROVED"]);
        //INTERVIEW Filter
        public const string PRW_HR_LIST_NAME = "pr_hr_list_name";
        public const string PRW_HR_LIST_STATUS = "pr_hr_list_status";
        public const string PRW_HR_LIST_NEED = "pr_hr_list_need";
        public const string PRW_HR_LIST_COLUMN = "pr_hr_list_column";
        public const string PRW_HR_LIST_ORDER = "pr_hr_list_order";
        public const string PRW_HR_LIST_PAGE_INDEX = "pr_hr_list_page_index";
        public const string PRW_HR_LIST_ROW_COUNT = "pr_hr_list_row_count";

        //Manager Filter
        public const string PRW_MANAGER_LIST_NAME = "pr_MANAGER_list_name";
        public const string PRW_MANAGER_LIST_STATUS = "pr_MANAGER_list_status";
        public const string PRW_MANAGER_LIST_NEED = "pr_MANAGER_list_need";
        public const string PRW_MANAGER_LIST_COLUMN = "pr_MANAGER_list_column";
        public const string PRW_MANAGER_LIST_ORDER = "pr_MANAGER_list_order";
        public const string PRW_MANAGER_LIST_PAGE_INDEX = "pr_MANAGER_list_page_index";
        public const string PRW_MANAGER_LIST_ROW_COUNT = "pr_MANAGER_list_row_count";
        #endregion

        #region PTO
        public const string PTO_PREFIX = "PTO";
        public const string PTO_STRING_PREFIX = "-";
        public const char PTO_CHAR_PREFIX = '-';
        public const string PTO_PARENT_FIRST_TYPE = "- Select PTO Type -";
        public const string PTO_FIRST_TYPE = "- Select PTO Category -";
        public const string PTO_FIRST_STATUS = "- Select Status -";
        public const string FIRST_MANAGER = "- Select Manager -";
        public const string PTO_TYPE = "PTO_Type";
        public const string PTO_STATUS = "PTO_Status";         
        public const string PTO_EMPLOYEE_LIST_STATUS = "pto_emp_list_status";
        public const string PTO_EMPLOYEE_LIST_TYPE_PARENT = "pto_emp_list_result_parent";
        public const string PTO_EMPLOYEE_LIST_TYPE = "pto_emp_list_result";
        public const string PTO_EMPLOYEE_LIST_DATE_FROM = "pto_emp_list_date_from";
        public const string PTO_EMPLOYEE_LIST_DATE_TO = "pto_emp_list_date_to";
        public const string PTO_EMPLOYEE_LIST_COLUMN = "pto_emp_list_column";
        public const string PTO_EMPLOYEE_LIST_ORDER = "pto_emp_list_order";
        public const string PTO_EMPLOYEE_LIST_PAGE_INDEX = "pto_emp_list_page_index";
        public const string PTO_EMPLOYEE_LIST_ROW_COUNT = "pto_emp_list_row_count";
        public const string PTO_EMPLOYEE_LIST_MONTH = "pto_emp_list_month";
        public const string PTO_EMPLOYEE_TILE_EXPORT_EXCEL = "PTO List";
        public const string PTO_EMPLOYEE_EXPORT_EXCEL_FILE_NAME = "PTO List.xls";
        public static readonly int PTO_STATUS_VERIFIED = int.Parse(ConfigurationManager.AppSettings["PTO_STATUS_VERIFIED"]);
        public static readonly int PTO_STATUS_CONFIRM = int.Parse(ConfigurationManager.AppSettings["PTO_STATUS_CONFIRM"]);
        public static readonly int PTO_STATUS_NEW = int.Parse(ConfigurationManager.AppSettings["PTO_STATUS_NEW"]);
        public static readonly int PTO_STATUS_REJECTED = int.Parse(ConfigurationManager.AppSettings["PTO_STATUS_REJECTED"]);
        public static readonly int PTO_STATUS_APPROVED = int.Parse(ConfigurationManager.AppSettings["PTO_STATUS_APPROVED"]);
        public static string PTO_EMAIL_EMPLOYEE_TEMPLATE_PATH = "~/Content/HtmlTemplate/PTOEmpEmailTemplate.htm";
        public static string PTO_EMAIL_FOOTER_TEMPLATE_PATH = "~/Content/HtmlTemplate/PTOEmailFooter.htm";
        public static string PTO_EMAIL_MANAGER_TEMPLATE_PATH = "~/Content/HtmlTemplate/PTOPMEmailTemplate.htm";
        public static string PTO_EMAIL_DELETE_TEMPLATE_PATH = "~/Content/HtmlTemplate/PTODeletedEmailTemplate.htm";
        public static string PTO_EMAIL_FIELD_EMPLOYEE_NAME = "[EmployeeName]";
        public static string PTO_EMAIL_FIELD_EMPLOYEE_SUBMIT_DATE = "[SubmitDate]";
        public static string PTO_EMAIL_FIELD_MANAGER_NAME = "[ManagerName]";
        public static string PTO_EMAIL_FIELD_PTO_ACTION = "[Action]";
        public static string PTO_EMAIL_FIELD_PTO_ID = "[ID]";
        public static string PTO_EMAIL_FIELD_PTO_REAL_ID = "[Real_PTO_ID]";
        public static string PTO_EMAIL_FIELD_PTO_TYPE_NAME = "[TypeName]";
        public static string PTO_EMAIL_FIELD_PTO_REASON = "[Reason]";
        public static string PTO_EMAIL_FIELD_PTO_DETAIL = "[Detail]";
        public static string PTO_EMAIL_FIELD_PTO_STATUS_NAME = "[StatusName]";
        public static string PTO_EMAIL_FIELD_PTO_USER = "[User]";
        public static string PTO_EMAIL_FIELD_PTO_RECIPENT = "[Recipient]";
        public static string PTO_PM_COMMENT_FIELD = "[PMComment]";
        public static string PTO_HR_COMMENT_FIELD = "[HRComment]";
        public static string PTO_EMAIL_FIELD_PTO_PTO_ID = "[PTO_ID]";
        public static string PTO_EMAIL_SUBJECT_NEW = "[CRM-PTO] {0} Submitted Successfully";
        public static string PTO_EMAIL_SUBJECT_VERIFIED = "[CRM-PTO] {0} has been verified";
        public static string PTO_EMAIL_SUBJECT_DELETE = "[CRM-PTO] {0} has been deleted";
        public static string PTO_EMAIL_SUBJECT_APPROVE_REJECT = "[CRM-PTO] {0} has been {1}";
        public static string PTO_EMAIL_SUBJECT_TO_MANAGER = "[CRM-PTO] {0} Submitted the New PTO Request";
        //public const string PTO_SENDER_EMAIL_ADDRESS = "crm@logigear.com";
        public static readonly string PTO_SENDER_NAME = ConfigurationManager.AppSettings["PTO_SENDER_NAME"];
        public static int PTO_TYPE_MATERINITY_LEAVE = 9;
        public const string PTO_DATE_OFF_KEY_PREFIX = "txtDateOff_";
        public const string PTO_FROM_HRS_INPUT_NAME_PREFIX = "ddl_From_";
        public const string PTO_TO_HRS_INPUT_NAME_PREFIX = "ddl_To_";
        public const string PTO_IS_COMPANY_PAY_CHECKBOX = "ddl_IsPay_";
        /*PTO remind email*/
        public static string PTO_REMIND_MANAGER_NAME = "[#MANAGERNAME]";
        public static string PTO_REMIND_EMAIL_PATH = "~/Content/HtmlTemplate/PTORemindToConfirmEmailTemplate.htm";
        public static string PTO_REMIND_MANAGER_DEAD_LINE = "[#DEADLINE]";
        public static string PTO_REMIND_MANAGER_ITEMS = "[#ITEMS]";
        public static string PTO_REMIND_EMAIL_SUBJECT = "[CRM-PTO] Remind to confirm PTO data";

        #endregion

        #region PTO Manager        
        public const string PTO_MANAGER_LIST_FILTER_TEXT = "pto_manager_filter_text";
        public const string PTO_MANAGER_LIST_STATUS = "pto_manager_list_status";
        public const string PTO_MANAGER_LIST_TYPE_PARENT = "pto_manager_list_result_parent";
        public const string PTO_MANAGER_LIST_TYPE = "pto_manager_list_result";

        public const string PTO_MANAGER_LIST_FILTER_MONTH = "pto_manager_list_filter_month";
        
        public const string PTO_MANAGER_LIST_COLUMN = "pto_manager_list_column";
        public const string PTO_MANAGER_LIST_ORDER = "pto_manager_list_order";
        public const string PTO_MANAGER_LIST_PAGE_INDEX = "pto_manager_list_page_index";
        public const string PTO_MANAGER_LIST_ROW_COUNT = "pto_manager_list_row_count";
        public const string FULLNAME_OR_PTOID = "Fullname or PTO ID";
        public const string PTO_MANAGER_TILE_EXPORT_EXCEL = "PTO List";
        public const string PTO_MANAGER_EXPORT_EXCEL_FILE_NAME = "PTO List.xls";
        public const string PTO_MANAGER_DATE_FORMAT = "MMM-yyyy";
        public static readonly int PTO_NOTIFICATION_DAYS = int.Parse(ConfigurationManager.AppSettings["PTO_NOTIFICATION_DAYS"]);
        
        #endregion        

        #region Portal
        //Porsition Filter
        public const string PORTAL_POSITION_NAME = "portal_position_name";
        public const string PORTAL_POSITION_DEPARTMENT = "portal_position_department";
        public const string PORTAL_POSITION_SUB_DEPARTMENT = "portal_position_sub_department";
        public const string PORTAL_POSITION_TITLE = "portal_position_title";
        public const string PORTAL_POSITION_FLOOR = "portal_position_floor";
        public const string PORTAL_POSITION_PROJECT = "portal_position_project";
        public const string PORTAL_POSITION_BRANCH = "portal_position_branch";
        public const string PORTAL_POSITION_OFFICE = "portal_position_office";
        public const string PORTAL_POSITION_SEATCODE = "portal_position_seatcode";
        public const string FIRST_SEATCODE = "SeatCode";

        public const string PORTAL_POSITION_COLUMN = "portal_position_column";
        public const string PORTAL_POSITION_ORDER = "portal_position_order";
        public const string PORTAL_POSITION_PAGE_INDEX = "portal_position_page_index";
        public const string PORTAL_POSITION_ROW_COUNT = "portal_position_row_count";
        
        public static List<ListItem> LoginRole
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Employee", "Employee"));
                items.Add(new ListItem("Manager", "Manager"));
                return items;
            }
        }

        public const string PORTAL_ROLE_EMPLOYEE = "Employee";
        public const string PORTAL_ROLE_MANAGER = "Manager";

        /*Performance Rerview: added by Tai Nguyen*/
        public static readonly int PER_REVIEW_RESOLUTION_NEW_ID = int.Parse(ConfigurationManager.AppSettings["PRW_RESOLUTION_NEW_ID"]);
        public static string PER_REVIEW_FIRST_KEY_WORD = "Employee Name or ID";
        public static string PER_REVIEW_FIRST_STATUS_LIST = "-Choose Status-";
        public static string PER_REVIEW_FIRST_DEPARTMENT_LIST = "-Choose Department-";
        public static string PER_REVIEW_FIRST_EFORM_LIST = "-Choose PR Form-";
        public static string PER_REVIEW_FIRST_ROLE_LIST = "-Choose Role-";
        public static string PER_REVIEW_FIRST_USER_LIST = "-Choose User-";
        public static string PER_REVIEW_FIRST_RESOLUTION_LIST = "-Choose Resolution-";
        public static string PER_REVIEW_NEED_PR_LIST = "Need Setup PR";
        public static string PER_REVIEW_NEED_ALL_LIST = "All";
        public static string PER_REVIEW_EFORM_MASTER_PREFIX = "PR-";
        public static string PER_REVIEW_ID_PREFIX = "PR_";
        public static string PER_REVIEW_ID_SEPARATOR = "_";
        public static string PER_REVIEW_RES_TEXT_SETUP = "Setup";
        public static string PER_REVIEW_RES_TEXT_FORWARD_TO = "Forwarded To";
        public static string PER_REVIEW_RES_TEXT_AGREED = "Agreed";
        public static string PER_REVIEW_RES_TEXT_EDIT = "Edited";
        public static string PER_REVIEW_RES_TEXT_DO_NOT_AGREE = "Didn't Agree";
        public static string PER_REVIEW_RES_TEXT_APPROVED = "Approved";
        public static string PER_REVIEW_RES_TEXT_CLOSED = "Closed";
        public static string PER_REVIEW_RES_TEXT_FILLED_DATA = "Filled Data";
        public static string PER_REVIEW_RES_TEXT_REJECTED = "Rejected";
        public static string PER_REVIEW_NEED_PR_LIST_ID = "1";
        public static readonly int PER_REVIEW_LOCKED_DAYS = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_LOCKED_DAYS"]);

        public static class PER_REVIEW_RESOLUTIONS
        {
            public readonly static int New = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_NEW"]);
            public readonly static int ToBeFilledData = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_TO_BE_FILLED_DATA"]);
            public readonly static int FilledData = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_FILLED_DATA"]);
            public readonly static int ToBeApprovedByEM = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_NEW_TO_BE_APPROVED_BY_EM"]);
            public readonly static int ToBeApprovedByEmployee = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_TO_BE_APPROVED_BY_EMPLOYEE"]);
            public readonly static int Agreed = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_NEW_AGREED"]);
            public readonly static int DonNotAgree = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_NEW_DO_NOT_AGREE"]);
            public readonly static int Rejected = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_REJECT"]);
            public readonly static int Approved = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_APPROVED"]);
            public readonly static int Completed = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_COMPLETED"]);
            public readonly static int Cancelled = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_CANCELLED"]);
            public readonly static int ToBeApprovedByHR = int.Parse(ConfigurationManager.AppSettings["PER_REVIEW_RES_TO_BE_APPROVED_BY_HR"]);
        };

        public static List<ListItem> NeedReview
        {
             get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem(PER_REVIEW_NEED_ALL_LIST, "0"));
                items.Add(new ListItem(PER_REVIEW_NEED_PR_LIST, "1"));
                return items;
            }
        }
        
        /*End Performance Rerview*/

        /*Position: added by Tai Nguyen*/
        public static string POSITION_FIRST_KEY_WORD = "Employee Name or ID";
        public static string STT_ID_PREFIX = "STT-";
        public static string EMP_ID_PREFIX = "E";
        public enum PositionModelType: int 
        { 
            Employee = 1,
            Stt = 2
        }
        public static string POSITION_FIRST_FLOOR_LIST = "-Floor-";
        public static List<string> FloorList = new List<string>() 
            {   "G", "M", "1", "2", "3", "4", "5", "6", "7", "8" };
        /*End Postion*/

        #region 4 Email
        public const string PR_PERFORMANCE_ID_HOLDER = "[#PERFORMANCE_ID]";
        public const string PR_EMPLOYEE_HOLDER = "[#EMPLOYEE]";
        public const string PR_TITLE_HOLDER = "[#TITLE]";
        public const string PR_MANAGER_HOLDER = "[#MANAGER]";
        #endregion

        #endregion

        #region Manage Workflow
        public const string MWF_WORKFLOW = "MWF_WORKFLOW";
        public const string MWF_ROLE = "MWF_ROLE";
        public const string MWF_RESOLUTION = "MWF_RESOLUTION";
        public const string MWF_STATUS = "MWF_STATUS";
        public const string MWF_COLUMN = "mwf_column";
        public const string MWF_ORDER = "mwf_order";
        public const string MWF_PAGE_INDEX = "mwf_page_index";
        public const string MWF_ROW_COUNT = "mwf_row_count";

        public static List<ListItem> WorkFlowStatus
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("True", "1"));
                items.Add(new ListItem("False", "0"));
                return items;
            }
        }
        public const string FIRST_ITEM_REQUEST = "-Select WorkFlow-";
        public const string FIRST_ITEM_ROLE = "-Select Role-";
        public const string FIRST_ITEM_RESOLUTION = "-Select Resolution-";
        public const string FIRST_ITEM_MANAGE_STATUS = "-Select Status-";
        public enum FlowType
        {
            FLOW_JOB_REQUEST = 1,
            FLOW_JOB_REQUEST_ITEM =2,
            FLOW_PERFORMANCE_REVIEW = 3,
            FLOW_PURCHASE_REQUEST = 4 ,
            FLOW_PURCHASE_REQUEST_US = 5  
        }

        public enum ActionType
        { 
            List = 1,
            Update = 2
        }
        #endregion
    
        public class FileType
        {
            private string fileExtension;

            public string FileExtension
            {
              get { return fileExtension; }
              set { fileExtension = value; }
            }
            private Dictionary<int, string> hexCode;

            public Dictionary<int, string> HexCode
            {
              get { return hexCode; }
              set { hexCode = value; }
            }
            public FileType(string fileExtension, Dictionary<int, string> hexCode)
            {
                this.fileExtension = fileExtension;
                this.hexCode = hexCode;
            }
        };
        public static List<FileType> FileTypes
        {
            get
            {
                List<FileType> items = new List<FileType>();
                items.Add(new FileType(".jpg", new Dictionary<int, string>
                    {
                        { 0, "FF"}, { 1, "D8"}, {2, "FF"}
                    } ));
                items.Add(new FileType(".jpeg", new Dictionary<int, string>
                    {
                        { 0, "FF"}, { 1, "D8"}, {2, "FF"}
                    } ));
                items.Add(new FileType(".gif", new Dictionary<int, string>
                    {
                        { 0, "47"}, { 1, "49"}, {2, "46"}, {3, "38"}, {5, "61"}
                    } ));
                items.Add(new FileType(".png", new Dictionary<int, string>
                    {
                        { 0, "89"}, { 1, "50"}, { 2, "4E"}, { 3, "47"}, 
                        { 4, "0D"}, { 5, "0A"}, { 5, "1A"}, { 7, "0A"}
                    } ));
                items.Add(new FileType(".bmp", new Dictionary<int, string>
                    {
                        { 0, "42"}, { 1, "4D"}
                    } ));

                //items.Add(new FileType(".jpeg", "FF D8 FF"));
                //items.Add(new FileType(".gif", "47 49 46 38"));
                //items.Add(new FileType(".png", "89 50 4E 47 0D 0A 1A 0A"));
                //items.Add(new FileType(".bmp", "42 4D"));
                //items.Add(new FileType(".doc", "42 4D"));
                //items.Add(new FileType(".docx", "42 4D"));
                //items.Add(new FileType(".pdf", "42 4D"));
                //items.Add(new FileType(".xls", "42 4D"));
                //items.Add(new FileType(".xlsx", "42 4D"));
                return items;
            }
        }

        public static string HELP_PATH = "~/FileUpload/Help/";
        //Location Filter added by huyly
        public const string LOCATION_LIST_BRANCH = "location_list_branch";
        public const string LOCATION_LIST_OFFICE = "location_list_office";
        public const string LOCATION_LIST_FLOOR = "location_list_floor";
        public const string LOCATION_LIST_SEATCODE = "location_list_seatcode";

        //Location Added by Tai Nguyen
        public static readonly int LOCATION_BRANCH_SAI_GON = int.Parse(ConfigurationManager.AppSettings["LOCATION_BRANCH_SAI_GON"]);
        public static readonly int LOCATION_BRANCH_DA_NANG = int.Parse(ConfigurationManager.AppSettings["LOCATION_BRANCH_DA_NANG"]);

        public const string LOCATION_LIST_BRANCH_LABEL = "--Choose Branch--";
        public const string LOCATION_LIST_OFFICE_LABEL = "--Choose Office--";
        public const string LOCATION_LIST_FLOOR_LABEL = "--Choose Floor--";
        public const string LOCATION_LIST_AVAILABLE_LABEL = "--Choose Status--";
        public const string LOCATION_TEXTBOX_KEYWORD = "Seat Code";

        public const string LOCATION_CODE_BRANCH_PREFIX = "B";
        public const string LOCATION_CODE_OFFICE_PREFIX = "O";
        public const string LOCATION_CODE_FLOOR_PREFIX = "F";
        public const string LOCATION_CODE_SEATCODE_PREFIX = "S";
        
        public const int LOCATION_CODE_BRANCH_INDEX = 0;
        public const int LOCATION_CODE_OFFICE_INDEX = 1;
        public const int LOCATION_CODE_FLOOR_INDEX = 2;
        public const int LOCATION_CODE_SEATCODE_INDEX = 3;

        public const string LOCATION_SEATCODE_AVAILABLE_TEXT = "Available";
        public static readonly int MAX_ROW_SEAT_CODE = int.Parse(ConfigurationManager.AppSettings["MAX_ROW_SEAT_CODE"]);

        public static List<ListItem> SeatCodeStatus
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Available", "1"));
                items.Add(new ListItem("Not available", "0"));
                return items;
            }
        }

        /// <summary>
        /// tan.tran add new 2011.05.25
        /// Time Range To Clear Logs Data
        /// </summary>
        public static List<ListItem> TimeRangeToClearLog
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Keep logs in 2 months", "2"));
                items.Add(new ListItem("Keep logs in 4 months", "4"));
                items.Add(new ListItem("Keep logs in 6 months", "6"));
                items.Add(new ListItem("Keep logs in 8 months", "8"));
                items.Add(new ListItem("Keep logs in 10 months", "10"));
                items.Add(new ListItem("Keep logs in one year", "12"));
                items.Add(new ListItem("Clear all", "0"));
                return items;
            }
        }

        /// <summary>
        /// @author : tai.pham
        /// @date : 2012.01.12
        /// Time Range To Clear Data, Using in Timemanagement Module
        /// </summary>
        public static List<ListItem> TimeRangeToClearData
        {
            get
            {
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("Keep data in 2 months", "2"));
                items.Add(new ListItem("Keep data in 4 months", "4"));
                items.Add(new ListItem("Keep data in 6 months", "6"));
                items.Add(new ListItem("Keep data in 8 months", "8"));
                items.Add(new ListItem("Keep data in 10 months", "10"));
                items.Add(new ListItem("Keep data in one year", "12"));
                items.Add(new ListItem("Clear all", "0"));
                return items;
            }
        }

        public static readonly int PTO_TAKE = int.Parse(ConfigurationManager.AppSettings["PTO_TAKE"]);
        public const string LOG_TYPE_DATA = "DataLog";
        public const string LOG_TYPE_PORTAL_ACCESS = "PortalAccess";
        public const string LOG_TYPE_ADMIN_ACCESS = "AdminAccess";
        public const int DAY_LOCK_ADD_VACATION_SERINORITY = 15;
        public const int DAY_MONTH_YEAR_REPORT = 1;

        public class OwnerModel
        {
            private string _ownerID;

            public string OwnerID
            {
                get { return _ownerID; }
                set { _ownerID = value; }
            }
            private string _ownerFullName;

            public string OwnerFullName
            {
                get { return _ownerFullName; }
                set { _ownerFullName = value; }
            }
            private bool _isSTT;

            public bool IsSTT
            {
                get { return _isSTT; }
                set { _isSTT = value; }
            }

            public string DisplayName()
            {
                if(!string.IsNullOrEmpty(OwnerID))
                    return "<b>" + this.OwnerID + "</b> - " + OwnerFullName + (IsSTT ? " (STT)" : " (Employee)");
                return string.Empty;
            }
        }

        //End Location

        //Menu added by Tai Nguyen
        public const string PROJECT = "Project";
        public const string MANAGER = "Manager";
        public const string FIRST_ITEM_MANAGER = "--Manager--";
        public const string MENU_PAGE_MODULE_LABEL = "--Module--";
        public const string MENU_PAGE_PERMISSION_LABEL = "--Permission--";
        public const string MENU_PAGE_MODULE_ID_PREFIX = "slModule_";
        public const string MENU_PAGE_PERMISSION_ID_PREFIX = "slPermission_";
        public const string MENU_PAGE_MODULE_NAME = "slModule";
        public const string MENU_PAGE_PERMISSION_NAME = "slPermission";
        public const string MENU_PAGE_ICON_FOLDER = "/Content/Images/Icons/Menu/";
        public const string MENU_PAGE_ICON_NAME_PREFIX = "[menuicon]";

        public static readonly long MENU_PAGE_IMAGE_MAX_SIZE = long.Parse(ConfigurationManager.AppSettings["MENU_PAGE_IMAGE_MAX_SIZE"]);//in MByte
        public static readonly string MENU_PAGE_IMAGE_FORMAT_ALLOWED = ConfigurationManager.AppSettings["MENU_PAGE_IMAGE_FORMAT_ALLOWED"];

        //public const int MENU_PAGE_IMAGE_MAX_SIZE = 1234;//in Byte
        //public const string MENU_PAGE_IMAGE_FORMAT_ALLOWED = ".jpeg";

        //End Menu

        #region Service Request
        public const string SR_SERVICE_REQUEST_PREFIX = "SR-";
        public static string SR_FIRST_KEY_WORD = "Title or ID";
        public const string SR_FIRST_CATEGORY = "- Category -";
        public const string SR_FIRST_SUBCATEGORY = "- Sub Category -";
        public const string SR_FIRST_URGENT = "- Urgent -";
        public const string SR_FIRST_STATUS = "- Status -";
        public const string SR_FIRST_ASSIGNTO = "- Assign to -";
        public const string SR_FIRST_REQUESTOR = "- Request user -";
        public const string SR_LIST_TITLE = "sr_title_name";
        public const string SR_LIST_COLUMN = "sr_list_column";
        public const string SR_LIST_ORDER = "sr_list_order";
        public const string SR_LIST_PAGE_INDEX = "sr_list_page_index";
        public const string SR_LIST_ROW_COUNT = "sr_list_row_count";
        public const string SR_CATEGORY_LIST = "sr_category_list";

        public const string SR_URGENT = "sr_urgent_list";



        public const string SR_SUBCATEGORY_LIST = "sr_subcategory_list";        
        public const string SR_STATUS_LIST = "sr_status_list";
        public const string SR_ASSIGNTO_LIST = "sr_assignto_list";
        public const string SR_ACTION = "sr_action";
        public const string SR_USER_LOGIN = "sr_user_login";
        
        public static readonly int SR_STATUS_NEW = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_NEW"]);
        public static readonly int SR_STATUS_CLOSED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_CLOSED"]);
        public static readonly int SR_STATUS_OPEN = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_OPEN"]);
        public static readonly int SR_STATUS_TO_BE_APPROVED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_TO_BE_APPROVED"]);
        public static readonly int SR_STATUS_VERIFIED_CLOSED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_VERIFIED_CLOSED"]);
        public static readonly int SR_STATUS_PENDING = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_PENDING"]);
        public static readonly int SR_STATUS_POSTPONED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_POSTPONED"]);
        public static readonly int SR_STATUS_APPROVED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_APPROVED"]);
        public static readonly int SR_STATUS_REJECTED = int.Parse(ConfigurationManager.AppSettings["SR_STATUS_REJECTED"]);
        public static readonly int SR_MAX_LENGTH_TITLE = int.Parse(ConfigurationManager.AppSettings["MAX_LENGTH_TITLE"]);
        public static readonly int SR_NUMBER_OF_NEW_SERVICE_REQUEST_DASHBOARD = int.Parse(ConfigurationManager.AppSettings["SR_NUMBER_OF_NEW_SERVICE_REQUEST_DASHBOARD"]);
        #endregion

        #region Group 
        //public static readonly int GROUP_IT_ID = int.Parse(ConfigurationManager.AppSettings["GROUP_IT_ID"]);
        #endregion

        #region Service Request added by Tai Nguyen
        public static string SR_TXT_KEYWORD_LABEL = "Category Name";
        public static string SR_LIST_TYPE_LABEL = "--Type--";
        public static string SR_LIST_CATEGORY_LABEL = "--Category--";
        
        public static string SR_LIST_SUB_CATEGORY_LABEL = "--Sub Category--";
        public static string SR_LIST_URGENCY_LABEL = "--Urgency--";
        public static string SR_LIST_STATUS_LABEL = "--Status--";
        public static string SR_SOLUTION_STRING_FORMAT = " (Last modified by {0} {1})";// (Last modified by duoc.nguyen 07-Jun-2011 7:05 PM)
        public static readonly int SR_FILE_MAX_SIZE = int.Parse(ConfigurationManager.AppSettings["SR_FILE_MAX_SIZE"]);
        public static readonly int SR_UPLOAD_MAX_QUANTITY = int.Parse(ConfigurationManager.AppSettings["SR_UPLOAD_MAX_QUANTITY"]);
        public static int SR_DESCRIPTION_LENGTH_ON_LIST = 100;
        public static string SR_UPLOAD_PATH = "/FileUpload/ServiceRequest/";
        public static string SR_FILE_SEPARATE_SIGN = ",";
        public static char SR_FILE_SEPARATE_CHAR = ',';
        public static string SR_FILENAME_SEPARATE_SIGN = "-";
        public static string SR_FILE_NAME_PREFIX_FORMAT = "hhmmssff";
        public static readonly int SR_URGENCY_URGENT_ID = int.Parse(ConfigurationManager.AppSettings["SR_URGENCY_URGENT_ID"]);
        public static readonly int SR_URGENCY_HIGH_ID = int.Parse(ConfigurationManager.AppSettings["SR_URGENCY_HIGH_ID"]);
        public static readonly int SR_URGENCY_NORMAL_ID = int.Parse(ConfigurationManager.AppSettings["SR_URGENCY_NORMAL_ID"]);


        public const string SR_ASSIGN_TO_HOLDER = "[#ASSIGN_TO]";
        public const string SR_REQUEST_ID_HOLDER = "[#REQUEST_ID]";
        public const string SR_FORWARD_TO_HOLDER = "[#FORWARD_TO]";
        public const string SR_CATEGORY_HOLDER = "[#CATEGORY]";
        public const string SR_SUB_CATEGORY_HOLDER = "[#SUB_CATEGORY]";
        public const string SR_TITLE_HOLDER = "[#TITLE]";
        public const string SR_DESCRIPTION_HOLDER = "[#DESCRIPTION]";
        public const string SR_SUBMITER_HOLDER = "[#SUBMITER]";
        public const string SR_REQUESTOR_HOLDER = "[#REQUESTOR]";
        public const string SR_SUBMITDATE_HOLDER = "[#SUBMITDATE]";
        public const string SR_DUEDATE_HOLDER = "[#DUEDATE]";
        public const string SR_CCLIST_HOLDER = "[#CCLIST]";
        public const string SR_URGENCY_HOLDER = "[#URGENCY]";
        public const string SR_PARENTID_HOLDER = "[#PARENTID]";
        public const string SR_STATUS = "[#SR_STATUS]";
        public const string SR_STATUS_HOLDER = "[#STATUS]";        
        public const string SR_ASSIGNEDTO_HOLDER = "[#ASSIGNEDTO]";
        public const string SR_HISTORY_HOLDER = "[#HISTORY]";
        public const string SR_COMMENTS_HOLDER = "[#COMMENTS]";
        public const string SR_LINK_EMPLOYEE_HOLDER = "[#LINK_EMPLOYEE]";
        public const string SR_LINK_IT_HOLDER = "[#LINK_IT]";
        public const string SR_LINK_SURVEY = "[#LINK_SURVEY]";
        #endregion

        #region Service Request Admin                
        public const string SR_LIST_ADMIN_TITLE = "sr_title_name_admin";
        public const string SR_LIST_ADMIN_COLUMN = "sr_list_column_admin";
        public const string SR_LIST_ADMIN_ORDER = "sr_list_order_admin";
        public const string SR_LIST_ADMIN_PAGE_INDEX = "sr_list_page_index_admin";
        public const string SR_LIST_ADMIN_ROW_COUNT = "sr_list_row_count_admin";
        public const string SR_ADMIN_CATEGORY_LIST = "sr_category_list_admin";
        public const string SR_ADMIN_SUBCATEGORY_LIST = "sr_subcategory_list_admin";
        public const string SR_ADMIN_STATUS_LIST = "sr_status_list_admin";
        public const string SR_ADMIN_ASSIGNTO_LIST = "sr_assignto_list_admin";
        public const string SR_ADMIN_REQUESTOR_LIST = "sr_requestor_list_admin";
        public const string SR_ADMIN_START_DATE = "sr_list_start_date";
        public const string SR_ADMIN_END_DATE = "sr_list_end_date";        

        public static string SR_LIST_ASSIGNED_TO_LABEL = "--User--";
        public static string SR_DUE_HOUR_FORMAT = "hh:mm tt";
        public static char SR_ACTIVITY_TOTAL_SEPARATE = ':';


        //Report open close status
        public const string SR_REPORT_OC_LIST_ADMIN_COLUMN = "sr_report_oc_list_column_admin";
        public const string SR_REPORT_OC_LIST_ADMIN_ORDER = "sr_report_oc_list_order_admin";
        public const string SR_REPORT_OC_LIST_ADMIN_PAGE_INDEX = "sr_report_oc_list_page_index_admin";
        public const string SR_REPORT_OC_LIST_ADMIN_ROW_COUNT = "sr_report_oc_list_row_count_admin";
        public const string SR_REPORT_OC_ADMIN_START_DATE = "sr_report_oc_list_start_date";
        public const string SR_REPORT_OC_ADMIN_END_DATE = "sr_report_oc_list_end_date";

        public const string SR_REPORT_OC_TILE_EXPORT_EXCEL = "Open close status List";
        public const string SR_REPORT_OC_EXPORT_EXCEL_NAME = "ReportOpenClosed.xls";

        // Report activity
        public const string SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN = "sr_report_ac_list_column_admin";
        public const string SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER = "sr_report_ac_list_order_admin";
        public const string SR_REPORT_ACTIVITY_LIST_ADMIN_PAGE_INDEX = "sr_report_ac_list_page_index_admin";
        public const string SR_REPORT_ACTIVITY_LIST_ADMIN_ROW_COUNT = "sr_report_ac_list_row_count_admin";
        public const string SR_REPORT_ACTIVITY_ADMIN_START_DATE = "sr_report_ac_list_start_date";
        public const string SR_REPORT_ACTIVITY_ADMIN_END_DATE = "sr_report_ac_list_end_date";

        public const string SR_REPORT_ACTIVITY_TILE_EXPORT_EXCEL = "Service Request Activity Summary";
        public const string SR_REPORT_ACTIVITY_EXPORT_EXCEL_NAME = "ReportActivity.xls";

        // Report WEEKLY
        public const string SR_REPORT_WEEKLY_REQUEST_CLOSED = "sr_list_request_closed";
        public const string SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN = "sr_report_act_list_column_admin";
        public const string SR_REPORT_ACTIVE_LIST_ADMIN_ORDER = "sr_report_act_list_order_admin";
        public const string SR_REPORT_ITEAM_LIST_ADMIN_COLUMN = "sr_report_it_list_column_admin";
        public const string SR_REPORT_ITEAM_LIST_ADMIN_ORDER = "sr_report_it_list_order_admin";
        public const string SR_REPORT_WEEKLY_ADMIN_START_DATE = "sr_report_weekly_list_start_date";
        public const string SR_REPORT_WEEKLY_ADMIN_END_DATE = "sr_report_weekly_list_end_date";
        
        public const string SR_REPORT_ITEAM_TILE_EXPORT_EXCEL = "Report Detail Team-Helpdesk";
        public const string SR_REPORT_WEEKLY_EXPORT_EXCEL_NAME = "WeeklyReport.xlsx";
        public const string SR_REPORT_LIST_DETAIL_EXCEL_NAME = "ListDetailReport.xlsx";
        public const string SR_REPORT_CLOSED_TILE_EXPORT_EXCEL = "All Request Closed";
        public const string SR_REPORT_ACTIVE_TILE_EXPORT_EXCEL = "Report Request Active";

        // Survey Report
        public const string SR_REPORT_SURVEY_START_DATE = "sr_report_survey_start_date";
        public const string SR_REPORT_SURVEY_END_DATE = "sr_report_survey_end_date";
        public const string SR_REPORT_SURVEY_ADMIN_USER_NAME = "sr_report_survey_admin_user_name";

        public const string SR_REPORT_SURVEY_GENERAL_COLUMN = "sr_report_survey_general_column";
        public const string SR_REPORT_SURVEY_GENERAL_ORDER = "sr_report_survey_general_order";

        public const string SR_REPORT_SURVEY_DETAIL_COLUMN = "sr_report_survey_detail_column";
        public const string SR_REPORT_SURVEY_DETAIL_ORDER = "sr_report_survey_detail_order";
        public const string SR_REPORT_SURVEY_PAGE_INDEX = "sr_report_survey_page_index_admin";
        public const string SR_REPORT_SURVEY_ROW_COUNT = "sr_report_survey_row_count_admin";

        public const string SR_REPORT_SURVEY_GENERAL_TILE_EXPORT_EXCEL = "General Survey Report";
        public const string SR_REPORT_SURVEY_DETAIL_TILE_EXPORT_EXCEL = "Detail Survey Report";
        public const string SR_REPORT_SURVEY_GENERAL_SHEET_NAME_EXPORT_EXCEL = "General Survey Report";
        public const string SR_REPORT_SURVEY_DETAIL_SHEET_NAME_EXPORT_EXCEL = "Detail Survey Report";
        public const string SR_REPORT_SURVEY_EXPORT_EXCEL_NAME = "SurveyReport.xlsx";
        
        #endregion

        #region Exam Question 
        public const string TRAINING_EXAM_QUESTIONS_KEYWORD = "KeyWord";
        public const string TRAINING_EXAM_QUESTIONS_SORT_COLUMN = "training_exam_questions_sort_column";
        public const string TRAINING_EXAM_QUESTIONS_SORT_ORDER = "training_exam_questions_sort_order";
        public const string TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX = "training_exam_questions_sort_page_index";
        public const string TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT = "training_exam_questions_sort_row_count";
        public const string TRAINING_EXAM_QUESTIONS_DEFAULT_VALUE = "Exam Question or ID";
        #endregion

        #region Training Admin Confirmation
        public const string TRAINING_ADMIN_CONFIRMATION_TEXT = "TRAINING_ADMIN_CONFIRMATION_TEXT";
        public const string TRAINING_ADMIN_CONFIRMATION_COURSE_PRO = "TRAINING_ADMIN_CONFIRMATION_COURSE_PRO";
        public const string TRAINING_ADMIN_CONFIRMATION_COURSE_ENG = "TRAINING_ADMIN_CONFIRMATION_COURSE_ENG";
        public const string TRAINING_ADMIN_CONFIRMATION_TYPE = "TRAINING_ADMIN_CONFIRMATION_TYPE";
        public const string TRAINING_ADMIN_CONFIRMATION_STATUS = "TRAINING_ADMIN_CONFIRMATION_STATUS";
        public const string TRAINING_ADMIN_CONFIRMATION_INSTRUCTOR = "TRAINING_ADMIN_CONFIRMATION_INSTRUCTOR";
        public const string TRAINING_ADMIN_CONFIRMATION_COLUMN = "TRAINING_ADMIN_CONFIRMATION_COLUMN";
        public const string TRAINING_ADMIN_CONFIRMATION_ORDER = "TRAINING_ADMIN_CONFIRMATION_ORDER";
        public const string TRAINING_ADMIN_CONFIRMATION_PAGE_INDEX = "TRAINING_ADMIN_CONFIRMATION_PAGE_INDEX";
        public const string TRAINING_ADMIN_CONFIRMATION_ROW = "TRAINING_ADMIN_CONFIRMATION_ROW_COUNT";

        public const string TRAINING_ADMIN_CONFIRMATION_CLASS_TEXT = "TRAINING_ADMIN_CONFIRMATION_CLASS_TEXT";
        public const string TRAINING_ADMIN_CONFIRMATION_CLASS_COURSE = "TRAINING_ADMIN_CONFIRMATION_CLASS_COURSE";
        public const string TRAINING_ADMIN_CONFIRMATION_CLASS_TYPE = "TRAINING_ADMIN_CONFIRMATION_CLASS_TYPE";
        public const string TRAINING_ADMIN_CONFIRMATION_CLASS_STATUS = "TRAINING_ADMIN_CONFIRMATION_CLASS_STATUS";
        public const string TRAINING_ADMIN_CONFIRMATION_CLASS_INSTRUCTOR = "TRAINING_ADMIN_CONFIRMATION_CLASS_INSTRUCTOR";

        public const string TRAINING_ADMIN_CONFIRMATION_ATTENDEE_COLUMN = "TRAINING_ADMIN_CONFIRMATION_ATTENDEE_COLUMN";
        public const string TRAINING_ADMIN_CONFIRMATION_ATTENDEE_ORDER = "TRAINING_ADMIN_CONFIRMATION_ATTENDEE_ORDER";

        public const string TRAINING_ADMIN_CONFIRMATION_PAGE_TITLE = "Training Admin Confirmation";
        public const string TRAINING_ADMIN_CONFIRMATION_EXPORT_TITLE_PRO = "PROFESSIONAL CLASS";
        public const string TRAINING_ADMIN_CONFIRMATION_EXPORT_TITLE_ENG = "ENGLISH CLASS";

        public const string TRAINING_ADMIN_CONFIRMATION_REGISTRATION_STATUS = "TRAINING_ADMIN_CONFIRMATION_REGISTRATION_STATUS";
        #endregion

        #region Training Center
        public const int TRAINING_REGISTRATION_STATUS_NEW = 1;
        public const int TRAINING_REGISTRATION_STATUS_APPROVED = 2;
        public const int TRAINING_REGISTRATION_STATUS_CONFIRMED = 3;
        public const int TRAINING_REGISTRATION_STATUS_REJECTED = 4;

        public const string TRAINING_EEI_LIST_LABEL = "--Skill Type--";
        public const string TRAINING_EEI_TXT_KEYWORD_LABEL = "Employee Name or ID";
        public const string TC_TEXT = "Class ID or Course Name";
        public const string TC_DURATION_PREFIX = "hours";

        public const string FIRST_ITEM_COURSE = "-Select Course-";
        public const string FIRST_ITEM_TYPE = "-Select Type-";
        public const string FIRST_ITEM_TRANING_STATUS = "-Select Status-";
        public const string FIRST_ITEM_INTRUCTOR = "-Select Instructor-";

        public const string TC_PROFESSIONAL_TEXT = "TC_PROFESSIONAL_TEXT";
        public const string TC_PROFESSIONAL_COURSE = "TC_PROFESSIONAL_COURSE";
        public const string TC_ENGLISH_COURSE = "TC_ENGLISH_COURSE";
        public const string TC_PROFESSIONAL_TYPE = "TC_PROFESSIONAL_TYPE";
        public const string TC_PROFESSIONAL_STATUS = "TC_PROFESSIONAL_STATUS";
        public const string TC_PROFESSIONAL_INSTRUCTOR = "TC_PROFESSIONAL_INSTRUCTOR";
        public const string TC_PROFESSIONAL_COLUMN = "TC_PROFESSIONAL_COLUMN";
        public const string TC_PROFESSIONAL_ORDER = "TC_PROFESSIONAL_ORDER";
        public const string TC_PROFESSIONAL_PAGE_INDEX = "TC_PROFESSIONAL_PAGE_INDEX";
        public const string TC_PROFESSIONAL_ROW_COUNT = "TC_PROFESSIONAL_ROW_COUNT";

        public const int TRAINING_CENTER_STATUS_OPEN_CLASS = 1;
        public const int TRAINING_CENTER_STATUS_CLOSE_CLASS = 1;

        public const string TRAINING_CENTER_CLASS_TEXT = "TRAINING_ADMIN_CONFIRMATION_CLASS_TEXT";
        public const string TRAINING_CENTER_CLASS_COURSE = "TRAINING_CENTER_CLASS_COURSE";
        public const string TRAINING_CENTER_CLASS_TYPE = "TRAINING_CENTER_CLASS_TYPE";
        public const string TRAINING_CENTER_CLASS_STATUS = "TRAINING_CENTER_CLASS_STATUS";
        public const string TRAINING_CENTER_CLASS_INSTRUCTOR = "TRAINING_CENTER_CLASS_INSTRUCTOR";

        public const string TRAINING_CENTER_REGISTRATION_STATUS_FIRST_ITEM = "-Select Status-";
        public const string TRAINING_CENTER_REGISTRATION_STATUS = "TRAINING_CENTER_STATUS_REGISTRATION";
        public const string TRAINING_CENTER_LIST_PROFESSIONAL = "TRAINING_CENTER_LIST";
        public const string TRAINING_CENTER_PROFESSIONAL_CLASS_TITLE = "TRAINING_CENTER_PROFESSIONAL_CLASS";
        public const string TRAINING_CENTER_ENGLISH_CLASS_TITLE = "TRAINING_CENTER_ENGLISH_CLASS";
        public const string TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL = "TRAINING_CENTER_ADMIN_LIST_PROFESSIONAL";
        public const string TRAINING_CENTER_ADMIN_LIST_CLASS = "TRAINING_CENTER_ADMIN_LIST_CLASS";
        public const string TRAINING_CENTER_LIST_CLASS = "TRAINING_CENTER_CLASS_LIST";
        public const string TRAINING_CENTER_PROFESSIONAL_TEXT = "TRAINING_CENTER_PROFESSIONAL_TEXT";
        public const string TRAINING_CENTER_PROFESSIONAL_COURSE = "TRAINING_CENTER_PROFESSIONAL_COURSE";
        public const string TRAINING_CENTER_ENGLISH_COURSE = "TRAINING_CENTER_PROFESSIONAL_COURSE";
        public const string TRAINING_CENTER_PROFESSIONAL_TYPE = "TRAINING_CENTER_PROFESSIONAL_TYPE";
        public const string TRAINING_CENTER_PROFESSIONAL_STATUS = "TRAINING_CENTER_PROFESSIONAL_STATUS";
        public const string TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR = "TRAINING_CENTER_PROFESSIONAL_INSTRUCTOR";
        public const string TRAINING_CENTER_PROFESSIONAL_COLUMN = "TRAINING_CENTER_PROFESSIONAL_COLUMN";
        public const string TRAINING_CENTER_PROFESSIONAL_ORDER = "TRAINING_CENTER_PROFESSIONAL_ORDER";
        public const string TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX = "TRAINING_CENTER_PROFESSIONAL_PAGE_INDEX";
        public const string TRAINING_CENTER_PROFESSIONAL_ROW = "TRAINING_CENTER_PROFESSIONAL_ROW";

        public static string HTML_TEMPLATE_PATH_TRAINING_CENTER_APPROVE = "~/Content/HtmlTemplate/TrainingCenter_Approved.html";
        public static string HTML_TEMPLATE_PATH_TRAINING_CENTER_REJECT = "~/Content/HtmlTemplate/TrainingCenter_Rejected.html";

        public static readonly int TRAINING_CENTER_SKILL_TYPE_TOEIC = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_SKILL_TYPE_TOEIC"]);
        public static readonly int TRAINING_CENTER_SKILL_TYPE_TOEIC_VERBAL = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_SKILL_TYPE_TOEIC_VERBAL"]);
        public static readonly int TRAINING_CENTER_COURSE_TYPE_PRO_SKILL = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_COURSE_TYPE_PRO_SKILL"]);
        public static readonly int TRAINING_CENTER_COURSE_TYPE_ENGLISH = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_COURSE_TYPE_ENGLISH"]);
        public static readonly int TRAINING_CENTER_COURSE_STATUS_OPEN = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_COURSE_STATUS_OPEN"]);
        public static readonly int TRAINING_CENTER_COURSE_STATUS_CLOSED = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_COURSE_STATUS_CLOSED"]);
        public static readonly int TRAINING_CENTER_CLASS_TYPE_PRO_SKILL = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_TYPE_PRO_SKILL"]);
        public static readonly int TRAINING_CENTER_CLASS_TYPE_ENGLISH = ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_TYPE_ENGLISH"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_TYPE_SCORE =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_TYPE_SCORE"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_TYPE_PASS_FAIL =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_TYPE_PASS_FAIL"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_TYPE_COMMENT =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_TYPE_COMMENT"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_TYPE_NO_RESULT =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_TYPE_NO_RESULT"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_PASS =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_PASS"]);
        public static readonly int TRAINING_CENTER_CLASS_RESULT_FAIL =
            ConvertUtil.ConvertToInt(ConfigurationManager.AppSettings["TRAINING_CENTER_CLASS_RESULT_FAIL"]);
        public const string TRAINING_CENTER_LIST_COURSE_TYPE_LABEL = "--Select Type--";
        public const string TRAINING_CENTER_LIST_COURSE_STATUS_LABEL = "--Select Status--";
        public const string TRAINING_CENTER_LIST_REG_STATUS_LABEL = "--Select Status--";
        public const string TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL = "Course Name or ID";
        public const string TRAINING_ACTION = "tc_action";


        public const string TRAINING_CENTER_LIST_E_CLASS_ATTEND = "tc_list_eclass";
        public const string TRAINING_CENTER_LIST_PROF_CLASS_ATTEND = "tc_list_profclass";
        public const string TRAINING_CENTER_LIST_E_CLASS_NOT_ATTEND = "tc_list_eclass_not_attend";
        public const string TRAINING_CENTER_LIST_PROF_CLASS_NOT_ATTEND = "tc_list_prof_class_not_attend";
        public const string TRAINING_CENTER_LIST_EXAM_ATTENDANCE = "tc_list_exam_attendance";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME = "tc_eng_course_attend_name";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE = "tc_eng_course_attend_title";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT = "tc_eng_course_attend_dept";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER = "tc_eng_course_attend_manager";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN = "TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER = "TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX = "TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX";
        public const string TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW = "TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW";

        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_NAME_LABEL = "EmpId or EmpName or Test Title";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_DEPT_LABEL = "--Select department--";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_TEST_LABEL = "Test Title";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_FROM_DATE_LABEL = "--Select From Date--";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_TO_DATE_LABEL = "--Select To Date--";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_NAME = "tc_eng_test_tracking_name";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_DEPT = "tc_eng_test_tracking_dept";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_TEST = "tc_eng_test_tracking_test";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_FROM = "tc_eng_test_tracking_from";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_TO = "tc_eng_test_tracking_to";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_TYPE = "tc_eng_test_tracking_to";
        public const int TRAINING_CENTER_LIST_ENG_TEST_TRACKING_WRITING = 1;
        public const int TRAINING_CENTER_LIST_ENG_TEST_TRACKING_VERBAL = 2;
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_COLUMN = "TRAINING_CENTER_LIST_ENG_TEST_TRACKING_COLUMN";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_ORDER = "TRAINING_CENTER_LIST_ENG_TEST_TRACKING_ORDER";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_PAGE_INDEX = "TRAINING_CENTER_LIST_ENG_TEST_TRACKING_PAGE_INDEX";
        public const string TRAINING_CENTER_LIST_ENG_TEST_TRACKING_ROW = "TRAINING_CENTER_LIST_ENG_TEST_TRACKING_ROW";
        
        public const int TRAINING_CENTER_COURSE_LIST_OVERVIEW_MAXLENGTH = 300;
        public const string TRAINING_CENTER_MY_PROFILE_CHART_SEPARATOR = "|";
        public const int DEPARTMENT_RnD_ID = 400;
        public const int DEPARTMENT_ES_ID = 500;
        public const int NUM_COURSES_PER_ROW = 3;
        public const int NUM_CLASSES_PER_ROW = 3;

        public const string TC_COURSE_DETAIL_COURSE = "TC_COURSE_DETAIL_COURSE";
        public const string TC_COURSE_DETAIL_TYPE = "TC_COURSE_DETAIL_TYPE";
        public const string TC_COURSE_DETAIL_COLUMN = "TC_COURSE_DETAIL_COLUMN";
        public const string TC_COURSE_DETAIL_ORDER = "TC_COURSE_DETAIL_ORDER";
        public const string TC_COURSE_DETAIL_PAGE_INDEX = "TC_COURSE_DETAIL_PAGE_INDEX";
        public const string TC_COURSE_DETAIL_ROW_COUNT = "TC_COURSE_DETAIL_ROW_COUNT";
        #endregion

        #region Training Material
        //public const string MATERIAL_FOLDER = "/FileUpload/TrainingMaterial/";
        public const string MATERIAL_FOLDER = TRAINING_CENTER_MATERIAL_UPLOAD_FOLDER;

        public const int TRAINING_PROF_CLASS = 1;
        public const int TRAINING_ENG_CLASS = 2;
        public const int TRAINING_PROF_COURSE = 3;
        public const int TRAINING_ENG_COURSE = 4;
        public const int TRAINING_MATERIAL_PROF_COURSE = 1;
        public const int TRAINING_MATERIAL_ENG = 2;
        public const int TRAINING_MATERIAL_CATEGORY = 3;


        public static readonly int TRAINING_MATERIAL_TYPE_COURSE = int.Parse(ConfigurationManager.AppSettings["TRAINING_MATERIAL_TYPE_COURSE"]);
        public static readonly int TRAINING_MATERIAL_TYPE_CATEGORY = int.Parse(ConfigurationManager.AppSettings["TRAINING_MATERIAL_TYPE_CATEGORY"]);
        public static readonly int TRAINING_MATERIAL_PERMISSON_PUBLIC = int.Parse(ConfigurationManager.AppSettings["TRAINING_MATERIAL_PERMISSON_PUBLIC"]);
        public static readonly int TRAINING_MATERIAL_PERMISSON_INCOURSE = int.Parse(ConfigurationManager.AppSettings["TRAINING_MATERIAL_PERMISSON_INCOURSE"]);
        public static readonly int TRAINING_MATERIAL_PERMISSON_ADMIN = int.Parse(ConfigurationManager.AppSettings["TRAINING_MATERIAL_PERMISSON_ADMIN"]);

        public const int TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE = 1;
        public const int TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE = 2;
        public const int TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY = 3;
        public const string TRAINING_CENTER_MATERIAL_TXT_KEYWORD_LABEL = "Name or Description";
        public const string TRAINING_CENTER_MATERIAL_LIST_COURSE_LABEL = "--Select Course--";
        public const string TRAINING_CENTER_MATERIAL_LIST_CATEGORY_LABEL = "--Select Category--";
        public const int TRAINING_CENTER_MATERIAL_LIST_DESCRIPTION_MAXLENGTH = 300;
        public const string TRAINING_CENTER_MATERIAL_UPLOAD_FOLDER = "/FileUpload/TrainingMaterial/";
        public const string TRAINING_CENTER_DASHBOARD_RECORD_LABEL = "Employee Name or ID";

        public const string MATERIAL_PAGE_ICON_NAME_PREFIX = "[materialfile]";
        public static readonly long MATERIAL_PAGE_FILE_MAX_SIZE = long.Parse(ConfigurationManager.AppSettings["MATERIAL_PAGE_FILE_MAX_SIZE"]);//in MByte
        public static readonly string MATERIAL_PAGE_FILE_FORMAT_ALLOWED = ConfigurationManager.AppSettings["MATERIAL_PAGE_FILE_FORMAT_ALLOWED"];
        #endregion

        #region Asset
        public const string ASSET_CAT_LIST_COLUMN = "asset_cat_list_column";
        public const string ASSET_CAT_LIST_ORDER = "asset_cat_list_order";
        public const string ASSET_CAT_LIST_PAGE_INDEX = "asset_cat_list_page_index";
        public const string ASSET_CAT_LIST_ROW_COUNT = "asset_cat_list_row_count";
        public const string ASSET_CAT_FIRST_ITEM_CATEGORY = "- Category -";

        public const string ASSET_PROP_LIST_COLUMN = "asset_prop_list_column";
        public const string ASSET_PROP_LIST_ORDER = "asset_prop_list_order";
        public const string ASSET_PROP_LIST_PAGE_INDEX = "asset_prop_list_page_index";
        public const string ASSET_PROP_LIST_ROW_COUNT = "asset_prop_list_row_count";

        public const string ASSET_PROP_EXPORT_EXCEL_NAME = "PropList.xls";
        public const string ASSET_PROP_TILE_EXPORT_EXCEL = "Property List";

        //AssetMaster
        public const string ASSET_MASTER_PROPERTY_LIST = "asset master property list";

        public const string ASSET_MASTER_FIRST_ITEM_CATEGORY = "-Category-";
        public const string ASSET_MASTER_FIRST_ITEM_STATUS = "-Status-";
        public const string ASSET_MASTER_FIRST_ITEM_PROJECT = "-Project-";

        public const int ASSET_MASTER_ACTIVE = 1;
        public const int ASSET_MASTER_NOT_ACTIVE = 0;
        public const string ASSET_MASTER_LIST = "asset_master_list";
        public const string ASSET_MASTER_LIST_COLUMN = "asset_master_list_column";
        public const string ASSET_MASTER_LIST_ORDER = "asset_master_list_order";
        public const string ASSET_MASTER_LIST_PAGE_INDEX = "asset_master_list_page_index";

        public const string ASSET_MASTER_LIST_ROW_COUNT = "asset_master_list_row_count";
        public const string ASSET_MASTER_PAGE_TITLE = "Asset Master List";


        public const string ASSET_MASTER_CATEGORY_LIST = "asset_master_category_list";
        public const string ASSET_MASTER_STATUS_LIST = "asset_master_status_list";

        public const char ASSET_MASTER_COLUMN_SIGN = '$';
        public const char ASSET_MASTER_ROW_SIGN = '|';
        public const char ASSET_MASTER_CUSTOM_SIGN = '|';

        //AssetMaster text search
        public const string ASSET_MASTER_ASSETID_USERNAME_USERID = "Asset ID or Username or User ID";
        public const string ASSET_LIST_NAME = "asset list name";
        public const string ASSET_LIST_CATEGORY = "asset list category";
        public const string ASSET_LIST_STATUS = "asset list status";
        public const string ASSET_LIST_EMPLOYEE_PROJECT = "asset list employee project";
        public const string ASSET_MASTER_EXPORT_EXCEL_NAME = "AssetMasterList.xls";
        public const string ASSET_MASTER_TITLE_EXPORT_EXCEL = "ASSET MASTER LIST";

        //Employee's Asset
        public const string EMPLOYEE_ASSET_DEFAULT = "Username or User ID";
        public const string EMPLOYEE_ASSET_FIRST_DEPARTMENT = "- Department -";
        public const string EMPLOYEE_ASSET_FIRST_JOB_TITLE = "- Job Title -";
        public const string EMPLOYEE_ASSET_FIRST_PROJECT = "- Project -";
        public const string EMPLOYEE_ASSET_LIST_NAME = "employee asset list name";
        public const string EMPLOYEE_ASSET_LIST_DEPARTMENT = "employee asset list department";
        public const string EMPLOYEE_ASSET_LIST_JOB_TITLE = "employee asset list job title";
        public const string EMPLOYEE_ASSET_LIST_PROJECT = "employee asset list project";
        public const string EMPLOYEE_ASSET_LIST = "employee_asset_list";
        public const string EMPLOYEE_ASSET_LIST_COLUMN = "employee_asset_list_column";
        public const string EMPLOYEE_ASSET_LIST_ORDER = "employee_asset_list_order";
        public const string EMPLOYEE_ASSET_LIST_PAGE_INDEX = "employee_asset_list_page_index";
        public const string EMPLOYEE_ASSET_LIST_ROW_COUNT = "employee_asset_list_row_count";
        public const string EMPLOYEE_ASSET_SUBLIST_COLUMN = "employee_asset_list_column";
        public const string EMPLOYEE_ASSET_SUBLIST_ORDER = "employee_asset_list_order";
        public const string EMPLOYEE_ASSET_SUBLIST_PAGE_INDEX = "employee_asset_list_page_index";
        public const string EMPLOYEE_ASSET_SUBLIST_ROW_COUNT = "employee_asset_list_row_count";
        public const string EMPLOYEE_ASSET_PAGE_TITLE = "Employee Asset List";

        // Assign Asset to Employee
        public const string ASSIGN_ASSET_TO_EMPLOYEE_EMPLOYEE_ID = "Employee ID";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_SEARCH_TEXT = "Asset ID";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_PAGE_TITLE = "Assign Asset To Employee";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_NAME = "assign asset to employee list name";
        public const string ASSIGN_ASSET_LIST_COLUMN = "assign_asset_list_column";
        public const string ASSIGN_ASSET_LIST_ORDER = "assign_asset_list_order";
        public const string ASSIGN_ASSET_LIST_PAGE_INDEX = "assign_asset_list_page_index";
        public const string ASSIGN_ASSET_LIST_ROW_COUNT = "assign_asset_list_row_count";
        public const int ASSIGN_ASSET_AVAILABLE_STATUS = 2;
        public const int ASSIGN_ASSET_INUSE_STATUS = 1;
        public const string ASSIGN_ASSET_EMPLOYEE_NULL = null;

        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_CATEGORY = "assign_asset_to_employee_list_category";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_COLUMN = "assign_asset_to_employee_list_column";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_ORDER = "assign_asset_to_employee_list_order";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_PAGE_INDEX = "assign_asset_to_employee_list_page_index";
        public const string ASSIGN_ASSET_TO_EMPLOYEE_LIST_ROW_COUNT = "assign_asset_to_employee_list_row_count";

        //Project's Asset
        public const string PROJECT_ASSET_DEFAULT_SEARCH_TEXT = "Asset Id or Employee or Seatcode or Project";
        public const string PROJECT_ASSET_DEFAULT_CATEGORY = "- Category -";
        public const string PROJECT_ASSET_DEFAULT_DEPARTMENT = "- Department -";
        public const string PROJECT_ASSET_DEFAULT_PROJECT = "- Project -";
        public const string PROJECT_ASSET_LIST_SEARCH_TEXT = "project_asset_list_search_text";
        public const string PROJECT_ASSET_LIST_DEPARTMENT = "project_asset_list_department";
        public const string PROJECT_ASSET_LIST_CATEGORY = "project_asset_list_category";
        public const string PROJECT_ASSET_LIST_PROJECT = "project_asset_list_project";
        public const string PROJECT_ASSET_LIST = "project_asset_list";
        public const string PROJECT_ASSET_LIST_COLUMN = "project_asset_list_column";
        public const string PROJECT_ASSET_LIST_ORDER = "project_asset_list_order";
        public const string PROJECT_ASSET_LIST_PAGE_INDEX = "project_asset_list_page_index";
        public const string PROJECT_ASSET_LIST_ROW_COUNT = "project_asset_list_row_count";
        public const string PROJECT_ASSET_PAGE_TITLE = "Project Asset List";
        #endregion

        #region AAsset
        // Control labels
        public const string A_ASSET_FILTER_TXT_KEYWORD_LABEL = "Asset or Employee ...";
        public const string A_ASSET_FILTER_DDL_CATEGORY_LABEL = "-Select Category-";
        public const string A_ASSET_FILTER_DDL_STATUS_LABEL = "-Select Status-";

        public const string A_ASSET_FILTER_DDL_BRANCH_LABEL = "-Select Branch-";
        public const string A_ASSET_FILTER_DDL_DEPT_LABEL = "-Select Department-";
        public const string A_ASSET_FILTER_DDL_PROJECT_LABEL = "-Select Project-";
        public const string A_ASSET_FILTER_DDL_MANGER_LABEL = "-Select Manager-";

        // Master Datas
        public const int A_STATUS_AVAILABLE = 1;
        public const int A_STATUS_IN_USE = 2;
        public const int A_STATUS_RESERVED = 3;
        public const int A_STATUS_MAINTENANCE = 4;

        /*--- Adding Sub-Assets ---*/
        public const string A_ASSET_ADD_SUB_FILTER_TXT_KEYWORD_LABEL = "Asset Id ...";
        public const string A_ASSET_ADD_SUB_FILTER_DDL_CATEGORY_LABEL = "-Select Category-";
        public const string A_ASSET_ADD_SUB_FILTER_DDL_STATUS_LABEL = "-Select Status-";

        /*--- Prefix and Suffix and separation ---*/
        public const string A_ASSET_FILTER_PREFIX_TOOLTIP_ID = "a_pre_tooltip_id_";

        public const string A_ASSET_ADVANCE_PREFIX_DYNAMIC_ID = "a_pre_id_";
        public const string A_ASSET_PREFIX_DDL_LABEL = "-Select ";
        public const string A_ASSET_SUFFIX_DDL_LABEL = "-";
        public const char A_ASSET_SEPARATION_PROPERTY_MASTER_DATA = ';';
        public const char A_ASSET_SEPARATION_PROPERTY_KEY_VALUE = '|';

        /*--- Advance Search ---*/
        public const string A_ASSET_ADVANCE_DDL_OWNER_LABEL = "-Select Owner-";

        /*--- Add/Edit Asset ---*/
        public const string A_ASSET_ADD_EDIT_ASSET_ID_LABEL = "Asset Id ...";
        public const string A_ASSET_ADD_EDIT_OWNER_LABEL = "- Select Owner -";
        public const string A_ASSET_ADD_EDIT_STATUS_LABEL = "- Select Status -";
        public const string A_ASSET_ADD_EDIT_CATEGORY_LABEL = "- Select Category -";

        public const string A_ASSET_ID = "ID";
        public const string A_ASSET_ASSET_ID = "AssetId";
        public const string A_ASSET_OWNER_ID = "OwnerId";
        public const string A_ASSET_OWNER_NAME = "OwnerName";
        public const string A_ASSET_CATEGORY_ID = "CategoryId";
        public const string A_ASSET_CATEGORY_NAME = "CategoryName";
        public const string A_ASSET_STATUS_ID = "StatusId";
        public const string A_ASSET_STATUS_NAME = "StatusName";
        public const string A_ASSET_PARENT_ID = "ParentId";
        public const string A_ASSET_PARENT_ASSET = "ParentAsset";
        public const string A_ASSET_REMARK = "Remark";

        public const char A_ASSET_COLUMN_SIGN = '$';
        public const char A_ASSET_ROW_SIGN = '|';
        public const char A_ASSET_VALUE_SIGN = '|';

        /*--- Asset Comment ---*/
        public const string A_ASSET_COMMENT_LIST = "Remark";
        #endregion

        #region Time Management
        // Storing names of InDoor
        public static List<string> Time_InDoor
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("MAN IN");
                list.Add("BLACK EXIT");
                return list;
            }
        }
        // Storing names of OutDoor
        public static List<string> Time_OutDoor
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("MAN OUT");
                list.Add("BLACK ENTER");
                return list;
            }
        } 

        public const int TIME_REPORT_KIND_LATE_AND_EARLY = 1;
        public const int TIME_REPORT_KIND_LATE_OR_EARLY = 2;
        public const int TIME_REPORT_KIND_LATE = 3;
        public const int TIME_REPORT_KIND_EARLY = 4;
        public const int TIME_REPORT_KIND_STAY_LATE = 5;
        public const int TIME_REPORT_KIND_NOT_IN = 6;
        public const int TIME_REPORT_KIND_NOT_OUT = 7;
        public const int TIME_REPORT_KIND_NOT_CHECK = 8;
        public const int TIME_REPORT_KIND_FULL = 9;
        public static List<ListItem> TimeManagementReportKindsList
        {
            get
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("Late coming & Early leaving", TIME_REPORT_KIND_LATE_AND_EARLY.ToString()));
                list.Add(new ListItem("Late coming or Early leaving", TIME_REPORT_KIND_LATE_OR_EARLY.ToString()));
                list.Add(new ListItem("Late coming", TIME_REPORT_KIND_LATE.ToString()));
                list.Add(new ListItem("Early leaving", TIME_REPORT_KIND_EARLY.ToString()));
                list.Add(new ListItem("Staying late", TIME_REPORT_KIND_STAY_LATE.ToString()));
                list.Add(new ListItem("Not checked-in", TIME_REPORT_KIND_NOT_IN.ToString()));
                list.Add(new ListItem("Not checked-out", TIME_REPORT_KIND_NOT_OUT.ToString()));
                list.Add(new ListItem("Not checked in & out", TIME_REPORT_KIND_NOT_CHECK.ToString()));
                list.Add(new ListItem("In & Out Report", TIME_REPORT_KIND_FULL.ToString()));
                return list;
            }
        }

        public static List<ListItem> TimeManagementHoursList
        {
            get
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("00", "00"));
                list.Add(new ListItem("01", "01"));
                list.Add(new ListItem("02", "02"));
                list.Add(new ListItem("03", "03"));
                list.Add(new ListItem("04", "04"));
                list.Add(new ListItem("05", "05"));
                list.Add(new ListItem("06", "06"));
                list.Add(new ListItem("07", "07"));
                list.Add(new ListItem("08", "08"));
                list.Add(new ListItem("09", "09"));
                list.Add(new ListItem("10", "10"));
                list.Add(new ListItem("11", "11"));
                list.Add(new ListItem("12", "12"));
                list.Add(new ListItem("13", "13"));
                list.Add(new ListItem("14", "14"));
                list.Add(new ListItem("15", "15"));
                list.Add(new ListItem("16", "16"));
                list.Add(new ListItem("17", "17"));
                list.Add(new ListItem("18", "18"));
                list.Add(new ListItem("19", "19"));
                list.Add(new ListItem("20", "20"));
                list.Add(new ListItem("21", "21"));
                list.Add(new ListItem("22", "22"));
                list.Add(new ListItem("23", "23"));
                return list;
            }
        }

        public static List<ListItem> TimeManagementMinutesList
        {
            get
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("00", "00"));
                list.Add(new ListItem("05", "05"));
                list.Add(new ListItem("10", "10"));
                list.Add(new ListItem("15", "15"));
                list.Add(new ListItem("20", "20"));
                list.Add(new ListItem("25", "25"));
                list.Add(new ListItem("30", "30"));
                list.Add(new ListItem("35", "35"));
                list.Add(new ListItem("40", "40"));
                list.Add(new ListItem("45", "45"));
                list.Add(new ListItem("50", "50"));
                list.Add(new ListItem("55", "55"));
                return list;
            }
        }

        public const string TIME_MANAGEMENT_FILTER_TXT_KEYWORD_LABEL = "Emp Id or Name";
        public const string TIME_MANAGEMENT_FILTER_TOP_N_LABEL = "Top N";
        public const string TIME_MANAGEMENT_FILTER_DEPARTMENT_LABEL = "Select Department";
        public const string TIME_MANAGEMENT_REPORT_KINDS_LABEL = "Select Reports";
        
        #endregion

        #region SR_Survey added by Nhan Tran
        public const string SR_SURVEY_QUESTION = "sr_survey_question";
        public static readonly int SR_SURVEY_DEFAULT_QUESTION_ID = Int32.Parse(ConfigurationManager.AppSettings["SR_SURVEY_DEFAULT_QUESTION_ID"]);
        public const char SEPARATE_MARKS_CHAR = ',';  
        #endregion
        #region UserAdmin
        public static string USER_ADMIN_DEFAULT_LABEL = "--Administrator--";
        #endregion

    }
}


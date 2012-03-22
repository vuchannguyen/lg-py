using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Models;

namespace CRM.Library.Common
{
    public class SessionKey
    {
        #region STT List
        public const string STT_LIST = "stt_list";
        public const string STT_FILTER = "stt_filter";     
        #endregion

        #region AAsset
        public const string A_ASSET_FILTER_VALUES = "a_asset_filter_values";
        public const string A_ASSET_ADD_SUB_FILTER_VALUES = "a_asset_add_sub_filter_values";
        #endregion


        #region Employee
        public const string EMPLOYEE_LIST_SORT = "employee_list_sort";
        public const string RESIGNED_EMPLOYEE_LIST_SORT = "resigned_employee_list_sort"; 
		public const string RESIGNED_EMPLOYEE_DEFAULT_VALUE = "Resigned_Employee";
        public const string EMPLOYEE_DEFAULT_VALUE = "Employee";
        
        #endregion

        #region Asset
        public const string ASSET_CATEGORY = "AssetCategory";
        public const string ASSET_PROPERTY = "AssetProperty";
        public const string ASSET_MASTER = "AssetMaster";
        public const string EMPLOYEE_ASSET = "EmployeeAsset";
        public const string ASSET_DEFAULT_VALUE = "Asset";
		public const string TRAINING_CENTER_ENGLISH_SKILL = "training_center_english_skill";
        public const string TRAINING_CENTER_VERBAL_SKILL = "training_center_verbal_skill";

        //Hang
        public const string TRAINING_CENTER_CLASS_ENGLISHSKILL = "EnglishSkill";
        public const string TRAINING_CENTER_CLASS_VERBALSKILL = "VerbalSkill";
        public const string TRAINING_CENTER_NONEMPOYEE_FILTER = "Nonempoyee";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST = "NonempoyeeCertification";
		#region Nonemployee Cerfitication
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_DEFAULT_VALUE = "training nonemployee certification";

        #endregion
        #endregion

        #region Asset Master
        public const string ASSET_MASTER_DEFAULT_VALUE = "Asset Master";
        public const string PROJECT_ASSET_DEFAULT_VALUE = "Project Asset";
        public const string EMPLOYEE_ASSET_DEFAULT_VALUE = "Employee Asset";
        public const string EMPLOYEE_ASSET_SUBLIST = "Employee Asset Sublist";
        public const string PREVIOUS_PAGE = "Previous Page";
        #endregion

        #region Assign Asset To Employee
        public const string ASSIGN_ASSET_DEFAULT_VALUE = "Assign Asset To Employee";
        public const string EMPLOYEE_ASSIGN_ASSET_DEFAULT_VALUE = "Assign Asset To Employee";
        public const string EMPLOYEE_ASSIGN_ASSET_EMPLOYEE_ID = "EAA Employee ID";
        #endregion

        #region Training Certification
        public const string TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE = "training ceertification master";

        #endregion
        
        #region Training Employee Certification
        public const string TRAINING_EMPLOYEE_CERTIFICATION_DEFAULT_VALUE = "training employee certification";
        #endregion

        #region WorkFlow Admin
        public const string JR_ADMIN_FILTER = "jr_admin_filter"; 
        #endregion

        #region Candidate
        public const string CANDIDATE_FILTER = "candidate_filter";
        public const string INTERVIEW_FILTER = "interview_filter";
        public const string INTERVIEW_HISTORY_FILTER = "interview_history_filter";
        public const string INTERVIEW_LIST = "interview_list";
        public const string INTERVIEW_HISTORY_LIST = "interview_history_list";
        public const string HOME_STATISTIC_LIST = "home_statistic_list";
        
        #endregion

        #region HitCounter
        public const string SESSION_BROWSE_ID = "session_browse_id";
        public const string SESSION_PORTAL_BROWSE_ID = "session_portal_browse_id";
        #endregion

        #region Purchase request
        public const string PURCHASE_FILTER = "purchase_filter";
        public const string PURCHASE_FILTER_US = "purchase_filter_us";
        public const string PURCHASE_FILTER_REPORT = "purchase_filter_report";        
        #endregion

        #region PTO
        public const string PTO_EMPLOYEE_FILTER = "pto_emp_filter";        
        #endregion

        #region PTO Manager
        public const string PTO_MANAGER_FILTER = "pto_manager_filter";
        #endregion

        #region Job Title Level
        public const string JTL_JOBTITLE_LEVEL_LIST = "JobTitleLevelList";
        #endregion

        #region Manage Workflow
        public const string MANAGE_WORKFLOW = "MANAGE_WORKFLOW";
        #endregion

        #region JobTitle

        public const string JOB_TITLE_DEFAULT_VALUE = "Job_Title";

        #endregion

        #region JobTitleLevel

        public const string JOB_TITLE_LEVEL_DEFAULT_VALUE = "Job_Title_Level";

        #endregion

        #region Group

        public const string GROUP_DEFAULT_VALUE = "Group";

        #endregion

        #region Account

        public const string ACCOUNT_DEFAULT_VALUE = "Account";

        #endregion

        #region Candidate List

        public const string CANDIDATELIST = "candidatelist";

        #endregion


        #region PR_List

        public const string PR_DEFAULT_VALUE = "PR_List";

        #endregion

        #region User_Log

        public const string USER_LOG_DEFAULT_VALUE = "User_Log";
        public const string USER_LOG_STATISTIC_VALUE = "User_Log_Statistic";

        #endregion

        #region Annual_Holiday

        public const string ANNUAL_HOLIDAY_DEFAULT_VALUE = "Annual_Holiday";

        #endregion

        #region PTO_Admin

        public const string PTO_ADMIN_DEFAULT_VALUE = "PTO_Admin";
        public const string PTO_USER_LOG_DEFAULT_VALUE = "Pto_User_Log";
        #endregion

        #region PTO_Confirm

        public const string PTO_CONFIRM_DEFAULT_VALUE = "PTO_Confirm";

        #endregion

        #region PTO_Report

        public const string PTO_REPORT_DEFAULT_VALUE = "PTO_Report";
        public const string PORTAL_PTO_REPORT_DEFAULT_VALUE = "portal_pto_report";

        #endregion

        #region Exam

        public const string EXAM_DEFAULT_VALUE = "Exam";

        #endregion

        #region Question

        public const string QUESTION_DEFAULT_VALUE = "Question";

        #endregion

        #region Performance review
        public const string PERFORMANCE_REVIEW_HR_FILTER = "pr_hr_filter";

        public const string PERFORMANCE_REVIEW_MANAGER = "performance_review_manager_filter";        
        #endregion

        #region Portal
        public const string PORTAL_POSITION = "portal_position";    
        #endregion

        #region Service Request
        public static string SR_CATEGORY_DEFAULT_SEARCH_VALUES = "sr_category_default_search_value";
        public static string SR_LIST_SEARCH_VALUES = "sr_list_default_search_value";
        public static string SR_LIST_ADMIN_SEARCH_VALUES = "sr_list_admin_default_search_value";
        public static string SR_REPORT_OC_LIST_ADMIN_SEARCH_VALUES = "sr_report_oc_list_admin_default_search_value";
        public static string SR_REPORT_ACTIVITY_LIST_ADMIN_SEARCH_VALUES = "sr_report_activity_list_admin_default_search_value";
        public static string SR_REPORT_WEEKLY_LIST_ADMIN_SEARCH_VALUES = "sr_report_weekly_list_admin_default_search_value";
        public static string SR_REPORT_LIST = "sr_report_list";
        public static string SR_REPORT_SURVEY_LIST_SEARCH_VALUES = "sr_report_survey_list_search_values";
        #endregion 

        #region Training Admin Confirmation
        public const string TRAINING_ADMIN_CONFIRMATION_LIST = "training_admin_confirmation_default_search_value";
        public const string TRAINING_ADMIN_CONFIRMATION_ATTENDEE_LIST = "training_admin_confirmation_attendee_default_search_value";
        #endregion

        #region Training Center
        public const string TRAINING_CENTER_ENGLISH_CLASS_DEFAULT_VALUES = "training_center_english_class";
        public const string TRAINING_EEI_DEFAULT_SEARCH_VALUES = "training_eei_default_search_value";
        public const string TRAINING_CENTER_ENG_COURSE_ATTEND_FILTER = "tc_eng_course_attend_filter";
        public const string TRAINING_CENTER_PRO_COURSE_ATTEND_FILTER = "tc_pro_course_attend_filter";
        public const string TRAINING_CENTER_ENG_SCORE_SHEET_FILTER = "tc_eng_score_sheet_filter";
        public const string TRAINING_CENTER_EXAM_QUESTION = "training_center_exam_question";
        public const string TRAINING_CENTER_ENGLISH_TEST_TRACKING = "training_center_english_test_tracking";

        #endregion

        #region TimeMangement

        public const string TIME_MANAGEMENT_FILTER_VALUES = "time_management_filter_values";

        #endregion
    }

    public class CommonDataKey
    {
        #region Portal Home

        public const string VIEW_LINK_POSITION = "view_link_position";
        public const string VIEW_LINK_CRM = "view_link_crm";

        public const string PORTAL_POSITION_BRANCH = "branch";
        public const string PORTAL_POSITION_FLOOR = "floor";
        public const string PORTAL_POSITION_SEATCODE = "seatcode";
        public const string PORTAL_POSITION_OFFICE = "office";
        #endregion

        #region Asset Master
        public const string AM_CATEGORY_LIST = "am_category_list";
        public const string AM_STATUS_LIST = "am_status_list";
        public const string AM_PROJECT_LIST = "am_project_list";
        #endregion

        #region Common
        public const string RETURN_URL = "ReturnUrl";
        public const string IS_ACCESSIBLE = "IsAccessible";
        #endregion

        #region Logigear Online Test

        #region Exam

        public const string EXAM_QUESTION = "ExamQuestion";
        public const string EXAM_QUESTION_ID = "ExamQuestionID";
        public const string EXAM_TYPE = "ExamType";
        public const string EXAM_ID = "ExamID";
        public const string EXAM_TITLE = "ExamTitle";
        public const string CANDIDATE_NAME = "CandidateName";
        public const string CANDIDATE_EXAM = "Candidate_Exam";

        #endregion

        #region Exam Question 

        public const string EXAM_QUESTION_TITLE = "ExamQuestionTitle";
        public const string SECTION_LIST = "SectionList";
        public const string NONE_VERBAL_SECTION_LIST = "NonVerbalSectionList";
        public const string EXAM_QUESTION_SECTION_ID = "ExamQuestionSectionID";
        public const string SECTION_SELECTED = "SectionSelected";
        public const string SECTION = "Section";
        public const string EXAM_QUESTION_SECTION = "ExamQuestion_Section";
        #endregion

        #endregion

        #region Question 
        /*Added by DuyTai.Nguyen*/
        public const string SECTION_NAME = "SectionName";
        public const string SELECTED_SECTION = "Selected_Section";
        public const string QUESTION = "Question";
        public const string QUESTION_ARR = "ArrQuestion";
        public const string QUESTION_ID_ARR = "QuestionIDs";
        public const string ANSWERS_LIST = "Answers";
        public const string TOPIC = "Topic";
        public const string REPEAT_TIMES = "RepeatTimes";
        public const string PROGRAMMING_SKILL_TYPE = "ProgrammingSkillType";
        public const string TEMP_SYSTEM_MESSAGE = "Message";
        public const string IS_IN_ANY_EXAM = "IsInAnyExam";
        public const string IS_ASSIGNED = "IsAssigned";
        public const string ANSWER_ARR = "ArrAnswers";
        public const string QUESTION_CONTENT = "Question_Content";
        public const string QUESTION_CONTENT_PROGRAMMING = "Question_Content_Programming";
        public const string QUESTION_TYPE_ID = "Question_Type_Id";
        #endregion

        #region PTOAdmin
        /*Added by DuyTai.Nguyen*/
        public const string PTO_STATUS_LIST = "PTO_StatusList";
        public const string PTO_TYPE_LIST = "PTOType_ID";
        public const string PTO_OBJECT = "PTO";
        public const string PTO_DETAILS = "PTO_Details";
        public const string PTO_IS_CONFIRM = "PTO_Is_Confirm";
        public const string PTO_MANAGER_LIST = "Manager";
        public const string PTO_ADMIN_TYPE_PARENT_ID = "TypeParent";
        public const string PTO_ADMIN_TYPE_ID = "Type";
        #endregion

        #region PTOManager
        /*Added by DuyTai.Nguyen*/
        public const string PTO_LOGIN_NAME = "PTO_StatusList";
        public const string PTO_IDS_IS_HOUR_TYPE = "pto_id_is_hour_type";
        public const string PTO_NOTICE_NEW = "pto_notice_new";
        public const string PTO_NOTICE_CONFIRM = "pto_notice_confirm";
        public const string PTO_USED_HOURS = "PTO_Used_hours";

        #endregion

        #region Purchase Request
        public const string IS_HOLD = "IsHold";
        public const string TO_GROUP = "ToGroup";
        public const string DEPARTMENT = "DepartmentName";
        public const string SUB_DEPARTMENT = "SubDepartment";
        public const string SALE_TAX_NAME = "SaleTaxName";
        public const string DDL_PR_PAYMENTMETHOD = "PRPayMethod";
        public const string DDL_PR_RESOLUTION = "WFResolutionID";
        public const string DDL_PR_STATUS = "WFStatusID";
        public const string DDL_PR_ASSIGN = "Assign";
        public const string SUB_TOTAL_ITEM = "SubTotalItem";
        public const string LIST_PURCHASE_ITEM = "ListItem";
        public const string COUNT_PURCHASE_ITEM = "CountItem";

        public const string COUNT_CERTIFICATION_ITEM = "CertificationItem";
        public const string PURCHASE_ID = "PurchaseID";
        public const string RESOLUTION_ID = "ResolutionId";
        public const string GROUP_APPROVAL_RESOLUTION = "Group_Approval_Resolution";
        public const string LIST_INVOICE = "ListInvoice";
        public const string LIST_APPROVAL = "ListApproval";
        public const string LIST_US_ACCOUNTING = "USAccounting";
        public const string PR_ROLE = "role";
        public const string PR_ROLE_ID = "roleId";
        public const string PR_COMMENT = "Comment";
        public const string PR_COMMENT_COUNT = "CommentCount";
        public const string PR_WORK_FLOW = "WorkFlow";
        public const string PR_LIST_COUNT = "ListCount";
        public const string PR_FORWARD = "Forward";
        public const string PR_MAX_APPROVAL = "MaxApproval";
        public const string PR_LIST = "PR_LIST";
        public const string PR_IS_VIEW_ALL = "ViewAllPR";
        public const string PR_USER_LOGIN_ID = "UserLoginId";
        public const string PR_USER_LOGIN_NAME = "UserLoginName";
        public const string PR_SELECTED_PURCHASING = "SelectedPurchasing";
        public const string PR_SELECTED_REQUESTOR = "SelecteRequestor";
        public const string PR_LOGIN_ROLE = "PR_LoginRole";
        public const string EXCHANGE_RATE = "ExchangeRate";
        #endregion

        #region Purchase Request US
        public const string IS_HOLD_US = "IsHold_Us";
        public const string TO_GROUP_US = "ToGroup_Us";
        public const string DEPARTMENT_US = "DepartmentName_Us";
        public const string SUB_DEPARTMENT_US = "SubDepartment_Us";
        public const string SALE_TAX_NAME_US = "SaleTaxName_Us";
        public const string DDL_PR_RESOLUTION_US = "WFResolutionID_Us";
        public const string DDL_PR_STATUS_US = "WFStatusID_Us";
        public const string DDL_PR_ASSIGN_US = "Assign_Us";
        public const string SUB_TOTAL_ITEM_US = "SubTotalItem_Us";
        public const string LIST_PURCHASE_ITEM_US = "ListItem_Us";
        public const string COUNT_PURCHASE_ITEM_US = "CountItem_Us";
        public const string PURCHASE_ID_US = "PurchaseID_Us";
        public const string RESOLUTION_ID_US = "ResolutionId_Us";
        public const string GROUP_APPROVAL_RESOLUTION_US = "Group_Approval_Resolution_Us";
        public const string LIST_INVOICE_US = "ListInvoice_Us";
        public const string LIST_APPROVAL_US = "ListApproval_Us";
        public const string LIST_US_ACCOUNTING_US = "USAccounting_Us";
        public const string PR_ROLE_US = "role_Us";
        public const string PR_ROLE_ID_US = "roleId_Us";
        public const string PR_COMMENT_US = "Comment_Us";
        public const string PR_COMMENT_COUNT_US = "CommentCount_Us";
        public const string PR_WORK_FLOW_US = "WorkFlow_Us";
        public const string PR_LIST_COUNT_US = "ListCount_Us";
        public const string PR_FORWARD_US = "Forward_Us";
        public const string PR_MAX_APPROVAL_US = "MaxApproval_Us";
        public const string PR_LIST_US = "PR_LIST_Us";
        public const string PR_IS_VIEW_ALL_US = "ViewAllPR_Us";
        public const string PR_USER_LOGIN_ID_US = "UserLoginId_Us";
        public const string PR_USER_LOGIN_NAME_US = "UserLoginName_Us";
        public const string PR_SELECTED_PURCHASING_US = "SelectedPurchasing_Us";
        public const string PR_SELECTED_REQUESTOR_US = "SelecteRequestor_Us";
        public const string PR_LOGIN_ROLE_US = "PR_LoginRole_Us";
        #endregion
        #region Candidate's Test List
        public const string CTL_WRITING_INFOS = "WritingInfos";
        public const string CTL_MAX_WRITING_MARK = "MaxWritingMark";        
        public const string CTL_DISPLAY_NAME = "DisplayName";
        public const string CTL_UPDATE_DATE = "UpdateDate";
        public const string CTL_WRITING_MARK = "WritingMark";
        public const string CTL_WRITING_COMMENT = "WritingComment";
        public const string CTL_MAX_PROGRAMING_MARK = "MaxProgramingMark";
        public const string CTL_PROGRAMING_MARK = "ProgramingMark";
        public const string CTL_PROGRAMING_COMMENT = "ProgramingComment";

        public const string CTL_TC_BUG_INFOS = "TcAndBugInfos";
        public const string CTL_TC_BUG_MARK = "TcAndBugMark";
        public const string CTL_MAX_TC_BUG_MARK = "MaxTcAndBugMark";
        public const string CTL_TC_BUG_COMMENT = "TcAndBugComment";

        #endregion

        #region PTO
        public const string PTO_TYPE_PARENT_ID = "PTOType_Parent_ID";
        public const string PTO_TYPE_ID = "PTOType_ID";
        public const string PTO_SUBMITTER = "Submitter";
        public const string PTO_VACATION_BALANCE= "Vacation_Balance";
        #endregion

        #region JobRequest
        public const string JR_CAN_VIEW_SALARY = "CanViewSalary";
        public const string JR_OBJECT = "CanViewSalary";
        public const string JR_ACTIONS = "JRActionButtons";
        #endregion

        #region Job Title Level

        public const string JTL_DEPARTMENT = "DepartmentId";
        public const string JTL_JOBTITLE = "JobTitleId";
        public const string JTL_JOBTITLE_LEVEL = "JobLevel";
        public const string JTL_JOBTITLE_LEVEL_LIST = "JobTitleLevelList";
        #endregion

        #region Performance Review: Added by Tai Nguyen
        public const string PER_REVIEW_STATUS_LIST = "status_list";
        public const string PER_REVIEW_NEED_LIST = "need_list";
        public const string PER_REVIEW_REQUESTOR_LIST = "requestor_list";
        public const string PER_REVIEW_DEPARTMENT_LIST = "department_list";
        public const string PER_REVIEW_EMPLOYEE_LIST = "pr_employee_list";
        public const string PER_REVIEW_EFORM_MASTER_LIST = "eform_master_list";
        public const string PER_REVIEW_ROLE_LIST = "role_list";
        public const string PER_REVIEW_USER_LIST = "user_list";
        public const string PER_REVIEW_RESOLUTION_LIST = "resolution_list";
        public const string PER_REVIEW_LOGIN_ROLE = "LoginRole";
        public const string PER_REVIEW_LOGIN_ROLE_NAME = "LoginRoleName";
        public const string PER_REVIEW_ID = "prId";
        public const string PER_REVIEW_PR_DATE = "PRDate";
        public const string PER_REVIEW_PR_NEXT_DATE = "NextReviewDate";
        public const string PER_REVIEW_PR_CC_EMAIL = "CCEmail";
        public const string PER_REVIEW_EMPLOYEE_OBJ = "prEmployeeObj";

        public const string PER_REVIEW_RESOLUTION = "WFResolutionID";
        public const string PER_REVIEW_TEMP_EMPLOYEE = "PER_REVIEW_TEMP_EMPLOYEE";
        public const string PER_REVIEW_ASSIGN = "Assign";
        public const string PER_REVIEW_FIRST_CHOISED_STATUS = "FirstChoiceStatus";
        public const string PER_REVIEW_FIRST_CHOISED_ASSIGN = "FirstChoiceAssign";
        public const string PER_REVIEW_OWNER_ID = "OwnerId";
        public const string PER_REVIEW_STATUS = "WFStatus";
        public const string PER_REVIEW_COMMENT = "CommentList";
        #endregion

        #region Position: Added by Tai Nguyen
        public const string POSTION_EMPLOYEE = "Employee";
        public const string POSTION_STT = "STT";
        public const string POSTION_MODEL_TYPE = "ModelType";
        public const string POSTION_FLOOR_LIST = "FLoor";
        #endregion

        #region Manage WorkFlow
        public const string MWF_WORKFLOW = "wfID";
        public const string MWF_ROLE = "roleID";
        public const string MWF_RESOLUTION = "resolutionID";
        public const string MWF_STATUS = "statusID";
        #endregion

        #region Perchase Request: Added by Tai Nguyen
        public const string PURCHASE_REQUEST_ACTIONS = "ActionButtons";
        public const string PURCHASE_REQUEST_PRIORITY = "Priority";
        #endregion

        #region Employee
        public const string TEMP_EMPLOYEE_PR = "TEMP_EMPLOYEE_PR";
        #endregion

        #region Work Location: Added by Tai Nguyen
        public const string LOCATION_TEXTBOX_KEYWORD = "txtKeyword";
        public const string LOCATION_LIST_BRANCH = "lstBranch";
        public const string LOCATION_LIST_OFFICE = "lstOffice";
        public const string LOCATION_LIST_AVAILABLE = "lstAvailable";
        public const string LOCATION_LIST_FLOOR = "lstFloor";
        #endregion

        #region Org chart
        public const string ORG_EMP_ID = "EmpId";
        public const string ORG_MAX_SEAT = "maxSeat";
        public const string MAX_SEAT_CODE = "maxSeatCode";
        public const string LOCATION_CODE = "LocationCode";
        #endregion

        #region Menu
        public const string MENU_LIST = "meu_list";
        public const string MENU_PERMISSION_LIST = "menu_permisson_list";
        #endregion

        #region Service Request List        
        public const string SR_LIST = "sr_list";
        public const string SR_STATUS = "sr_status";
        public const string SR_CATEGORY = "sr_category";
        public const string SR_SUB_CATEGORY = "sr_sub_category";
        public const string SR_ASSIGN_TO = "sr_assign";
        public const string SR_ADMIN_STATUS = "sr_status_admin";
        public const string SR_ADMIN_CATEGORY = "sr_category_admin";
        public const string SR_ADMIN_SUB_CATEGORY = "sr_sub_category_admin";
        public const string SR_ADMIN_ASSIGN_TO = "sr_assign_admin";
        public const string SR_ADMIN_REQUESTOR = "sr_requestor_admin";
        public const string SR_LIST_LOGIN_ROLE_NAME = "sr_list_login_name";
        public const string SR_LIST_LOGIN_ROLE = "sr_list_login_role";
        public const string SR_URGENT = "sr_urgent";
        #endregion

        #region ServiceRequest added by Tai Nguyen
        public const string SR_CATEGORY_SEARCH_TEXT = "sr_category_search_text";
        public const string SR_CATEGORY_SEARCH_TYPE = "sr_category_search_type";// category or subcategory
        public const string SR_CATEGORY_SEARCH_CATEGORY = "sr_category_search_category";
        public const string SR_CATEGORY_SEARCH_STATUS = "sr_category_search_status";//is active or not
        public const string SR_CATEGORY_SEARCH_SORT_COLUMN = "sr_category_search_sort_column";
        public const string SR_CATEGORY_SEARCH_SORT_ORDER = "sr_category_search_sort_order";
        public const string SR_CATEGORY_SEARCH_PAGE_INDEX = "sr_category_search_sort_page_index";
        public const string SR_CATEGORY_SEARCH_ROW_COUNT = "sr_category_search_sort_row_count";
        public const string SR_CATEGORY_LIST = "sr_category_list";
        public const string SR_SUB_CATEGORY_LIST = "sr_sub_category_list";
        public const string SR_URGENCY_LIST = "sr_urgency_list";
        public const string SR_HOURS_LIST = "sr_hours_list";
        public const string SR_MINUTES_LIST = "sr_minutes_list";
        public const string SR_HOURS_LIST_START_TIME = "sr_hours_list_start_time";
        public const string SR_HOURS_LIST_END_TIME = "sr_hours_list_end_time";
        public const string SR_STATUS_LIST = "sr_status_list";
        public const string SR_ASSIGNED_TO_LIST = "sr_assigned_to_list";
        #endregion

        #region Training Center
        public const string TRAINING_EEI_SKILL_TYPE_LIST = "training_eei_skill_type_list";
        public const string TRAINING_EEI_SEARCH_TEXT = "training_eei_search_text";
        public const string TRAINING_EEI_SEARCH_TYPE = "training_eei_search_type";

        public const string TRAINING_EEI_SEARCH_SORT_COLUMN = "training_eei_search_sort_column";
        public const string TRAINING_EEI_SEARCH_SORT_ORDER = "training_eei_search_sort_order";
        public const string TRAINING_EEI_SEARCH_PAGE_INDEX = "training_eei_search_page_index";
        public const string TRAINING_EEI_SEARCH_ROW_COUNT = "training_eei_search_row_count";

        public const string TRAINING_CENTER_COURSE_TYPE = "training_center_course_type";
        public const string TRAINING_CENTER_COURSE_STATUS = "training_center_course_status";
        public const string TRAINING_CENTER_REG_STATUS = "training_center_reg_status";
        public const string TRAINING_CENTER_EMP_ID = "training_center_emp_id";

        public const string TRAINING_CENTER_ENGLISH_SKILL = "training_center_english_skill";
        public const string TRAINING_CENTER_VERBAL_SKILL = "training_center_verbal_skill";

        
        public const string TRAINING_CENTER_CLASS_ENGLISHSKILL = "EnglishSkill";
        public const string TRAINING_CENTER_CLASS_VERBALSKILL = "VerbalSkill";
        public const string TRAINING_CENTER_NONEMPOYEE_FILTER = "Nonempoyee";
        
        public const string TRAINING_CENTER_NONEMPLOYEE_CERTIFICATION_LIST = "tc_certificationList";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_LIST = "NonempoyeeCertification";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATE_VALUE = "none_employee_certificate_value";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATION_DETAIL = "tc_nonemployee_certification_Details";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATE_ID = "none_employee_certificate_id";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATE_NAME = "LastName";
        public const string TRAINING_NONEMPLOYEE_CERTIFICATE_TYPE = "TypeID";

        public const string TRAINING_CENTER_SCORE_SKILL = "training_center_score_skill";
        public const string TRAINING_CENTER_LEVEL_SKILL = "training_center_level_skill";
        public const string TRAINING_CENTER_CERT_SCORE_SKILL = "training_center_cert_score_skill";
        public const string TRAINING_CENTER_CERT_LEVEL_SKILL = "training_center_cert_level_skill";
        public const string TRAINING_CENTER_SCORE_VERBAL = "training_center_score_verbal";
        public const string TRAINING_CENTER_TOEIC_VERBAL = "training_center_toeic_verbal";
        public const string TRAINING_CENTER_LEVEL_VERBAL = "training_center_level_verbal";
        public const string TRAINING_CENTER_CERT_SCORE_VERBAL = "training_center_cert_score_verbal";
        public const string TRAINING_CENTER_CERT_LEVEL_VERBAL = "training_center_cert_level_verbal";
        public const string TRAINING_CENTER_MARK_VERBAL = "training_center_mark_verbal";

        public const string TRAINING_CENTER_ATTEND_LIST = "tc_attendList";
        public const string TRAINING_CENTER_COUNT_ATTEND = "tc_countAttend";
        public const string TRAINING_CENTER_SKILL_TYPE = "tc_skillType";
        public const string TRAINING_CENTER_CLASS_LIST = "ClassList";
        
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT = "training_admin_session_filter_text";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_TYPE = "training_admin_session_filter_type";
        public const string TRAINING_ADMIN_SESSION_NAVIGATION_COURSE_TYPE = "training_admin_session_navigation_type";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN = "training_admin_session_filter_sort_collumn";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER = "training_admin_session_filter_sort_order";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_PAGE_INDEX = "training_admin_session_filter_page_index";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE_ROW_COUNT = "training_admin_session_filter_row_count";
        public const string TRAINING_ADMIN_SESSION_FILTER_COURSE = "training_admin_session_filter";

        #endregion

        #region Material
        public const string TRAINING_CENTER_MATERIAL_COURSE = "training_center_material_course";
        public const string TRAINING_CENTER_SUBLIST_NAME = "training_center_sublist_name";
        
        public const string TRAINING_CENTER_MATERIAL_CATEGORY = "training_center_material_category";
        public const string TRAINING_CENTER_MATERIAL_TYPE = "training_center_material_type";
        public const string MATERIAL_CATEGORY = "MATERIAL_CATEGORY";
        public const string TRAINING_CENTER_MATERIAL_PAGE_NAME = "training_center_material_page_name";
        
        
        public const string TRAINING_MATERIAL_SEARCH_SESSION_PRO = "training_material_search_session_pro";
        public const string TRAINING_MATERIAL_SEARCH_SESSION_ENG = "training_material_search_session_eng";
        public const string TRAINING_MATERIAL_SEARCH_SESSION_CAT = "training_material_search_session_cat";

        public const string TRAINING_MATERIAL_SEARCH_ROWNUM = "training_material_search_rownum";
        public const string TRAINING_MATERIAL_SEARCH_PAGE_INDEX = "training_material_search_page_index";
        public const string TRAINING_MATERIAL_SEARCH_NAME = "training_material_search_name";
        public const string TRAINING_MATERIAL_SEARCH_KEY = "training_material_search_key";
        //public const string TRAINING_MATERIAL_SEARCH_SESSION = "training_material_search_session";

        #endregion

        #region Course - Portal

        public const string TRAINING_COURSE_SEARCH_SESSION_PRO = "training_course_search_session_pro";
        public const string TRAINING_COURSE_SEARCH_SESSION_ENG = "training_course_search_session_eng";

        public const string TRAINING_COURSE_SEARCH_ROWNUM = "training_course_search_rownum";
        public const string TRAINING_COURSE_SEARCH_PAGE_INDEX = "training_course_search_page_index";
        public const string TRAINING_COURSE_SEARCH_NAME = "training_course_search_name";
        public const string TRAINING_COURSE_SEARCH_SKILLTYPE = "training_course_search_skilltype";

        #endregion

        #region LOT - Verbal Skill
        public const string LOT_VERBAL_MARK = "lot_verbal_mark";
        public const string LOT_VERBAL_TESTED_BY = "lot_verbal_tested_by";
        public const string LOT_VERBAL_MARK_TYPE = "lot_verbal_mark_type";
        public const string LOT_VERBAL_COMMENT = "lot_verbal_comment";
        public const string LOT_VERBAL_UPDATE_DATE = "lot_verbal_update_date";
        public const string LOT_VERBAL_LEVEL_LIST = "lot_verbal_level_list";
        
        
        #endregion

        #region Time Management

        public const string TIME_MANAGEMENT_FILTER_DEPARTMENT_LIST = "tm_department_list";
        public const string TIME_MANAGEMENT_FILTER_REPORT_KINDS = "tm_report_kinds";
        // Hours and Minutes
        public const string TIME_MANAGEMENT_FILTER_COME_AFTER_HOUR = "tm_come_after_hour";
        public const string TIME_MANAGEMENT_FILTER_COME_AFTER_MINUTE = "tm_come_after_minute";
        public const string TIME_MANAGEMENT_FILTER_LEAVE_BEFORE_HOUR = "tm_leave_before_hour";
        public const string TIME_MANAGEMENT_FILTER_LEAVE_BEFORE_MINUTE = "tm_leave_before_minute";

        public const string TIME_MANAGEMENT_SESSION_FILTER_KEYWORD = "time_management_session_filter_keyword";
        public const string TIME_MANAGEMENT_SESSION_FILTER_DEPARTMENT = "time_management_session_filter_department";
        public const string TIME_MANAGEMENT_SESSION_FILTER_TOP_N = "time_management_session_filter_top_n";
        public const string TIME_MANAGEMENT_SESSION_FILTER_DATE_FROM = "time_management_session_filter_date_from";
        public const string TIME_MANAGEMENT_SESSION_FILTER_DATE_TO = "time_management_session_filter_date_to";
        public const string TIME_MANAGEMENT_SESSION_FILTER_REPORT_KINDS = "time_management_session_filter_report_kinds";
        // Hours and Minutes
        public const string TIME_MANAGEMENT_SESSION_FILTER_COME_AFTER_HOUR = "time_management_session_filter_come_after_hour";
        public const string TIME_MANAGEMENT_SESSION_FILTER_COME_AFTER_MINUTE = "time_management_session_filter_come_after_minute";
        public const string TIME_MANAGEMENT_SESSION_FILTER_LEAVE_BEFORE_HOUR = "time_management_session_filter_leave_before_hour";
        public const string TIME_MANAGEMENT_SESSION_FILTER_LEAVE_BEFORE_MINUTE = "time_management_session_filter_leave_before_minute";
        public const string TIME_MANAGEMENT_SESSION_FILTER_STAY_LATE_HOUR = "time_management_session_filter_stay_late_hour";
        public const string TIME_MANAGEMENT_SESSION_FILTER_STAY_LATE_MINUTE = "time_management_session_filter_stay_late_minute";
        
        public const string TIME_MANAGEMENT_SESSION_FILTER_SORT_COLUMN = "time_management_filter_session_collumn";
        public const string TIME_MANAGEMENT_SESSION_FILTER_SORT_ORDER = "time_management_filter_session_order";
        public const string TIME_MANAGEMENT_SESSION_FILTER_PAGE_INDEX = "time_management_filter_session_index";
        public const string TIME_MANAGEMENT_SESSION_FILTER_ROW_COUNT = "time_management_filter_session_count";

        #endregion

        #region AAsset
        // Sub-Session
        public const string A_ASSET_SESSION_FILTER_KEYWORD = "a_asset_session_filter_keyword";
        public const string A_ASSET_SESSION_FILTER_CATEGORY = "a_asset_session_filter_category";
        public const string A_ASSET_SESSION_FILTER_STATUS = "a_asset_session_filter_status";

        public const string A_ASSET_SESSION_FILTER_BRANCH = "a_asset_session_filter_branch";
        public const string A_ASSET_SESSION_FILTER_DEPT = "a_asset_session_filter_dept";
        public const string A_ASSET_SESSION_FILTER_PROJECT = "a_asset_session_filter_project";
        public const string A_ASSET_SESSION_FILTER_MANGER = "a_asset_session_filter_manger";

        public const string A_ASSET_SESSION_FILTER_FROM_DATE = "a_asset_session_filter_from_date";
        public const string A_ASSET_SESSION_FILTER_TO_DATE = "a_asset_session_filter_to_date";
            // Advance
        public const string A_ASSET_SESSION_FILTER_OWNER = "a_asset_session_filter_owner";
        public const string A_ASSET_SESSION_FILTER_ADVANCE = "a_asset_session_filter_advance";
        // Mapping data
        public const string A_ASSET_FILTER_LIST_CATEGORY = "a_asset_filter_list_category";
        public const string A_ASSET_FILTER_LIST_STATUS = "a_asset_filter_list_status";

        public const string A_ASSET_FILTER_LIST_BRANCH = "a_asset_filter_list_branch";
        public const string A_ASSET_FILTER_LIST_DEPT = "a_asset_filter_list_dept";
        public const string A_ASSET_FILTER_LIST_PROJECT = "a_asset_filter_list_project";
        public const string A_ASSET_FILTER_LIST_MANAGER = "a_asset_filter_list_manager";
        // JqGrid Sorting
        public const string A_ASSET_SESSION_FILTER_SORT_COLUMN = "a_asset_session_filter_sort_column";
        public const string A_ASSET_SESSION_FILTER_SORT_ORDER = "a_asset_session_filter_sort_order";
        public const string A_ASSET_SESSION_FILTER_PAGE_INDEX = "a_asset_session_filter_page_index";
        public const string A_ASSET_SESSION_FILTER_ROW_COUNT = "a_asset_session_filter_row_count";
        // Sub-JqGrid Sorting
        public const string A_ASSET_SESSION_FILTER_SUBGRID_SORT_COLUMN = "a_asset_session_filter_subgrid_sort_column";
        public const string A_ASSET_SESSION_FILTER_SUBGRID_SORT_ORDER = "a_asset_session_filter_subgrid_sort_order";

        /*--- Adding Sub-Assets ---*/
        public const string A_ASSET_ADD_SUB_P_ID = "a_asset_add_sub_p_id";
            // Mapping data
        public const string A_ASSET_ADD_SUB_FILTER_LIST_CATEGORY = "a_asset_add_sub_filter_list_category";
        public const string A_ASSET_ADD_SUB_FILTER_LIST_STATUS = "a_asset_add_sub_filter_list_status";
            // JqGrid Sorting
        public const string A_ASSET_ADD_SUB_SESSION_FILTER_SORT_COLUMN = "a_asset_add_sub_session_filter_sort_column";
        public const string A_ASSET_ADD_SUB_SESSION_FILTER_SORT_ORDER = "a_asset_add_sub_session_filter_sort_order";
        public const string A_ASSET_ADD_SUB_SESSION_FILTER_PAGE_INDEX = "a_asset_add_sub_session_filter_page_index";
        public const string A_ASSET_ADD_SUB_SESSION_FILTER_ROW_COUNT = "a_asset_add_sub_session_filter_row_count";

        /*--- Advance Search ---*/
            // Mapping data
        public const string A_ASSET_ADVANCE_LIST_PROPERTY = "a_asset_advance_list_property";
        public const string A_ASSET_ADVANCE_LIST_PROPERTY_BY_PERMISSION = "a_asset_advance_list_property_by_permission";
        public const string A_ASSET_ADVANCE_OWNER = "a_asset_advance_owner";
        public const string A_ASSET_ADVANCE_LIST_PROPERTY_VALUE = "a_asset_advance_list_property_value";

        /*--- ToolTip ---*/
        public const string A_ASSET_TOOLTIP_ENTITY = "a_asset_tooltip_entity";
        #endregion
        #region UserAmin
        public const string USER_ADMIN_LIST = "user_admin_list";
        #endregion
    }
}
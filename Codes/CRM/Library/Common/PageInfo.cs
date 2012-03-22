using System;

namespace CRM.Library.Common.PageInfo
{
    public static class CommonPageInfo
    {
        public static string AppName = "CRM";
        public static string AppSepChar = " - ";
        public static string AppDetailSepChar = " » ";
    }

    public static class ModulePermissonPageInfo
    {
        public static string ComName = "Module Permisson";
    }

    public static class ManageWorkFlowPageInfo
    {
        public static string ComName = "Manage WorkFlow";
    }

    public static class JobTitlePageInfo
    {
        public static string MenuName = "Job Title";
        public static string ComName = "Job Title";
        public static string List = "Job Title Level List";
        public static string JobTitle = "Job Title List";
    }
    //Hang
      public static class TrainingNonemployeeCertification
    {

        public static string MenuName = "Training Nonemployee Certification";
        public static string ComName = "Training Nonemployee Certification Management";
        public static string List = "Training Nonemployee Certification List";
        public static string TrainingNonemployee = "Training Nonemployee Certification List";
        public static string FuncNonemployee = "Nonemployee";
        public static string NonemployeeDetail = "Nonemployee Detail";
    }
   
    public static class UserConfigPageInfo
    {
        public static string MenuName = "User Config";
    }
    public static class STTPageInfo
    {
        public static string MenuName = "Employee";
        public static string List = "STT List";
        public static string ComName = "STT";
        public static string Create = "Add New STT";
        public static string Update = "Edit STT";
    }
    
    public static class HiringCenterPageInfo
    {
        //component
        public static string MenuName = "Hiring Center";
        public static string ComName = "Hiring Center";
        //modules
        public static string ModCandidate = "Candidate Profiles";
        public static string ModInterview = "Interview List";
        public static string ModInterviewHistory = "Interview History";
        //functions
        public static string FuncAddNewCandidate = "Add New Candidate Profile";
        public static string FuncEditCandidate = "Edit Candidate Profile";
        public static string FuncSetupInterview = "Setup an Interview";
        public static string FuncEditInterview = "Edit Interview";
        public static string FuncInterviewResult = "Interview Result";
        public static string FuncInterviewDetails = "Interview Details";
        public static string FuncSendMeetingRequest = "Send Meeting Request";
        public static string FuncSendEmailToCandidate = "Send Email to Candidate";
        public static string FuncTransferToEmployee = "Transfer to Employee Profile";
        public static string FuncTransferToSTT = "Transfer to STT Profile";
        public static string FuncInterviewHistoryDetails = "Interview History Details";
    }
    
    public static class EmsPageInfo
    {
        public static string MenuName = "Employee";
        //component
        public static string ComName = "Employee Management System";
        //modules
        public static string ModActiveEmployees = "Active List";
        public static string ModStt = "Software Testing Trainee";
        public static string ModResignedEmployees = "Resigned List";
        public static string ModPerformanceReview = "Performance Review";
        public static string ModContractRenewal = "Contract Renewal";
        public static string ModEmployeeHistory = "Employee History";
        public static string ModReActivateEmployee = "Re-Activate Employee";
        //functions
        public static string FuncAddNewEmployee = "Add New Employee Profile";
        public static string FuncEditEmployee = "Edit Employee Profile";
        public static string FuncViewEmployeeProfile = "View Employee Profile";
    }

    public static class AssetCategoryPageInfo
    {
        public static string ComName = "Asset Management System";
        public static string AssetCategory = "Asset Category";        
    }

    public static class AssetPropertyPageInfo
    {
        public static string ComName = "Asset Management System";
        public static string AssetProperty = "Asset Property";        
    }

    public static class AssetMasterPageInfo
    {
        public static string MenuName = "Asset Master";
        public static string ComName = "Asset Management System";
        public static string AssetMaster = "Asset Master";
        public static string List = "Asset Master List";
        public static string FuncAddNewAssetMaster = "Add New Asset Master";
        public static string FuncEditAssetMaster = "Edit Asset Master";
    }

    public static class EmployeeAssetPageInfo
    {
        public static string MenuName = "Employee Asset";
        public static string ComName = "Asset Management System";
        public static string List = "Employee Asset List";
        public static string FuncAddNewAssetMaster = "Assign Employee Asset";
    }

    public static class ProjectAssetPageInfo
    {
        public static string MenuName = "Project Asset";
        public static string ComName = "Asset Management System";
        public static string List = "Project Asset List";
        public static string FuncAddNewAssetMaster = "Assign Project Asset";
    }

    public static class GroupPageInfo
    {
        public static string GroupName = "Groups";
        
    }

    public static class JobRequestPageInfo
    {
        //modules
        public static string ModJobRequest = "Job Request";
    }

    public static class HomePageInfo
    {
        //modules
        public static string ModHome = "Home";
        //functions
        public static string FuncUserLoginStatistic = "User Login Statistic";
    }

    public static class ServiceRequestInfo
    {
        //component
        public static string MenuName = "Service Request Management";
        public static string FuncSettting = "Service Request Setting";

    }

    public static class PurchaseRequestPageInfo
    {
        //modules
        public static string ComName = "Purchase Request";
        public static string FuncPurchaseDetails = "Purchase Request Details";
        public static string FuncAddNewPurchaseRequest = "Add New Purchase Request";
        public static string FuncEditPurchaseRequest = "Edit Purchase Request";
        public static string FuncPurchasingConfirm = "Purchasing Confirm";
        public static string FuncPurchasingFillData = "Purchasing Fill Data";
        public static string FuncUsAccountingProcess= "Us Accounting";
        public static string FuncSetupApproval = "Setup Approval";
        public static string ComNameReport = "Purchase Request Report";
    }

    public static class LOTPageInfo
    {
        //component
        public static string MenuName = "Online Test";
        public static string ComName = "Logigear Online Test";
        //modules
        public static string ExamQuestion = "Exam Question";
        public static string ViewDetail = "View Detail";
        public static string AssignQuestion = "Assign Question";
        public static string AssignCandidate = "Assign Candidate";
        public static string Exam = "Exam List";
        public static string CandidateTestList = "Candidate's Test List";
        public static string Question = "Question List";
        public static string EditQuestion = "Edit Question";
        public static string AddNewQuestion = "Add New Question";

    }
    public static class PTOPageInfo
    {
        public static string MenuName = "PTO";
        public static string AnnualHoliday = "Annual Holiday";
        public static string ComName = "Personal Time Off";
        public static string Admin = "PTO Admin";
        public static string Need_To_Confirm = "PTO Reminder";
        public static string Report = "PTO Report";
        public static string FuncPTOEmpList = "PTO";
        public static string FuncPTOManager = "Manager";
        public static string FuncNameDashBoardPortal = "Dashboard";
    }

    public static class PortalHomePageInfo
    {
        //component
        public static string ComName = "CRM Portal";
        public static string ComEmployee = "CRM Portal";
        public static string PTO = "PTO";
        public static string PTO_Report = "PTO Report";
        public static string Manager = "Manager";
        public static string Need_To_Confirm = "PTO Reminder";
    }

    public static class AdminAccountPageInfo
    {
        //component
        public static string MenuName = "Admin Accounts";
        public static string ComGroup = "Group";
        public static string ComAccount = "Account";
        public static string SetPermissionForGroup = "Set permission for group";

    }

    public static class TrainingCertificationPageInfo
    {

        public static string MenuName = "Training Certification";
        public static string ComName = "Training Certification Management";
        public static string List = "Training Certification List";
        public static string TrainingCertification = "Training Certification List";
    }

    public static class TrainingEmployeeCertificationPageInfo
    {

        public static string MenuName = "Training Employee Certification";
        public static string ComName = "Training Employee Certification Management";
        public static string List = "Training Employee Certification List";
        public static string TrainingEmployeeCertification = "Training Employee Certification List";
    }

    public static class WorkFlowPageInfo
    {
        //component
        public static string MenuName = "Workflow Accounts";
        public static string ComName = "Workflow Admin";

    }

    public static class SystemLogInfo
    {
        public static string MenuName = "System Logs";
        public static string ComDataLog = "User Logs";
        public static string ComAdminAccess = "Admin Access Logs";
        public static string ComUserAccess = "User Access Logs";
    }
    public static class PositionPageInfo
    {
        public static string ModName = "Update Employee Info";
    }

    public static class PerformanceReviewPageInfo
    {
        public static string ModName = "Performance Review";
        public static string ListForManager = "PR List for Manager";
        public static string ListFor = "PR List for {0}";
    }

    public static class PortalMyProfile
    {
        //component
        public static string ComName = "CRM Portal";
        public static string ModName = "My Profile";
    }
    public static class HelpPageInfo
    {
        public static string ComName = "Help";
    }
    public static class LocationChatPageInfo
    {
        public static string MenuName = "Work Location Chart";
    }
    public static class MenuPageInfo
    {
        public static string FunctionName = "Menu Management";
    }

    public static class ServiceRequestPageInfo
    {
        public static string ComName = "Service Request Management";
        public static string ComNamePortal = "Service Request";
        public static string FuncNameDashBoardPortal = "Dash Board";
        public static string FuncNameCategory = "Category";
        public static string FuncNameSRList = "Service Request";
        public static string FuncNameSubmitNew = "Submit New Service Request";
        public static string FuncNameEdit = "Edit Service Request";
        public static string FuncNameReportOC = "Opened/Closed Requests Per Category";
        public static string FuncNameReportActivity = "Service Request Activity Summary";
        public static string FuncNameReportWeekly = "Weekly Report For IT Team";       

        public static string FuncNameDetail = "Detail";

    }

    public static class TrainingAdminConfirmationPageInfo
    {
        public static string MenuName = "Training Admin Confirmation";
        public static string ComName = "Training Admin Confirmation Management";
    }

    public static class TrainingCenterPageInfo
    {
        public static string ComName = "Training Center Management";
        public static string ComNamePortal = "Training Center";
        public static string FuncNameEEI = "Employee English Information";
        public static string FuncProfessionalTraining = "Professional Training";
        public static string FuncClassTraining = "English Class Training";
        public static string FuncProDetail = "Professional Class Detail";
        public static string FuncEngDetail = "English Class Detail";
        public static string FuncMyProfile = "My Profile";
        public static string FuncEngCourseAttend = "English Course Attendance";
        public static string FuncProCourseAttend = "Professional Course Attendance";
        public static string FuncEnglishScoreSheet = "English Score Sheet";
        

        public static string FuncTrainingDashboard = "Training Dashboard";
        public static string FuncMaterial = "Material";
        public static string FuncMaterial_Pro = "Professional Skill Materials";
        public static string FuncMaterial_English = "English Materials";
        public static string FuncMaterial_Category = "Materials by Categories";
        public static string FuncMaterial_SubList = "Sub List";
        public static string FuncChildPro = "Professional";
        public static string FuncChildEng = "English";
        public static string FuncChildCat = "Other";
        public static string FuncCourses = "Course";
        public static string FuncClasses = "Class";
        public static string FuncCourses_Pro = "List of Professional Skill Courses";
        public static string FuncCourses_Eng = "List of English Courses";
        public static string FuncTrainingRecord = "Training Record Management";
        public static string FuncEngLishLevelMapping = "English Mapping Table";
        public static string FuncScoreSheet = "Master English Score Sheet";
        public static string FuncCourseAttendance_Pro = "Professional Courses Attendance";
        public static string FuncCourseAttendance_End = "English Courses Attendance";
        public static string FuncDashBoard = "Dash Board";
        public static string FuncEmpEnglishInfo = "Employee English Info";
        public static string FuncNonemployeeCertification = "Nonemployee Certification";
        public static string FuncNonemployeeCertificationChild = "Information";

        public static string FuncPortalMyTrainingProfile = "My Training Profile";
        public static string FuncPortalCourses = "Course";
        public static string FuncPortalClasses = "Class";
        public static string FuncPortalRegistration = "Registration";
        public static string FuncPortalMaterials = "Material";
        public static string FuncPortalChildPro = "Professional";
        public static string FuncPortalChildEng = "English";
        public static string FuncPortalChildCate = "Other";
        public static string FuncPortalRegistrationList = "Registration List";
    }

    public static class TrainingMaterialPageInfo
    {
        public static string ComName = "Training Material";
        public static string FuncDashBoard = "DashBoard";
        public static string FuncMaterialList = "Material List";

        public static string FuncCreate = "Create Material";
        public static string FuncEdit = "Edit Material";
    }

    public static class TimeManagementPageInfo
    {
        public static string MenuName = "Time Management";
        //component
        public static string ComName = "Time Management";
        //modules
        public static string ModTimeInOutReport = "Time In-Out Report";
        public static string ModImport = "Importing Data";
    }
}
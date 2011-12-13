USE [CRM_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeeForExport]    Script Date: 12/13/2011 16:29:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[sp_GetEmployeeForExport] --null,0,0,0,0,0,null
	@Text nvarchar(50) = NULL,
	@Department int = 0,
	@SubDepartment int = 0,
	@Title int = 0,
	@isActive int=0,
	@Status int =0,
	@locationCode varchar(100) = null
As
Begin 		
	set @Text = replace(@Text,'[','[|[]')
	set @Text = replace(@Text,'%','[%]')
	set @Text = replace(@Text,'_','[_]')
	
	SELECT emp.ID,
	case 
		when emp.MiddleName is null then emp.FirstName + ' ' + emp.LastName
		else emp.FirstName+ ' ' +  emp.MiddleName+  ' ' + emp.LastName
	end DisplayName,
	dep.DepartmentName,
	dbo.GetDepartmentNameBySubId(emp.DepartmentId) as Department,
	title.DisplayName as TitleName,
	case 
		when emp.VnMiddleName is null then emp.VnFirstName + ' ' + emp.VnLastName
		else emp.VnFirstName + ' ' +  emp.VnMiddleName+  ' ' + emp.VnLastName
	end VnName,	
	emp.VnPOB,
	emp.VnPlaceOfOrigin,
	emp.Degree,
	emp.OtherDegree,
	emp.Race,
	emp.IDIssueLocation,
	emp.VnIDIssueLocation,
	emp.TaxID,
	emp.TaxIssueDate,
	emp.SocialInsuranceNo,
	hos.Name as Hospital,
	emp.Gender,
	emp.IDNumber,
	emp.StartDate,
	emp.ContractedDate,
	emp.Photograph,
	emp.JR,
	emp.Major,
	emp.JRApproval,	
	emp.HomePhone,
	emp.CellPhone,
	emp.PersonalEmail,
	emp.OfficeEmail,
	emp.ExtensionNumber,
	emp.DOB,
	emp.POB,
	emp.Nationality,
	emp.PlaceOfOrigin,
	emp.IssueDate,
	emp.BankAccount,
	emp.BankName,
	emp.ResignedDate,
	emp.ResignedReason,
	emp.ResignedAllowance,
	emp.LaborUnion,
	emp.LaborUnionDate,
	emp.Remarks,
	emp.MarriedStatus,
	emp.[Floor],
	emp.Religion,
	emp.EmergencyContactName,
	emp.EmergencyContactPhone,
	emp.EmergencyContactRelationship,
	emp.SkypeId,
	emp.YahooId,
	emp.SeatCode,
	emp.CVFile,
	emp.EmpStatusid,
	emps.StatusName,
	emp.Project,
	emp.Manager,
	[dbo].[StringOnly](emp.PermanentAddress)+ '  '+ [dbo].[StringOnly](emp.PermanentArea)+ '  '+[dbo].[StringOnly](emp.PermanentDistrict)+ '  '+[dbo].[StringOnly](emp.PermanentCityProvince)+ '  '+[dbo].[StringOnly](emp.PermanentCountry) as PermanentAddress,
	[dbo].[StringOnly](emp.VnPermanentAddress) + '  '+ [dbo].[StringOnly](emp.VnPermanentArea) + '  '+[dbo].[StringOnly](emp.VnPermanentDistrict) + '  '+[dbo].[StringOnly](emp.VnPermanentCityProvince)+'  '+[dbo].[StringOnly](emp.VnPermanentCountry) as VnPermanentAddress,
	[dbo].[StringOnly](emp.TempAddress) + '  '+ [dbo].[StringOnly](emp.TempArea) + '  '+[dbo].[StringOnly](emp.TempDistrict) + '  '+[dbo].[StringOnly](emp.TempCityProvince)+'  '+[dbo].[StringOnly](emp.TempCountry) as TempAddress,
	[dbo].[StringOnly](emp.VnTempAddress) + '  '+ [dbo].[StringOnly](emp.VnTempArea) + '  '+[dbo].[StringOnly](emp.VnTempDistrict) + '  '+[dbo].[StringOnly](emp.VnTempCityProvince)+'  '+[dbo].[StringOnly](emp.VnTempCountry) as VnTempAddress
	FROM dbo.Employee emp	
	inner join dbo.Department dep on emp.DepartmentId = dep.DepartmentId
	inner join dbo.JobTitleLevel title on emp.TitleId = title.Id
	left join dbo.EmployeeStatus emps on emp.EmpStatusid = emps.StatusId
	left join dbo.InsuranceHospital hos on emp.InsuranceHospitalID = hos.ID
	where 
	(		
		(@Text IS NOT NULL AND emp.MiddleName is not null AND (emp.ID + ' ' + emp.FirstName + ' ' + emp.MiddleName + ' ' + emp.LastName) like '%' + replace(@Text,' ','%') + '%' ESCAPE '|' )		
		OR
		(@Text IS NOT NULL AND emp.MiddleName is  null AND (emp.ID + ' ' + emp.FirstName + ' ' + emp.LastName) like '%' + replace(@Text,' ','%') + '%' ESCAPE '|' )		
		OR
		(@Text IS NULL)
	)
	AND
	(		 
		(@SubDepartment <> 0 AND emp.DepartmentId = @SubDepartment)
		OR
		(@SubDepartment = 0)
	)
	AND
	(		 
		(@Department <> 0 AND dbo.GetDepartmentIdBySubId(emp.DepartmentId)= @Department)
		OR
		(@Department = 0)
	)
	AND
	(		 
		(@Title <> 0 AND emp.TitleId = @Title)
		OR
		(@Title = 0)
	)
	AND
	(
		(@isActive = 1 AND (emp.EmpStatusId is null or emp.EmpStatusId not in (@Status)))
		OR
		(@isActive = 0 AND (emp.EmpStatusId is not null and emp.EmpStatusId in (@Status)))
		OR
		(@Status = 0 AND @isActive = 0)
	)
	AND
	(		
		(
			(emp.LocationCode like '%'+@locationCode+'[^0-9]%'
				OR emp.LocationCode like '%'+@locationCode
			)
			AND emp.LocationCode is not null
		)
		OR @locationCode is null
	)
	AND emp.DeleteFlag = 0
		
End 




USE [CRM_Dev]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployee]    Script Date: 12/13/2011 17:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROC [dbo].[sp_GetEmployee]
	@Text nvarchar(50) = NULL,
	@Department int = 0,
	@SubDepartment int = 0,
	@Title int = 0,
	@isActive int=0,
	@Status int =0,
	@locationCode varchar(100) = null
As
Begin 	
	set @Text = replace(@Text,'[','[[]')	
	set @Text = replace(@Text,'%','[%]')
	set @Text = replace(@Text,'_','[_]')
	
	SELECT emp.ID,
	case 
		when emp.MiddleName is null then emp.FirstName + ' ' + emp.LastName
		else emp.FirstName + ' ' +  emp.MiddleName+  ' ' + emp.LastName
	end DisplayName,
	dep.DepartmentName,
	title.DisplayName as TitleName,
	emp.StartDate,
	emp.ResignedDate,
	emp.ResignedReason,
	emp.EmpStatusId,
	emps.StatusName,
	emp.OfficeEmail,
	emp.JR,
	dbo.GetDepartmentNameBySubId(emp.DepartmentId) as Department,
	emp.LocationCode
	FROM dbo.Employee emp
	inner join dbo.Department dep on emp.DepartmentId = dep.DepartmentId
	inner join dbo.JobTitleLevel title on emp.TitleId = title.Id
	left join dbo.EmployeeStatus emps on emp.EmpStatusId = emps.StatusId
	
	where 
	(		
		(emp.MiddleName is not null AND (emp.ID + ' ' + emp.FirstName + ' ' + emp.MiddleName + ' ' + emp.LastName) like '%' + replace(@Text,' ','%') + '%' )		
		OR
		(emp.MiddleName is  null AND (emp.ID + ' ' + emp.FirstName + ' ' + emp.LastName) like '%' + replace(@Text,' ','%') + '%'  )		
		OR
		(emp.FirstName + ' ' + emp.LastName like '%' + replace(@Text,' ','%') + '%'  )		
		OR
		(LEFT(emp.OfficeEmail,CHARINDEX('@',emp.OfficeEmail)) like '%' + replace(@Text,' ','%') + '%'  )		
		OR
		(emp.ID like '%' + replace(@Text,' ','%') + '%'  )
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
/*  @Text nvarchar(50) = NULL,
	@Department int = 0,
	@SubDepartment int = 0,
	@Title int = 0,
	@isActive int=0,
	@Status int =0,
	@branch int =0,
	@office int =0,
	@floor int = 0*/
--exec [dbo].[sp_GetEmployee] null,0,0,0,0,0,'B2O2F11'

select charindex('@', '123a@')
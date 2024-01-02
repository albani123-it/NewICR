function formValidation(
	LvlID,
	LvlNm,			
	chkAll,
	chkAdmin,
	chkAdminUser,
	chkAdminUserManagement,
	chkAdminUserManagementNew,
	chkAdminUserManagementEdit,
	chkAdminUserManagementDelete,
	chkAdminSystem,
	chkAdminSystemEdit,
	chkAdminLog,
	chkParameters,
	chkParameterScoreCard,
	chkParameterScoreCardNew,
	chkParameterScoreCardEdit,
	chkParameterScoreCardDelete,
	chkParameterScoreCardActivate,
	chkParameterProfit,
	chkParameterStrategy,
	chkParameterStrategyRetention,
	chkParameterStrategyRetentionNew,
	chkParameterStrategyRetentionEdit,
	chkParameterStrategyRetentionDelete,
	chkParameterStrategyOffering,
	chkParameterStrategyOfferingNew,
	chkParameterStrategyOfferingEdit,
	chkParameterStrategyOfferingDelete,
	chkParameterSystem,
	chkParameterSystemActivity,
	chkParameterSystemActivityNew,
	chkParameterSystemActivityEdit,
	chkParameterSystemActivityDelete,
	chkActivity,
	chkActivityAccountInfo,
	chkActivityWaivingDetail,
	chkActivityHistory,
	chkActivityHistoryEdit,
	chkReport,
	chkReportScore,
	chkReportScoreView,
	chkReportScoreDownload,
	chkReportScorePrint,
	chkReportOperation,
	chkReportOperationView,
	chkReportOperationDownload,
	chkReportOperationPrint,
	chkReportQuery,
	chkReportQueryNew,
	chkReportQueryEdit,
	chkReportQueryDelete,
	chkReportQueryExecute,
	chkReportQueryDownload) 
{		
	if (LvlID.value=='') {
		alert('Access Level ID value should not be empty');
		LvlID.focus();
		return false;
	}
	
	if (LvlNm.value=='') {
		alert('Access Level Name value should not be empty');
		LvlNm.focus();
		return false;
	}
							
	if(!(isNaN(LvlNm.value))) {
		alert('No numeric on name value');
		LvlNm.focus();
		return false;
	}
	
	if (!((chkAdmin.checked ==  true) || (chkAdminUser.checked ==  true ) || (chkAdminUserManagement.checked ==  true ) || (chkAdminUserManagementNew.checked ==  true ) || 
		(chkAdminUserManagementEdit.checked ==  true ) || (chkAdminUserManagementDelete.checked ==  true ) || (chkAdminSystem.checked ==  true ) || 
		(chkAdminSystemEdit.checked ==  true ) || (chkAdminLog.checked ==  true ) || (chkParameters.checked ==  true ) || 
		(chkParameterScoreCard.checked ==  true ) || (chkParameterScoreCardNew.checked ==  true ) || 
		(chkParameterScoreCardEdit.checked ==  true ) || (chkParameterScoreCardDelete.checked ==  true ) || 
		(chkParameterScoreCardActivate.checked ==  true ) || (chkParameterProfit.checked ==  true ) || 
		(chkParameterStrategy.checked ==  true ) || (chkParameterStrategyRetention.checked ==  true ) || 
		(chkParameterStrategyRetentionNew.checked ==  true ) || (chkParameterStrategyRetentionEdit.checked ==  true ) || 
		(chkParameterStrategyRetentionDelete.checked ==  true ) || (chkParameterStrategyOffering.checked ==  true ) || 
		(chkParameterStrategyOfferingNew.checked ==  true ) || (chkParameterStrategyOfferingEdit.checked ==  true ) || 
		(chkParameterStrategyOfferingDelete.checked ==  true ) || (chkParameterSystem.checked ==  true ) || 
		(chkParameterSystemActivity.checked ==  true ) || (chkParameterSystemActivityNew.checked ==  true ) || 
		(chkParameterSystemActivityEdit.checked ==  true ) || (chkParameterSystemActivityDelete.checked ==  true ) || 
		(chkActivity.checked ==  true ) || (chkActivityAccountInfo.checked ==  true ) || (chkActivityWaivingDetail.checked ==  true ) || 
		(chkActivityHistory.checked ==  true ) || (chkActivityHistoryEdit.checked ==  true ) || 
		(chkReport.checked ==  true ) || (chkReportScore.checked ==  true ) || (chkReportScoreView.checked ==  true ) || 
		(chkReportScoreDownload.checked ==  true ) || (chkReportScorePrint.checked ==  true ) || (chkReportOperation.checked ==  true ) || 
		(chkReportOperationView.checked ==  true ) || (chkReportOperationDownload.checked ==  true ) || (chkReportOperationPrint.checked ==  true ) || 
		(chkReportQuery.checked ==  true ) || (chkReportQueryNew.checked ==  true ) || (chkReportQueryEdit.checked ==  true ) ||
		(chkReportQueryDelete.checked ==  true ) || (chkReportQueryExecute.checked ==  true ) || (chkReportQueryDownload.checked ==  true )))
	{	alert('Tick one box to choose the access level');
		chkAll.focus();
		return false;
	}
	return true;		
}

function ChildChecking	(
	chkNm,
	chkAll,
	chkAdmin,
	chkAdminUser,
	chkAdminUserManagement,
	chkAdminUserManagementNew,
	chkAdminUserManagementEdit,
	chkAdminUserManagementDelete,
	chkAdminSystem,
	chkAdminSystemEdit,
	chkAdminLog,
	chkParameters,
	chkParameterScoreCard,
	chkParameterScoreCardNew,
	chkParameterScoreCardEdit,
	chkParameterScoreCardDelete,
	chkParameterScoreCardActivate,
	chkParameterProfit,
	chkParameterStrategy,
	chkParameterStrategyRetention,
	chkParameterStrategyRetentionNew,
	chkParameterStrategyRetentionEdit,
	chkParameterStrategyRetentionDelete,
	chkParameterStrategyOffering,
	chkParameterStrategyOfferingNew,
	chkParameterStrategyOfferingEdit,
	chkParameterStrategyOfferingDelete,
	chkParameterSystem,
	chkParameterSystemActivity,
	chkParameterSystemActivityNew,
	chkParameterSystemActivityEdit,
	chkParameterSystemActivityDelete,
	chkActivity,
	chkActivityAccountInfo,
	chkActivityWaivingDetail,
	chkActivityHistory,
	chkActivityHistoryEdit,
	chkReport,
	chkReportScore,
	chkReportScoreView,
	chkReportScoreDownload,
	chkReportScorePrint,
	chkReportOperation,
	chkReportOperationView,
	chkReportOperationDownload,
	chkReportOperationPrint,
	chkReportQuery,
	chkReportQueryNew,
	chkReportQueryEdit,
	chkReportQueryDelete,
	chkReportQueryExecute,
	chkReportQueryDownload
) 
{
	if (chkNm == 'chkAll') {  				
		chkAll.checked = chkAll.checked;
		chkAdmin.checked = chkAll.checked;
		chkAdminUser.checked = chkAll.checked;
		chkAdminUserManagement.checked = chkAll.checked;
		chkAdminUserManagementNew.checked = chkAll.checked;
		chkAdminUserManagementEdit.checked = chkAll.checked;
		chkAdminUserManagementDelete.checked = chkAll.checked;
		chkAdminSystem.checked = chkAll.checked;
		chkAdminSystemEdit.checked = chkAll.checked;
		chkAdminLog.checked = chkAll.checked;
		chkParameters.checked = chkAll.checked;
		chkParameterScoreCard.checked = chkAll.checked;
		chkParameterScoreCardNew.checked = chkAll.checked;
		chkParameterScoreCardEdit.checked = chkAll.checked;
		chkParameterScoreCardDelete.checked = chkAll.checked;
		chkParameterScoreCardActivate.checked = chkAll.checked;
		chkParameterProfit.checked = chkAll.checked;
		chkParameterStrategy.checked = chkAll.checked;
		chkParameterStrategyRetention.checked = chkAll.checked;
		chkParameterStrategyRetentionNew.checked = chkAll.checked;
		chkParameterStrategyRetentionEdit.checked = chkAll.checked;
		chkParameterStrategyRetentionDelete.checked = chkAll.checked;
		chkParameterStrategyOffering.checked = chkAll.checked;
		chkParameterStrategyOfferingNew.checked = chkAll.checked;
		chkParameterStrategyOfferingEdit.checked = chkAll.checked;
		chkParameterStrategyOfferingDelete.checked = chkAll.checked;
		chkParameterSystem.checked = chkAll.checked;
		chkParameterSystemActivity.checked = chkAll.checked;
		chkParameterSystemActivityNew.checked = chkAll.checked;
		chkParameterSystemActivityEdit.checked = chkAll.checked;
		chkParameterSystemActivityDelete.checked = chkAll.checked;
		chkActivity.checked = chkAll.checked;
		chkActivityAccountInfo.checked = chkAll.checked;
		chkActivityWaivingDetail.checked = chkAll.checked;
		chkActivityHistory.checked = chkAll.checked;
		chkActivityHistoryEdit.checked = chkAll.checked;
		chkReport.checked = chkAll.checked;
		chkReportScore.checked = chkAll.checked;
		chkReportScoreView.checked = chkAll.checked;
		chkReportScoreDownload.checked = chkAll.checked;
		chkReportScorePrint.checked = chkAll.checked;
		chkReportOperation.checked = chkAll.checked;
		chkReportOperationView.checked = chkAll.checked;
		chkReportOperationDownload.checked = chkAll.checked;
		chkReportOperationPrint.checked = chkAll.checked;
		chkReportQuery.checked = chkAll.checked;
		chkReportQueryNew.checked = chkAll.checked;
		chkReportQueryEdit.checked = chkAll.checked;
		chkReportQueryDelete.checked = chkAll.checked;
		chkReportQueryExecute.checked = chkAll.checked;
		chkReportQueryDownload.checked = chkAll.checked;
	}
	
	if (chkNm == 'chkAdmin') {  				
		chkAdminUser.checked = chkAdmin.checked;chkAdminUserManagement.checked = chkAdmin.checked;
		chkAdminUserManagementNew.checked = chkAdmin.checked;chkAdminUserManagementEdit.checked = chkAdmin.checked;
		chkAdminUserManagementDelete.checked = chkAdmin.checked;chkAdminSystem.checked = chkAdmin.checked;
		chkAdminSystemEdit.checked = chkAdmin.checked;chkAdminLog.checked = chkAdmin.checked;
		chkAdminSystem.checked = chkAdmin.checked;chkAdminSystemEdit.checked = chkAdmin.checked;
		chkAdminLog.checked = chkAdmin.checked;
	}
	
	if (chkNm == 'chkAdminUser') {  				
		chkAdminUserManagement.checked = chkAdminUser.checked;
		chkAdminUserManagementNew.checked = chkAdminUser.checked;
		chkAdminUserManagementEdit.checked = chkAdminUser.checked;
		chkAdminUserManagementDelete.checked = chkAdminUser.checked;				
	}
	
	if (chkNm == 'chkAdminUserManagement') {  
		chkAdminUserManagementNew.checked = chkAdminUserManagement.checked;
		chkAdminUserManagementEdit.checked = chkAdminUserManagement.checked;
		chkAdminUserManagementDelete.checked = chkAdminUserManagement.checked;				
	}
				
	if (chkNm == 'chkAdminSystem') {  
		chkAdminSystemEdit.checked = chkAdminSystem.checked;
	}
						
	if (chkNm == 'chkParameters') {  
		chkParameterScoreCard.checked = chkParameters.checked;
		chkParameterScoreCardNew.checked = chkParameters.checked;
		chkParameterScoreCardEdit.checked = chkParameters.checked;
		chkParameterScoreCardDelete.checked = chkParameters.checked;
		chkParameterScoreCardActivate.checked = chkParameters.checked;
		chkParameterProfit.checked = chkParameters.checked;
		chkParameterStrategy.checked = chkParameters.checked;
		chkParameterStrategyRetention.checked = chkParameters.checked;
		chkParameterStrategyRetentionNew.checked = chkParameters.checked;
		chkParameterStrategyRetentionEdit.checked = chkParameters.checked;
		chkParameterStrategyRetentionDelete.checked = chkParameters.checked;
		chkParameterStrategyOffering.checked = chkParameters.checked;
		chkParameterStrategyOfferingNew.checked = chkParameters.checked;
		chkParameterStrategyOfferingEdit.checked = chkParameters.checked;
		chkParameterStrategyOfferingDelete.checked = chkParameters.checked;
		chkParameterSystem.checked = chkParameters.checked;
		chkParameterSystemActivity.checked = chkParameters.checked;
		chkParameterSystemActivityNew.checked = chkParameters.checked;
		chkParameterSystemActivityEdit.checked = chkParameters.checked;
		chkParameterSystemActivityDelete.checked = chkParameters.checked;
	}
	
	if (chkNm == 'chkParameterScoreCard') {  
		chkParameterScoreCardNew.checked = chkParameterScoreCard.checked;
		chkParameterScoreCardEdit.checked = chkParameterScoreCard.checked;
		chkParameterScoreCardDelete.checked = chkParameterScoreCard.checked;
		chkParameterScoreCardActivate.checked = chkParameterScoreCard.checked;
	}
							
	if (chkNm == 'chkParameterStrategy') {  
		chkParameterStrategyRetention.checked = chkParameterStrategy.checked;
		chkParameterStrategyRetentionNew.checked = chkParameterStrategy.checked;
		chkParameterStrategyRetentionEdit.checked = chkParameterStrategy.checked;
		chkParameterStrategyRetentionDelete.checked = chkParameterStrategy.checked;
		chkParameterStrategyOffering.checked = chkParameterStrategy.checked;
		chkParameterStrategyOfferingNew.checked = chkParameterStrategy.checked;
		chkParameterStrategyOfferingEdit.checked = chkParameterStrategy.checked;
		chkParameterStrategyOfferingDelete.checked = chkParameterStrategy.checked;
	}
	
	if (chkNm == 'chkParameterStrategyRetention') {  
		chkParameterStrategyRetentionNew.checked = chkParameterStrategyRetention.checked;
		chkParameterStrategyRetentionEdit.checked = chkParameterStrategyRetention.checked;
		chkParameterStrategyRetentionDelete.checked = chkParameterStrategyRetention.checked;				
	}
				
	if (chkNm == 'chkParameterStrategyOffering') {  				
		chkParameterStrategyOfferingNew.checked = chkParameterStrategyOffering.checked;
		chkParameterStrategyOfferingEdit.checked = chkParameterStrategyOffering.checked;
		chkParameterStrategyOfferingDelete.checked = chkParameterStrategyOffering.checked;
	}
	
	if (chkNm == 'chkParameterSystem') { 
		chkParameterSystemActivity.checked = chkParameterSystem.checked;
		chkParameterSystemActivityNew.checked = chkParameterSystem.checked;
		chkParameterSystemActivityEdit.checked = chkParameterSystem.checked;
		chkParameterSystemActivityDelete.checked = chkParameterSystem.checked;
	}
					
	if (chkNm == 'chkParameterSystemActivity') {  	
		chkParameterSystemActivityNew.checked = chkParameterSystemActivity.checked;
		chkParameterSystemActivityEdit.checked = chkParameterSystemActivity.checked;
		chkParameterSystemActivityDelete.checked = chkParameterSystemActivity.checked;
	}
				
	if (chkNm == 'chkActivity') {  
		chkActivityAccountInfo.checked = chkActivity.checked;
		chkActivityWaivingDetail.checked = chkActivity.checked;
		chkActivityHistory.checked = chkActivity.checked;
		chkActivityHistoryEdit.checked = chkActivity.checked;		
	}
	
	if (chkNm == 'chkActivityHistory') {  
		chkActivityHistoryEdit.checked = chkActivityHistory.checked;		
	}
				
	if (chkNm == 'chkReport') {  
		chkReportScore.checked = chkReport.checked;
		chkReportScoreView.checked = chkReport.checked;
		chkReportScoreDownload.checked = chkReport.checked;
		chkReportScorePrint.checked = chkReport.checked;
		chkReportOperation.checked = chkReport.checked;
		chkReportOperationView.checked = chkReport.checked;
		chkReportOperationDownload.checked = chkReport.checked;
		chkReportOperationPrint.checked = chkReport.checked;
		chkReportQuery.checked = chkReport.checked;
		chkReportQueryNew.checked = chkReport.checked;
		chkReportQueryEdit.checked = chkReport.checked;
		chkReportQueryDelete.checked = chkReport.checked;
		chkReportQueryExecute.checked = chkReport.checked;
		chkReportQueryDownload.checked = chkReport.checked;
	}
	if (chkNm == 'chkReportScore') {  
		chkReportScoreView.checked = chkReportScore.checked;
		chkReportScoreDownload.checked = chkReportScore.checked;
		chkReportScorePrint.checked = chkReportScore.checked;
	}
				
	if (chkNm == 'chkReportOperation') {  
		chkReportOperationView.checked = chkReportOperation.checked;
		chkReportOperationDownload.checked = chkReportOperation.checked;
		chkReportOperationPrint.checked = chkReportOperation.checked;
	}
				
	if (chkNm == 'chkReportQuery') {  
		chkReportQueryNew.checked = chkReportQuery.checked;
		chkReportQueryEdit.checked = chkReportQuery.checked;
		chkReportQueryDelete.checked = chkReportQuery.checked;
		chkReportQueryExecute.checked = chkReportQuery.checked;
		chkReportQueryDownload.checked = chkReportQuery.checked;
	}			
}

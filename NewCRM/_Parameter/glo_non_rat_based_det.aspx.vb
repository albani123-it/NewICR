Imports Obout.Grid

Public Class glo_non_rat_based_det
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim gnr_ID As String = Request.QueryString("gnrID")
            If Request.QueryString("gnrID") <> "" Then
                btnCancel.Attributes.Add("OnClick", "javascript:CloseMe();")
                getListGlobalNonRating(gnr_ID)
                getListGlobalNonRatingBasedRiskWeightDetail(gnr_ID)
            Else
                    'bbt.Attributes.Add("style", "display:none")
                    typ.Visible = False
                    bbt.Visible = False
                    grd.Visible = False
                getListGlobalNonRatingBasedRiskWeightDetail(0)
            End If
        End If

    End Sub

    Sub RebindGrid()
        getListGlobalNonRatingBasedRiskWeightDetail(Request.QueryString("gnrID"))
    End Sub

    Sub getListGlobalNonRating(ByVal gnrID)
        With New lglobalSettings
            Dim ds As New DataSet
            ds = .getListGloNonRatingBasedRiskWeight(gnrID)
            txtJenisTagihan.Text = ds.Tables(0).Rows(0).Item("gnr_Billing_type")
            Try
                txtBobotResiko.Text = ds.Tables(0).Rows(0).Item("gnr_rwa")
            Catch ex As Exception

            End Try

            hidGNRID.Value = ds.Tables(0).Rows(0).Item("gnr_id")
        End With
    End Sub

    Sub getListGlobalNonRatingBasedRiskWeightDetail(ByVal gnrID)
        With New lglobalSettings
            Try
                Dim ds As New DataSet
                ds = .getDetailNonRatingBasedRiskWeightDetail(gnrID)
                gridGlobalRatingBasedDetail.DataSource = ds.Tables(0)
                gridGlobalRatingBasedDetail.DataBind()

                If ds.Tables(0).Rows.Count > 0 Then
                    rdoMultiple.Checked = True
                    bbt.Attributes.Add("style", "display:none")
                    grd.Attributes.Add("style", "display:table-row")
                Else
                    rdoSingle.Checked = True
                    bbt.Attributes.Add("style", "display:table-row")
                    grd.Attributes.Add("style", "display:none")
                End If

            Catch ex As Exception

            End Try
        End With

    End Sub

    Sub InsertRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim gnrID As Integer = hidGNRID.Value
        Dim JenisTagihan As String = e.Record("gnd_billing_type")
        Dim RiskWeight As String = e.Record("gnd_rwa")

        With New lglobalSettings
            .insertGlobalNonRatingBasedDetail(gnrID, JenisTagihan, RiskWeight)
        End With
    End Sub
    

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim gndID As Integer = e.Record("gnd_id")
        Dim JenisTagihan As String = e.Record("gnd_billing_type")
        Dim RiskWeight As String = e.Record("gnd_rwa")

        With New lglobalSettings
            .updateGlobalNonRatingBasedDetail(gndID, JenisTagihan, RiskWeight)
        End With
    End Sub

    Sub DeleteRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        With New lglobalSettings
            .deleteGlobalNonRatingBasedDetailGrid(1, e.Record("gnd_id"))
        End With
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Request.QueryString("gnrID") <> "" Then
            Dim JenisTagihan As String = txtJenisTagihan.Text
            Dim Bobot As String = txtBobotResiko.Text

            With New lglobalSettings
                If rdoSingle.Checked = True Then
                    .editGlobalNonRatingBasedDetail(hidGNRID.Value, JenisTagihan, Bobot)
                    .deleteGlobalNonRatingBasedDetailGrid(2, hidGNRID.Value)
                    liBodyLoadAction.Text = "alert('Data Successfully Updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Else
                    .editGlobalNonRatingBasedDetail(hidGNRID.Value, JenisTagihan, DBNull.Value)
                    liBodyLoadAction.Text = "alert('Data Successfully Added.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                End If
            End With
        Else
            saveScorecardSingle()
        End If
    End Sub

    Sub saveScorecardSingle()
        If liBodyLoadAction.Text = "" Then
            Dim JenisTagihan As String = txtJenisTagihan.Text
            With New lglobalSettings
                .insertGlobalNonRatingBased(JenisTagihan)
                liBodyLoadAction.Text = "alert('Scorecard Definition Saved.');CloseMe();"
            End With
        End If

    End Sub
End Class
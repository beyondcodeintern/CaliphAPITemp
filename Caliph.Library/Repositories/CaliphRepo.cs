using Caliph.Library.Helper;
using Caliph.Library.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caliph.Library.Repositories
{
    public class CaliphRepo
    {
        private string _connectionString = string.Empty;

        internal CaliphRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal List<MasterDatasEnt> GetMasterDataByMasterId(long masterId)
        {
            List<MasterDatasEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
                        	*
                        FROM MasterDatas a WITH (NOLOCK)
                        WHERE a.MasterId = @MasterId
                        AND StatusId = 1
                    ";

                    var param = new DynamicParameters();
                    param.Add("@MasterId", masterId, dbType: DbType.Int64);

                    oResult = conn.Query<MasterDatasEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        /// <summary>
        /// Add client
        /// </summary>
        /// <param name="oClient"></param>
        /// <param name="oClientHP"></param>
        /// <returns></returns>
        internal bool AddClient(ClientsEnt oClient)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        DECLARE @ClientId_new BIGINT = NULL;

                        BEGIN TRY
                        	BEGIN TRANSACTION
                        	INSERT INTO [dbo].[Clients] ([NickName]
                        	, [Name]
                        	, [ICNo]
                        	, [ContactNo]
                        	, [EmailAdd]
                        	, [SourceId]
                        	, [StatusId]
                        	, [AnnualIncomeId]
                        	, [AgeId]
                        	, [OccupationId]
                        	, [MaritalId]
                        	, [LengthOfTimeKnownId]
                        	, [HowWellKnownId]
                        	, [HowOftenSeenInAYearId]
                        	, [CouldApproachOnBusinessId]
                        	, [AbilityToProvideRefId]
                        	, [GenderId]
                        	, [EducationDesc]
                        	, [IncomeYearDesc]
                        	, [OtherSourceofIncomeDesc]
                        	, [FilingDate]
                        	, [CareerDesc]
                        	, [CreatedBy]
                        	, [CreatedDate])
                        		SELECT
                        			@NickName
                        		   ,@Name
                        		   ,@ICNo
                        		   ,@ContactNo
                        		   ,@EmailAdd
                        		   ,@SourceId
                        		   ,@StatusId
                        		   ,@AnnualIncomeId
                        		   ,@AgeId
                        		   ,@OccupationId
                        		   ,@MaritalId
                        		   ,@LengthOfTimeKnownId
                        		   ,@HowWellKnownId
                        		   ,@HowOftenSeenInAYearId
                        		   ,@CouldApproachOnBusinessId
                        		   ,@AbilityToProvideRefId
                        		   ,@GenderId
                        		   ,@EducationDesc
                        		   ,@IncomeYearDesc
                        		   ,@OtherSourceofIncomeDesc
                        		   ,@FilingDate
                        		   ,@CareerDesc
                        		   ,@CreatedBy
                        		   ,GETDATE();
                                                
                            COMMIT TRANSACTION
                        END TRY
                        BEGIN CATCH
                        	DECLARE @Error_Number INT
                        		   ,@Error_Severity INT
                        		   ,@Error_State INT
                        		   ,@Error_Procedure VARCHAR(1000)
                        		   ,@Error_Line INT
                        		   ,@Error_Message VARCHAR(8000);
                        
                        	SELECT
                        		@Error_Number = ERROR_NUMBER()
                        	   ,@Error_Severity = ERROR_SEVERITY()
                        	   ,@Error_State = ERROR_STATE()
                        	   ,@Error_Procedure = ERROR_PROCEDURE()
                        	   ,@Error_Line = ERROR_LINE()
                        	   ,@Error_Message = ERROR_MESSAGE();
                        
                        	ROLLBACK TRANSACTION;
                        	RAISERROR (@Error_Message, @Error_Severity, @Error_State);
                        END CATCH;
                    ";

                    var param = new DynamicParameters();
                    //Clients
                    param.Add("@NickName", oClient.NickName, dbType: DbType.AnsiString);
                    param.Add("@Name", oClient.Name, dbType: DbType.AnsiString);
                    param.Add("@ICNo", oClient.ICNo, dbType: DbType.AnsiString);
                    param.Add("@ContactNo", oClient.ContactNo, dbType: DbType.AnsiString);
                    param.Add("@EmailAdd", oClient.EmailAdd, dbType: DbType.AnsiString);
                    param.Add("@SourceId", oClient.SourceId, dbType: DbType.Int64);
                    param.Add("@StatusId", oClient.StatusId, dbType: DbType.Int64);
                    param.Add("@AnnualIncomeId", oClient.AnnualIncomeId, dbType: DbType.Int64);
                    param.Add("@AgeId", oClient.AgeId, dbType: DbType.Int64);
                    param.Add("@OccupationId", oClient.OccupationId, dbType: DbType.Int64);
                    param.Add("@MaritalId", oClient.MaritalId, dbType: DbType.Int64);
                    param.Add("@LengthOfTimeKnownId", oClient.LengthOfTimeKnownId, dbType: DbType.Int64);
                    param.Add("@HowWellKnownId", oClient.HowWellKnownId, dbType: DbType.Int64);
                    param.Add("@HowOftenSeenInAYearId", oClient.HowOftenSeenInAYearId, dbType: DbType.Int64);
                    param.Add("@CouldApproachOnBusinessId", oClient.CouldApproachOnBusinessId, dbType: DbType.Int64);
                    param.Add("@AbilityToProvideRefId", oClient.AbilityToProvideRefId, dbType: DbType.Int64);
                    param.Add("@GenderId", oClient.GenderId, dbType: DbType.Int64);
                    param.Add("@EducationDesc", oClient.EducationDesc, dbType: DbType.AnsiString);
                    param.Add("@IncomeYearDesc", oClient.IncomeYearDesc, dbType: DbType.AnsiString);
                    param.Add("@OtherSourceofIncomeDesc", oClient.OtherSourceofIncomeDesc, dbType: DbType.AnsiString);
                    param.Add("@FilingDate", oClient.FilingDate, dbType: DbType.DateTime);
                    param.Add("@CareerDesc", oClient.CareerDesc, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", oClient.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientDetailsEnt> GetClientAll(long? clientId)
        {
            List<ClientDetailsEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT
                        	(SELECT COUNT(0) FROM ClientDeals d WHERE d.StatusId = 1 AND d.ClientId = a.ClientId) [TotalDeals]
                            ,a.ClientId
                           ,a.Name
                           ,a.NickName
                           ,a.ContactNo
                           ,a.ICNo
                           ,a.DOB
                           ,a.CareerDesc
                           ,a.CurrentAddress
                           ,a.EmailAdd
                           ,a.EducationDesc
                           ,a.IncomeYearDesc
                           ,a.OtherSourceofIncomeDesc
                           ,a.FilingDate
                           ,a.CreatedBy
                           ,a.CreatedDate
                           ,a.UpdatedBy
                           ,a.UpdatedDate
                           ,a.SourceId
                           ,SourceMaster.Name [SourceDesc]
                           ,a.StatusId
                           ,StatusMaster.Name [StatusDesc]
                           ,a.AnnualIncomeId
                           ,AnnualIncomeMaster.Name [AnnualIncomeDesc]
                           ,a.AgeId
                           ,AgeMaster.Name [AgeDesc]
                           ,a.OccupationId
                           ,OccupationMaster.Name [OccupationDesc]
                           ,a.MaritalId
                           ,MaritalMaster.Name [MaritalDesc]
                           ,a.LengthOfTimeKnownId
                           ,LengthOfTimeKnownMaster.Name [LengthOfTimeKnownDesc]
                           ,a.HowOftenSeenInAYearId
                           ,HowOftenSeenInAYearMaster.Name [HowOftenSeenInAYearDesc]
                           ,a.HowWellKnownId
                           ,HowWellKnownMaster.Name [HowWellKnownDesc]
                           ,a.CouldApproachOnBusinessId
                           ,CouldApproachOnBusinessMaster.Name [CouldApproachOnBusinessDesc]
                           ,a.AbilityToProvideRefId
                           ,AbilityToProvideRefMaster.Name [AbilityToProvideRefDesc]
                           ,a.GenderId
                           ,GenderMaster.Name [GenderDesc]
                           ,a.KIVDate
                        FROM [Clients] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas SourceMaster WITH (NOLOCK)
                        	ON SourceMaster.MasterId = 10
                        		AND a.SourceId = SourceMaster.MasterDataId
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas AnnualIncomeMaster WITH (NOLOCK)
                        	ON AnnualIncomeMaster.MasterId = 3
                        		AND a.AnnualIncomeId = AnnualIncomeMaster.MasterDataId
                        LEFT JOIN MasterDatas AgeMaster WITH (NOLOCK)
                        	ON AgeMaster.MasterId = 12
                        		AND a.AgeId = AgeMaster.MasterDataId
                        LEFT JOIN MasterDatas OccupationMaster WITH (NOLOCK)
                        	ON OccupationMaster.MasterId = 4
                        		AND a.OccupationId = OccupationMaster.MasterDataId
                        LEFT JOIN MasterDatas MaritalMaster WITH (NOLOCK)
                        	ON MaritalMaster.MasterId = 5
                        		AND a.MaritalId = MaritalMaster.MasterDataId
                        LEFT JOIN MasterDatas LengthOfTimeKnownMaster WITH (NOLOCK)
                        	ON LengthOfTimeKnownMaster.MasterId = 2
                        		AND a.LengthOfTimeKnownId = LengthOfTimeKnownMaster.MasterDataId
                        LEFT JOIN MasterDatas HowOftenSeenInAYearMaster WITH (NOLOCK)
                        	ON HowOftenSeenInAYearMaster.MasterId = 7
                        		AND a.HowOftenSeenInAYearId = HowOftenSeenInAYearMaster.MasterDataId
                        LEFT JOIN MasterDatas HowWellKnownMaster WITH (NOLOCK)
                        	ON HowWellKnownMaster.MasterId = 6
                        		AND a.HowWellKnownId = HowWellKnownMaster.MasterDataId
                        LEFT JOIN MasterDatas CouldApproachOnBusinessMaster WITH (NOLOCK)
                        	ON CouldApproachOnBusinessMaster.MasterId = 8
                        		AND a.CouldApproachOnBusinessId = CouldApproachOnBusinessMaster.MasterDataId
                        LEFT JOIN MasterDatas AbilityToProvideRefMaster WITH (NOLOCK)
                        	ON AbilityToProvideRefMaster.MasterId = 9
                        		AND a.AbilityToProvideRefId = AbilityToProvideRefMaster.MasterDataId
                        LEFT JOIN MasterDatas GenderMaster WITH (NOLOCK)
                        	ON GenderMaster.MasterId = 13
                        		AND a.GenderId = GenderMaster.MasterDataId
                        WHERE (@ClientId IS NULL OR a.ClientId = @ClientId)
                                AND a.StatusId NOT IN (2)
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);

                    oResult = conn.Query<ClientDetailsEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        /// <summary>
        /// Filter KIV status
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="statusId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        internal List<ClientDetailsEnt> GetClientByFilter(long? clientId, long? statusId, DateTime? KIVDateFrom, DateTime? KIVDateTo, DateTime? CreatedDateFrom, DateTime? CreatedDateTo, long pageSize, long pageNumber, bool filterKIV, string createdBy, string statusIdList, string name = "", string contact=null)
        {
            List<ClientDetailsEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT
                            (SELECT COUNT(0) FROM ClientDeals d WHERE d.StatusId = 1 AND d.ClientId = a.ClientId) [TotalDeals]
                           ,a.ClientId
                           ,a.Name
                           ,a.NickName
                           ,a.ContactNo
                           ,a.ICNo
                           ,a.DOB
                           ,a.CareerDesc
                           ,a.CurrentAddress
                           ,a.EmailAdd
                           ,a.EducationDesc
                           ,a.IncomeYearDesc
                           ,a.OtherSourceofIncomeDesc
                           ,a.FilingDate
                           ,a.CreatedBy
                           ,a.CreatedDate
                           ,a.UpdatedBy
                           ,a.UpdatedDate
                           ,a.SourceId
                           ,SourceMaster.Name [SourceDesc]
                           ,a.StatusId
                           ,StatusMaster.Name [StatusDesc]
                           ,a.AnnualIncomeId
                           ,AnnualIncomeMaster.Name [AnnualIncomeDesc]
                           ,a.AgeId
                           ,AgeMaster.Name [AgeDesc]
                           ,a.OccupationId
                           ,OccupationMaster.Name [OccupationDesc]
                           ,a.MaritalId
                           ,MaritalMaster.Name [MaritalDesc]
                           ,a.LengthOfTimeKnownId
                           ,LengthOfTimeKnownMaster.Name [LengthOfTimeKnownDesc]
                           ,a.HowOftenSeenInAYearId
                           ,HowOftenSeenInAYearMaster.Name [HowOftenSeenInAYearDesc]
                           ,a.HowWellKnownId
                           ,HowWellKnownMaster.Name [HowWellKnownDesc]
                           ,a.CouldApproachOnBusinessId
                           ,CouldApproachOnBusinessMaster.Name [CouldApproachOnBusinessDesc]
                           ,a.AbilityToProvideRefId
                           ,AbilityToProvideRefMaster.Name [AbilityToProvideRefDesc]
                           ,a.GenderId
                           ,GenderMaster.Name [GenderDesc]
                           ,a.KIVDate
                        FROM [Clients] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas SourceMaster WITH (NOLOCK)
                        	ON SourceMaster.MasterId = 10
                        		AND a.SourceId = SourceMaster.MasterDataId
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas AnnualIncomeMaster WITH (NOLOCK)
                        	ON AnnualIncomeMaster.MasterId = 3
                        		AND a.AnnualIncomeId = AnnualIncomeMaster.MasterDataId
                        LEFT JOIN MasterDatas AgeMaster WITH (NOLOCK)
                        	ON AgeMaster.MasterId = 12
                        		AND a.AgeId = AgeMaster.MasterDataId
                        LEFT JOIN MasterDatas OccupationMaster WITH (NOLOCK)
                        	ON OccupationMaster.MasterId = 4
                        		AND a.OccupationId = OccupationMaster.MasterDataId
                        LEFT JOIN MasterDatas MaritalMaster WITH (NOLOCK)
                        	ON MaritalMaster.MasterId = 5
                        		AND a.MaritalId = MaritalMaster.MasterDataId
                        LEFT JOIN MasterDatas LengthOfTimeKnownMaster WITH (NOLOCK)
                        	ON LengthOfTimeKnownMaster.MasterId = 2
                        		AND a.LengthOfTimeKnownId = LengthOfTimeKnownMaster.MasterDataId
                        LEFT JOIN MasterDatas HowOftenSeenInAYearMaster WITH (NOLOCK)
                        	ON HowOftenSeenInAYearMaster.MasterId = 7
                        		AND a.HowOftenSeenInAYearId = HowOftenSeenInAYearMaster.MasterDataId
                        LEFT JOIN MasterDatas HowWellKnownMaster WITH (NOLOCK)
                        	ON HowWellKnownMaster.MasterId = 6
                        		AND a.HowWellKnownId = HowWellKnownMaster.MasterDataId
                        LEFT JOIN MasterDatas CouldApproachOnBusinessMaster WITH (NOLOCK)
                        	ON CouldApproachOnBusinessMaster.MasterId = 8
                        		AND a.CouldApproachOnBusinessId = CouldApproachOnBusinessMaster.MasterDataId
                        LEFT JOIN MasterDatas AbilityToProvideRefMaster WITH (NOLOCK)
                        	ON AbilityToProvideRefMaster.MasterId = 9
                        		AND a.AbilityToProvideRefId = AbilityToProvideRefMaster.MasterDataId
                        LEFT JOIN MasterDatas GenderMaster WITH (NOLOCK)
                        	ON GenderMaster.MasterId = 13
                        		AND a.GenderId = GenderMaster.MasterDataId                        
                        WHERE (@ClientId IS NULL OR a.ClientId = @ClientId)
                        AND (@Name IS NULL OR a.Name LIKE '%' + @Name +'%')
                        AND (@ContactNo IS NULL OR a.ContactNo  = @ContactNo )
                        --AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        {(statusIdList == null ? "" : $"AND a.StatusId IN ({statusIdList})")}
                        AND (@KIVDateFrom IS NULL OR a.KIVDate >= @KIVDateFrom)
                        AND (@KIVDateTo IS NULL OR a.KIVDate <= @KIVDateTo)
                        AND (CAST(@CreatedDateFrom AS DATE) IS NULL OR CAST(a.CreatedDate AS DATE) >= @CreatedDateFrom)
                        AND (CAST(@CreatedDateTo AS DATE) IS NULL OR CAST(a.CreatedDate AS DATE) <= @CreatedDateTo)
                        AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                        {(filterKIV ? "AND a.StatusId != 6 -- KIV" : "")}
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.Name
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    //param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@Name", name, dbType: DbType.AnsiString);
                    param.Add("@ContactNo", contact, dbType: DbType.AnsiString);
                    param.Add("@KIVDateFrom", KIVDateFrom, dbType: DbType.DateTime);
                    param.Add("@KIVDateTo", KIVDateTo, dbType: DbType.DateTime);
                    param.Add("@CreatedDateFrom", CreatedDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", CreatedDateTo, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientDetailsEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<UsersEnt> GetSystemUserByUsername(SystemUserRequest obj)
        {
            List<UsersEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
	                        u.*
                           ,StatusMaster.Name [StatusDesc]
                           ,r.Code RoleCode
                           ,r.Name RoleName
                           ,up.Username UplineUsername
                           ,up.DisplayName UplineDisplayName
                        FROM Users u
                        LEFT JOIN Roles r
	                        ON u.RoleId = r.RoleId
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	                        ON StatusMaster.MasterId = 1
		                        AND u.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN Users up
	                        ON up.UserId = u.UplineUserId
                        WHERE (@Username IS NULL OR u.Username = @Username)
                        AND (@RoleId IS NULL OR u.RoleId = @RoleId)
                        AND (@UserId IS NULL OR u.UserId = @UserId)
                        AND (@UplineUserId IS NULL OR u.UplineUserId = @UplineUserId)
                        AND (@StatusId IS NULL OR u.StatusId = @StatusId)
                        AND (@CreatedBy IS NULL OR u.CreatedBy = @CreatedBy)
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.AnsiString);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@UplineUserId", obj.UplineUserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    oResult = conn.Query<UsersEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }


        internal List<UsersEnt> GetSystemUserByFilter(SystemUserFilter obj, long pageSize, long pageNumber)
        {
            List<UsersEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
	                        u.*
                           ,StatusMaster.Name [StatusDesc]
                           ,r.Code RoleCode
                           ,r.Name RoleName
                           ,up.Username UplineUsername
                           ,up.DisplayName UplineDisplayName
                        FROM Users u
                        LEFT JOIN Roles r
	                        ON u.RoleId = r.RoleId
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	                        ON StatusMaster.MasterId = 1
		                        AND u.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN Users up
	                        ON up.UserId = u.UplineUserId
                        WHERE u.StatusId>0
                        AND (@Username IS NULL OR u.Username = @Username)
                        AND (@IcNo IS NULL OR u.IcNo = @IcNo)
                        AND (@RoleId IS NULL OR u.RoleId = @RoleId)
                        AND (@UplineUserId IS NULL OR u.UplineUserId = @UplineUserId)
                        AND (@StatusId IS NULL OR u.StatusId = @StatusId)
                        AND (@CreatedBy IS NULL OR u.CreatedBy = @CreatedBy)
                        ORDER BY u.Username
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.AnsiString);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@UplineUserId", obj.UplineUserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@IcNo", obj.IcNo, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UsersEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<UsersMenuEnt> GetMenuByUser(string username)
        {
            List<UsersMenuEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                         SELECT
                             mm.MainMenuId
                            ,mm.Name MainMenuName
                            ,sm.SubMenuId
                            ,sm.Name SubMenuName  
                            ,sm.PageAction
                            ,sm.PageController
                         FROM Users u
                         LEFT JOIN Roles r
                         	ON u.RoleId = r.RoleId
                         LEFT JOIN RoleMenus rm
                         	ON rm.RoleId = u.RoleId
                         LEFT JOIN SubMenus sm
                         	ON sm.SubMenuId = rm.SubMenuId
                         		AND rm.StatusId = 1
                         LEFT JOIN MainMenus mm
                         	ON mm.MainMenuId = sm.MainMenuId
                         		AND mm.StatusId = 1
                         WHERE u.Username = @Username
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.AnsiString);

                    oResult = conn.Query<UsersMenuEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateClientStatus(long clientId, long statusId, string updatedBy, bool IsRevert)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        UPDATE Clients
                            SET StatusId = @StatusId,
                            UpdatedBy = @UpdatedBy,
                            UpdatedDate = GETDATE(),
                            UpdatedDateStatus = GETDATE()
                        WHERE ClientId = @ClientId;
                    ";

                    if (IsRevert)
                    {
                        query += $@"
                      INSERT INTO [ClientKIVHistory] (ClientId
                      , FilingDate
                      , KIVDate
                      , RevertDate
                      , CreatedBy
                      , CreatedDate)
                      	SELECT
                      		c.ClientId
                      	   ,FilingDate
                      	   ,KIVDate
                      	   ,GETDATE()
                      	   ,@UpdatedBy
                      	   ,GETDATE()
                      	FROM Clients c
                      	WHERE c.ClientId = @ClientId;";
                    }

                    var param = new DynamicParameters();
                    //Clients
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientBasicInfo(ClientsEnt oClient)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        UPDATE Clients
                        SET [Name] = @Name
                           ,NickName = @NickName
                           ,ContactNo = @ContactNo
                           ,SourceId = @SourceId
                           ,AnnualIncomeId = @AnnualIncomeId
                           ,AgeId = @AgeId
                           ,OccupationId = @OccupationId
                           ,MaritalId = @MaritalId
                           ,OtherSourceofIncomeDesc = @OtherSourceofIncomeDesc
                           ,GenderId = @GenderId
                           ,CurrentAddress = @CurrentAddress
                           ,ICNo = @ICNo
                           ,EmailAdd = @EmailAdd
                           ,DOB = @DOB
                           ,EducationDesc = @EducationDesc
                           ,CareerDesc = @CareerDesc
                           ,UpdatedBy = @UpdatedBy
                           ,UpdatedDate = GETDATE()
                        WHERE ClientId = @ClientId
                    ";

                    var param = new DynamicParameters();

                    param.Add("@ClientId", oClient.ClientId, dbType: DbType.Int64);
                    param.Add("@Name", oClient.Name, dbType: DbType.AnsiString);
                    param.Add("@NickName", oClient.NickName, dbType: DbType.AnsiString);
                    param.Add("@@DOB", oClient.DOB, dbType: DbType.Date);
                    param.Add("@ContactNo", oClient.ContactNo, dbType: DbType.AnsiString);
                    param.Add("@SourceId", oClient.SourceId, dbType: DbType.Int64);
                    param.Add("@AnnualIncomeId", oClient.AnnualIncomeId, dbType: DbType.Int64);
                    param.Add("@AgeId", oClient.AgeId, dbType: DbType.Int64);
                    param.Add("@OccupationId", oClient.OccupationId, dbType: DbType.Int64);
                    param.Add("@MaritalId", oClient.MaritalId, dbType: DbType.Int64);
                    param.Add("@OtherSourceofIncomeDesc", oClient.OtherSourceofIncomeDesc, dbType: DbType.AnsiString);
                    param.Add("@CurrentAddress", oClient.CurrentAddress, dbType: DbType.AnsiString);
                    param.Add("@GenderId", oClient.GenderId, dbType: DbType.Int64);
                    param.Add("@ICNo", oClient.ICNo, dbType: DbType.AnsiString);
                    param.Add("@EmailAdd", oClient.EmailAdd, dbType: DbType.AnsiString);
                    param.Add("@EducationDesc", oClient.EducationDesc, dbType: DbType.AnsiString);
                    param.Add("@CareerDesc", oClient.CareerDesc, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", oClient.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientRelationshipInfo(ClientsEnt oClient)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        UPDATE Clients
                        SET FilingDate = @FilingDate
                           ,LengthOfTimeKnownId = @LengthOfTimeKnownId
                           ,HowWellKnownId = @HowWellKnownId
                           ,HowOftenSeenInAYearId = @HowOftenSeenInAYearId
                           ,CouldApproachOnBusinessId = @CouldApproachOnBusinessId
                           ,AbilityToProvideRefId = @AbilityToProvideRefId
                           ,UpdatedBy = @UpdatedBy
                           ,UpdatedDate = GETDATE()
                        WHERE ClientId = @ClientId
                    ";

                    var param = new DynamicParameters();

                    param.Add("@ClientId", oClient.ClientId, dbType: DbType.Int64);
                    param.Add("@FilingDate", oClient.FilingDate, dbType: DbType.DateTime);
                    param.Add("@LengthOfTimeKnownId", oClient.LengthOfTimeKnownId, dbType: DbType.Int64);
                    param.Add("@HowWellKnownId", oClient.HowWellKnownId, dbType: DbType.Int64);
                    param.Add("@HowOftenSeenInAYearId", oClient.HowOftenSeenInAYearId, dbType: DbType.Int64);
                    param.Add("@CouldApproachOnBusinessId", oClient.CouldApproachOnBusinessId, dbType: DbType.Int64);
                    param.Add("@AbilityToProvideRefId", oClient.AbilityToProvideRefId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", oClient.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        #region Client Policy
        internal bool AddClientPolicy(ClientPolicyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[ClientPolicy] ([ClientId]
, [StatusId]
, [CompanyDesc]
, [PolicyNo]
, [PolicyTypeDesc]
, [SumAssured]
, [Premium]
, CriticaIIllnessVal
, PersonalAccidentVal
, MedicalCardVal
, CoverageTerms
, [DateInForced]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ClientId, @StatusId, @CompanyDesc, @PolicyNo, @PolicyTypeDesc, @SumAssured, @Premium, @CriticaIIllnessVal, @PersonalAccidentVal, @MedicalCardVal, @CoverageTerms, @DateInForced, @CreatedBy, GETDATE());
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CompanyDesc", obj.CompanyDesc, dbType: DbType.AnsiString);
                    param.Add("@PolicyNo", obj.PolicyNo, dbType: DbType.AnsiString);
                    param.Add("@PolicyTypeDesc", obj.PolicyTypeDesc, dbType: DbType.AnsiString);
                    param.Add("@SumAssured", obj.SumAssured, dbType: DbType.Decimal);
                    param.Add("@Premium", obj.Premium, dbType: DbType.Decimal);
                    param.Add("@CriticaIIllnessVal", obj.CriticaIIllnessVal, dbType: DbType.Decimal);
                    param.Add("@PersonalAccidentVal", obj.PersonalAccidentVal, dbType: DbType.Decimal);
                    param.Add("@MedicalCardVal", obj.MedicalCardVal, dbType: DbType.Decimal);
                    param.Add("@CoverageTerms", obj.CoverageTerms, dbType: DbType.String);
                    param.Add("@DateInForced", obj.DateInForced, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientPolicy(ClientPolicyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientPolicy]
SET CompanyDesc = @CompanyDesc
   ,PolicyNo = @PolicyNo
   ,PolicyTypeDesc = @PolicyTypeDesc
   ,SumAssured = @SumAssured
   ,Premium = @Premium
   ,CriticaIIllnessVal = @CriticaIIllnessVal
   ,PersonalAccidentVal = @PersonalAccidentVal
   ,MedicalCardVal = @MedicalCardVal
   ,CoverageTerms = @CoverageTerms
   ,DateInForced = @DateInForced
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientPolicyId = @ClientPolicyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientPolicyId", obj.ClientPolicyId, dbType: DbType.Int64);
                    param.Add("@CompanyDesc", obj.CompanyDesc, dbType: DbType.AnsiString);
                    param.Add("@PolicyNo", obj.PolicyNo, dbType: DbType.AnsiString);
                    param.Add("@PolicyTypeDesc", obj.PolicyTypeDesc, dbType: DbType.AnsiString);
                    param.Add("@SumAssured", obj.SumAssured, dbType: DbType.Decimal);
                    param.Add("@Premium", obj.Premium, dbType: DbType.Decimal);
                    param.Add("@CriticaIIllnessVal", obj.CriticaIIllnessVal, dbType: DbType.Decimal);
                    param.Add("@PersonalAccidentVal", obj.PersonalAccidentVal, dbType: DbType.Decimal);
                    param.Add("@MedicalCardVal", obj.MedicalCardVal, dbType: DbType.Decimal);
                    param.Add("@CoverageTerms", obj.CoverageTerms, dbType: DbType.String);
                    param.Add("@DateInForced", obj.DateInForced, dbType: DbType.DateTime);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientPolicyStatus(long clientPolicyId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientPolicy]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientPolicyId = @ClientPolicyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientPolicyId", clientPolicyId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientPolicyEnt> GetClientPolicyByFilter(long? clientId, long? clientPolicyId, long? statusId, long pageSize, long pageNumber)
        {
            List<ClientPolicyEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                        FROM [ClientPolicy] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId                        
                        WHERE (@ClientId IS NULL OR a.ClientId = @ClientId)
                        AND (@ClientPolicyId IS NULL OR a.ClientPolicyId = @ClientPolicyId)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.CompanyDesc
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@ClientPolicyId", clientPolicyId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientPolicyEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Client Family
        internal bool AddClientFamily(ClientFamilyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[ClientFamily] ([ClientId]
, [Name]
, [StatusId]
, [RelationId]
, [DOB]
, [GenderId]
, [HobbyDesc]
, [HPDesc]
, [SchoolDesc]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ClientId, @Name, @StatusId, @RelationId, @DOB, @GenderId, @HobbyDesc, @HPDesc, @SchoolDesc, @Remarks, @CreatedBy, GETDATE());
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@RelationId", obj.RelationId, dbType: DbType.Int64);
                    param.Add("@DOB", obj.DOB, dbType: DbType.DateTime);
                    param.Add("@GenderId", obj.GenderId, dbType: DbType.Int64);
                    param.Add("@HobbyDesc", obj.HobbyDesc, dbType: DbType.AnsiString);
                    param.Add("@HPDesc", obj.HPDesc, dbType: DbType.AnsiString);
                    param.Add("@SchoolDesc", obj.SchoolDesc, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientFamily(ClientFamilyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientFamily]
SET Name = @Name
   ,RelationId = @RelationId
   ,DOB = @DOB
   ,GenderId = @GenderId
   ,HobbyDesc = @HobbyDesc
   ,HPDesc = @HPDesc
   ,SchoolDesc = @SchoolDesc
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientFamilyId = @ClientFamilyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientFamilyId", obj.ClientFamilyId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@RelationId", obj.RelationId, dbType: DbType.Int64);
                    param.Add("@DOB", obj.DOB, dbType: DbType.DateTime);
                    param.Add("@GenderId", obj.GenderId, dbType: DbType.Int64);
                    param.Add("@HobbyDesc", obj.HobbyDesc, dbType: DbType.AnsiString);
                    param.Add("@HPDesc", obj.HPDesc, dbType: DbType.AnsiString);
                    param.Add("@SchoolDesc", obj.SchoolDesc, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientFamilyStatus(long clientFamilyId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientFamily]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientFamilyId = @ClientFamilyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientFamilyId", clientFamilyId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientFamilyEnt> GetClientFamilyByFilter(long? clientId, long? clientFamilyId, long? statusId, long pageSize, long pageNumber)
        {
            List<ClientFamilyEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,GenderMaster.Name [GenderDesc]
                            ,RelationMaster.Name [RelationDesc]
                        FROM [ClientFamily] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas GenderMaster WITH (NOLOCK)
                        	ON GenderMaster.MasterId = 13
                        		AND a.GenderId = GenderMaster.MasterDataId
                        LEFT JOIN MasterDatas RelationMaster WITH (NOLOCK)
                        	ON RelationMaster.MasterId = 15
                        		AND a.RelationId = RelationMaster.MasterDataId
                        WHERE (@ClientId IS NULL OR a.ClientId = @ClientId)
                        AND (@ClientFamilyId IS NULL OR a.ClientFamilyId = @ClientFamilyId)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.Name
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@ClientFamilyId", clientFamilyId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientFamilyEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Client Deal
        internal bool AddClientDeal(ClientDealEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"

INSERT INTO [dbo].[ClientDeals] ([ClientId]
, [StatusId]
, [DealTitleId]
, [Name]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ClientId, @StatusId, @DealTitleId, @Name, @Remarks, @CreatedBy, GETDATE());
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealId", obj.ClientDealId, dbType: DbType.Int64);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@DealTitleId", obj.DealTitleId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientDeal(ClientDealEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientDeals]
SET DealTitleId = @DealTitleId   
   ,Remarks = @Remarks
   ,Name = @Name
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientDealId = @ClientDealId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealId", obj.ClientDealId, dbType: DbType.Int64);
                    param.Add("@DealTitleId", obj.DealTitleId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientDealStatus(long clientDealId, long statusId, string updatedBy, bool IsClientConfirmed, long clientId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var reqUpdateClientToConfirmQuery = $@"
UPDATE Clients
    SET StatusId = {(long)MasterDataEnum.Status_Confirm},
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE ClientId = {clientId};
";

                    var query = $@"
{(IsClientConfirmed ? reqUpdateClientToConfirmQuery : "")};

UPDATE [ClientDeals]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientDealId = @ClientDealId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealId", clientDealId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        conn.Execute(query, param, commandType: CommandType.Text, transaction: transaction);
                        transaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientDealEnt> GetClientDealByFilter(long? clientId, long? clientDealId, long? statusId, long? dealTitleId, string name, string clientName,
            string createdBy, string clientCreatedBy, DateTime? createdDateFrom, DateTime? createdDateTo, long pageSize, long pageNumber)
        {
            List<ClientDealEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,DealTitleMaster.Name [DealTitleDesc]
                            ,c.Name [ClientName]
                            ,c.ContactNo
                            ,c.CreatedBy ClientCreatedBy
                        FROM [ClientDeals] a WITH (NOLOCK)
                         LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                         	ON StatusMaster.MasterId = 1
                         		AND a.StatusId = StatusMaster.MasterDataId
                         LEFT JOIN MasterDatas DealTitleMaster WITH (NOLOCK)
                         	ON DealTitleMaster.MasterId = 14
                        		AND a.DealTitleId = DealTitleMaster.MasterDataId
                         LEFT JOIN Clients c WITH (NOLOCK)
                         	ON a.ClientId = c.ClientId
                         WHERE (@ClientId IS NULL OR a.ClientId = @ClientId)
                           AND (@ClientDealId IS NULL OR a.ClientDealId = @ClientDealId)
                           AND (@DealTitleId IS NULL OR a.DealTitleId = @DealTitleId)
                           AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                           AND (@Name IS NULL OR a.Name LIKE '%' + @Name +'%')
                           AND (@ClientName IS NULL OR c.Name LIKE '%' + @ClientName +'%')
                           AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                           AND (@ClientCreatedBy IS NULL OR c.CreatedBy = @ClientCreatedBy)
                           AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
                           AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
                           AND a.StatusId NOT IN (2)
                         ORDER BY DealTitleMaster.Name
                         OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                         FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@ClientDealId", clientDealId, dbType: DbType.Int64);
                    param.Add("@DealTitleId", dealTitleId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@Name", name, dbType: DbType.AnsiString);
                    param.Add("@ClientName", clientName, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);
                    param.Add("@ClientCreatedBy", clientCreatedBy, dbType: DbType.AnsiString);
                    param.Add("@CreatedDateFrom", createdDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", createdDateTo, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientDealEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Client Activity
        internal long AddClientDealActivity(ClientActivityEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[ClientDealActivities] ([ClientDealId]
, [ActivityPointId]
, [Points]
, [StatusId]
, [ActivityStartDate]
, [ActivityEndDate]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ClientDealId, @ActivityPointId, @Points, @StatusId, @ActivityStartDate, @ActivityEndDate, @Remarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealId", obj.ClientDealId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@Points", obj.Points, dbType: DbType.Int32);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    //conn.Execute(query, param, commandType: CommandType.Text);
                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateClientDealActivity(ClientActivityEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientDealActivities]
SET ActivityPointId = @ActivityPointId   
   ,ActivityStartDate = @ActivityStartDate
   ,ActivityEndDate = @ActivityEndDate
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientDealActivityId = @ClientDealActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealActivityId", obj.ClientDealActivityId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientDealActivityStatus(long clientDealActivityId, long statusId, int points, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientDealActivities]
SET StatusId = @StatusId
   ,points = @Points
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientDealActivityId = @ClientDealActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealActivityId", clientDealActivityId, dbType: DbType.Int64);
                    param.Add("@Points", points, dbType: DbType.Int32);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientActivityEnt> GetClientDealActivityByFilter(long? clientId, long? clientDealActivityId, long? dealTitleId, long? clientDealId, long? statusId, DateTime? activityStartDate, DateTime? activityEndDate, string createdBy, long? clientDealStatusId, long pageSize, long pageNumber)
        {
            List<ClientActivityEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	a.*
   ,cd.ClientId
   ,StatusMaster.Name [StatusDesc]
   ,cd.DealTitleId
   ,DealTitleMaster.Name [DealTitleDesc]
   ,ap.Name [ActivityPointsDesc]
   ,ap.Points PointSetting
   ,ap.ColorCode
   ,c.Name [ClientName]
   ,c.ContactNo
   ,c.StatusId [ClientStatusId]
   ,ClientStatusMaster.Name [ClientStatusDesc]
   ,cd.StatusId [ClientDealStatusId]
   ,ClientDealStatusMaster.Name [ClientDealStatusDesc]
FROM [ClientDealActivities] a WITH (NOLOCK)
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	ON StatusMaster.MasterId = 1
		AND a.StatusId = StatusMaster.MasterDataId
LEFT JOIN ClientDeals cd
	ON cd.ClientDealId = a.ClientDealId
LEFT JOIN MasterDatas ClientDealStatusMaster WITH (NOLOCK)
	ON ClientDealStatusMaster.MasterId = 1
		AND cd.StatusId = ClientDealStatusMaster.MasterDataId
LEFT JOIN MasterDatas DealTitleMaster WITH (NOLOCK)
	ON DealTitleMaster.MasterId = 14
		AND cd.DealTitleId = DealTitleMaster.MasterDataId
LEFT JOIN ActivityPoints ap
	ON ap.ActivityPointId = a.ActivityPointId
LEFT JOIN Clients c WITH (NOLOCK)
	ON cd.ClientId = c.ClientId
LEFT JOIN MasterDatas ClientStatusMaster WITH (NOLOCK)
	ON ClientStatusMaster.MasterId = 1
		AND c.StatusId = ClientStatusMaster.MasterDataId
WHERE (@ClientId IS NULL OR cd.ClientId = @ClientId)
AND (@ClientDealActivityId IS NULL OR a.ClientDealActivityId = @ClientDealActivityId)
AND (@DealTitleId IS NULL OR cd.DealTitleId = @DealTitleId)
AND (@ClientDealId IS NULL OR a.ClientDealId = @ClientDealId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)
AND (@ClientDealStatusId IS NULL OR cd.StatusId = @ClientDealStatusId)
AND (@ActivityStartDate IS NULL OR a.ActivityStartDate >= @ActivityStartDate)
AND (@ActivityEndDate IS NULL OR a.ActivityEndDate <= @ActivityEndDate)
AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
AND a.StatusId NOT IN (2)
ORDER BY DealTitleMaster.Name
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@ClientDealActivityId", clientDealActivityId, dbType: DbType.Int64);
                    param.Add("@DealTitleId", dealTitleId, dbType: DbType.Int64);
                    param.Add("@ClientDealId", clientDealId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@ClientDealStatusId", clientDealStatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", activityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", activityEndDate, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientActivityEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateClientDealActivityPoint(long clientDealActivityId, int points)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientDealActivities]
SET points = @Points
WHERE ClientDealActivityId = @ClientDealActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealActivityId", clientDealActivityId, dbType: DbType.Int64);
                    param.Add("@Points", points, dbType: DbType.Int32);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region Client Lead
        internal bool AddClientLead(ClientLeadEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[ClientLeads] ([ClientDealActivityId]
, [ClientId]
, [Name]
, [HP]
, [StatusId]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ClientDealActivityId, @ClientId, @Name, @HP, @StatusId, @Remarks, @CreatedBy, GETDATE())
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientDealActivityId", obj.ClientDealActivityId, dbType: DbType.Int64);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@HP", obj.HP, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientLead(ClientLeadEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientLeads]
SET ClientId = @ClientId
   ,Name = @Name
   ,HP = @HP
   ,Remarks=@Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientLeadId = @ClientLeadId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientLeadId", obj.ClientLeadId, dbType: DbType.Int64);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@HP", obj.HP, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateClientLeadStatus(long clientLeadId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ClientLeads]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientLeadId = @ClientLeadId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientLeadId", clientLeadId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ClientLeadEnt> GetClientLeadByFilter(long? clientLeadId, long? clientDealActivityId, long? statusId, long pageSize, long pageNumber)
        {
            List<ClientLeadEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                        FROM [ClientLeads] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId                        
                        WHERE (@ClientLeadId IS NULL OR a.ClientLeadId = @ClientLeadId)
                        AND (@ClientDealActivityId IS NULL OR a.ClientDealActivityId = @ClientDealActivityId)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.Name
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientLeadId", clientLeadId, dbType: DbType.Int64);
                    param.Add("@ClientDealActivityId", clientDealActivityId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientLeadEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        internal List<ActivityPointsEnt> GetActivityPoint()
        {
            List<ActivityPointsEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
                        	*
                        FROM [ActivityPoints] a WITH (NOLOCK)
                        WHERE a.StatusId = 1
                    ";

                    var param = new DynamicParameters();

                    oResult = conn.Query<ActivityPointsEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<ClientKIVRevertHistory> GetClientKIVRevertHistoryByFilter(long? clientId, string name, DateTime? KIVDateFrom, DateTime? KIVDateTo, string createdBy, long pageSize, long pageNumber)
        {
            List<ClientKIVRevertHistory> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	c.Name
   ,c.ContactNo
   ,h.*
FROM [ClientKIVHistory] h
LEFT JOIN Clients c
	ON h.ClientId = c.ClientId
WHERE (@ClientId IS NULL OR h.ClientId = @ClientId)
AND (@Name IS NULL OR c.Name LIKE '%' + @Name +'%')
AND (@KIVDateFrom IS NULL OR h.KIVDate >= @KIVDateFrom)
AND (@KIVDateTo IS NULL OR h.KIVDate <= @KIVDateTo)
AND (@CreatedBy IS NULL OR h.CreatedBy = @CreatedBy)
                        ORDER BY c.Name
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ClientId", clientId, dbType: DbType.Int64);
                    param.Add("@Name", name, dbType: DbType.AnsiString);
                    param.Add("@KIVDateFrom", KIVDateFrom, dbType: DbType.DateTime);
                    param.Add("@KIVDateTo", KIVDateTo, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientKIVRevertHistory>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<ClientSummaryEnt> GetClientSummaryByFilter(DateTime? createdDateFrom, DateTime? createdDateTo, string createdBy)
        {
            List<ClientSummaryEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
WITH SourceMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		1 Sort
	   ,SortOrder
	   ,COUNT(SourceId) TotalSource
	   ,a.SourceId
	   ,SourceMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas SourceMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.SourceId = SourceMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE SourceMaster.MasterId = 10
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.SourceId
			,SourceMaster.Name
			,MasterId),
AnnualIncomeMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		2 Sort
	   ,SortOrder
	   ,COUNT(AnnualIncomeId) TotalSource
	   ,a.AnnualIncomeId
	   ,AnnualIncomeMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas AnnualIncomeMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.AnnualIncomeId = AnnualIncomeMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE AnnualIncomeMaster.MasterId = 3
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.AnnualIncomeId
			,AnnualIncomeMaster.Name
			,MasterId),
AgeMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		3 Sort
	   ,SortOrder
	   ,COUNT(AgeId) TotalSource
	   ,a.AgeId
	   ,AgeMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas AgeMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.AgeId = AgeMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE AgeMaster.MasterId = 12
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.AgeId
			,AgeMaster.Name
			,MasterId),
OccupationMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		4 Sort
	   ,SortOrder
	   ,COUNT(OccupationId) TotalSource
	   ,a.OccupationId
	   ,OccupationMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas OccupationMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.OccupationId = OccupationMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE OccupationMaster.MasterId = 4
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.OccupationId
			,OccupationMaster.Name
			,MasterId),
MaritalMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		5 Sort
	   ,SortOrder
	   ,COUNT(MaritalId) TotalSource
	   ,a.MaritalId
	   ,MaritalMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas MaritalMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.MaritalId = MaritalMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE MaritalMaster.MasterId = 5
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.MaritalId
			,MaritalMaster.Name
			,MasterId),
LengthOfTimeKnownMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		6 Sort
	   ,SortOrder
	   ,COUNT(LengthOfTimeKnownId) TotalSource
	   ,a.LengthOfTimeKnownId
	   ,LengthOfTimeKnownMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas LengthOfTimeKnownMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.LengthOfTimeKnownId = LengthOfTimeKnownMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE LengthOfTimeKnownMaster.MasterId = 2
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.LengthOfTimeKnownId
			,LengthOfTimeKnownMaster.Name
			,MasterId),
HowWellKnownMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		7 Sort
	   ,SortOrder
	   ,COUNT(HowWellKnownId) TotalSource
	   ,a.HowWellKnownId
	   ,HowWellKnownMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas HowWellKnownMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.HowWellKnownId = HowWellKnownMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE HowWellKnownMaster.MasterId = 6
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.HowWellKnownId
			,HowWellKnownMaster.Name
			,MasterId),
HowOftenSeenInAYearMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		8 Sort
	   ,SortOrder
	   ,COUNT(HowOftenSeenInAYearId) TotalSource
	   ,a.HowOftenSeenInAYearId
	   ,HowOftenSeenInAYearMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas HowOftenSeenInAYearMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.HowOftenSeenInAYearId = HowOftenSeenInAYearMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE HowOftenSeenInAYearMaster.MasterId = 7
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.HowOftenSeenInAYearId
			,HowOftenSeenInAYearMaster.Name
			,MasterId),
CouldApproachOnBusinessMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		9 Sort
	   ,SortOrder
	   ,COUNT(CouldApproachOnBusinessId) TotalSource
	   ,a.CouldApproachOnBusinessId
	   ,CouldApproachOnBusinessMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas CouldApproachOnBusinessMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.CouldApproachOnBusinessId = CouldApproachOnBusinessMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE CouldApproachOnBusinessMaster.MasterId = 8
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.CouldApproachOnBusinessId
			,CouldApproachOnBusinessMaster.Name
			,MasterId),
AbilityToProvideRefMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		10 Sort
	   ,SortOrder
	   ,COUNT(AbilityToProvideRefId) TotalSource
	   ,a.AbilityToProvideRefId
	   ,AbilityToProvideRefMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas AbilityToProvideRefMaster WITH (NOLOCK)
	LEFT JOIN [Clients] a WITH (NOLOCK)
		ON a.AbilityToProvideRefId = AbilityToProvideRefMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	WHERE AbilityToProvideRefMaster.MasterId = 9
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
	GROUP BY SortOrder
			,a.AbilityToProvideRefId
			,AbilityToProvideRefMaster.Name
			,MasterId)
SELECT
	M.Name MasterName
   ,T.Name MasterDataName
   ,T.Total INTO #TempSummary
FROM (SELECT
		*
	FROM SourceMaster
	UNION ALL
	SELECT
		*
	FROM AnnualIncomeMaster
	UNION ALL
	SELECT
		*
	FROM AgeMaster

	UNION ALL
	SELECT
		*
	FROM OccupationMaster
	UNION ALL
	SELECT
		*
	FROM MaritalMaster

	UNION ALL
	SELECT
		*
	FROM LengthOfTimeKnownMaster
	UNION ALL
	SELECT
		*
	FROM HowWellKnownMaster
	UNION ALL
	SELECT
		*
	FROM HowOftenSeenInAYearMaster
	UNION ALL
	SELECT
		*
	FROM CouldApproachOnBusinessMaster
	UNION ALL
	SELECT
		*
	FROM AbilityToProvideRefMaster) T
LEFT JOIN Masters M
	ON M.MasterId = T.MasterId
ORDER BY Sort, SortOrder;

SELECT 
MasterName, COUNT(MasterName) MasterNameCount, SUM(Total) SumTotal INTO #TempSummaryGroup
FROM #TempSummary 
GROUP BY MasterName;

SELECT a.*, (a.Total * 1 * 100) / CAST(b.SumTotal AS FLOAT) [Percentage]
FROM #TempSummary a LEFT JOIN #TempSummaryGroup b ON a.MasterName = b.MasterName;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@CreatedDateFrom", createdDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", createdDateTo, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);

                    oResult = conn.Query<ClientSummaryEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<AgentEnt> GetSystemUserByFilter(string username, long? roleId, long? uplineUserId)
        {
            List<AgentEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
                        	u.*
                           ,r.RoleId
                           ,r.Code RoleCode
                           ,r.Name RoleName
                           ,up.Username UplineUsername
                           ,up.DisplayName UplineDisplayName
                        FROM Users u
                        LEFT JOIN Roles r
                        	ON u.RoleId = r.RoleId
                        LEFT JOIN Users up
                        	ON up.UserId = u.UplineUserId
                        WHERE u.StatusId=1 AND (@Username IS NULL OR u.Username = @Username)
                        AND (@RoleId IS NULL OR u.RoleId = @RoleId)
                        AND (@UplineUserId IS NULL OR u.UplineUserId = @UplineUserId)
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.AnsiString);
                    param.Add("@RoleId", roleId, dbType: DbType.Int64);
                    param.Add("@UplineUserId", uplineUserId, dbType: DbType.Int64);

                    oResult = conn.Query<AgentEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        #region Activity Review
        internal long AddActivityReview(ActivityReviewEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[ActivityReviews] ( [ActivityReviewTypeId]
, [UserId]
, [DateInWeek]
, [ReviewText1]
, [ReviewText2]
, [ReviewText3]
, [ReviewText4]
, [ReviewText5]
, [ReviewText6]
, [ReviewText7]
, [ReviewText8]
, [ReviewText9]
, [ReviewText10]
, [ReviewText11]
, [Remarks]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@ActivityReviewTypeId, @UserId, @DateInWeek, @ReviewText1, @ReviewText2, @ReviewText3, @ReviewText4, @ReviewText5, @ReviewText6, @ReviewText7, @ReviewText8, @ReviewText9, @ReviewText10, @ReviewText11, @Remarks, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ActivityReviewTypeId", obj.ActivityReviewTypeId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@DateInWeek", obj.DateInWeek, dbType: DbType.DateTime);
                    param.Add("@ReviewText1", obj.ReviewText1, dbType: DbType.AnsiString);
                    param.Add("@ReviewText2", obj.ReviewText2, dbType: DbType.AnsiString);
                    param.Add("@ReviewText3", obj.ReviewText3, dbType: DbType.AnsiString);
                    param.Add("@ReviewText4", obj.ReviewText4, dbType: DbType.AnsiString);
                    param.Add("@ReviewText5", obj.ReviewText5, dbType: DbType.AnsiString);
                    param.Add("@ReviewText6", obj.ReviewText6, dbType: DbType.AnsiString);
                    param.Add("@ReviewText7", obj.ReviewText7, dbType: DbType.AnsiString);
                    param.Add("@ReviewText8", obj.ReviewText8, dbType: DbType.AnsiString);
                    param.Add("@ReviewText9", obj.ReviewText9, dbType: DbType.AnsiString);
                    param.Add("@ReviewText10", obj.ReviewText10, dbType: DbType.AnsiString);
                    param.Add("@ReviewText11", obj.ReviewText11, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateActivityReview(ActivityReviewEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [ActivityReviews]
SET UserId = @UserId
   ,ActivityReviewTypeId = @ActivityReviewTypeId
   ,DateInWeek = @DateInWeek
   ,ReviewText1 = @ReviewText1
   ,ReviewText2 = @ReviewText2
   ,ReviewText3 = @ReviewText3
   ,ReviewText4 = @ReviewText4
   ,ReviewText5 = @ReviewText5
   ,ReviewText6 = @ReviewText6
   ,ReviewText7 = @ReviewText7
   ,ReviewText8 = @ReviewText8
   ,ReviewText9 = @ReviewText9
   ,ReviewText10 = @ReviewText10
   ,ReviewText11 = @ReviewText11
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ActivityReviewId = @ActivityReviewId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ActivityReviewId", obj.ActivityReviewId, dbType: DbType.Int64);
                    param.Add("@ActivityReviewTypeId", obj.ActivityReviewTypeId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@DateInWeek", obj.DateInWeek, dbType: DbType.DateTime);
                    param.Add("@ReviewText1", obj.ReviewText1, dbType: DbType.AnsiString);
                    param.Add("@ReviewText2", obj.ReviewText2, dbType: DbType.AnsiString);
                    param.Add("@ReviewText3", obj.ReviewText3, dbType: DbType.AnsiString);
                    param.Add("@ReviewText4", obj.ReviewText4, dbType: DbType.AnsiString);
                    param.Add("@ReviewText5", obj.ReviewText5, dbType: DbType.AnsiString);
                    param.Add("@ReviewText6", obj.ReviewText6, dbType: DbType.AnsiString);
                    param.Add("@ReviewText7", obj.ReviewText7, dbType: DbType.AnsiString);
                    param.Add("@ReviewText8", obj.ReviewText8, dbType: DbType.AnsiString);
                    param.Add("@ReviewText9", obj.ReviewText9, dbType: DbType.AnsiString);
                    param.Add("@ReviewText10", obj.ReviewText10, dbType: DbType.AnsiString);
                    param.Add("@ReviewText11", obj.ReviewText11, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<ActivityReviewEnt> GetActivityReviewByFilter(ActivityReviewFilter obj, long pageSize, long pageNumber)
        {
            List<ActivityReviewEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,u.Username
                            ,ActivityReviewType.Name [ActivityReviewTypeDesc]
                        FROM [ActivityReviews] a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId
                            LEFT JOIN MasterDatas ActivityReviewType WITH (NOLOCK)
                        	ON ActivityReviewType.MasterId = 18
                        		AND a.ActivityReviewTypeId = ActivityReviewType.MasterDataId
                            LEFT JOIN Users u WITH (NOLOCK)
                            ON a.UserId= u.UserId
                            WHERE (@ActivityReviewId IS NULL OR a.ActivityReviewId = @ActivityReviewId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@ActivityReviewTypeId IS NULL OR a.ActivityReviewTypeId = @ActivityReviewTypeId)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND (@DateInWeekFrom IS NULL OR a.DateInWeek >= @DateInWeekFrom)
                            AND (@DateInWeekTo IS NULL OR a.DateInWeek <= @DateInWeekTo)
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.ActivityReviewId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ActivityReviewId", obj.ActivityReviewId, dbType: DbType.Int64);
                    param.Add("@ActivityReviewTypeId", obj.ActivityReviewTypeId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@DateInWeekFrom", obj.DateInWeekFrom, dbType: DbType.DateTime);
                    param.Add("@DateInWeekTo", obj.DateInWeekTo, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ActivityReviewEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region User Activity
        internal long AddUserActivity(UserActivityEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[UserActivities] ([UserId]
, [ActivityPointId]
, [StatusId]
, [ActivityStartDate]
, [ActivityEndDate]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@UserId, @ActivityPointId, @StatusId, @ActivityStartDate, @ActivityEndDate, @Remarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateUserActivity(UserActivityEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserActivities]
SET UserId = @UserId
   ,ActivityPointId = @ActivityPointId   
   ,ActivityStartDate = @ActivityStartDate
   ,ActivityEndDate = @ActivityEndDate
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE UserActivityId = @UserActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserActivityId", obj.UserActivityId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<UserActivityEnt> GetUserActivityByFilter(UserActivityFilter obj, long pageSize, long pageNumber)
        {
            List<UserActivityEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,u.Username
                            ,ap.Name [ActivityPointsDesc]
                            ,ap.Points PointSetting
                            ,ap.ColorCode
                        FROM UserActivities a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId                        
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId= u.UserId
                            LEFT JOIN ActivityPoints ap
                            	ON ap.ActivityPointId = a.ActivityPointId
                            WHERE (@UserActivityId IS NULL OR a.UserActivityId = @UserActivityId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND (@ActivityStartDate IS NULL OR a.ActivityStartDate >= @ActivityStartDate)
                            AND (@ActivityEndDate IS NULL OR a.ActivityEndDate <= @ActivityEndDate)
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.UserActivityId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserActivityId", obj.UserActivityId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UserActivityEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Users
        internal long AddUsers(UsersEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[Users] ([Username]
, [DisplayName]
, [Fullname]
, [PW]
, [RoleId]
, [UplineUserId]
, [IcNo]
, [ContactNo]
, [Email]
, [JoinDate]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@Username, @DisplayName, @Fullname, @PW, @RoleId, @UplineUserId, @IcNo, @ContactNo, @Email, @JoinDate, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@DisplayName", obj.DisplayName, dbType: DbType.String);
                    param.Add("@Fullname", obj.Fullname, dbType: DbType.String);
                    param.Add("@PW", obj.PW, dbType: DbType.String);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@UplineUserId", obj.UplineUserId, dbType: DbType.Int64);
                    param.Add("@IcNo", obj.IcNo, dbType: DbType.String);
                    param.Add("@ContactNo", obj.ContactNo, dbType: DbType.String);
                    param.Add("@Email", obj.Email, dbType: DbType.String);
                    param.Add("@JoinDate", obj.JoinDate, dbType: DbType.DateTime);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateUsers(UsersEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [dbo].[Users]  
SET   [Fullname]=@Fullname  
, [IcNo]=@IcNo
, [ContactNo]=@ContactNo
, [Email]=@Email  
, [RoleId]=@RoleId  
,UpdatedBy = @UpdatedBy
,UpdatedDate = GETDATE()
 WHERE UserId = @UserId  ";

                    var param = new DynamicParameters();
                    param.Add("@Fullname", obj.Fullname, dbType: DbType.String);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@IcNo", obj.IcNo, dbType: DbType.String);
                    param.Add("@ContactNo", obj.ContactNo, dbType: DbType.String);
                    param.Add("@Email", obj.Email, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.CreatedBy, dbType: DbType.String);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);


                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateUserStatus(string username, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Users]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE Username = @Username;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.String);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateUserPW(string username, string pw, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Users]
SET PW = @PW
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE Username = @Username;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.String);
                    param.Add("@PW", pw, dbType: DbType.String);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// users: update username, roleid
        /// clients: update createdby, updatedby
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newUsername"></param>
        /// <param name="roleId"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        internal bool ConvertOne2OneAgent(string username, string newUsername, long roleId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
BEGIN TRY
	BEGIN TRANSACTION

    UPDATE [Users]
    SET RoleId = @RoleId
       ,Username = @NewUsername
       ,UpdatedBy = @NewUsername
       ,UpdatedDate = GETDATE()
    WHERE Username = @Username;

    UPDATE [Clients]
    SET CreatedBy = @NewUsername
    WHERE CreatedBy = @Username;

    UPDATE [Clients]
    SET UpdatedBy = @NewUsername
    WHERE UpdatedBy = @Username;

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	DECLARE @Error_Number INT
		    ,@Error_Severity INT
		    ,@Error_State INT
		    ,@Error_Procedure VARCHAR(1000)
		    ,@Error_Line INT
		    ,@Error_Message VARCHAR(8000);

	SELECT
		@Error_Number = ERROR_NUMBER()
	    ,@Error_Severity = ERROR_SEVERITY()
	    ,@Error_State = ERROR_STATE()
	    ,@Error_Procedure = ERROR_PROCEDURE()
	    ,@Error_Line = ERROR_LINE()
	    ,@Error_Message = ERROR_MESSAGE();

	ROLLBACK TRANSACTION;
	RAISERROR (@Error_Message, @Error_Severity, @Error_State);
END CATCH;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.String);
                    param.Add("@NewUsername", newUsername, dbType: DbType.String);
                    param.Add("@RoleId", roleId, dbType: DbType.Int64);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }


        internal List<UsersStaffEnt> GetSystemStaffByFilter(SystemUserFilter obj, long pageSize, long pageNumber)
        {
            List<UsersStaffEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
	                        u.*
                           ,StatusMaster.Name [StatusDesc]
                           ,r.Code RoleCode
                           ,r.Name RoleName
                           ,up.Username UplineUsername
                           ,up.DisplayName UplineDisplayName
                        FROM Users u
                        LEFT JOIN Roles r
	                        ON u.RoleId = r.RoleId
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	                        ON StatusMaster.MasterId = 1
		                        AND u.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN Users up
	                        ON up.UserId = u.UplineUserId
                        WHERE R.RoleId Not IN (2,3,4) 
                        AND (@Username IS NULL OR u.Username = @Username)
                        AND (@IcNo IS NULL OR u.IcNo = @IcNo)
                        AND (@RoleId IS NULL OR u.RoleId = @RoleId)
                        AND (@StatusId IS NULL OR u.StatusId = @StatusId)
                        AND (@CreatedBy IS NULL OR u.CreatedBy = @CreatedBy)
                        ORDER BY u.Username
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.AnsiString);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@UplineUserId", obj.UplineUserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@IcNo", obj.IcNo, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UsersStaffEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Announcement
        internal long AddAnnouncement(AnnouncementEnt obj, string userIdList)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    string queryForAddUserAnnouncement = $@"
                        INSERT INTO [dbo].[UserAnnouncement] (
                          [AnnouncementId]
                        , [UserId]
                        , [StatusId]
                        , [CreatedBy]
                        , [CreatedDate])
	                        SELECT
		                        @AnnouncementId_new
	                           ,u.UserId
	                           ,@StatusId
	                           ,@CreatedBy
	                           ,GETDATE()
	                        FROM Users u
	                        WHERE u.UserId IN ({userIdList});
                    ";

                    var query = $@"
                        BEGIN TRY
	                        BEGIN TRANSACTION

                            DECLARE @AnnouncementId_new BIGINT = NULL;

	                        INSERT INTO [dbo].[Announcement] ([Title]
	                        , [Remarks]
	                        , [StatusId]
	                        , [AnnouncementTypeId]
	                        , [PublishStartDate]
	                        , [PublishEndDate]
	                        , [CreatedBy]
	                        , [CreatedDate])
		                        VALUES (@Title, @Remarks, @StatusId, @AnnouncementTypeId, @PublishStartDate, @PublishEndDate, @CreatedBy, GETDATE());

	                        SELECT @AnnouncementId_new = SCOPE_IDENTITY();

	                        {(obj.AnnouncementTypeId == (long)AnnouncementType.specified_user ? queryForAddUserAnnouncement : "")}

	                        SELECT @AnnouncementId_new;

	                        COMMIT TRANSACTION
                        END TRY
                        BEGIN CATCH
	                        DECLARE @Error_Number INT
		                           ,@Error_Severity INT
		                           ,@Error_State INT
		                           ,@Error_Procedure VARCHAR(1000)
		                           ,@Error_Line INT
		                           ,@Error_Message VARCHAR(8000);

	                        SELECT
		                        @Error_Number = ERROR_NUMBER()
	                           ,@Error_Severity = ERROR_SEVERITY()
	                           ,@Error_State = ERROR_STATE()
	                           ,@Error_Procedure = ERROR_PROCEDURE()
	                           ,@Error_Line = ERROR_LINE()
	                           ,@Error_Message = ERROR_MESSAGE();

	                        ROLLBACK TRANSACTION;
	                        RAISERROR (@Error_Message, @Error_Severity, @Error_State);
                        END CATCH;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AnnouncementId", obj.AnnouncementId, dbType: DbType.Int64);
                    param.Add("@Title", obj.Title, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@AnnouncementTypeId", obj.AnnouncementTypeId, dbType: DbType.Int64);
                    param.Add("@PublishStartDate", obj.PublishStartDate, dbType: DbType.DateTime);
                    param.Add("@PublishEndDate", obj.PublishEndDate, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAnnouncement(AnnouncementEnt obj, string userIdList)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    string queryForAddUserAnnouncement = $@"
                        INSERT INTO [dbo].[UserAnnouncement] (
                          [AnnouncementId]
                        , [UserId]
                        , [StatusId]
                        , [CreatedBy]
                        , [CreatedDate])
	                        SELECT
		                        @AnnouncementId
	                           ,u.UserId
	                           ,@StatusId
	                           ,@UpdatedBy
	                           ,GETDATE()
	                        FROM Users u
	                        WHERE u.UserId IN ({userIdList}) AND NOT EXISTS (SELECT a.UserId FROM UserAnnouncement a WHERE a.UserId = U.UserId AND AnnouncementId = @AnnouncementId);
                    ";

                    var query = $@"
                        BEGIN TRY
	                        BEGIN TRANSACTION

                            UPDATE [dbo].[Announcement]
                            SET Title = @Title
                               ,Remarks = @Remarks
                               ,StatusId = @StatusId
                               ,AnnouncementTypeId = @AnnouncementTypeId
                               ,PublishStartDate = @PublishStartDate
                               ,PublishEndDate = @PublishEndDate
                               ,UpdatedBy = @UpdatedBy
                               ,UpdatedDate = GETDATE()
                            WHERE AnnouncementId = @AnnouncementId

                            DELETE FROM UserAnnouncement WHERE AnnouncementId = @AnnouncementId;

	                        {(obj.AnnouncementTypeId == (long)AnnouncementType.specified_user ? queryForAddUserAnnouncement : "")}

	                        COMMIT TRANSACTION
                        END TRY
                        BEGIN CATCH
	                        DECLARE @Error_Number INT
		                           ,@Error_Severity INT
		                           ,@Error_State INT
		                           ,@Error_Procedure VARCHAR(1000)
		                           ,@Error_Line INT
		                           ,@Error_Message VARCHAR(8000);

	                        SELECT
		                        @Error_Number = ERROR_NUMBER()
	                           ,@Error_Severity = ERROR_SEVERITY()
	                           ,@Error_State = ERROR_STATE()
	                           ,@Error_Procedure = ERROR_PROCEDURE()
	                           ,@Error_Line = ERROR_LINE()
	                           ,@Error_Message = ERROR_MESSAGE();

	                        ROLLBACK TRANSACTION;
	                        RAISERROR (@Error_Message, @Error_Severity, @Error_State);
                        END CATCH;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AnnouncementId", obj.AnnouncementId, dbType: DbType.Int64);
                    param.Add("@Title", obj.Title, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@AnnouncementTypeId", obj.AnnouncementTypeId, dbType: DbType.Int64);
                    param.Add("@PublishStartDate", obj.PublishStartDate, dbType: DbType.DateTime);
                    param.Add("@PublishEndDate", obj.PublishEndDate, dbType: DbType.DateTime);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.String);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AnnouncementEnt> GetAnnouncementByFilter(AnnouncementFilter obj, long pageSize, long pageNumber, bool withUserAnnouncement = false)
        {
            List<AnnouncementEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	a.*
   ,StatusMaster.Name [StatusDesc]
   ,AnnouncementTypeMaster.Name [AnnouncementTypeDesc]";

                    if (withUserAnnouncement)
                    {
                        query += $@"
    ,ua.UserId
   ,u.Username
";
                    }

                    query += $@"FROM [Announcement] a WITH (NOLOCK)
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	ON StatusMaster.MasterId = 1
		AND a.StatusId = StatusMaster.MasterDataId
LEFT JOIN MasterDatas AnnouncementTypeMaster WITH (NOLOCK)
	ON AnnouncementTypeMaster.MasterId = 17
		AND a.AnnouncementTypeId = AnnouncementTypeMaster.MasterDataId";
                    if (withUserAnnouncement)
                    {
                        query += $@"    
                    LEFT JOIN UserAnnouncement ua WITH (NOLOCK)
 	ON a.AnnouncementId = ua.AnnouncementId
 LEFT JOIN Users u WITH (NOLOCK)
 	ON ua.UserId = u.UserId
";
                    }
                    query += $@"
WHERE (@AnnouncementId IS NULL OR a.AnnouncementId = @AnnouncementId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
AND (@PublishStartDate IS NULL OR  @PublishStartDate>=a.PublishStartDate)
AND (@PublishEndDate IS NULL OR  @PublishEndDate<=a.PublishEndDate)
AND a.StatusId NOT IN (2)
ORDER BY a.PublishStartDate
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AnnouncementId", obj.AnnouncementId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PublishStartDate", obj.PublishStartDate, dbType: DbType.DateTime);
                    param.Add("@PublishEndDate", obj.PublishEndDate, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AnnouncementEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<UserAnnouncementEnt> GetAnnouncementByFilter_UserList(AnnouncementFilter obj, long pageSize, long pageNumber, bool withUserAnnouncement = false)
        {
            List<UserAnnouncementEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	a.*
   ,StatusMaster.Name [StatusDesc]
   ,AnnouncementTypeMaster.Name [AnnouncementTypeDesc]";

                    if (withUserAnnouncement)
                    {
                        query += $@"
    ,ua.UserId
   ,u.Username
";
                    }

                    query += $@"FROM [Announcement] a WITH (NOLOCK)
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	ON StatusMaster.MasterId = 1
		AND a.StatusId = StatusMaster.MasterDataId
LEFT JOIN MasterDatas AnnouncementTypeMaster WITH (NOLOCK)
	ON AnnouncementTypeMaster.MasterId = 17
		AND a.AnnouncementTypeId = AnnouncementTypeMaster.MasterDataId";
                    if (withUserAnnouncement)
                    {
                        query += $@"    
                    LEFT JOIN UserAnnouncement ua WITH (NOLOCK)
 	ON a.AnnouncementId = ua.AnnouncementId
 LEFT JOIN Users u WITH (NOLOCK)
 	ON ua.UserId = u.UserId
";
                    }
                    query += $@"
WHERE (@AnnouncementId IS NULL OR a.AnnouncementId = @AnnouncementId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
AND (@PublishStartDate IS NULL OR  @PublishStartDate >= a.PublishStartDate )
AND (@PublishEndDate IS NULL OR  @PublishEndDate<= a.PublishEndDate )
AND a.StatusId NOT IN (2)
ORDER BY a.PublishStartDate
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AnnouncementId", obj.AnnouncementId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PublishStartDate", obj.PublishStartDate, dbType: DbType.DateTime);
                    param.Add("@PublishEndDate", obj.PublishEndDate, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UserAnnouncementEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }


        internal bool UpdateAnnouncementStatus(long clientId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        UPDATE Announcement
                            SET StatusId = @StatusId,
                            UpdatedBy = @UpdatedBy,
                            UpdatedDate = GETDATE()
                        WHERE AnnouncementId = @AnnouncementId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AnnouncementId", clientId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region Budget
        internal long AddBudget(BudgetEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[Budget] ([UserId]
, [BudgetYear]
, [BudgetMonth]
, [ProductPricePlan]
, [ProductStartMonth]
, [ProductQtyMonth1]
, [ProductQtyMonth2]
, [ProductQtyMonth3]
, [ProductQtyMonth4]
, [ProductQtyMonth5]
, [ProductQtyMonth6]
, [ProductQtyMonth7]
, [ProductQtyMonth8]
, [ProductQtyMonth9]
, [ProductQtyMonth10]
, [ProductQtyMonth11]
, [ProductQtyMonth12]
, [RecruitmentCount1]
, [RecruitmentCount2]
, [RecruitmentCount3]
, [RecruitmentCount4]
, [RecruitmentCount5]
, [RecruitmentCount6]
, [RecruitmentCount7]
, [RecruitmentCount8]
, [RecruitmentCount9]
, [RecruitmentCount10]
, [RecruitmentCount11]
, [RecruitmentCount12]
, [Remarks]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@UserId, @BudgetYear, @BudgetMonth, @ProductPricePlan, @ProductStartMonth, @ProductQtyMonth1, @ProductQtyMonth2, @ProductQtyMonth3, @ProductQtyMonth4, @ProductQtyMonth5, @ProductQtyMonth6, @ProductQtyMonth7, @ProductQtyMonth8, @ProductQtyMonth9, @ProductQtyMonth10, @ProductQtyMonth11, @ProductQtyMonth12, @RecruitmentCount1, @RecruitmentCount2, @RecruitmentCount3, @RecruitmentCount4, @RecruitmentCount5, @RecruitmentCount6, @RecruitmentCount7, @RecruitmentCount8, @RecruitmentCount9, @RecruitmentCount10, @RecruitmentCount11, @RecruitmentCount12, @Remarks, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    param.Add("@ProductPricePlan", obj.ProductPricePlan, dbType: DbType.Int64);
                    param.Add("@ProductStartMonth", obj.ProductStartMonth, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth1", obj.ProductQtyMonth1, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth2", obj.ProductQtyMonth2, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth3", obj.ProductQtyMonth3, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth4", obj.ProductQtyMonth4, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth5", obj.ProductQtyMonth5, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth6", obj.ProductQtyMonth6, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth7", obj.ProductQtyMonth7, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth8", obj.ProductQtyMonth8, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth9", obj.ProductQtyMonth9, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth10", obj.ProductQtyMonth10, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth11", obj.ProductQtyMonth11, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth12", obj.ProductQtyMonth12, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount1", obj.RecruitmentCount1, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount2", obj.RecruitmentCount2, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount3", obj.RecruitmentCount3, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount4", obj.RecruitmentCount4, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount5", obj.RecruitmentCount5, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount6", obj.RecruitmentCount6, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount7", obj.RecruitmentCount7, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount8", obj.RecruitmentCount8, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount9", obj.RecruitmentCount9, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount10", obj.RecruitmentCount10, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount11", obj.RecruitmentCount11, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount12", obj.RecruitmentCount12, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateBudget(BudgetEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Budget]
SET UserId = @UserId,
    -- BudgetYear = @BudgetYear,
    BudgetMonth = @BudgetMonth,
    ProductPricePlan = @ProductPricePlan,
    ProductStartMonth = @ProductStartMonth,
    ProductQtyMonth1 = @ProductQtyMonth1,
    ProductQtyMonth2 = @ProductQtyMonth2,
    ProductQtyMonth3 = @ProductQtyMonth3,
    ProductQtyMonth4 = @ProductQtyMonth4,
    ProductQtyMonth5 = @ProductQtyMonth5,
    ProductQtyMonth6 = @ProductQtyMonth6,
    ProductQtyMonth7 = @ProductQtyMonth7,
    ProductQtyMonth8 = @ProductQtyMonth8,
    ProductQtyMonth9 = @ProductQtyMonth9,
    ProductQtyMonth10 = @ProductQtyMonth10,
    ProductQtyMonth11 = @ProductQtyMonth11,
    ProductQtyMonth12 = @ProductQtyMonth12,
    RecruitmentCount1 = @RecruitmentCount1,
    RecruitmentCount2 = @RecruitmentCount2,
    RecruitmentCount3 = @RecruitmentCount3,
    RecruitmentCount4 = @RecruitmentCount4,
    RecruitmentCount5 = @RecruitmentCount5,
    RecruitmentCount6 = @RecruitmentCount6,
    RecruitmentCount7 = @RecruitmentCount7,
    RecruitmentCount8 = @RecruitmentCount8,
    RecruitmentCount9 = @RecruitmentCount9,
    RecruitmentCount10 = @RecruitmentCount10,
    RecruitmentCount11 = @RecruitmentCount11,
    RecruitmentCount12 = @RecruitmentCount12,
    Remarks = @Remarks,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE BudgetId = @BudgetId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    //param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    param.Add("@ProductPricePlan", obj.ProductPricePlan, dbType: DbType.Int64);
                    param.Add("@ProductStartMonth", obj.ProductStartMonth, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth1", obj.ProductQtyMonth1, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth2", obj.ProductQtyMonth2, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth3", obj.ProductQtyMonth3, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth4", obj.ProductQtyMonth4, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth5", obj.ProductQtyMonth5, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth6", obj.ProductQtyMonth6, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth7", obj.ProductQtyMonth7, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth8", obj.ProductQtyMonth8, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth9", obj.ProductQtyMonth9, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth10", obj.ProductQtyMonth10, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth11", obj.ProductQtyMonth11, dbType: DbType.Int64);
                    param.Add("@ProductQtyMonth12", obj.ProductQtyMonth12, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount1", obj.RecruitmentCount1, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount2", obj.RecruitmentCount2, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount3", obj.RecruitmentCount3, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount4", obj.RecruitmentCount4, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount5", obj.RecruitmentCount5, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount6", obj.RecruitmentCount6, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount7", obj.RecruitmentCount7, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount8", obj.RecruitmentCount8, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount9", obj.RecruitmentCount9, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount10", obj.RecruitmentCount10, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount11", obj.RecruitmentCount11, dbType: DbType.Int64);
                    param.Add("@RecruitmentCount12", obj.RecruitmentCount12, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<BudgetEnt> GetBudgetByFilter(BudgetFilter obj, long pageSize, long pageNumber)
        {
            List<BudgetEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,u.Username
                        FROM Budget a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId                        
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId= u.UserId
                            WHERE (@BudgetId IS NULL OR a.BudgetId = @BudgetId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND (@BudgetYear IS NULL OR a.BudgetYear = @BudgetYear)                           
                            AND (@BudgetMonth IS NULL OR a.BudgetMonth = @BudgetMonth)                           
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.BudgetId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<BudgetEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Budget - Target Appt
        internal bool UpdateTargetApptClosingRatio(BudgetEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Budget]
SET TargetApptClosingRatio = @TargetApptClosingRatio,TargetApptCallRatio = @TargetApptCallRatio,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE BudgetId = @BudgetId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@TargetApptClosingRatio", obj.TargetApptClosingRatio, dbType: DbType.Decimal);
                    param.Add("@TargetApptCallRatio", obj.TargetApptCallRatio, dbType: DbType.Decimal);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region Budget Proportion
        internal bool UpdateBudgetProportion(BudgetProportionEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Budget]
SET BudgetProportionStartMonth = @BudgetProportionStartMonth
   ,BudgetProportionPercentage1 = @BudgetProportionPercentage1
   ,BudgetProportionPercentage2 = @BudgetProportionPercentage2
   ,BudgetProportionPercentage3 = @BudgetProportionPercentage3
   ,BudgetProportionPercentage4 = @BudgetProportionPercentage4
   ,BudgetProportionPercentage5 = @BudgetProportionPercentage5
   ,BudgetProportionPercentage6 = @BudgetProportionPercentage6
   ,BudgetProportionPercentage7 = @BudgetProportionPercentage7
   ,BudgetProportionPercentage8 = @BudgetProportionPercentage8
   ,BudgetProportionPercentage9 = @BudgetProportionPercentage9
   ,BudgetProportionPercentage10 = @BudgetProportionPercentage10
   ,BudgetProportionPercentage11 = @BudgetProportionPercentage11
   ,BudgetProportionPercentage12 = @BudgetProportionPercentage12
   ,BudgetProportionAmt1 = @BudgetProportionAmt1
   ,BudgetProportionAmt2 = @BudgetProportionAmt2
   ,BudgetProportionAmt3 = @BudgetProportionAmt3
   ,BudgetProportionAmt4 = @BudgetProportionAmt4
   ,BudgetProportionAmt5 = @BudgetProportionAmt5
   ,BudgetProportionAmt6 = @BudgetProportionAmt6
   ,BudgetProportionAmt7 = @BudgetProportionAmt7
   ,BudgetProportionAmt8 = @BudgetProportionAmt8
   ,BudgetProportionAmt9 = @BudgetProportionAmt9
   ,BudgetProportionAmt10 = @BudgetProportionAmt10
   ,BudgetProportionAmt11 = @BudgetProportionAmt11
   ,BudgetProportionAmt12 = @BudgetProportionAmt12
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE BudgetId = @BudgetId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@BudgetProportionStartMonth", obj.BudgetProportionStartMonth, dbType: DbType.Int64);
                    param.Add("@BudgetProportionPercentage1", obj.BudgetProportionPercentage1, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage2", obj.BudgetProportionPercentage2, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage3", obj.BudgetProportionPercentage3, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage4", obj.BudgetProportionPercentage4, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage5", obj.BudgetProportionPercentage5, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage6", obj.BudgetProportionPercentage6, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage7", obj.BudgetProportionPercentage7, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage8", obj.BudgetProportionPercentage8, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage9", obj.BudgetProportionPercentage9, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage10", obj.BudgetProportionPercentage10, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage11", obj.BudgetProportionPercentage11, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionPercentage12", obj.BudgetProportionPercentage12, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt1", obj.BudgetProportionPercentage1, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt2", obj.BudgetProportionPercentage2, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt3", obj.BudgetProportionPercentage3, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt4", obj.BudgetProportionPercentage4, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt5", obj.BudgetProportionPercentage5, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt6", obj.BudgetProportionPercentage6, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt7", obj.BudgetProportionPercentage7, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt8", obj.BudgetProportionPercentage8, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt9", obj.BudgetProportionPercentage9, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt10", obj.BudgetProportionPercentage10, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt11", obj.BudgetProportionPercentage11, dbType: DbType.Decimal);
                    param.Add("@BudgetProportionAmt12", obj.BudgetProportionPercentage12, dbType: DbType.Decimal);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region Budget Group
        internal List<BudgetGroupEnt> GetBudgetGroupByFilter(BudgetFilter obj)
        {
            List<BudgetGroupEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,u.Username
                        FROM BudgetGroup a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId                        
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId = u.UserId
                            WHERE (@BudgetId IS NULL OR a.BudgetId = @BudgetId)
                            AND (@BudgetGroupId IS NULL OR a.BudgetGroupId = @BudgetGroupId)
                            AND (@BudgetGroupUserId IS NULL OR a.UserId = @BudgetGroupUserId)
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.BudgetId
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@BudgetGroupId", obj.BudgetGroupId, dbType: DbType.Int64);
                    param.Add("@BudgetGroupUserId", obj.BudgetGroupUserId, dbType: DbType.Int64);

                    oResult = conn.Query<BudgetGroupEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal long AddBudgetGroup(BudgetGroupEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[BudgetGroup] ([BudgetId]
, [BudgetTitle]
, [UserId]
, [TargetCount]
, [TargetComm]
, [TotalCase]
, [Remarks]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@BudgetId, @BudgetTitle, @UserId, @TargetCount, @TargetComm, @TotalCase, @Remarks, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetId", obj.BudgetId, dbType: DbType.Int64);
                    param.Add("@BudgetTitle", obj.BudgetTitle, dbType: DbType.String);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@TargetCount", obj.TargetCount, dbType: DbType.Int64);
                    param.Add("@TargetComm", obj.TargetComm, dbType: DbType.Decimal);
                    param.Add("@TotalCase", obj.TotalCase, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateBudgetGroup(BudgetGroupEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [BudgetGroup]
SET BudgetTitle = @BudgetTitle
   ,UserId = @UserId
   ,TargetCount = @TargetCount
   ,TargetComm = @TargetComm
   ,TotalCase = @TotalCase
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE BudgetGroupId = @BudgetGroupId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetGroupId", obj.BudgetGroupId, dbType: DbType.Int64);
                    param.Add("@BudgetTitle", obj.BudgetTitle, dbType: DbType.String);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@TargetCount", obj.TargetCount, dbType: DbType.Int64);
                    param.Add("@TargetComm", obj.TargetComm, dbType: DbType.Decimal);
                    param.Add("@TotalCase", obj.TotalCase, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateStatusBudgetGroup(long budgetGroupId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [BudgetGroup]
SET
    StatusId = @StatusId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE BudgetGroupId = @BudgetGroupId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetGroupId", budgetGroupId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        internal List<ClientLeadReport> GetClientLeadReport(ClientLeadReportFilter obj)
        {
            List<ClientLeadReport> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	c.ClientId
   ,c.Name [ClientName]
   ,a.Name [ReferralName]
   ,a.CreatedBy
   ,a.CreatedDate
   ,a.HP
   ,a.Remarks
   ,a.StatusId
   ,StatusMaster.Name [StatusDesc]
FROM ClientLeads a
LEFT JOIN Clients c
	ON a.ClientId = c.ClientId
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
    ON StatusMaster.MasterId = 1
        AND a.StatusId = StatusMaster.MasterDataId
WHERE (@ReferralName IS NULL OR a.Name LIKE '%' + @ReferralName +'%')
AND (@ClientName IS NULL OR c.Name LIKE '%' + @ClientName +'%')
AND (@CreatedBy IS NULL OR a.CreatedBy LIKE '%' + @CreatedBy +'%')
AND (@ClientId IS NULL OR a.ClientId = @ClientId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)
AND a.StatusId NOT IN (2)
ORDER BY a.Name
    OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
    FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ReferralName", obj.ReferralName, dbType: DbType.String);
                    param.Add("@ClientName", obj.ClientName, dbType: DbType.String);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", obj.PageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", obj.PageSize, dbType: DbType.Int64);

                    oResult = conn.Query<ClientLeadReport>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        #region Monthly budget
        internal long AddBudgetMonthly(BudgetMonthlyEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[BudgetMonthly] ([UserId]
, [BudgetYear]
, [BudgetMonth]
, [MonthlyBudgetTypeId]
, [MonthlyBudgetPercentage]
, [NoOfCases]
, [ClientId]
, [PersonName]
, [BudgetValue]
, [AchieveValue]
, [Remarks]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@UserId, @BudgetYear, @BudgetMonth, @MonthlyBudgetTypeId, @MonthlyBudgetPercentage, @NoOfCases, @ClientId, @PersonName, @BudgetValue, @AchieveValue, @Remarks, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    param.Add("@MonthlyBudgetTypeId", obj.MonthlyBudgetTypeId, dbType: DbType.Int64);
                    param.Add("@MonthlyBudgetPercentage", obj.MonthlyBudgetPercentage, dbType: DbType.Decimal);
                    param.Add("@NoOfCases", obj.NoOfCases, dbType: DbType.Int64);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@PersonName", obj.PersonName, dbType: DbType.String);
                    param.Add("@BudgetValue", obj.BudgetValue, dbType: DbType.Decimal);
                    param.Add("@AchieveValue", obj.AchieveValue, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateBudgetMonthly(BudgetMonthlyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [BudgetMonthly]
SET 
    MonthlyBudgetPercentage = @MonthlyBudgetPercentage
   ,NoOfCases = @NoOfCases
   ,ClientId = @ClientId
   ,PersonName = @PersonName
   ,BudgetValue = @BudgetValue
   ,AchieveValue = @AchieveValue
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE BudgetMonthlyId = @BudgetMonthlyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetMonthlyId", obj.BudgetMonthlyId, dbType: DbType.Int64);
                    //param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    //param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    //param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    //param.Add("@MonthlyBudgetTypeId", obj.MonthlyBudgetTypeId, dbType: DbType.Int64);
                    param.Add("@MonthlyBudgetPercentage", obj.MonthlyBudgetPercentage, dbType: DbType.Decimal);
                    param.Add("@NoOfCases", obj.NoOfCases, dbType: DbType.Int64);
                    param.Add("@ClientId", obj.ClientId, dbType: DbType.Int64);
                    param.Add("@PersonName", obj.PersonName, dbType: DbType.String);
                    param.Add("@BudgetValue", obj.BudgetValue, dbType: DbType.Decimal);
                    param.Add("@AchieveValue", obj.AchieveValue, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<BudgetMonthlyEnt> GetBudgetMonthlyByFilter(BudgetMonthlyFilter obj, long pageSize, long pageNumber)
        {
            List<BudgetMonthlyEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,MonthlyBudgetTypeMaster.Name [MonthlyBudgetTypeDesc]
                            ,u.Username
                            ,c.Name [ClientName]
                        FROM BudgetMonthly a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId
                            LEFT JOIN MasterDatas MonthlyBudgetTypeMaster WITH (NOLOCK)
                            ON MonthlyBudgetTypeMaster.MasterId = 19
                                AND a.MonthlyBudgetTypeId = MonthlyBudgetTypeMaster.MasterDataId
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId= u.UserId
                            LEFT JOIN Clients c WITH (NOLOCK)
                         	    ON a.ClientId = c.ClientId
                            WHERE (@BudgetMonthlyId IS NULL OR a.BudgetMonthlyId = @BudgetMonthlyId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND (@BudgetYear IS NULL OR a.BudgetYear = @BudgetYear)                           
                            AND (@BudgetMonth IS NULL OR a.BudgetMonth = @BudgetMonth)                           
                            AND (@MonthlyBudgetTypeId IS NULL OR a.MonthlyBudgetTypeId = @MonthlyBudgetTypeId)                           
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.BudgetMonthlyId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetMonthlyId", obj.BudgetMonthlyId, dbType: DbType.Int64);
                    param.Add("@BudgetYear", obj.BudgetYear, dbType: DbType.Int64);
                    param.Add("@BudgetMonth", obj.BudgetMonth, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@MonthlyBudgetTypeId", obj.MonthlyBudgetTypeId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<BudgetMonthlyEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateStatusBudgetMonthly(long budgetMonthlyId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [BudgetMonthly]
SET 
    StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE BudgetMonthlyId = @BudgetMonthlyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetMonthlyId", budgetMonthlyId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region budget strategy
        internal long AddBudgetStrategy(BudgetStrategyEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[BudgetStrategy] ([UserId]
, [BudgetStrategyYear]
, [NoOfCasesForTheYear]
, [GoalACEValue]
, [HighEndPercentage]
, [LowEndPercentage]
, [HighEndACEValue]
, [LowEndACEValue]
, [HighEndAveragePremium]
, [LowEndAveragePremium]
, [HighEndNoOfCases]
, [LowEndNoOfCases]
, [Remarks]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@UserId, @BudgetStrategyYear, @NoOfCasesForTheYear, @GoalACEValue, @HighEndPercentage, @LowEndPercentage, @HighEndACEValue, @LowEndACEValue, @HighEndAveragePremium, @LowEndAveragePremium, @HighEndNoOfCases, @LowEndNoOfCases, @Remarks, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    //param.Add("@BudgetStrategyId", obj.BudgetStrategyId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@BudgetStrategyYear", obj.BudgetStrategyYear, dbType: DbType.String);
                    param.Add("@NoOfCasesForTheYear", obj.NoOfCasesForTheYear, dbType: DbType.Int64);
                    param.Add("@GoalACEValue", obj.GoalACEValue, dbType: DbType.Decimal);
                    param.Add("@HighEndPercentage", obj.HighEndPercentage, dbType: DbType.Decimal);
                    param.Add("@LowEndPercentage", obj.LowEndPercentage, dbType: DbType.Decimal);
                    param.Add("@HighEndACEValue", obj.HighEndACEValue, dbType: DbType.Decimal);
                    param.Add("@LowEndACEValue", obj.LowEndACEValue, dbType: DbType.Decimal);
                    param.Add("@HighEndAveragePremium", obj.HighEndAveragePremium, dbType: DbType.Decimal);
                    param.Add("@LowEndAveragePremium", obj.LowEndAveragePremium, dbType: DbType.Decimal);
                    param.Add("@HighEndNoOfCases", obj.HighEndNoOfCases, dbType: DbType.Int64);
                    param.Add("@LowEndNoOfCases", obj.LowEndNoOfCases, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateBudgetStrategy(BudgetStrategyEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [BudgetStrategy]
SET GoalACEValue = @GoalACEValue
   ,NoOfCasesForTheYear = @NoOfCasesForTheYear
   ,HighEndPercentage = @HighEndPercentage
   ,LowEndPercentage = @LowEndPercentage
   ,HighEndACEValue = @HighEndACEValue
   ,LowEndACEValue = @LowEndACEValue
   ,HighEndAveragePremium = @HighEndAveragePremium
   ,LowEndAveragePremium = @LowEndAveragePremium
   ,HighEndNoOfCases = @HighEndNoOfCases
   ,LowEndNoOfCases = @LowEndNoOfCases
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE BudgetStrategyId = @BudgetStrategyId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetStrategyId", obj.BudgetStrategyId, dbType: DbType.Int64);
                    param.Add("@NoOfCasesForTheYear", obj.NoOfCasesForTheYear, dbType: DbType.Int64);
                    param.Add("@GoalACEValue", obj.GoalACEValue, dbType: DbType.Decimal);
                    param.Add("@HighEndPercentage", obj.HighEndPercentage, dbType: DbType.Decimal);
                    param.Add("@LowEndPercentage", obj.LowEndPercentage, dbType: DbType.Decimal);
                    param.Add("@HighEndACEValue", obj.HighEndACEValue, dbType: DbType.Decimal);
                    param.Add("@LowEndACEValue", obj.LowEndACEValue, dbType: DbType.Decimal);
                    param.Add("@HighEndAveragePremium", obj.HighEndAveragePremium, dbType: DbType.Decimal);
                    param.Add("@LowEndAveragePremium", obj.LowEndAveragePremium, dbType: DbType.Decimal);
                    param.Add("@HighEndNoOfCases", obj.HighEndNoOfCases, dbType: DbType.Int64);
                    param.Add("@LowEndNoOfCases", obj.LowEndNoOfCases, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<BudgetStrategyEnt> GetBudgetStrategyByFilter(BudgetStrategyFilter obj, long pageSize, long pageNumber)
        {
            List<BudgetStrategyEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,u.Username
                        FROM BudgetStrategy a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId= u.UserId
                            WHERE (@BudgetStrategyId IS NULL OR a.BudgetStrategyId = @BudgetStrategyId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@BudgetStrategyYear IS NULL OR a.BudgetStrategyYear = @BudgetStrategyYear)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.BudgetStrategyId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@BudgetStrategyId", obj.BudgetStrategyId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@BudgetStrategyYear", obj.BudgetStrategyYear, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<BudgetStrategyEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Agent Recruit
        internal long AddAgentRecruit(AgentRecruitEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentRecruit] ([Name]
, [ContactNo]
, [StatusId]
, [EducationBgId]
, [AgeId]
, [EmailAdd]
, [AnnualIncomeId]
, [OccupationId]
, [MaritalId]
, [TypeId]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@Name, @ContactNo, @StatusId, @EducationBgId, @AgeId,@EmailAdd, @AnnualIncomeId, @OccupationId, @MaritalId
, @TypeId, @Remarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Name", obj.Name, dbType: DbType.String);
                    param.Add("@ContactNo", obj.ContactNo, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@EducationBgId", obj.EducationBgId, dbType: DbType.Int64);
                    param.Add("@AgeId", obj.AgeId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@EmailAdd", obj.EmailAdd, dbType: DbType.String);
                    param.Add("@AnnualIncomeId", obj.AnnualIncomeId, dbType: DbType.Int64);
                    param.Add("@OccupationId", obj.OccupationId, dbType: DbType.Int64);
                    param.Add("@MaritalId", obj.MaritalId, dbType: DbType.Int64);
                    param.Add("@TypeId", obj.TypeId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAgentRecruit(AgentRecruitEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentRecruit]
SET Name = @Name
   ,ContactNo = @ContactNo
   ,EducationBgId = @EducationBgId
   ,AgeId = @AgeId
   ,EmailAdd = @EmailAdd
   ,AnnualIncomeId = @AnnualIncomeId
   ,OccupationId = @OccupationId
  ,MaritalId = @MaritalId
   ,TypeId = @TypeId
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentRecruitId = @AgentRecruitId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitId", obj.AgentRecruitId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.String);
                    param.Add("@ContactNo", obj.ContactNo, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@EducationBgId", obj.EducationBgId, dbType: DbType.Int64);
                    param.Add("@AgeId", obj.AgeId, dbType: DbType.Int64);
                    param.Add("@EmailAdd", obj.EmailAdd, dbType: DbType.String);
                    param.Add("@AnnualIncomeId", obj.AnnualIncomeId, dbType: DbType.Int64);
                    param.Add("@OccupationId", obj.OccupationId, dbType: DbType.Int64);
                    param.Add("@MaritalId", obj.MaritalId, dbType: DbType.Int64);
                    param.Add("@TypeId", obj.TypeId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateAgentRecruitStatus(AgentRecruitEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentRecruit]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentRecruitId = @AgentRecruitId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitId", obj.AgentRecruitId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AgentRecruitEnt> GetAgentRecruitByFilter(AgentRecruitFilter obj, long pageSize, long pageNumber)
        {
            List<AgentRecruitEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,AgeMaster.Name [AgeDesc]
                            ,EducationBgMaster.Name [EducationBgDesc]
                            ,AnnualIncomeMaster.Name [AnnualIncomeDesc]
                            ,OccupationMaster.Name [OccupationDesc]
                            ,MaritalMaster.Name [MaritalDesc]
                            ,TypeMaster.Name [TypeDesc]
                        FROM AgentRecruit a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId
                            LEFT JOIN MasterDatas AgeMaster WITH (NOLOCK)
                        	ON AgeMaster.MasterId = 12
                        		AND a.AgeId = AgeMaster.MasterDataId
                        	LEFT JOIN MasterDatas EducationBgMaster WITH (NOLOCK)
                        	ON EducationBgMaster.MasterId = 20
                        		AND a.EducationBgId = EducationBgMaster.MasterDataId
                            LEFT JOIN MasterDatas AnnualIncomeMaster WITH (NOLOCK)
                            ON AnnualIncomeMaster.MasterId = 3
                                AND a.AnnualIncomeId = AnnualIncomeMaster.MasterDataId
                            LEFT JOIN MasterDatas OccupationMaster WITH (NOLOCK)
                            ON OccupationMaster.MasterId = 4
                                AND a.OccupationId = OccupationMaster.MasterDataId
                            LEFT JOIN MasterDatas MaritalMaster WITH (NOLOCK)
                            ON MaritalMaster.MasterId = 5
                                AND a.MaritalId = MaritalMaster.MasterDataId
                            LEFT JOIN MasterDatas TypeMaster WITH (NOLOCK)
                            ON TypeMaster.MasterId = 31
                                AND a.TypeId = TypeMaster.MasterDataId
                            WHERE (@AgentRecruitId IS NULL OR a.AgentRecruitId = @AgentRecruitId)
                            AND (@Name IS NULL OR a.Name LIKE '%' + @Name +'%')
                            AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                            AND (CAST(@CreatedDateFrom AS DATE) IS NULL OR CAST(a.CreatedDate AS DATE) >= @CreatedDateFrom)
                            AND (CAST(@CreatedDateTo AS DATE) IS NULL OR CAST(a.CreatedDate AS DATE) <= @CreatedDateTo)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.AgentRecruitId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitId", obj.AgentRecruitId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@CreatedDateFrom", obj.CreatedDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", obj.CreatedDateTo, dbType: DbType.DateTime);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentRecruitEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<ClientSummaryEnt> GetAgentReruitSummaryByFilter(DateTime? createdDateFrom, DateTime? createdDateTo, string createdBy)
        {
            List<ClientSummaryEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
WITH  AnnualIncomeMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		1 Sort
	   ,SortOrder
	   ,COUNT(AnnualIncomeId) TotalSource
	   ,a.AnnualIncomeId
	   ,AnnualIncomeMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas AnnualIncomeMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.AnnualIncomeId = AnnualIncomeMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE AnnualIncomeMaster.MasterId = 3 AND AnnualIncomeMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.AnnualIncomeId
			,AnnualIncomeMaster.Name
			,MasterId),
AgeMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		2 Sort
	   ,SortOrder
	   ,COUNT(AgeId) TotalSource
	   ,a.AgeId
	   ,AgeMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas AgeMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.AgeId = AgeMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE AgeMaster.MasterId = 12 AND AgeMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.AgeId
			,AgeMaster.Name
			,MasterId),
OccupationMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		3 Sort
	   ,SortOrder
	   ,COUNT(OccupationId) TotalSource
	   ,a.OccupationId
	   ,OccupationMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas OccupationMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.OccupationId = OccupationMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE OccupationMaster.MasterId = 4  AND OccupationMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.OccupationId
			,OccupationMaster.Name
			,MasterId),
MaritalMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		4 Sort
	   ,SortOrder
	   ,COUNT(MaritalId) TotalSource
	   ,a.MaritalId
	   ,MaritalMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas MaritalMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.MaritalId = MaritalMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE MaritalMaster.MasterId = 5  AND MaritalMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.MaritalId
			,MaritalMaster.Name
			,MasterId),
    
EducationBackgroundMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		10 Sort
	   ,SortOrder
	   ,COUNT(EducationBgId) TotalSource
	   ,a.EducationBgId
	   ,EducationBackgroundMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas EducationBackgroundMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.EducationBgId = EducationBackgroundMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE EducationBackgroundMaster.MasterId = 20  AND EducationBackgroundMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.EducationBgId
			,EducationBackgroundMaster.Name
			,MasterId),

TypeMaster (Sort, SortOrder, Total, Id, Name, MasterId)
AS
(SELECT
		10 Sort
	   ,SortOrder
	   ,COUNT(TypeId) TotalSource
	   ,a.TypeId
	   ,TypeMaster.Name [SourceDesc]
	   ,MasterId
	FROM MasterDatas TypeMaster WITH (NOLOCK)
	LEFT JOIN [AgentRecruit] a WITH (NOLOCK)
		ON a.TypeId = TypeMaster.MasterDataId
		AND (@CreatedDateFrom IS NULL OR a.CreatedDate >= @CreatedDateFrom)
	    AND (@CreatedDateTo IS NULL OR a.CreatedDate <= @CreatedDateTo)
	AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)

	WHERE TypeMaster.MasterId = 31  AND TypeMaster.StatusId = 1 
	GROUP BY SortOrder
			,a.TypeId
			,TypeMaster.Name
			,MasterId)
SELECT
	M.Name MasterName
   ,T.Name MasterDataName
   ,T.Total INTO #TempSummary
FROM (SELECT
		*
	 
	FROM AnnualIncomeMaster
	UNION ALL
	SELECT
		*
	FROM AgeMaster

	UNION ALL
	SELECT
		*
	FROM OccupationMaster
	UNION ALL
	SELECT
		*
	FROM MaritalMaster

	UNION ALL
	SELECT
		*
	FROM EducationBackgroundMaster
	UNION ALL
	SELECT
		*
	FROM TypeMaster
 ) T
LEFT JOIN Masters M
	ON M.MasterId = T.MasterId
ORDER BY Sort, SortOrder;

SELECT 
MasterName, COUNT(MasterName) MasterNameCount, SUM(Total) SumTotal INTO #TempSummaryGroup
FROM #TempSummary 
GROUP BY MasterName;

SELECT a.*,CASE WHEN B.SumTotal=0 THEN 0 ELSE  (a.Total * 1 * 100) / CAST(b.SumTotal AS FLOAT) END AS  [Percentage]
FROM #TempSummary a LEFT JOIN #TempSummaryGroup b ON a.MasterName = b.MasterName;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@CreatedDateFrom", createdDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", createdDateTo, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", createdBy, dbType: DbType.AnsiString);

                    oResult = conn.Query<ClientSummaryEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Agent Recruit Track
        internal long AddAgentRecruitTrack(AgentRecruitTrackEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentRecruitTrack] ([AgentRecruitId]
, [TrackRemarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@AgentRecruitId, @TrackRemarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitId", obj.AgentRecruitId, dbType: DbType.Int64);
                    param.Add("@TrackRemarks", obj.TrackRemarks, dbType: DbType.String);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAgentRecruitTrack(AgentRecruitTrackEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentRecruitTrack]
SET TrackRemarks = @TrackRemarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentRecruitTrackId = @AgentRecruitTrackId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitTrackId", obj.AgentRecruitTrackId, dbType: DbType.Int64);
                    param.Add("@TrackRemarks", obj.TrackRemarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AgentRecruitTrackEnt> GetAgentRecruitTrackByFilter(AgentRecruitTrackFilter obj, long pageSize, long pageNumber)
        {
            List<AgentRecruitTrackEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                         FROM AgentRecruitTrack a WITH (NOLOCK)
                            WHERE (@AgentRecruitTrackId IS NULL OR a.AgentRecruitTrackId = @AgentRecruitTrackId)
                            AND (@AgentRecruitId IS NULL OR a.AgentRecruitId = @AgentRecruitId)
                            ORDER BY a.AgentRecruitTrackId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentRecruitTrackId", obj.AgentRecruitTrackId, dbType: DbType.Int64);
                    param.Add("@AgentRecruitId", obj.AgentRecruitId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentRecruitTrackEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Agent Activity
        internal long AddAgentActivity(AgentActivityEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentActivities] ([AgentId]
, [ActivityPointId]
, [Points]
, [StatusId]
, [ActivityStartDate]
, [ActivityEndDate]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@AgentId, @ActivityPointId, @Points, @StatusId, @ActivityStartDate, @ActivityEndDate, @Remarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentId", obj.AgentId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@Points", obj.Points, dbType: DbType.Int32);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    //conn.Execute(query, param, commandType: CommandType.Text);
                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAgentActivity(AgentActivityEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentActivities]
SET ActivityPointId = @ActivityPointId   
   ,ActivityStartDate = @ActivityStartDate
   ,ActivityEndDate = @ActivityEndDate
   ,Remarks = @Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentActivityId = @AgentActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentActivityId", obj.AgentActivityId, dbType: DbType.Int64);
                    param.Add("@ActivityPointId", obj.ActivityPointId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AgentActivityEnt> GetAgentActivityByFilter(AgentActivityFilter obj, long pageSize, long pageNumber)
        {
            List<AgentActivityEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	a.*
   ,StatusMaster.Name [StatusDesc]  
   ,ap.Name [ActivityPointsDesc]
   ,ap.Points PointSetting
   ,ap.ColorCode
   ,u.Name [AgentName]   
   ,u.StatusId [AgentStatusId]
   ,AgentStatusMaster.Name [AgentStatusDesc]
FROM [AgentActivities] a WITH (NOLOCK)
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	ON StatusMaster.MasterId = 1
		AND a.StatusId = StatusMaster.MasterDataId
LEFT JOIN ActivityPoints ap
	ON ap.ActivityPointId = a.ActivityPointId
LEFT JOIN AgentRecruit u WITH (NOLOCK)
	ON u.AgentRecruitId = a.AgentId
LEFT JOIN MasterDatas AgentStatusMaster WITH (NOLOCK)
	ON AgentStatusMaster.MasterId = 1
		AND u.StatusId = AgentStatusMaster.MasterDataId
WHERE (@AgentActivityId IS NULL OR a.AgentActivityId = @AgentActivityId)
AND (@AgentId IS NULL OR a.AgentId = @AgentId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)
AND (@ActivityStartDate IS NULL OR a.ActivityStartDate >= @ActivityStartDate)
AND (@ActivityEndDate IS NULL OR a.ActivityEndDate <= @ActivityEndDate)
AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
AND a.StatusId NOT IN (2)
ORDER BY u.Name
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentActivityId", obj.AgentActivityId, dbType: DbType.Int64);
                    param.Add("@AgentId", obj.AgentId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@ActivityStartDate", obj.ActivityStartDate, dbType: DbType.DateTime);
                    param.Add("@ActivityEndDate", obj.ActivityEndDate, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentActivityEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Agent Lead
        internal long AddAgentLead(AgentLeadEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentLeads] ( [AgentId]
, [AgentActivityId]
, [Name]
, [HP]
, [StatusId]
, [Remarks]
, [CreatedBy]
, [CreatedDate])
	VALUES (@AgentId, @AgentActivityId, @Name, @HP, @StatusId, @Remarks, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentId", obj.AgentId, dbType: DbType.Int64);
                    param.Add("@AgentActivityId", obj.AgentActivityId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@HP", obj.HP, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    //conn.Execute(query, param, commandType: CommandType.Text);
                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAgentLead(AgentLeadEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentLeads]
SET AgentId = @AgentId
   ,Name = @Name
   ,HP = @HP
   ,Remarks=@Remarks
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentLeadId = @AgentLeadId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentLeadId", obj.AgentLeadId, dbType: DbType.Int64);
                    param.Add("@AgentId", obj.AgentId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@HP", obj.HP, dbType: DbType.AnsiString);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.AnsiString);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateAgentLeadStatus(long agentLeadId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentLeads]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE AgentLeadId = @AgentLeadId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentLeadId", agentLeadId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AgentLeadEnt> GetAgentLeadByFilter(AgentLeadFilter obj, long pageSize, long pageNumber)
        {
            List<AgentLeadEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,ar.Name [AgentRecruitName]
                        FROM [AgentLeads] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN AgentRecruit ar WITH (NOLOCK)
                            ON a.AgentId = ar.AgentRecruitId
                        WHERE (@AgentActivityId IS NULL OR a.AgentActivityId = @AgentActivityId)
                        AND (@AgentLeadId IS NULL OR a.AgentLeadId = @AgentLeadId)
                        AND (@AgentId IS NULL OR a.AgentId = @AgentId)
                        AND (@Name IS NULL OR a.Name LIKE '%' + @Name +'%')
                        AND (@AgentRecruitName IS NULL OR ar.Name LIKE '%' + @AgentRecruitName +'%')
                        AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.Name
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentActivityId", obj.AgentActivityId, dbType: DbType.Int64);
                    param.Add("@AgentLeadId", obj.AgentLeadId, dbType: DbType.Int64);
                    param.Add("@AgentId", obj.AgentId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@AgentRecruitName", obj.AgentRecruitName, dbType: DbType.AnsiString);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentLeadEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateAgentActivityPoint(long agentActivityId, int points)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentActivities]
SET Points = @Points
WHERE AgentActivityId = @AgentActivityId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentActivityId", agentActivityId, dbType: DbType.Int64);
                    param.Add("@Points", points, dbType: DbType.Int32);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region Agent Simulator
        internal long AddAgentSimulator(AgentSimulatorEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentSimulator] (
UserId
,AgentSimulatorTypeId
,AgentSimulatorYear
,AgentSimulatorMonth
,GrowthPercentage
,Manpower_YTDRecruit1
,Manpower_YTDRecruit2
,Manpower_YTDRecruit3
,Manpower_YTDRecruit4
,Manpower_YTDRecruit5
,Manpower_YTDRecruit6
,Manpower_YTDRecruit7
,Manpower_YTDRecruit8
,Manpower_YTDRecruit9
,Manpower_YTDRecruit10
,Manpower_YTDRecruit11
,Manpower_YTDRecruit12
,ActiveAgent_YTDRecruit1
,ActiveAgent_YTDRecruit2
,ActiveAgent_YTDRecruit3
,ActiveAgent_YTDRecruit4
,ActiveAgent_YTDRecruit5
,ActiveAgent_YTDRecruit6
,ActiveAgent_YTDRecruit7
,ActiveAgent_YTDRecruit8
,ActiveAgent_YTDRecruit9
,ActiveAgent_YTDRecruit10
,ActiveAgent_YTDRecruit11
,ActiveAgent_YTDRecruit12
,ActiveAgent_TotalCases1
,ActiveAgent_TotalCases2
,ActiveAgent_TotalCases3
,ActiveAgent_TotalCases4
,ActiveAgent_TotalCases5
,ActiveAgent_TotalCases6
,ActiveAgent_TotalCases7
,ActiveAgent_TotalCases8
,ActiveAgent_TotalCases9
,ActiveAgent_TotalCases10
,ActiveAgent_TotalCases11
,ActiveAgent_TotalCases12
,ACE_TotalCases1
,ACE_TotalCases2
,ACE_TotalCases3
,ACE_TotalCases4
,ACE_TotalCases5
,ACE_TotalCases6
,ACE_TotalCases7
,ACE_TotalCases8
,ACE_TotalCases9
,ACE_TotalCases10
,ACE_TotalCases11
,ACE_TotalCases12
,Remarks
,StatusId
,CreatedBy
,CreatedDate
)
	VALUES (
 @UserId
,@AgentSimulatorTypeId
,@AgentSimulatorYear
,@AgentSimulatorMonth
,@GrowthPercentage
,@Manpower_YTDRecruit1
,@Manpower_YTDRecruit2
,@Manpower_YTDRecruit3
,@Manpower_YTDRecruit4
,@Manpower_YTDRecruit5
,@Manpower_YTDRecruit6
,@Manpower_YTDRecruit7
,@Manpower_YTDRecruit8
,@Manpower_YTDRecruit9
,@Manpower_YTDRecruit10
,@Manpower_YTDRecruit11
,@Manpower_YTDRecruit12
,@ActiveAgent_YTDRecruit1
,@ActiveAgent_YTDRecruit2
,@ActiveAgent_YTDRecruit3
,@ActiveAgent_YTDRecruit4
,@ActiveAgent_YTDRecruit5
,@ActiveAgent_YTDRecruit6
,@ActiveAgent_YTDRecruit7
,@ActiveAgent_YTDRecruit8
,@ActiveAgent_YTDRecruit9
,@ActiveAgent_YTDRecruit10
,@ActiveAgent_YTDRecruit11
,@ActiveAgent_YTDRecruit12
,@ActiveAgent_TotalCases1
,@ActiveAgent_TotalCases2
,@ActiveAgent_TotalCases3
,@ActiveAgent_TotalCases4
,@ActiveAgent_TotalCases5
,@ActiveAgent_TotalCases6
,@ActiveAgent_TotalCases7
,@ActiveAgent_TotalCases8
,@ActiveAgent_TotalCases9
,@ActiveAgent_TotalCases10
,@ActiveAgent_TotalCases11
,@ActiveAgent_TotalCases12
,@ACE_TotalCases1
,@ACE_TotalCases2
,@ACE_TotalCases3
,@ACE_TotalCases4
,@ACE_TotalCases5
,@ACE_TotalCases6
,@ACE_TotalCases7
,@ACE_TotalCases8
,@ACE_TotalCases9
,@ACE_TotalCases10
,@ACE_TotalCases11
,@ACE_TotalCases12
,@Remarks
,@StatusId
,@CreatedBy
,GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    //param.Add("@AgentSimulatorId", obj.AgentSimulatorId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorTypeId", obj.AgentSimulatorTypeId, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorYear", obj.AgentSimulatorYear, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorMonth", obj.AgentSimulatorMonth, dbType: DbType.Int64);
                    param.Add("@GrowthPercentage", obj.GrowthPercentage, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit1", obj.Manpower_YTDRecruit1, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit2", obj.Manpower_YTDRecruit2, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit3", obj.Manpower_YTDRecruit3, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit4", obj.Manpower_YTDRecruit4, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit5", obj.Manpower_YTDRecruit5, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit6", obj.Manpower_YTDRecruit6, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit7", obj.Manpower_YTDRecruit7, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit8", obj.Manpower_YTDRecruit8, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit9", obj.Manpower_YTDRecruit9, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit10", obj.Manpower_YTDRecruit10, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit11", obj.Manpower_YTDRecruit11, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit12", obj.Manpower_YTDRecruit12, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit1", obj.ActiveAgent_YTDRecruit1, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit2", obj.ActiveAgent_YTDRecruit2, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit3", obj.ActiveAgent_YTDRecruit3, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit4", obj.ActiveAgent_YTDRecruit4, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit5", obj.ActiveAgent_YTDRecruit5, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit6", obj.ActiveAgent_YTDRecruit6, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit7", obj.ActiveAgent_YTDRecruit7, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit8", obj.ActiveAgent_YTDRecruit8, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit9", obj.ActiveAgent_YTDRecruit9, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit10", obj.ActiveAgent_YTDRecruit10, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit11", obj.ActiveAgent_YTDRecruit11, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit12", obj.ActiveAgent_YTDRecruit12, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases1", obj.ActiveAgent_TotalCases1, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases2", obj.ActiveAgent_TotalCases2, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases3", obj.ActiveAgent_TotalCases3, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases4", obj.ActiveAgent_TotalCases4, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases5", obj.ActiveAgent_TotalCases5, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases6", obj.ActiveAgent_TotalCases6, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases7", obj.ActiveAgent_TotalCases7, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases8", obj.ActiveAgent_TotalCases8, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases9", obj.ActiveAgent_TotalCases9, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases10", obj.ActiveAgent_TotalCases10, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases11", obj.ActiveAgent_TotalCases11, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases12", obj.ActiveAgent_TotalCases12, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases1", obj.ACE_TotalCases1, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases2", obj.ACE_TotalCases2, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases3", obj.ACE_TotalCases3, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases4", obj.ACE_TotalCases4, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases5", obj.ACE_TotalCases5, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases6", obj.ACE_TotalCases6, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases7", obj.ACE_TotalCases7, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases8", obj.ACE_TotalCases8, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases9", obj.ACE_TotalCases9, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases10", obj.ACE_TotalCases10, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases11", obj.ACE_TotalCases11, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases12", obj.ACE_TotalCases12, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);
                    //param.Add("@CreatedDate", obj.CreatedDate, dbType: DbType.DateTime);
                    //param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.String);
                    //param.Add("@UpdatedDate", obj.UpdatedDate, dbType: DbType.DateTime);

                    //conn.Execute(query, param, commandType: CommandType.Text);
                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }
        internal bool UpdateAgentSimulator(AgentSimulatorEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentSimulator]
SET 
    GrowthPercentage = @GrowthPercentage,
    Manpower_YTDRecruit1 = @Manpower_YTDRecruit1,
    Manpower_YTDRecruit2 = @Manpower_YTDRecruit2,
    Manpower_YTDRecruit3 = @Manpower_YTDRecruit3,
    Manpower_YTDRecruit4 = @Manpower_YTDRecruit4,
    Manpower_YTDRecruit5 = @Manpower_YTDRecruit5,
    Manpower_YTDRecruit6 = @Manpower_YTDRecruit6,
    Manpower_YTDRecruit7 = @Manpower_YTDRecruit7,
    Manpower_YTDRecruit8 = @Manpower_YTDRecruit8,
    Manpower_YTDRecruit9 = @Manpower_YTDRecruit9,
    Manpower_YTDRecruit10 = @Manpower_YTDRecruit10,
    Manpower_YTDRecruit11 = @Manpower_YTDRecruit11,
    Manpower_YTDRecruit12 = @Manpower_YTDRecruit12,
    ActiveAgent_YTDRecruit1 = @ActiveAgent_YTDRecruit1,
    ActiveAgent_YTDRecruit2 = @ActiveAgent_YTDRecruit2,
    ActiveAgent_YTDRecruit3 = @ActiveAgent_YTDRecruit3,
    ActiveAgent_YTDRecruit4 = @ActiveAgent_YTDRecruit4,
    ActiveAgent_YTDRecruit5 = @ActiveAgent_YTDRecruit5,
    ActiveAgent_YTDRecruit6 = @ActiveAgent_YTDRecruit6,
    ActiveAgent_YTDRecruit7 = @ActiveAgent_YTDRecruit7,
    ActiveAgent_YTDRecruit8 = @ActiveAgent_YTDRecruit8,
    ActiveAgent_YTDRecruit9 = @ActiveAgent_YTDRecruit9,
    ActiveAgent_YTDRecruit10 = @ActiveAgent_YTDRecruit10,
    ActiveAgent_YTDRecruit11 = @ActiveAgent_YTDRecruit11,
    ActiveAgent_YTDRecruit12 = @ActiveAgent_YTDRecruit12,
    ActiveAgent_TotalCases1 = @ActiveAgent_TotalCases1,
    ActiveAgent_TotalCases2 = @ActiveAgent_TotalCases2,
    ActiveAgent_TotalCases3 = @ActiveAgent_TotalCases3,
    ActiveAgent_TotalCases4 = @ActiveAgent_TotalCases4,
    ActiveAgent_TotalCases5 = @ActiveAgent_TotalCases5,
    ActiveAgent_TotalCases6 = @ActiveAgent_TotalCases6,
    ActiveAgent_TotalCases7 = @ActiveAgent_TotalCases7,
    ActiveAgent_TotalCases8 = @ActiveAgent_TotalCases8,
    ActiveAgent_TotalCases9 = @ActiveAgent_TotalCases9,
    ActiveAgent_TotalCases10 = @ActiveAgent_TotalCases10,
    ActiveAgent_TotalCases11 = @ActiveAgent_TotalCases11,
    ActiveAgent_TotalCases12 = @ActiveAgent_TotalCases12,
    ACE_TotalCases1 = @ACE_TotalCases1,
    ACE_TotalCases2 = @ACE_TotalCases2,
    ACE_TotalCases3 = @ACE_TotalCases3,
    ACE_TotalCases4 = @ACE_TotalCases4,
    ACE_TotalCases5 = @ACE_TotalCases5,
    ACE_TotalCases6 = @ACE_TotalCases6,
    ACE_TotalCases7 = @ACE_TotalCases7,
    ACE_TotalCases8 = @ACE_TotalCases8,
    ACE_TotalCases9 = @ACE_TotalCases9,
    ACE_TotalCases10 = @ACE_TotalCases10,
    ACE_TotalCases11 = @ACE_TotalCases11,
    ACE_TotalCases12 = @ACE_TotalCases12,
    Remarks = @Remarks,
    UpdatedBy = @UpdatedBy,
    UpdatedDate =GETDATE()
WHERE AgentSimulatorId = @AgentSimulatorId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentSimulatorId", obj.AgentSimulatorId, dbType: DbType.Int64);
                    param.Add("@GrowthPercentage", obj.GrowthPercentage, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit1", obj.Manpower_YTDRecruit1, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit2", obj.Manpower_YTDRecruit2, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit3", obj.Manpower_YTDRecruit3, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit4", obj.Manpower_YTDRecruit4, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit5", obj.Manpower_YTDRecruit5, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit6", obj.Manpower_YTDRecruit6, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit7", obj.Manpower_YTDRecruit7, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit8", obj.Manpower_YTDRecruit8, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit9", obj.Manpower_YTDRecruit9, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit10", obj.Manpower_YTDRecruit10, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit11", obj.Manpower_YTDRecruit11, dbType: DbType.Decimal);
                    param.Add("@Manpower_YTDRecruit12", obj.Manpower_YTDRecruit12, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit1", obj.ActiveAgent_YTDRecruit1, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit2", obj.ActiveAgent_YTDRecruit2, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit3", obj.ActiveAgent_YTDRecruit3, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit4", obj.ActiveAgent_YTDRecruit4, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit5", obj.ActiveAgent_YTDRecruit5, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit6", obj.ActiveAgent_YTDRecruit6, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit7", obj.ActiveAgent_YTDRecruit7, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit8", obj.ActiveAgent_YTDRecruit8, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit9", obj.ActiveAgent_YTDRecruit9, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit10", obj.ActiveAgent_YTDRecruit10, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit11", obj.ActiveAgent_YTDRecruit11, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_YTDRecruit12", obj.ActiveAgent_YTDRecruit12, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases1", obj.ActiveAgent_TotalCases1, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases2", obj.ActiveAgent_TotalCases2, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases3", obj.ActiveAgent_TotalCases3, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases4", obj.ActiveAgent_TotalCases4, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases5", obj.ActiveAgent_TotalCases5, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases6", obj.ActiveAgent_TotalCases6, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases7", obj.ActiveAgent_TotalCases7, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases8", obj.ActiveAgent_TotalCases8, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases9", obj.ActiveAgent_TotalCases9, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases10", obj.ActiveAgent_TotalCases10, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases11", obj.ActiveAgent_TotalCases11, dbType: DbType.Decimal);
                    param.Add("@ActiveAgent_TotalCases12", obj.ActiveAgent_TotalCases12, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases1", obj.ACE_TotalCases1, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases2", obj.ACE_TotalCases2, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases3", obj.ACE_TotalCases3, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases4", obj.ACE_TotalCases4, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases5", obj.ACE_TotalCases5, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases6", obj.ACE_TotalCases6, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases7", obj.ACE_TotalCases7, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases8", obj.ACE_TotalCases8, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases9", obj.ACE_TotalCases9, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases10", obj.ACE_TotalCases10, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases11", obj.ACE_TotalCases11, dbType: DbType.Decimal);
                    param.Add("@ACE_TotalCases12", obj.ACE_TotalCases12, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.String);
                    param.Add("@UpdatedDate", obj.UpdatedDate, dbType: DbType.DateTime);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        internal List<AgentSimulatorEnt> GetAgentSimulatorByFilter(AgentSimulatorFilter obj, long pageSize, long pageNumber)
        {
            List<AgentSimulatorEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                             SELECT a.*
                                ,StatusMaster.Name [StatusDesc]                            
                                ,AgentSimulatorTypeMaster.Name [AgentSimulatorTypeDesc]                            
                                ,u.Username
                            FROM AgentSimulator a WITH (NOLOCK)
                            LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                            ON StatusMaster.MasterId = 1
                                AND a.StatusId = StatusMaster.MasterDataId
                            LEFT JOIN MasterDatas AgentSimulatorTypeMaster WITH (NOLOCK)
                        	ON AgentSimulatorTypeMaster.MasterId = 21
                        		AND a.AgentSimulatorTypeId = AgentSimulatorTypeMaster.MasterDataId
                            LEFT JOIN Users u WITH (NOLOCK)
                                ON a.UserId= u.UserId
                            WHERE (@AgentSimulatorId IS NULL OR a.AgentSimulatorId = @AgentSimulatorId)
                            AND (@UserId IS NULL OR a.UserId = @UserId)
                            AND (@AgentSimulatorTypeId IS NULL OR a.AgentSimulatorTypeId = @AgentSimulatorTypeId)
                            AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
                            AND (@AgentSimulatorYear IS NULL OR a.AgentSimulatorYear = @AgentSimulatorYear)                           
                            AND (@AgentSimulatorMonth IS NULL OR a.AgentSimulatorMonth = @AgentSimulatorMonth)                                                       
                            AND a.StatusId NOT IN (2)
                            ORDER BY a.AgentSimulatorId
                            OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                            FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@AgentSimulatorId", obj.AgentSimulatorId, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorTypeId", obj.AgentSimulatorTypeId, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorYear", obj.AgentSimulatorYear, dbType: DbType.Int64);
                    param.Add("@AgentSimulatorMonth", obj.AgentSimulatorMonth, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentSimulatorEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region Event
        internal long AddEvent(EventEnt obj)
        {
            long returnId = 0;
            try
            {

                var roleids = "";
                foreach (var role in obj.EventRoleList)
                {
                    roleids += role.EventRoleId+ ",";
                }

                roleids = roleids.TrimEnd(',');


                using (var conn = new SqlConnection(_connectionString))
                {

                    var insertRoleQuery = "";
                    if (!string.IsNullOrEmpty(roleids))
                    {
                        insertRoleQuery = $@"INSERT INTO EventRoles (
                                        [EventId]  
                                        ,[RoleID]  
                                        ,[StatusId]
                                        ,[CreatedBy]
                                        ,[CreatedDate])
                                        SELECT 
                                        @ID 
                                        , RoleId
                                        , @StatusId  
                                        , @CreatedBy
                                        , GETDATE() FROM Roles WHERE RoleId IN ({roleids})";
                    }
                    var query = $@"
INSERT INTO [dbo].[Events]
           ([EventName]
           ,[EventTypeId]
           ,[EventHostId]
           ,[EventChannelId]
           ,[EventChannelLocation]
           ,[EventFees]
           ,[Remarks]
           ,[PaxLimit]
           ,[CPDPoint]
           ,[AttendantTypeId]
           ,[StatusId]
           ,[CreatedBy]
           ,[CreatedDate])
     VALUES(
 @EventName
,@EventTypeId
,@EventHostId
,@EventChannelId
,@EventChannelLocation
,@EventFees
,@Remarks
,@PaxLimit
,@CPDPoint
,@AttendantTypeId
,@StatusId
,@CreatedBy
,GETDATE());

DECLARE @ID INT = (SELECT SCOPE_IDENTITY());  {insertRoleQuery} SELECT @ID";

                    var param = new DynamicParameters();
                    param.Add("@EventName", obj.EventName, dbType: DbType.String);
                    param.Add("@EventTypeId", obj.EventTypeId, dbType: DbType.Int64);
                    param.Add("@EventHostId", obj.EventHostId, dbType: DbType.Int64);
                    param.Add("@EventChannelId", obj.EventChannelId, dbType: DbType.Int64);
                    param.Add("@EventChannelLocation", obj.EventChannelLocation, dbType: DbType.String);
                    param.Add("@EventFees", obj.EventFees, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@PaxLimit", obj.PaxLimit, dbType: DbType.Int32);
                    param.Add("@CPDPoint", obj.CPDPoint, dbType: DbType.Int32);
                    param.Add("@AttendantTypeId", obj.AttendantTypeId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateEvent(EventEnt obj)
        {
            try
            {

                var roleids = "";
                foreach (var role in obj.EventRoleList)
                {
                    roleids += role.EventRoleId + ",";
                }

                roleids = roleids.TrimEnd(',');


                using (var conn = new SqlConnection(_connectionString))
                {

                    var insertRoleQuery = "";
                    if (!string.IsNullOrEmpty(roleids))
                    {
                        insertRoleQuery = $@"INSERT INTO EventRoles (
                                        [EventId]  
                                        ,[RoleID]  
                                        ,[StatusId]
                                        ,[CreatedBy]
                                        ,[CreatedDate])
                                        SELECT 
                                        @EventId 
                                        , RoleId
                                        , 1  
                                        , @UpdatedBy
                                        , GETDATE() FROM Roles WHERE RoleId IN ({roleids})";
                    }

                    var query = $@"
UPDATE [Events]
SET
    EventName =@EventName,
    EventTypeId =@EventTypeId,
    EventHostId =@EventHostId,
    EventChannelId =@EventChannelId,
    EventChannelLocation =@EventChannelLocation,
    EventFees =@EventFees,
    Remarks =@Remarks,
    PaxLimit =@PaxLimit,
    CPDPoint =@CPDPoint,
    AttendantTypeId =@AttendantTypeId,    
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE EventId = @EventId;
          

DELETE FROM EventRoles WHERE EventId = @EventId ; {insertRoleQuery}
";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventName", obj.EventName, dbType: DbType.String);
                    param.Add("@EventTypeId", obj.EventTypeId, dbType: DbType.Int64);
                    param.Add("@EventHostId", obj.EventHostId, dbType: DbType.Int64);
                    param.Add("@EventChannelId", obj.EventChannelId, dbType: DbType.Int64);
                    param.Add("@EventChannelLocation", obj.EventChannelLocation, dbType: DbType.String);
                    param.Add("@EventFees", obj.EventFees, dbType: DbType.Decimal);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@PaxLimit", obj.PaxLimit, dbType: DbType.Int32);
                    param.Add("@CPDPoint", obj.CPDPoint, dbType: DbType.Int32);
                    param.Add("@AttendantTypeId", obj.AttendantTypeId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        /*
        internal List<EventEnt> GetEventByFilter(EventFilter obj, long pageSize, long pageNumber)
        {
            List<EventEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.EventId
                            ,a.EventName
                            ,a.EventTypeId
                            ,a.EventHostId
                            ,a.EventChannelId
                            ,a.EventChannelLocation
                            ,a.EventFees
                            ,a.Remarks
                            ,a.PaxLimit
                            ,a.CPDPoint
                            ,a.AttendantTypeId
                            ,a.StatusId
                            ,a.CreatedBy
                            ,a.CreatedDate
                            ,a.UpdatedBy
                            ,a.UpdatedDate
                            ,StatusMaster.Name [StatusDesc]
                            ,EventTypeMaster.Name [EventTypeDesc]
                            ,EventHostMaster.Name [EventHostDesc]
                            ,EventChannelMaster.Name [EventChannelDesc]
                            ,AttendantTypeMaster.Name [AttendantTypeDesc]
                        FROM [Events] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas EventTypeMaster WITH (NOLOCK)
                        	ON EventTypeMaster.MasterId = 22
                        		AND a.EventTypeId = EventTypeMaster.MasterDataId
                        LEFT JOIN MasterDatas EventHostMaster WITH (NOLOCK)
                        	ON EventHostMaster.MasterId = 23
                        		AND a.EventHostId = EventHostMaster.MasterDataId
                        LEFT JOIN MasterDatas EventChannelMaster WITH (NOLOCK)
                        	ON EventChannelMaster.MasterId = 24
                        		AND a.EventChannelId = EventChannelMaster.MasterDataId
                        LEFT JOIN MasterDatas AttendantTypeMaster WITH (NOLOCK)
                        	ON AttendantTypeMaster.MasterId = 25
                        		AND a.AttendantTypeId = AttendantTypeMaster.MasterDataId
                        LEFT JOIN EventDates ed
                        	ON a.EventId = ed.EventId
                        WHERE (@EventId IS NULL OR a.EventId = @EventId)
                        AND (@EventTypeId IS NULL OR a.EventTypeId = @EventTypeId)
                        AND (@EventHostId IS NULL OR a.EventHostId = @EventHostId)
                        AND (@EventName IS NULL OR a.EventName LIKE '%' + @EventName +'%')
                        AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                        AND (CAST(@EventDateFrom AS DATE) IS NULL OR CAST(ed.EventDateFrom AS DATE) >= @EventDateFrom)
                        AND (CAST(@EventDateTo AS DATE) IS NULL OR CAST(ed.EventDateTo AS DATE) <= @EventDateTo)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        GROUP BY
                             a.EventId
                            ,a.EventName
                            ,a.EventTypeId
                            ,a.EventHostId
                            ,a.EventChannelId
                            ,a.EventChannelLocation
                            ,a.EventFees
                            ,a.Remarks
                            ,a.PaxLimit
                            ,a.CPDPoint
                            ,a.AttendantTypeId
                            ,a.StatusId
                            ,a.CreatedBy
                            ,a.CreatedDate
                            ,a.UpdatedBy
                            ,a.UpdatedDate
                            ,StatusMaster.Name 
                            ,EventTypeMaster.Name  
                            ,EventHostMaster.Name 
                            ,EventChannelMaster.Name  
                            ,AttendantTypeMaster.Name 
                        ORDER BY a.EventName
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventName", obj.EventName, dbType: DbType.String);
                    param.Add("@EventTypeId", obj.EventTypeId, dbType: DbType.Int64);
                    param.Add("@EventHostId", obj.EventHostId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<EventEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        */

        internal List<EventEnt> GetEventByFilter(EventFilter obj, long pageSize, long pageNumber)
        {
            List<EventEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,EventTypeMaster.Name [EventTypeDesc]
                            ,EventHostMaster.Name [EventHostDesc]
                            ,EventChannelMaster.Name [EventChannelDesc]
                            ,AttendantTypeMaster.Name [AttendantTypeDesc]
                        FROM [Events] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas EventTypeMaster WITH (NOLOCK)
                        	ON EventTypeMaster.MasterId = 22
                        		AND a.EventTypeId = EventTypeMaster.MasterDataId
                        LEFT JOIN MasterDatas EventHostMaster WITH (NOLOCK)
                        	ON EventHostMaster.MasterId = 23
                        		AND a.EventHostId = EventHostMaster.MasterDataId
                        LEFT JOIN MasterDatas EventChannelMaster WITH (NOLOCK)
                        	ON EventChannelMaster.MasterId = 24
                        		AND a.EventChannelId = EventChannelMaster.MasterDataId
                        LEFT JOIN MasterDatas AttendantTypeMaster WITH (NOLOCK)
                        	ON AttendantTypeMaster.MasterId = 25
                        		AND a.AttendantTypeId = AttendantTypeMaster.MasterDataId                        
                        WHERE (@EventId IS NULL OR a.EventId = @EventId)
                        AND (@EventTypeId IS NULL OR a.EventTypeId = @EventTypeId)
                        AND (@EventHostId IS NULL OR a.EventHostId = @EventHostId)
                        AND (@EventName IS NULL OR a.EventName LIKE '%' + @EventName +'%')
                        AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy) ";

                    if (obj.EventDateFrom != null || obj.EventDateTo != null)
                    {
                        query += $@" AND a.EventId IN (SELECT ed.EventId FROM EventDates ed  WITH (NOLOCK)
                                                      WHERE ed.StatusId NOT IN (2) AND (CAST(@EventDateFrom AS DATE) IS NULL OR CAST(ed.EventDateFrom AS DATE) >= @EventDateFrom)
                                                        AND (CAST(@EventDateTo AS DATE) IS NULL OR CAST(ed.EventDateTo AS DATE) <= @EventDateTo)) ";
                    }

                    query += $@"
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.EventName
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventName", obj.EventName, dbType: DbType.String);
                    param.Add("@EventTypeId", obj.EventTypeId, dbType: DbType.Int64);
                    param.Add("@EventHostId", obj.EventHostId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<EventEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateEventStatus(long eventId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Events]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE EventId = @EventId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", eventId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        #endregion

        #region Event Dates
        internal long AddEventDate(EventDateEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[EventDates]
           ([EventId]
           ,[EventDateFrom]
           ,[EventDateTo]
           ,[RegClosingDate]
           ,[EventDateStatusId]
           ,[StatusId]
           ,[CreatedBy]
           ,[CreatedDate])
     VALUES(
 @EventId
,@EventDateFrom
,@EventDateTo
,@RegClosingDate
,@EventDateStatusId
,@StatusId
,@CreatedBy
,GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@RegClosingDate", obj.RegClosingDate, dbType: DbType.DateTime);
                    param.Add("@EventDateStatusId", obj.EventDateStatusId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateEventDate(EventDateEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [EventDates]
SET
    EventDateFrom =@EventDateFrom,
    EventDateTo =@EventDateTo,
    RegClosingDate =@RegClosingDate,  
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE EventDateId = @EventDateId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventDateId", obj.EventDateId, dbType: DbType.Int64);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@RegClosingDate", obj.RegClosingDate, dbType: DbType.DateTime);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<EventDateEnt> GetEventDateByFilter(EventDateFilter obj, string eventIdList, long pageSize, long pageNumber)
        {
            List<EventDateEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,(SELECT COUNT(0) FROM UserEvent ue WITH (NOLOCK) WHERE ue.EventDateId = a.EventDateId AND ue.StatusId = {(long)MasterDataEnum.UserEventStatusId_Going}) TotalAttendance
                        FROM [EventDates] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        WHERE (@EventDateId IS NULL OR a.EventDateId = @EventDateId)
                        AND (@EventId IS NULL OR a.EventId = @EventId) ";

                    if (!string.IsNullOrEmpty(eventIdList))
                    {
                        query += $@"AND a.EventId IN ({eventIdList}) ";
                    }

                    query += $@"AND a.StatusId NOT IN (2)
                        ORDER BY a.EventDateFrom
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventDateId", obj.EventDateId, dbType: DbType.Int64);
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<EventDateEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateEventDateStatus(long eventDateId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [EventDates]
SET
    StatusId = @StatusId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE EventDateId = @EventDateId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventDateId", eventDateId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }


        internal List<EventRoleEnt> GetEventRoleByFilter(EventDateFilter obj, string eventIdList)
        {
            List<EventRoleEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                        FROM [EventRoles] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        WHERE  (@EventId IS NULL OR a.EventId = @EventId) ";

                    if (!string.IsNullOrEmpty(eventIdList))
                    {
                        query += $@"AND a.EventId IN ({eventIdList}) ";
                    }

                    query += $@"AND a.StatusId NOT IN (2)
                       
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    oResult = conn.Query<EventRoleEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        #endregion

        #region Event Attachment
        internal long AddEventAttachment(EventAttachmentEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[EventAttachments]
           ([EventId]
           ,[EventAttachmentName]
           ,[EventAttachmentPath]
           ,[Remarks]
           ,[StatusId]
           ,[CreatedBy]
           ,[CreatedDate])
     VALUES(
 @EventId
,@EventAttachmentName
,@EventAttachmentPath
,@Remarks
,@StatusId
,@CreatedBy
,GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventAttachmentName", obj.EventAttachmentName, dbType: DbType.String);
                    param.Add("@EventAttachmentPath", obj.EventAttachmentPath, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateEventAttachment(EventAttachmentEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [EventAttachments]
SET
    EventAttachmentName =@EventAttachmentName,
    EventAttachmentPath =@EventAttachmentPath,
    Remarks =@Remarks,  
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE EventAttachmentId = @EventAttachmentId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventAttachmentId", obj.EventAttachmentId, dbType: DbType.Int64);
                    param.Add("@EventAttachmentName", obj.EventAttachmentName, dbType: DbType.String);
                    param.Add("@EventAttachmentPath", obj.EventAttachmentPath, dbType: DbType.String);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<EventAttachmentEnt> GetEventAttachmentByFilter(EventAttachmentFilter obj, long pageSize, long pageNumber)
        {
            List<EventAttachmentEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                        FROM [EventAttachments] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        WHERE (@EventAttachmentId IS NULL OR a.EventAttachmentId = @EventAttachmentId)
                        AND (@EventId IS NULL OR a.EventId = @EventId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.EventAttachmentId
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventAttachmentId", obj.EventAttachmentId, dbType: DbType.Int64);
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<EventAttachmentEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateEventAttachmentStatus(long eventAttachmentId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [EventAttachments]
SET
    StatusId = @StatusId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE EventAttachmentId = @EventAttachmentId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@EventAttachmentId", eventAttachmentId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region User Event
        internal long AddUserEvent(UserEventEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[UserEvent]
           ([UserId]
           ,[EventDateId]
           ,[AttendanceId]
           ,[QuizScoreId]
           ,[Remarks]
           ,[CPDPoint]
           ,[IsEmailSent]
           ,[StatusId]
           ,[CreatedBy]
           ,[CreatedDate])
     VALUES(
 @UserId
,@EventDateId
,@AttendanceId
,@QuizScoreId
,@Remarks
,@CPDPoint
,@IsEmailSent
,@StatusId
,@CreatedBy
,GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventId", obj.UserEventId, dbType: DbType.Int64);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@EventDateId", obj.EventDateId, dbType: DbType.Int64);
                    param.Add("@AttendanceId", obj.AttendanceId, dbType: DbType.Int64);
                    param.Add("@QuizScoreId", obj.QuizScoreId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@CPDPoint", obj.CPDPoint, dbType: DbType.Int64);
                    param.Add("@IsEmailSent", obj.IsEmailSent, dbType: DbType.Boolean);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateUserEvent(UserEventEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserEvent]
SET
    EventDateId =@EventDateId,
    AttendanceId =@AttendanceId,
    QuizScoreId =@QuizScoreId,  
    Remarks =@Remarks,  
    CPDPoint =@CPDPoint,  
    IsEmailSent =@IsEmailSent,  
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE UserEventId = @UserEventId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventId", obj.UserEventId, dbType: DbType.Int64);
                    //param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@EventDateId", obj.EventDateId, dbType: DbType.Int64);
                    param.Add("@AttendanceId", obj.AttendanceId, dbType: DbType.Int64);
                    param.Add("@QuizScoreId", obj.QuizScoreId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@CPDPoint", obj.CPDPoint, dbType: DbType.Int64);
                    param.Add("@IsEmailSent", obj.IsEmailSent, dbType: DbType.Boolean);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<UserEventEnt> GetUserEventByFilter(UserEventFilter obj, long pageSize, long pageNumber)
        {
            List<UserEventEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        SELECT a.*, e.*, ed.*, a.CreatedDate JoinDate, a.StatusId UserEventStatusId
                            ,u.Username Username
                            ,u.FullName DisplayName
                            ,uep.PaymentChannelId PaymentChannelId
                            ,StatusMaster.Name [StatusDesc]
                            ,AttendanceMaster.Name [AttendanceDesc]
                            ,QuizScoreMaster.Name [QuizScoreDesc]
                            ,PaymentChannelMaster.Name [PaymentChannelDesc]
                            ,UserEventStatusMaster.Name [UserEventStatusDesc]
                        FROM [UserEvent] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN [EventDates] ed WITH (NOLOCK)
                        	ON ed.EventDateId = a.EventDateId
                        LEFT JOIN Users u WITH (NOLOCK)
	                        ON u.UserId = a.UserId
                        LEFT JOIN Events e WITH (NOLOCK)
	                        ON e.EventId = ed.EventId
                        LEFT JOIN MasterDatas AttendanceMaster WITH (NOLOCK)
                        	ON AttendanceMaster.MasterId = 26
                        		AND a.AttendanceId = AttendanceMaster.MasterDataId
                        LEFT JOIN MasterDatas QuizScoreMaster WITH (NOLOCK)
                        	ON QuizScoreMaster.MasterId = 27
                        		AND a.QuizScoreId = QuizScoreMaster.MasterDataId
                        LEFT JOIN [UserEventPayment] uep WITH (NOLOCK)
                        	                    ON uep.UserEventId = a.UserEventId AND uep.StatusId =1 AND uep.PayementStatusId = 181
                        LEFT JOIN MasterDatas PaymentChannelMaster WITH (NOLOCK)
                        	ON PaymentChannelMaster.MasterId = 29
                        		AND uep.PaymentChannelId = PaymentChannelMaster.MasterDataId
                        LEFT JOIN MasterDatas UserEventStatusMaster WITH (NOLOCK)
                        	ON UserEventStatusMaster.MasterId = 28
                        		AND a.StatusId = UserEventStatusMaster.MasterDataId
                        WHERE (@UserEventId IS NULL OR a.UserEventId = @UserEventId)
                        AND (@EventId IS NULL OR ed.EventId = @EventId)
                        AND (@UserId IS NULL OR a.UserId = @UserId)
                        AND (@EventName IS NULL OR e.EventName LIKE '%' + @EventName +'%')
                        AND (@PaymentChannelId IS NULL OR uep.PaymentChannelId = @PaymentChannelId)
                        AND (@EventDateId IS NULL OR ed.EventDateId = @EventDateId)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)
                        AND (@AttendanceId IS NULL OR a.AttendanceId = @AttendanceId)
                        AND (CAST(@EventDateFrom AS DATE) IS NULL OR CAST(ed.EventDateFrom AS DATE) >= @EventDateFrom)
                        AND (CAST(@EventDateTo AS DATE) IS NULL OR CAST(ed.EventDateTo AS DATE) <= @EventDateTo)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.UserEventId
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventId", obj.UserEventId, dbType: DbType.Int64);
                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventName", obj.EventName, dbType: DbType.AnsiString);
                    param.Add("@PaymentChannelId", obj.PaymentChannelId, dbType: DbType.Int64);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@UserId", obj.UserId, dbType: DbType.Int64);
                    param.Add("@EventDateId", obj.EventDateId, dbType: DbType.Int64);
                    param.Add("@AttendanceId", obj.AttendanceId, dbType: DbType.Int32);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int32);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UserEventEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateUserEventStatus(long UserEventId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserEvent]
SET
    StatusId = @StatusId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE UserEventId = @UserEventId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventId", UserEventId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateUserEventAttendance(long UserEventId, long attendanceId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserEvent]
SET
    AttendanceId = @AttendanceId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE UserEventId = @UserEventId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventId", UserEventId, dbType: DbType.Int64);
                    param.Add("@AttendanceId", attendanceId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<UpcomingEventEnt> GetUpcomingEventByFilter(EventFilter obj, long pageSize, long pageNumber)
        {
            List<UpcomingEventEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT
                        	a.*
                           ,ed.*
                           ,ue.*
                           ,er.*
                           ,StatusMaster.Name [StatusDesc]
                           ,EventTypeMaster.Name [EventTypeDesc]
                           ,EventHostMaster.Name [EventHostDesc]
                           ,EventChannelMaster.Name [EventChannelDesc]
                           ,AttendantTypeMaster.Name [AttendantTypeDesc]
                           ,AttendantTypeMaster.Name [AttendantTypeDesc]
                           ,ue.StatusId UserEventStatusId
                           ,UserEventStatusMaster.Name [UserEventStatusDesc]
                        FROM [Events] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas EventTypeMaster WITH (NOLOCK)
                        	ON EventTypeMaster.MasterId = 22
                        		AND a.EventTypeId = EventTypeMaster.MasterDataId
                        LEFT JOIN MasterDatas EventHostMaster WITH (NOLOCK)
                        	ON EventHostMaster.MasterId = 23
                        		AND a.EventHostId = EventHostMaster.MasterDataId
                        LEFT JOIN MasterDatas EventChannelMaster WITH (NOLOCK)
                        	ON EventChannelMaster.MasterId = 24
                        		AND a.EventChannelId = EventChannelMaster.MasterDataId
                        LEFT JOIN MasterDatas AttendantTypeMaster WITH (NOLOCK)
                        	ON AttendantTypeMaster.MasterId = 25
                        		AND a.AttendantTypeId = AttendantTypeMaster.MasterDataId
                        LEFT JOIN EventDates ed
                        	ON a.EventId = ed.EventId
                        LEFT JOIN UserEvent ue
                        	ON ed.EventDateId = ue.EventDateId
                        	AND ue.UserId = @UserEventUserId
                     LEFT JOIN EventRoles er
                        	ON a.EventId = er.EventId
                        LEFT JOIN MasterDatas UserEventStatusMaster WITH (NOLOCK)
                        	ON UserEventStatusMaster.MasterId = 28
                        		AND ue.StatusId = UserEventStatusMaster.MasterDataId
                        WHERE (@EventId IS NULL OR a.EventId = @EventId)
                        AND (@EventTypeId IS NULL OR a.EventTypeId = @EventTypeId)
                        AND (@RoleId IS NULL OR er.RoleId = @RoleId)
                        AND (@EventHostId IS NULL OR a.EventHostId = @EventHostId)
                        AND (@EventName IS NULL OR a.EventName LIKE '%' + @EventName +'%')
                        AND (@CreatedBy IS NULL OR a.CreatedBy = @CreatedBy)
                        AND (CAST(@EventDateFrom AS DATE) IS NULL OR CAST(ed.EventDateFrom AS DATE) >= @EventDateFrom)
                        AND (CAST(@EventDateTo AS DATE) IS NULL OR CAST(ed.EventDateTo AS DATE) <= @EventDateTo)
                        AND (@StatusId IS NULL OR a.StatusId = @StatusId)                  
      AND (@UserEventStatusId IS NULL OR ue.StatusId = @UserEventStatusId)        
      AND (@AttendanceId IS NULL OR ue.AttendanceId = @AttendanceId)        
                        ORDER BY ed.EventDateTo DESC
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();

                    param.Add("@EventId", obj.EventId, dbType: DbType.Int64);
                    param.Add("@EventName", obj.EventName, dbType: DbType.String);
                    param.Add("@EventTypeId", obj.EventTypeId, dbType: DbType.Int64);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@EventHostId", obj.EventHostId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);
                    param.Add("@EventDateFrom", obj.EventDateFrom, dbType: DbType.DateTime);
                    param.Add("@EventDateTo", obj.EventDateTo, dbType: DbType.DateTime);
                    param.Add("@UserEventUserId", obj.UserEventUserId, dbType: DbType.Int64);
                    param.Add("@UserEventStatusId", obj.UserEventStatusId, dbType: DbType.Int64);
                    param.Add("@AttendanceId", obj.AttendanceId, dbType: DbType.Int64);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UpcomingEventEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        #region User Event Payment 
        internal long AddUserEventPayment(UserEventPaymentEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[UserEventPayment]
           ([UserEventId]
           ,[UserEventPaymentRefNo]
           ,[PaymentChannelId]
           ,[PaymentRefId]
           ,[PaymentResponse]
           ,[PayementStatusId]
           ,[PayementCreatedDate]
           ,[Remarks]
           ,[StatusId]
           ,[CreatedBy]
           ,[CreatedDate] )
     VALUES
           (
 @UserEventId
,@UserEventPaymentRefNo
,@PaymentChannelId
,@PaymentRefId
,@PaymentResponse
,@PayementStatusId
,@PayementCreatedDate
,@Remarks
,@StatusId
,@CreatedBy
,GETDATE())

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventPaymentId", obj.UserEventPaymentId, dbType: DbType.Int64);
                    param.Add("@UserEventId", obj.UserEventId, dbType: DbType.Int64);
                    param.Add("@UserEventPaymentRefNo", obj.UserEventPaymentRefNo, dbType: DbType.String);
                    param.Add("@PaymentChannelId", obj.PaymentChannelId, dbType: DbType.Int64);
                    param.Add("@PaymentRefId", obj.PaymentRefId, dbType: DbType.String);
                    param.Add("@PaymentResponse", obj.PaymentResponse, dbType: DbType.String);
                    param.Add("@PayementStatusId", obj.PayementStatusId, dbType: DbType.Int64);
                    param.Add("@PayementCreatedDate", obj.PayementCreatedDate, dbType: DbType.Date);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateUserEventPayment(UserEventPaymentEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserEventPayment]
SET
    PaymentRefId =@PaymentRefId,
    PaymentResponse =@PaymentResponse,
    PayementStatusId =@PayementStatusId,
    Remarks =@Remarks,  
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE UserEventPaymentId = @UserEventPaymentId;



UPDATE a 
set a.StatusId = 2, Remarks='remove pending payment due to success payment'
from 
UserEventPayment a inner join UserEventPayment b on a.UserEventId = b.UserEventId
 
where b.UserEventPaymentId = @UserEventPaymentId
AND A.UserEventPaymentId <>  b.UserEventPaymentId 
AND A.StatusId=1
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventPaymentId", obj.UserEventPaymentId, dbType: DbType.Int64);
                    param.Add("@PaymentRefId", obj.PaymentRefId, dbType: DbType.String);
                    param.Add("@PaymentResponse", obj.PaymentResponse, dbType: DbType.String);
                    param.Add("@PayementStatusId", obj.PayementStatusId, dbType: DbType.Int64);
                    param.Add("@Remarks", obj.Remarks, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<UserEventPaymentEnt> GetUserEventPaymentByFilter(UserEventPaymentFilter obj, long pageSize, long pageNumber)
        {
            List<UserEventPaymentEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                            ,PaymentStatusMaster.Name [PayementStatusDesc]
                            ,PaymentChannelMaster.Name [PaymentChannelDesc]
                        FROM [UserEventPayment] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        LEFT JOIN MasterDatas PaymentChannelMaster WITH (NOLOCK)
                        	ON PaymentChannelMaster.MasterId = 29
                        		AND a.PaymentChannelId = PaymentChannelMaster.MasterDataId
                        LEFT JOIN MasterDatas PaymentStatusMaster WITH (NOLOCK)
                        	ON PaymentStatusMaster.MasterId = 30
                        		AND a.PayementStatusId = PaymentStatusMaster.MasterDataId
                        WHERE (@UserEventPaymentId IS NULL OR a.UserEventPaymentId = @UserEventPaymentId)
                        AND (@UserEventId IS NULL OR a.UserEventId = @UserEventId)
                        AND (@UserEventPaymentRefNo IS NULL OR a.UserEventPaymentRefNo = @UserEventPaymentRefNo)
                        AND (@PayementStatusId IS NULL OR a.PayementStatusId = @PayementStatusId)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.UserEventPaymentId
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventPaymentId", obj.UserEventPaymentId, dbType: DbType.Int64);
                    param.Add("@UserEventId", obj.UserEventId, dbType: DbType.Int64);
                    param.Add("@PayementStatusId", obj.PayementStatusId, dbType: DbType.Int64);
                    param.Add("@UserEventPaymentRefNo", obj.UserEventPaymentRefNo, dbType: DbType.String);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<UserEventPaymentEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        /*
        internal bool UpdateUserEventPaymentStatus(long UserEventPaymentId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [UserEventPayment]
SET
    StatusId = @StatusId,
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE UserEventPaymentId = @UserEventPaymentId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@UserEventPaymentId", UserEventPaymentId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        */
        #endregion

        #region Agent Commission
        internal long AddAgentCommission(AgentCommissionEnt obj)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
INSERT INTO [dbo].[AgentCommission] ([Username]
, [AgentName]
, [PayoutDate]
, [CycleStartDate]
, [CycleEndDate]
, [CommAmt]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@Username, @AgentName, @PayoutDate, @CycleStartDate, @CycleEndDate, @CommAmt, @StatusId, @CreatedBy, GETDATE());

SELECT SCOPE_IDENTITY();
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@AgentName", obj.AgentName, dbType: DbType.String);
                    param.Add("@PayoutDate", obj.PayoutDate, dbType: DbType.DateTime);
                    param.Add("@CycleStartDate", obj.CycleStartDate, dbType: DbType.DateTime);
                    param.Add("@CycleEndDate", obj.CycleEndDate, dbType: DbType.DateTime);
                    param.Add("@CommAmt", obj.CommAmt, dbType: DbType.Decimal);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateAgentCommission(AgentCommissionEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [AgentCommission]
SET AgentName = @AgentName
   ,PayoutDate = @PayoutDate
   ,CycleStartDate = @CycleStartDate
   ,CycleEndDate = @CycleEndDate
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE Username = @Username;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@AgentName", obj.AgentName, dbType: DbType.String);
                    param.Add("@PayoutDate", obj.PayoutDate, dbType: DbType.DateTime);
                    param.Add("@CycleStartDate", obj.CycleStartDate, dbType: DbType.DateTime);
                    param.Add("@CycleEndDate", obj.CycleEndDate, dbType: DbType.DateTime);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<AgentCommissionEnt> GetAgentCommissionByFilter(AgentCommissionFilter obj, long pageSize, long pageNumber)
        {
            List<AgentCommissionEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT a.*
                            ,StatusMaster.Name [StatusDesc]
                        FROM [AgentCommission] a WITH (NOLOCK)
                        LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
                        	ON StatusMaster.MasterId = 1
                        		AND a.StatusId = StatusMaster.MasterDataId
                        WHERE (@Username IS NULL OR a.Username = @Username)
                        AND (@AgentCommissionId IS NULL OR a.AgentCommissionId = @AgentCommissionId)
                        AND (@PayoutDateFrom IS NULL OR a.PayoutDate >= @PayoutDateFrom)
                        AND (@PayoutDateTo IS NULL OR a.PayoutDate <= @PayoutDateTo)
                        AND a.StatusId NOT IN (2)
                        ORDER BY a.Username
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@AgentCommissionId", obj.AgentCommissionId, dbType: DbType.Int64);
                    param.Add("@PayoutDateFrom", obj.PayoutDateFrom, dbType: DbType.DateTime);
                    param.Add("@PayoutDateTo", obj.PayoutDateTo, dbType: DbType.DateTime);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<AgentCommissionEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion

        internal bool ConvertPotentialClientToKIVIfLongerThanNDays()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
/* convert potential client to KIV if longer than 90 days */

DECLARE @LastNumberOfDays INT = -90;

UPDATE Clients
SET StatusId = 6 -- kiv
WHERE StatusId = 4 -- potential
AND CAST(UpdatedDateStatus AS DATE) < DATEADD(DAY, @LastNumberOfDays, GETDATE());
                    ";

                    var param = new DynamicParameters();

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        #region Users
        internal bool UpdateUserLastLogin(string username)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Users]
SET LastLogin = GETDATE()
WHERE Username = @Username;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", username, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }
        #endregion

        #region report
        internal List<LastLoginReport> GetLoginActivityByFilter(LastLoginReportFilter obj)
        {
            List<LastLoginReport> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	u.*
    ,r.Name RoleName
FROM [Users] u
LEFT JOIN Roles r
    ON u.RoleId = r.RoleId
WHERE (@Username IS NULL OR u.Username = @Username)
AND (@RoleId IS NULL OR u.RoleId = @RoleId)
AND (@LastLoginStartDate IS NULL OR u.LastLogin >= @LastLoginStartDate)
AND (@LastLoginEndDate IS NULL OR u.LastLogin <= @LastLoginEndDate)
                        ORDER BY u.Username
                        OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
                        FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@LastLoginStartDate", obj.LastLoginStartDate, dbType: DbType.DateTime);
                    param.Add("@LastLoginEndDate", obj.LastLoginEndDate, dbType: DbType.DateTime);
                    param.Add("@PAGENO", obj.PageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", obj.PageSize, dbType: DbType.Int64);

                    oResult = conn.Query<LastLoginReport>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<DuplicateUserReport> GetDuplicateUserByFilter(DuplicateUserFilter obj)
        {
            List<DuplicateUserReport> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
DECLARE @Fullname VARCHAR(100) = '';
DECLARE @IcNo VARCHAR(20) = '';

SELECT
	@Fullname = Fullname
   ,@IcNo = u.IcNo
FROM Users u
WHERE u.Username = @Username;

WITH CTE
AS
(SELECT
		u.Username
	   ,u.UserId
	   ,u.ContactNo
	   ,u.CreatedDate
	   ,Fullname
	   ,u.IcNo
	   ,RN = ROW_NUMBER() OVER (PARTITION BY Fullname
		ORDER BY Fullname)
	FROM Users u
	WHERE (@Fullname IS NULL
	OR u.Fullname = @Fullname)
	AND (@IcNo IS NULL
	OR u.IcNo = @IcNo))
SELECT
	u.Username
   ,u.UserId
   ,u.ContactNo
   ,u.CreatedDate
   ,Fullname
   ,u.IcNo
FROM CTE u
ORDER BY u.Fullname
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.String);
                    param.Add("@PAGENO", obj.PageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", obj.PageSize, dbType: DbType.Int64);

                    oResult = conn.Query<DuplicateUserReport>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion


        #region Role Menu

        internal List<MainMenuEnt> GetMainMenuByRoleId(long roleId )
        {
            List<MainMenuEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                         SELECT distinct mm.MainMenuId , mm.name FROM MainMenus mm WHERE mm.StatusId = 1
                        INNER JOIN RoleMenus rm ON mm.MenuId = rm.MenuId AND rm.Status=1
                        AND  rm.RoleId = @RoleId";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", roleId, dbType: DbType.Int64);

                    oResult = conn.Query<MainMenuEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<long> GetSubMenuByRoleId(long roleId)
        {
            List<long> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                         SELECT distinct mm.SubMenuId  FROM SubMenus mm
                        INNER JOIN RoleMenus rm ON mm.SubMenuId = rm.SubMenuId AND rm.StatusId=1
 WHERE mm.StatusId = 1
                        AND  rm.RoleId = @RoleId";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", roleId, dbType: DbType.Int64);

                    oResult = conn.Query<long>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<MainMenuEnt> GetAllMainMenu()
        {
            List<MainMenuEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @" SELECT distinct mm.MainMenuId , mm.name FROM MainMenus mm WHERE mm.StatusId = 1 ";
                    var param = new DynamicParameters();
                    oResult = conn.Query<MainMenuEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal List<SubMenuEnt> GetSubMenuByMainMenuId(long mainMenuId)
        {
            List<SubMenuEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT *
                        FROM   SubMenus sm  WHERE   sm.StatusId=1
                        AND  sm.MainMenuId = @MainMenuId";

                    var param = new DynamicParameters();
                    param.Add("@MainMenuId", mainMenuId, dbType: DbType.Int64);
                    oResult = conn.Query<SubMenuEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }


        internal long AddRole(RolesEnt obj, string subMenuIdList)
        {
            long returnId = 0;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    string queryForAddRoleMenus = $@"
                        INSERT INTO [dbo].[RoleMenus] (
                          [RoleId]
                        , [SubMenuId]
                        , [StatusId]
                        , [CreatedBy]
                        , [CreatedDate])
	                        SELECT
		                        @RoleId_new
	                           ,sm.SubMenuId
	                           ,@StatusId
	                           ,@CreatedBy
	                           ,GETDATE()
	                        FROM SubMenus sm
	                        WHERE sm.SubMenuId IN ({subMenuIdList});
                    ";

                    var query = $@"
                        BEGIN TRY
	                        BEGIN TRANSACTION

                            DECLARE @RoleId_new BIGINT = NULL;

	                        INSERT INTO [dbo].[Roles] ([Code]
	                        , [Name]
	                        , [StatusId]
	                        , [CreatedBy]
	                        , [CreatedDate])
		                        VALUES (@Code, @Name, @StatusId, @CreatedBy, GETDATE());

	                        SELECT @RoleId_new = SCOPE_IDENTITY();

	                        {  queryForAddRoleMenus}

	                        SELECT @RoleId_new;

	                        COMMIT TRANSACTION
                        END TRY
                        BEGIN CATCH
	                        DECLARE @Error_Number INT
		                           ,@Error_Severity INT
		                           ,@Error_State INT
		                           ,@Error_Procedure VARCHAR(1000)
		                           ,@Error_Line INT
		                           ,@Error_Message VARCHAR(8000);

	                        SELECT
		                        @Error_Number = ERROR_NUMBER()
	                           ,@Error_Severity = ERROR_SEVERITY()
	                           ,@Error_State = ERROR_STATE()
	                           ,@Error_Procedure = ERROR_PROCEDURE()
	                           ,@Error_Line = ERROR_LINE()
	                           ,@Error_Message = ERROR_MESSAGE();

	                        ROLLBACK TRANSACTION;
	                        RAISERROR (@Error_Message, @Error_Severity, @Error_State);
                        END CATCH;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@Code", obj.Code, dbType: DbType.String);
                    param.Add("@Name", obj.Name, dbType: DbType.String);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.String);

                    returnId = conn.ExecuteScalar<long>(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return returnId;
        }

        internal bool UpdateRole(RolesEnt obj, string subMenuIdList)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    string queryForAddRoleMenu = $@"
                        INSERT INTO [dbo].[RoleMenus] (
                          [RoleId]
                        , [SubMenuId]
                        , [StatusId]
                        , [CreatedBy]
                        , [CreatedDate])
	                        SELECT
		                        @RoleId
	                           ,sm.SubMenuId
	                           ,1
	                           ,@UpdatedBy
	                           ,GETDATE()
	                        FROM SubMenus sm
	                        WHERE sm.SubMenuId IN ({subMenuIdList}) AND NOT EXISTS (SELECT a.SubMenuId FROM RoleMenus a WHERE a.SubMenuId = sm.SubMenuId AND RoleId = @RoleId);
                    ";

                    var query = $@"
                        BEGIN TRY
	                        BEGIN TRANSACTION

                            UPDATE [dbo].[Roles]
                            SET Name = @Name
                               ,Code = @Code
                               ,UpdatedBy = @UpdatedBy
                               ,UpdatedDate = GETDATE()
                            WHERE RoleId = @RoleId

                            DELETE FROM RoleMenus WHERE RoleId = @RoleId;

	                        {queryForAddRoleMenu}

	                        COMMIT TRANSACTION
                        END TRY
                        BEGIN CATCH
	                        DECLARE @Error_Number INT
		                           ,@Error_Severity INT
		                           ,@Error_State INT
		                           ,@Error_Procedure VARCHAR(1000)
		                           ,@Error_Line INT
		                           ,@Error_Message VARCHAR(8000);

	                        SELECT
		                        @Error_Number = ERROR_NUMBER()
	                           ,@Error_Severity = ERROR_SEVERITY()
	                           ,@Error_State = ERROR_STATE()
	                           ,@Error_Procedure = ERROR_PROCEDURE()
	                           ,@Error_Line = ERROR_LINE()
	                           ,@Error_Message = ERROR_MESSAGE();

	                        ROLLBACK TRANSACTION;
	                        RAISERROR (@Error_Message, @Error_Severity, @Error_State);
                        END CATCH;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@Code", obj.Code, dbType: DbType.String);
                    param.Add("@Name", obj.Name, dbType: DbType.String);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.String);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal List<RolesEnt> GetRoleByFilter(RoleFilter obj, long pageSize, long pageNumber)
        {
            List<RolesEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
SELECT
	a.*
   ,StatusMaster.Name [StatusDesc] 
FROM [Roles] a WITH (NOLOCK)
LEFT JOIN MasterDatas StatusMaster WITH (NOLOCK)
	ON StatusMaster.MasterId = 1
		AND a.StatusId = StatusMaster.MasterDataId 

WHERE (@RoleId IS NULL OR a.RoleId = @RoleId)
AND (@StatusId IS NULL OR a.StatusId = @StatusId)                           
AND (@Code IS NULL OR a.Code= @Code)
AND (@Name IS NULL OR a.Name = @Name)
AND a.StatusId NOT IN (2)
ORDER BY a.CreatedDate
OFFSET (@PAGENO - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY
                    ";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", obj.RoleId, dbType: DbType.Int64);
                    param.Add("@Code", obj.Code, dbType: DbType.AnsiString);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@PAGENO", pageNumber, dbType: DbType.Int64);
                    param.Add("@PAGESIZE", pageSize, dbType: DbType.Int64);

                    oResult = conn.Query<RolesEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }

        internal bool UpdateRoleStatus(long roleId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                        UPDATE Roles
                            SET StatusId = @StatusId,
                            UpdatedBy = @UpdatedBy,
                            UpdatedDate = GETDATE()
                        WHERE RoleId = @RoleId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@RoleId", roleId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        #endregion


        //=====

        #region Resource
        internal bool AddResource(ResourcesEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"

INSERT INTO [dbo].[Resource] (
[Name]
, [Url]
, [UserName]
, [StatusId]
, [CreatedBy]
, [CreatedDate])
	VALUES (@Name, @Url, @UserName, @StatusId, @CreatedBy, GETDATE());
                    ";

                    var param = new DynamicParameters();
                   
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@Url", obj.Url, dbType: DbType.AnsiString);
                    param.Add("@UserName", obj.UserName, dbType: DbType.AnsiString);
                   
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@CreatedBy", obj.CreatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateResource (ResourcesEnt obj)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
UPDATE [Resource]
SET Name = @Name
    ,Url = @Url
    ,UserName = @UserName
    ,StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ResourceId = @ResourceId;
                    ";

                    var param = new DynamicParameters();
                    param.Add("@ResourceId", obj.ResourceId, dbType: DbType.Int64);
                    param.Add("@Name", obj.Name, dbType: DbType.AnsiString);
                    param.Add("@Url", obj.Url, dbType: DbType.AnsiString);
                    param.Add("@UserName", obj.UserName, dbType: DbType.AnsiString);
                    param.Add("@StatusId", obj.StatusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", obj.UpdatedBy, dbType: DbType.AnsiString);

                    conn.Execute(query, param, commandType: CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

        internal bool UpdateResourceStatus(long ResourceId, long statusId, string updatedBy)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var reqUpdateResourceToConfirmQuery = $@"
UPDATE Resource
    SET StatusId = {(long)MasterDataEnum.Status_Inactive},
    UpdatedBy = @UpdatedBy,
    UpdatedDate = GETDATE()
WHERE ResourceId = {ResourceId};
";

                    /*
                    var query = $@"
{(IsClientConfirmed ? reqUpdateClientToConfirmQuery : "")};

UPDATE [ClientDeals]
SET StatusId = @StatusId
   ,UpdatedBy = @UpdatedBy
   ,UpdatedDate = GETDATE()
WHERE ClientDealId = @ClientDealId;
                    ";

                    */

                    var param = new DynamicParameters();
                    param.Add("@ResourceId",  ResourceId, dbType: DbType.Int64);
                    param.Add("@StatusId", statusId, dbType: DbType.Int64);
                    param.Add("@UpdatedBy", updatedBy, dbType: DbType.AnsiString);

                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        conn.Execute(reqUpdateResourceToConfirmQuery, param, commandType: CommandType.Text, transaction: transaction);
                        transaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return true;
        }

       


        internal List<ResourcesEnt> GetResourceByFilter(long? ResourceId, string Name,
       DateTime? createdDateFrom, DateTime? createdDateTo)
        {
            List<ResourcesEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = $@"
                         SELECT
                        R.*,
                        U.Username
                        FROM 
                        Resource R inner join Users U on
                        R.UserName = U.UserName
                        WHERE    
                        (@Name IS NULL OR R.Name LIKE '%' + @Name + '%') AND
                        (@CreatedDateFrom IS NULL OR R.CreatedDate >= 2022/11/26) AND
                        (@CreatedDateTo IS NULL OR R.CreatedDate <= @CreatedDateTo) AND
                        (@ResourceId IS NULL OR  ResourceId = @ResourceId) AND
                        R.StatusId NOT IN (2)
                        ORDER BY
                        R.ResourceId;
                        
                    ";

                    var param = new DynamicParameters();

                    param.Add("@ResourceId", ResourceId, dbType: DbType.Int64);
                    param.Add("@Name", Name, dbType: DbType.AnsiString);
                    param.Add("@CreatedDateFrom", createdDateFrom, dbType: DbType.DateTime);
                    param.Add("@CreatedDateTo", createdDateTo, dbType: DbType.DateTime);
                 


                    oResult = conn.Query<ResourcesEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }


        internal List<ResourcesEnt> GetResourceByUsername(ResourceUserRequest obj)
        {
            List<ResourcesEnt> oResult = null;
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT
	                    Username
                        FROM
                       Users 
                        WHERE
                        Username = @Username
                       
                    ";

                    var param = new DynamicParameters();
                    param.Add("@Username", obj.Username, dbType: DbType.AnsiString);
                   

                    oResult = conn.Query<ResourcesEnt>(query, param, commandType: CommandType.Text).ToList();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, "", ex.ToString()));
                throw ex;
            }

            return oResult;
        }
        #endregion


        //=====
    }
}

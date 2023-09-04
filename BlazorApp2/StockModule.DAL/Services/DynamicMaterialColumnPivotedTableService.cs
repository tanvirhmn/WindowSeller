using Microsoft.EntityFrameworkCore;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class DynamicMaterialColumnPivotedTableService : IDynamicMaterialColumnPivotedTableService
    {
        private readonly IntusPrefContext _dbContext;
        public DynamicMaterialColumnPivotedTableService(IntusPrefContext dbContext)
        {
            _dbContext = dbContext;
        }

        DataTable IDynamicMaterialColumnPivotedTableService.GetAll()
        {
            var table = new DataTable();
            using (_dbContext)
            {
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = $@"DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX);
                SET @cols = (SELECT STRING_AGG(CONVERT(NVARCHAR(max), ISNULL(dbo.MaterialColumn.[Name],'N/A')), ',') FROM dbo.MaterialColumn);

                SET @query ='SELECT * FROM
                (SELECT dbo.MaterialColumn.[Name], dbo.MaterialColumnValue.RowNo, dbo.MaterialColumnValue.[Value]
                FROM     dbo.MaterialColumn RIGHT OUTER JOIN
                                  dbo.MaterialColumnValue ON dbo.MaterialColumn.Id = dbo.MaterialColumnValue.MaterialColumnId) p
                PIVOT
                (
                MAX([Value])
                 FOR [Name] IN ('+ @cols +')
                ) AS pvt ORDER BY [RowNo]';  
                execute(@query);";


                if (cmd.Connection!.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cmd.Connection!.Open();
                table.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            return table;
        }

        DataTable IDynamicMaterialColumnPivotedTableService.GetAllColumnControlled()
        {
            var table = new DataTable();
            using (_dbContext)
            {
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = $@"DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX);
                SET @cols = (SELECT STRING_AGG(CONVERT(NVARCHAR(max), ISNULL(dbo.MaterialColumn.[Name],'N/A')), ',') FROM dbo.MaterialColumn
				    WHERE dbo.MaterialColumn.[ID] NOT IN (SELECT [MaterialColumnId]
                  FROM [dbo].[DynamicMaterialCoulmnGridHiding]
                  WHERE [UserID] IS NULL));

                SET @query ='SELECT * FROM
                (SELECT dbo.MaterialColumn.[Name], dbo.MaterialColumnValue.RowNo, dbo.MaterialColumnValue.[Value]
                FROM     dbo.MaterialColumn RIGHT OUTER JOIN
                                  dbo.MaterialColumnValue ON dbo.MaterialColumn.Id = dbo.MaterialColumnValue.MaterialColumnId) p
                PIVOT
                (
                MAX([Value])
                 FOR [Name] IN ('+ @cols +')
                ) AS pvt ORDER BY [RowNo]';  
                execute(@query);";


                if (cmd.Connection!.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cmd.Connection!.Open();
                table.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            return table;
        }
    }
}

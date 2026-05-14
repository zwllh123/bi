using NewLife;
using NewLife.Data;
using NewLife.Serialization;
using XCode;
using XCode.DataAccessLayer;

namespace Olive.BI.Entity;

/// <summary>数据源连接配置</summary>
public class DataSourceConfig
{
    /// <summary>数据库类型：mysql / sqlserver / postgresql / oracle / sqlite</summary>
    public String? DbType { get; set; }

    /// <summary>ADO.NET 连接字符串</summary>
    public String? ConnectionString { get; set; }

    /// <summary>提供者（可选，用于覆盖默认 provider）</summary>
    public String? Provider { get; set; }
}

/// <summary>SQL 执行结果</summary>
public class SqlExecuteResult
{
    /// <summary>列名列表</summary>
    public List<String> Columns { get; set; } = [];

    /// <summary>数据行：每行为列名->值字典</summary>
    public List<Dictionary<String, Object?>> Rows { get; set; } = [];

    /// <summary>总行数</summary>
    public Int32 Total => Rows.Count;
}

/// <summary>数据源。业务扩展</summary>
public partial class ReportDataSource
{
    #region 业务方法

    /// <summary>解析连接配置</summary>
    /// <returns>配置对象</returns>
    public DataSourceConfig ParseConfig()
    {
        if (SourceConfig.IsNullOrEmpty()) return new DataSourceConfig { DbType = SourceType };

        var text = SourceConfig!.Trim();
        DataSourceConfig? cfg = null;

        // 以 { 开头视为 JSON 对象；否则视为原始连接字符串
        if (text.StartsWith('{'))
        {
            try
            {
                cfg = text.ToJsonEntity<DataSourceConfig>();
            }
            catch
            {
                // 解析失败时回退到连接字符串模式
                cfg = null;
            }
        }

        cfg ??= new DataSourceConfig { ConnectionString = text };

        if (cfg.DbType.IsNullOrEmpty()) cfg.DbType = SourceType;
        return cfg;
    }

    /// <summary>分页搜索</summary>
    /// <param name="keywords">关键字</param>
    /// <param name="enableFlag">启用过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>数据源列表</returns>
    public static IList<ReportDataSource> SearchPage(String? keywords, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;
        if (!keywords.IsNullOrEmpty())
            exp &= _.SourceCode.Contains(keywords) | _.SourceName.Contains(keywords);
        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;

        return FindAll(exp, page);
    }

    /// <summary>测试连接</summary>
    /// <returns>错误信息，null=成功</returns>
    public String? TestConnection()
    {
        var cfg = ParseConfig();
        if (cfg.ConnectionString.IsNullOrEmpty()) return "连接字符串为空";

        try
        {
            var dal = DataSourceExecutor.CreateDal(SourceCode!, cfg);
            using var conn = dal.Db.Factory.CreateConnection();
            conn!.ConnectionString = dal.ConnStr;
            conn.Open();
            return null;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>执行 SQL 并返回结果</summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数字典</param>
    /// <param name="maxRows">最大返回行数，0表示不限制</param>
    /// <returns>SQL 执行结果</returns>
    public SqlExecuteResult Execute(String sql, IDictionary<String, Object?>? parameters = null, Int32 maxRows = 1000)
        => DataSourceExecutor.Execute(SourceCode!, ParseConfig(), sql, parameters, maxRows);

    #endregion
}

/// <summary>数据源执行器（静态工具）</summary>
public static class DataSourceExecutor
{
    /// <summary>根据配置创建 DAL 实例</summary>
    /// <param name="sourceCode">数据源编码</param>
    /// <param name="cfg">配置</param>
    /// <returns>DAL 实例</returns>
    public static DAL CreateDal(String sourceCode, DataSourceConfig cfg)
    {
        var name = "ds_" + sourceCode;
        var connStr = cfg.ConnectionString ?? "";
        var provider = cfg.Provider.IsNullOrEmpty() ? (cfg.DbType ?? "mysql") : cfg.Provider!;

        // 已存在则比较连接串决定是否覆盖
        var connStrs = DAL.ConnStrs;
        if (connStrs.TryGetValue(name, out var old))
        {
            if (old != connStr) DAL.AddConnStr(name, connStr, null, provider);
        }
        else
        {
            DAL.AddConnStr(name, connStr, null, provider);
        }

        return DAL.Create(name);
    }

    /// <summary>执行 SQL</summary>
    /// <param name="sourceCode">数据源编码</param>
    /// <param name="cfg">配置</param>
    /// <param name="sql">SQL</param>
    /// <param name="parameters">参数</param>
    /// <param name="maxRows">最大行数</param>
    /// <returns>结果</returns>
    public static SqlExecuteResult Execute(String sourceCode, DataSourceConfig cfg, String sql, IDictionary<String, Object?>? parameters, Int32 maxRows)
    {
        var result = new SqlExecuteResult();
        if (sql.IsNullOrEmpty()) return result;

        var dal = CreateDal(sourceCode, cfg);

        // 简单参数替换：将 ${name} 替换为预格式值
        var finalSql = sql;
        if (parameters != null)
        {
            foreach (var kv in parameters)
            {
                var placeholder = "${" + kv.Key + "}";
                var value = kv.Value?.ToString() ?? "";
                finalSql = finalSql.Replace(placeholder, value);
            }
        }

        var dt = dal.Query(finalSql, null);
        if (dt == null) return result;

        foreach (var col in dt.Columns) result.Columns.Add(col);

        var count = 0;
        foreach (var row in dt.Rows)
        {
            if (maxRows > 0 && count >= maxRows) break;
            var dict = new Dictionary<String, Object?>();
            for (var i = 0; i < dt.Columns.Length; i++)
            {
                dict[dt.Columns[i]] = i < row.Length ? row[i] : null;
            }
            result.Rows.Add(dict);
            count++;
        }

        return result;
    }
}

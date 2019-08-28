using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using BilibiliSpider.Entity.Database;
using ServiceStack.OrmLite;

namespace BilibiliSpider.DB
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class DBSet
    {
        private static OrmLiteConnectionFactory factory =
            new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider, false);

        static DBSet()
        {
            OrmLiteConfig.DialectProvider = SqliteDialect.Provider;
        }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        public static IDbConnection GetCon(SqliteDBName nameType)
        {
            var sqliteName = nameType + ".sqlite";

            if (!OrmLiteConnectionFactory.NamedConnections.ContainsKey(sqliteName))
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db");
                
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var sqliteFileName = Path.Combine(path, sqliteName);
                factory.RegisterConnection(sqliteName, sqliteFileName, SqliteDialect.Provider);
            }

            return factory.Open(sqliteName);
        }

        public static void Init()
        {
            using (var db = GetCon(SqliteDBName.Bilibili))
            {
                db.CreateTable<AV>();
                db.CreateTable<UP>();
                db.CreateTable<ImageDetect>();
            }

            using (var db = GetCon(SqliteDBName.History))
            {
                db.CreateTable<AVHistory>();
            }
        }

        /// <summary>
        /// 用来保存数据sqlite的主名字
        /// </summary>
        public enum SqliteDBName
        {
            /// <summary>
            /// b站主数据名字
            /// 注意，不要随意修改
            /// </summary>
            Bilibili,

            /// <summary>
            /// 用来放采集历史的
            /// </summary>
            History,
        }
    }
}

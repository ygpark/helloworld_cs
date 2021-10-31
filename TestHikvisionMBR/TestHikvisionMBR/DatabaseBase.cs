using System;
using System.Data.SQLite;
using System.IO;
using System.Data;
using Recovery.Video.Enum;

namespace Recovery.Video.Base
{
    public class DatabaseBase : IDisposable
    {
        //
        // IDisposable
        //
        private bool disposed = false;
        //
        // 멤버변수
        //
        private bool _isOpen = false;
        protected SQLiteCommand Command = null;
        readonly object obj = new object();
        SQLiteTransaction _singleTransaction = null;
        private bool autoCommitWhenClose = false;
        //
        // 이벤트
        //
        public event BeforeAnalyzeEventHandler BeforeAnalyze;
        //
        // Property
        //
        public string FilePath { get; }
        public string ConnectionString { get => string.Format($"Data Source={FilePath};Version=3"); }

        /// <summary>
        /// 경로 workingDir에 데이터베이스 파일을 생성한다.
        /// </summary>
        public DatabaseBase(string filePath, EFileMode mode = EFileMode.CreateNew)
        {
            Console.WriteLine("DatabaseBase.DatabaseBase(string, EFileMode)");
            FilePath = filePath;

            
            if (File.Exists(FilePath) == false)
            {
                IOException ex = new IOException(string.Format($"{FilePath} 파일이 존재하지 않습니다."));
                ex.Data["FilePath"] = FilePath;
                throw ex;
            }
        }

        ~DatabaseBase()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            //관리되는 자원 해제
            if (disposing)
            {
                if (autoCommitWhenClose == true)
                {
                    this.Commit();
                }
                Command.Connection.Dispose();
                Command.Dispose();
                SQLiteConnection.ClearAllPools();

                //DB파일 핸들이 완전히 닫히기까지 대기
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //관리되지 않는 자원 해제

            //상태값 변환
            this.disposed = true;
        }

        public void Close()
        {
            if (_isOpen == false)
            {
                return;
            }

            Command.Connection.Close();
            _isOpen = false;
        }

        public void Open()
        {
            Console.WriteLine("DatabaseBase.Open");
            if (_isOpen == true)
            {
                return;
            }

            Command = new SQLiteCommand(new SQLiteConnection(ConnectionString));
            Command.Connection.Open();
            _isOpen = true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSolationLevel">SQLite 라이브러리에서 Serializable, ReadCommitted and Unspecified(커넥션 스트링에서 지정하지 않으면 Serializable 적용됨)만 지원하고 있음.</param>
        /// <param name="autoCommitWhenClose"></param>
        public void BeginTransaction(IsolationLevel iSolationLevel = IsolationLevel.ReadCommitted, bool autoCommitWhenClose = true)
        {
            Console.WriteLine("DatabaseBase.BeginTransaction()");
            if (iSolationLevel == IsolationLevel.ReadUncommitted)
            {
                throw new NotSupportedException("IsolationLevel은 Serializable, ReadCommitted and Unspecified 만 지원합니다.");
            }
            this.autoCommitWhenClose = autoCommitWhenClose;
            if (_singleTransaction == null)
            {
                _singleTransaction = Command.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            else
            {
                throw new NotSupportedException("Transaction은 한 번만 허용합니다.");
            }
        }

        /// <summary>
        /// thread-safe 버전
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            int affectedRow;
            lock (obj)
            {
                Command.CommandText = sql;
                affectedRow = Command.ExecuteNonQuery();
            }
            return affectedRow;
        }

        protected SQLiteDataReader ExecuteReader(string sql)
        {
            Command.CommandText = sql;
            return Command.ExecuteReader();
        }

        protected object ExecuteScalar(string sql)
        {
            Command.CommandText = sql;
            return Command.ExecuteScalar(System.Data.CommandBehavior.SingleResult);
        }

        public void Commit()
        {
            Console.WriteLine("DatabaseBase.Commit()");
            if (_singleTransaction != null)
            {
                _singleTransaction.Commit();
                _singleTransaction.Dispose();
                _singleTransaction = null;
            }
        }

        public void Analyze()
        {
            if (BeforeAnalyze != null)
                BeforeAnalyze();

            Console.WriteLine("DatabaseBase.Analyze()");
            ExecuteNonQuery("analyze;");
        }


        #region 가상 메소드

        public virtual SQLiteDataReader Select()
        {
            throw new NotImplementedException();
        }
        public virtual SQLiteDataReader Select(string dateStart, string dateEnd)
        {
            throw new NotImplementedException();
        }
        public virtual long SelectCount()
        {
            throw new NotImplementedException();
        }
        public virtual long SelectCount(string dateStart, string dateEnd)
        {
            throw new NotImplementedException();
        }

        public virtual string[] SelectPeriodOfRecord()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    //
    // 델리게이트
    //
    public delegate void BeforeAnalyzeEventHandler();
}

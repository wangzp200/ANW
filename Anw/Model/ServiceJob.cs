using System.Threading;
using AnwConnector.Common;

namespace AnwConnector.Model
{
    /// <summary>
    ///     工作项
    /// </summary>
    public abstract class ServiceJob
    {
        //任务是否在运行中
        protected bool MIsRunning;

        protected int SleepTime;


        /// <summary>
        ///     构造函数
        /// </summary>
        protected ServiceJob()
        {
            //变量初始化
            SleepTime = Globle.SleepTime;
        }

        public CancellationTokenSource CancelTokenSource { set; get; }

        public abstract void TaskJob();

        /// <summary>
        ///     开始工作
        /// </summary>
        public void StartJob()
        {
            if (!MIsRunning)
            {
                MIsRunning = true;
                Start();
            }
        }

        /// <summary>
        ///     停止工作
        /// </summary>
        public void StopJob()
        {
            if (MIsRunning)
            {
                MIsRunning = false;
                Stop();
            }
        }

        #region 子类必需实现的抽象成员

        /// <summary>
        ///     开始工作
        /// </summary>
        protected abstract void Start();

        /// <summary>
        ///     停止工作
        /// </summary>
        protected abstract void Stop();

        #endregion
    }
}
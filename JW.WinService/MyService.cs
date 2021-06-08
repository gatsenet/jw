using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JW.WinService
{
    public partial class MyService : ServiceBase
    {
        Timer tStockFull = new Timer(), tStockDate = new Timer();
        bool isStockFull = false, isStockDate = false;
        public MyService()
        {
            InitializeComponent();

            tStockFull.Interval = 1;
            tStockFull.AutoReset = true;
            tStockFull.Elapsed += tStockFull_Elapsed;

            tStockDate.Interval = 1;
            tStockDate.AutoReset = true;
            tStockDate.Elapsed += tStockDate_Elapsed;
        }

        protected override void OnStart(string[] args)
        {
            isStockDate = isStockFull = false;
            tStockFull.Start();tStockDate.Start();
        }

        protected override void OnStop()
        {
            isStockDate = isStockFull = false;
            tStockFull.Stop();tStockDate.Stop(); 
        }

        private void tStockFull_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isStockFull)
            {
                isStockFull = true; DateTime dateb = DateTime.Now;
                if (tStockFull.Interval == 1)//如果是第一次执行
                {
                    tStockFull.Interval = 60 * 60 * 1000;//设置Interval为想要的间隔时间。
                }
                try
                {
                    if (DB.BasicSetting.Stock_FullUpdate_Enable)
                    {
                        int hour = DB.BasicSetting.Stock_FullUpdate_Period;
                        if (DateTime.Now.Hour==hour)
                        {
                            API.ToRun.UpdateFullStock();
                            DB.LogSystem.AddLog(true, "供应商库存服务", "按款号获取", "", "", "", dateb, DateTime.Now);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DB.LogSystem.AddLog(false, "供应商库存服务", "按款号获取", ex.Message, "", "", dateb, DateTime.Now);
                }
                finally
                {
                    isStockFull = false;
                }
            }
        }

        private void tStockDate_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isStockDate & !isStockFull)
            {
                DateTime dateb = DateTime.Now;isStockDate = true;
                if (tStockDate.Interval == 1)//如果是第一次执行
                {
                    tStockDate.Interval = DB.BasicSetting.Stock_TimeUpdate_Period * 60 * 60 * 1000;//设置Interval为想要的间隔时间。
                }
                try
                {
                    if (DB.BasicSetting.Stock_TimeUpdate_Enable)
                    {
                        DateTime lastTime = DB.BasicSetting.Stock_TimeUpdate_LastTime, nowTime = DateTime.Now;
                        API.ToRun.UpdateStockByDate(lastTime, nowTime);
                        DB.LogSystem.AddLog(false, "供应商库存服务", "按时间获取", string.Format("{0}至{1}", lastTime.ToString(), nowTime.ToString()), "", "", dateb, DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    DB.LogSystem.AddLog(false, "供应商库存服务", "按时间获取", ex.Message, "", "", dateb, DateTime.Now);
                }
                finally
                {
                    isStockDate = false;
                }
            }
        }
    }
}

/*
*
* �ļ���    ��Program     
* ����ʱ��  : 2020-05-21 11:44 
*/


namespace Consumption.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Consumption.EFCore.Context;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Writers;
    using NLog.Web;

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /*
        *  �״�������Ŀ��֪:
        *  1.���� appsettings.json �� MySqlNoteConnection �����Ƿ��뵱ǰ�Ļ���һ��
        *  2.��ȷ�� ���ݿ��Ǩ���ļ��Ѿ����µ����mysql���С�
        *    2.1. �򿪳�����������̨, ȷ��API��ĿΪ������
        *    2.2. Ĭ����Ŀ�趨EfCore, ����̨���� update-database ����ǰ���ڵ�Ǩ���ļ����ɵ����ݿ⵱��
        *   
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                     .ConfigureLogging(logging =>
                     {
                         logging.ClearProviders();
                         logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                     }).UseNLog()
                     .UseUrls("http://*:5001");
                });
    }
}

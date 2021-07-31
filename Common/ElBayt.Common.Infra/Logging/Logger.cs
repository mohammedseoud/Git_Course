using ElBayt.Common.Entities;
using ElBayt.Common.Logging;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Infra.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger(ILog logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex.Message, ex);
        }

        public void Info(string message)
        {
            GlobalContext.Properties.Clear();
            _logger.Info(message);
        }

        public void Info(string message, params object[] arguments)
        {
            _logger.Info(string.Format(message, arguments));
        }

        public void InfoInDetail(string message, LogModel logModel)
        {
            GlobalContext.Properties["notes1"] = logModel?.NotesA;
            GlobalContext.Properties["notes2"] = logModel?.NotesB;
            GlobalContext.Properties["trackingGuid"] = logModel?.CorrelationId;
            GlobalContext.Properties["order"] = logModel?.Step;
            GlobalContext.Properties["username"] = logModel?.Username;
            _logger.Info(message);
        }

        public void ErrorInDetail(Exception ex, LogModel logModel)
        {
            GlobalContext.Properties["notes1"] = logModel?.NotesA;
            GlobalContext.Properties["notes2"] = logModel?.NotesB;
            GlobalContext.Properties["trackingGuid"] = logModel?.CorrelationId;
            GlobalContext.Properties["order"] = logModel?.Step;
            GlobalContext.Properties["username"] = logModel?.Username;
            _logger.Error(ex.Message, ex);
        }

        public void Trace(string message)
        {
            _logger.Debug(message);
        }

        public void Trace(string message, params object[] arguments)
        {
            _logger.Debug(string.Format(message, arguments));
        }

        public void InfoInDetail(object context, Guid correlationGuid, string notesB, string message, int step, string userName)
        {
            var notesA = JsonConvert.SerializeObject(context, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var logModel = new LogModel(notesA, notesB, correlationGuid, step, userName);

            InfoInDetail(message, logModel);
        }

        public void ErrorInDetail(object item, Guid correlationGuid, string notesB, Exception ex, int step, string userName)
        {
            var notesA = JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var logModel = new LogModel(notesA, notesB, correlationGuid, step, userName);

            ErrorInDetail(ex, logModel);
        }
    }
}

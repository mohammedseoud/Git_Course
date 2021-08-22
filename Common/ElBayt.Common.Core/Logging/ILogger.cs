using ElBayt.Common.Entities;
using System;

namespace ElBayt.Common.Core.Logging
{
    public interface ILogger
    {
        void Error(Exception ex);

        void Info(string message);

        void Info(string message, params object[] arguments);

        void Trace(string message);

        void Trace(string message, params object[] arguments);

        void InfoInDetail(string message, LogModel logModel);

        void ErrorInDetail(Exception ex, LogModel logModel);

        void InfoInDetail(object context, Guid correlationGuid, string notesB, string message, int step, string userName);

        void ErrorInDetail(object item, Guid correlationGuid, string notesB, Exception ex, int step, string userName);
    }
}

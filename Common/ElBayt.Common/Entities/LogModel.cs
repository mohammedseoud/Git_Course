using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class LogModel
    {
        public LogModel(string notesA, string notesB, Guid correlationId, int step, string username)
        {
            NotesA = notesA;
            NotesB = notesB;
            CorrelationId = correlationId;
            Step = step;
            Username = username;
        }

        public string NotesA { get; set; }
        public string NotesB { get; set; }
        public Guid CorrelationId { get; set; }
        public int Step { get; set; }
        public string Username { get; set; }
    }
}

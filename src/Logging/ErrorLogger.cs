using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Backup_Utility.src.Logging
{
    class ErrorLogger
    {
        private string FilePath;

        public ErrorLogger(string filePath = "")
        {
            if (filePath != "") 
                FilePath = filePath;
        }
    }
}

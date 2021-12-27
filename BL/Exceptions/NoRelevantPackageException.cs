﻿using System;

namespace BlApi
{
    class NoRelevantPackageException : Exception
    {
        public NoRelevantPackageException() : base($"No packages found that drone can deliver with current battery level") { }
    }
}

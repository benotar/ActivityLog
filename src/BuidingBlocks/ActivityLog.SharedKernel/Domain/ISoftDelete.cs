﻿namespace ActivityLog.SharedKernel.Domain;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
